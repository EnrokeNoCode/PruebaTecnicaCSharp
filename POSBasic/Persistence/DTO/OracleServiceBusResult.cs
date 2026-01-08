using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSBasic.Persistence.DTO
{
    public class OracleServiceBusResult
    {
        public bool Ok { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Tns { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
    }
}
