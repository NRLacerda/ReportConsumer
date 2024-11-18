using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReportConsumer.Entities
{
    public class TaskEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("status")]
        [MaxLength(3)]
        public string? Status { get; set; }

        [Column("start_range")]
        public long? StartRange { get; set; }

        [Column("final_range")]
        public long? FinalRange { get; set; }

        [Column("instances")]
        public int? Instances { get; set; }

        [Column("last_processed")]
        public long? LastProcessed { get; set; }

        [Column("started")]
        public DateTime? Started { get; set; }

        [Column("ended")]
        public DateTime? Ended { get; set; }
    }

}
