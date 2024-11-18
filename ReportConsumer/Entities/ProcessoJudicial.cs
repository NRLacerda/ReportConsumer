using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportConsumer.Entities
{
    public class ProcessoJudicialEntity
    {
        [Column("procedimento")]
        [MaxLength(3)] 
        public string? Procedimento { get; set; }

        [Column("numero")]
        public long Numero { get; set; }


        [Column("numero_cnj")]
        public long? NumeroCnj { get; set; }

        [Column("data_protocolo_trf")]
        public DateTime? DataProtocoloTrf { get; set; }

        [Column("situacao_protocolo")]
        public string? SituacaoProtocolo { get; set; }

        [Column("oficio_requisitorio")]
        public long? OficioRequisitorio { get; set; }

        [Column("juizo_origem")]
        public string? JuizoOrigem { get; set; }

        [Column("processos_originarios")]
        public string? ProcessosOriginarios { get; set; }

        [Column("requerido")]
        public string? Requerido { get; set; }

        [Column("requerentes")]
        public string? Requerentes { get; set; }

        [Column("advogado")]
        public string? Advogado { get; set; }

        [Column("ano_proposta")]
        public string? AnoProposta { get; set; }

        [Column("data_conta_liquidacao")]
        public DateTime? DataContaLiquidacao { get; set; }

        [Column("valor_solicitado")]
        public string? ValorSolicitado { get; set; }

        [Column("valor_inscrito_proposta")]
        public string? ValorInscritoProposta { get; set; }

        [Column("requisicao_bloqueada")]
        public string? RequisicaoBloqueada { get; set; }

        [Column("situacao_requisicao")]
        public string? SituacaoRequisicao { get; set; }

        [Column("banco")]
        public string? Banco { get; set; }

        [Column("natureza")]
        public string? Natureza { get; set; }

        [Column("id_task")]
        public int? IdTask { get; set; }

        [ForeignKey("IdTask")]
        public TaskEntity Task { get; set; }

        [Column("date_processed")]
        public DateTime DateProcessed { get; set; }

    }

}
