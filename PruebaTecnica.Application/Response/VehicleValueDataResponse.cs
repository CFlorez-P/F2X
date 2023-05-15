using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Application.Response
{
    public class VehicleValueDataResponse
    {
        public string Estacion { get; set; }
        public string Fecha { get; set; }
        public int TotalCantidad { get; set; }
        public int TotalValorTabulado { get; set; }
    }
}
