using AutoMapper;
using PruebaTecnica.Application.Abstraction;
using PruebaTecnica.Application.Request;
using PruebaTecnica.Application.Response;
using PruebaTecnica.Domain.Abstraction;
using PruebaTecnica.Domain.Abstraction.Shared;
using PruebaTecnica.Domain.Common;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Domain.Request;
using PruebaTecnica.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Application.Service
{
    public class VehicleApplication: IVehicleApplication
    {
        private readonly IDataServiceDomain DataServiceDomain;
        private readonly IVehicleDomain VehicleDomain;

        public VehicleApplication(IDataServiceDomain dataServiceDomain, IVehicleDomain vehicleDomain)
        {
            DataServiceDomain = dataServiceDomain;
            VehicleDomain = vehicleDomain;
        }

        public List<string> GetAllStations()
        {

            return VehicleDomain.GetAllStations();
        }

        public List<string> GetAllDates()
        {     
            return VehicleDomain.GetAllDates();
        }

        public List<VehicleAllDataResponse> GetAllVehicleData(VehicleAllDataRequest request)
        {
            
            VehicleDataRequest vehicleDataRequest = new VehicleDataRequest()
            {
                Estacion = request.Estacion,
                FechaDesde = request.FechaDesde,
                FechaHasta = request.FechaHasta,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
            List<Vehicle> vehicleResponse = VehicleDomain.GetAllVehicleData(vehicleDataRequest);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Vehicle, VehicleAllDataResponse>();
            });

            var mapper = config.CreateMapper();

            return mapper.Map<List<VehicleAllDataResponse>>(vehicleResponse);
        }

        public List<VehicleValueDataResponse> GetVehicleValueData()
        {
            List<VehicleValueResponse> vehicleValues = VehicleDomain.GetVehicleValueData();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VehicleValueResponse, VehicleValueDataResponse>();
            });

            var mapper = config.CreateMapper();
            return mapper.Map<List<VehicleValueDataResponse>>(vehicleValues);
        }

        public async Task<bool> SetAllData()
        {
            string token = await DataServiceDomain.GetToken();
            DateTime startDate = new DateTime(2021, 10, 7);
            DateTime currentDate = DateTime.Now.Date;

            while (startDate <= currentDate)
            {
                string dateString = startDate.ToString("yyyy-MM-dd");

                List<Vehicle> vehicleCount = await DataServiceDomain.GetVehicleCountData(token, dateString);
                List<Vehicle> vehicleCollections = await DataServiceDomain.GetVehicleCollectionData(token, dateString);


                List<Vehicle> vehicles = (from count in vehicleCount
                                            join collection in vehicleCollections
                                            on new { count.Estacion, count.Sentido, count.Hora, count.Categoria }
                                            equals new { collection.Estacion, collection.Sentido, collection.Hora, collection.Categoria }
                                            select new Vehicle
                                            {
                                                Estacion = count.Estacion,
                                                Sentido = count.Sentido,
                                                Hora = count.Hora,
                                                Categoria = count.Categoria,
                                                Cantidad = count.Cantidad,
                                                ValorTabulado = collection.ValorTabulado
                                            })
                                            .GroupBy(v => new { v.Estacion, v.Sentido, v.Hora, v.Categoria })
                                            .Select(g => new Vehicle
                                            {
                                                Estacion = g.Key.Estacion,
                                                Sentido = g.Key.Sentido,
                                                Hora = g.Key.Hora,
                                                Categoria = g.Key.Categoria,
                                                Cantidad = g.Sum(v => v.Cantidad),
                                                ValorTabulado = g.Sum(v => v.ValorTabulado)
                                            })
                                            .ToList();


                if (vehicles.Count > 0)
                {
                    foreach (Vehicle vehicle in vehicles)
                    {
                        vehicle.Fecha = dateString;
                    }
                    VehicleDomain.SaveVehicleData(vehicles);
                }

                startDate = startDate.AddDays(1);
            }
            return true;
        }
    }
}
