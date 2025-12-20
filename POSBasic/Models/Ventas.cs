using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSBasic.Models
{
    public class Ventas
    {
        public int codventa { get; set; }
        public int codcliente { get; set; }
        public DateOnly fechavent { get; set; }
        public string numventa { get; set; }
        public decimal totalventa { get; set; }
        public List<VentasDet> Detalles { get; set; } = new();
    }

    public class VentasDet
    {
        public int codventa {  get; set; }
        public int codproducto { get; set; }
        public decimal cantidadventa { get; set; }
        public decimal precioventa { get; set; }
        public decimal totallinea { get; set; }
        public int numlinea { get; set; }
    }
}
