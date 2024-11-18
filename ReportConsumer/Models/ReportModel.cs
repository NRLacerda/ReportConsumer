using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportConsumer.Models
{
    public class ReportModel
    {
        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public Guid UserGuid { get; set; }

        public string TipoProcedimento { get; set; } = String.Empty;
    }
}
