using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Domain.Response
{
    public class VehicleValueResponse
    {
        public string Estacion { get; set; }
        public string Fecha { get; set; }
        public int TotalCantidad { get; set; }
        public int TotalValorTabulado { get; set; }
    }
}
