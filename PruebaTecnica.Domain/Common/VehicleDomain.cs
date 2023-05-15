using MediatR;
using PruebaTecnica.Domain.Abstraction;
using PruebaTecnica.Domain.Abstraction.Shared;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Domain.Request;
using PruebaTecnica.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Domain.Common
{
    public class VehicleDomain : IVehicleDomain
    {
        private readonly IVehicleRepository VehicleRepository;

        public VehicleDomain(IVehicleRepository vehicleRepository)
        {
            VehicleRepository = vehicleRepository;
        }

        public List<Vehicle> GetAllVehicleData(VehicleDataRequest request)
        {
            return VehicleRepository.GetAllVehicleData(request);
        }

        public List<VehicleValueResponse> GetVehicleValueData()
        {
            return VehicleRepository.GetVehicleValueData();
        }

        public bool SaveVehicleData(List<Vehicle> vehicles)
        {
            return VehicleRepository.SaveVehicleData(vehicles);         
        }
        public List<string> GetAllStations()
        {
            return VehicleRepository.GetAllStations();
        }
        public List<string> GetAllDates()
        {
            return VehicleRepository.GetAllDates();
        }

    }
}
