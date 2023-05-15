using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Domain.Request;
using PruebaTecnica.Domain.Response;

namespace PruebaTecnica.Domain.Abstraction
{
    public interface IVehicleDomain
    {
        bool SaveVehicleData(List<Vehicle> vehicleCounts);
        List<string> GetAllStations();
        List<string> GetAllDates();

        List<Vehicle> GetAllVehicleData(VehicleDataRequest request);

        List<VehicleValueResponse> GetVehicleValueData();

    }
}
