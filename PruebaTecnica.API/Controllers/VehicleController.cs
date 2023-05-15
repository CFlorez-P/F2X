using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Application.Abstraction;
using PruebaTecnica.Application.Request;
using PruebaTecnica.Application.Response;

namespace PruebaTecnica.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : Controller
    {
        private IVehicleApplication VehicleApplication { set; get; }


        public VehicleController(IVehicleApplication vehicleApplication)
        {
            VehicleApplication = vehicleApplication;
        }

        [HttpGet]
        [Route("data")]
        public async Task<bool> SetAllData()
        {
            return await VehicleApplication.SetAllData();
        }

        [HttpGet]
        [Route("GetAllData")]
        public List<VehicleAllDataResponse> GetAllVehicleData(string? estacion, string? fechaDesde, string? fechaHasta, int pageIndex, int pageSize)
        {
            var request = new VehicleAllDataRequest
            {
                Estacion = estacion,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            return VehicleApplication.GetAllVehicleData(request);
        }

        [HttpGet]
        [Route("GetVehicleValueData")]
        public List<VehicleValueDataResponse> GetVehicleValueData()
        {
            return VehicleApplication.GetVehicleValueData();
        }

        [HttpGet]
        [Route("GetAllStations")]
        public List<string> GetAllStations()
        {
            return VehicleApplication.GetAllStations();
        }

        [HttpGet]
        [Route("GetAllDates")]
        public List<string> GetAllDates()
        {
            return VehicleApplication.GetAllDates();
        }
    }
}
