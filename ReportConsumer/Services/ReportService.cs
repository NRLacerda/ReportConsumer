using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportConsumer.Models;
using ReportConsumer.Repositories;

namespace ReportConsumer.Services
{
    public class ReportService
    {
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
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }



            }
        }
    }
}
