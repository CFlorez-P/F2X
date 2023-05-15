using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Domain.Request;
using PruebaTecnica.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Domain.Abstraction.Shared
{
    public interface IVehicleRepository
    {
        bool SaveVehicleData(List<Vehicle> vehicleCounts);
        List<string> GetAllStations();
        List<string> GetAllDates();
        List<Vehicle> GetAllVehicleData(VehicleDataRequest request);
        List<VehicleValueResponse> GetVehicleValueData();
    }
}
