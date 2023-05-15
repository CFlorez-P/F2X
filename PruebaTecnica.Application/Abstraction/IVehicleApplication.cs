using PruebaTecnica.Application.Request;
using PruebaTecnica.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Application.Abstraction
{
    public interface IVehicleApplication
    {
        Task<bool> SetAllData();
        List<VehicleAllDataResponse> GetAllVehicleData(VehicleAllDataRequest request);
        List<string> GetAllStations();
        List<string> GetAllDates();
        List<VehicleValueDataResponse> GetVehicleValueData();

    }
}
