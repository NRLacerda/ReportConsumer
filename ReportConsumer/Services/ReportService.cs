using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using ReportConsumer.Entities;
using ReportConsumer.Models;
using ReportConsumer.Repositories;

namespace ReportConsumer.Services
{
    public class ReportService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName = "bacen-reports";

        public ReportService()
        {
            string jsonPath = @"C:\Users\nroch\OneDrive\Área de Trabalho\PROJETOS\ESTUDO\DOTNET\KEYS\org-hub-439600-30a266b2c435.json";

            GoogleCredential credentials = GoogleCredential.FromFile(jsonPath);

            _storageClient = StorageClient.Create(credentials);
        }


        public void createReport(ReportModel report)
        {
            using (var context = new JudicialContext())
            {
                try
                {
                    var processos = context.ProcessosJudiciais
                    .Where(p => p.Procedimento == report.TipoProcedimento)
                    .ToList();

                    if (processos == null)
                    {
                        Console.WriteLine("Nenhum processo encontrado.");
                    }
                    else
                    {
                        Console.WriteLine($"Found {processos.Count} matching records.");
                        foreach (var processo in processos)
                        {
                            Console.WriteLine($"Processo: {processo.Numero}, Requerido: {processo.Requerido}");
                        }

                        GenerateAndUploadCsv(processos);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }



            }
        }

        public void GenerateAndUploadCsv(List<ProcessoJudicialEntity> processos)
        {
            try
            {
                var csvContent = new StringBuilder();
                csvContent.AppendLine("Numero,Requerido,Advogado,ValorSolicitado"); 

                foreach (var processo in processos)
                {
                    csvContent.AppendLine($"{processo.Numero},{processo.Requerido},{processo.Advogado},{processo.ValorSolicitado}");
                }

                string fileName = $"bacen-reports-{DateTime.Now.ToString("ddMMyy")}.csv";

                UploadCsvToBucket(csvContent.ToString(), fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UploadCsvToBucket(string csvContent, string fileName)
        {
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(csvContent);

                using (var stream = new MemoryStream(byteArray))
                {
                    _storageClient.UploadObject(_bucketName, fileName, "text/csv", stream);
                    Console.WriteLine($"File {fileName} uploaded successfully to {_bucketName}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to upload file: {ex.Message}");
            }
        }
    }
}
