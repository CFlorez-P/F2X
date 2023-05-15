﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Domain.Request
{
    public class VehicleDataRequest
    {
        public string? FechaDesde { get; set; }
        public string? FechaHasta { get; set; }
        public string? Estacion { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
