using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Application.Response
{
    public class VehicleAllDataResponse
    {
        public string Estacion { get; set; }
        public string Sentido { get; set; }
        public int Hora { get; set; }
        public string Categoria { get; set; }
        public string Fecha { get; set; }
        public int Cantidad { get; set; }
        public int ValorTabulado { get; set; }
    }
}
