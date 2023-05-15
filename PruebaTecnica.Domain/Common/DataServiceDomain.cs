using PruebaTecnica.Domain.Abstraction.Shared;
using PruebaTecnica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Domain.Common
{
    public class DataServiceDomain : IDataServiceDomain
    {
        private readonly IDataService DataService;

        public DataServiceDomain(IDataService dataService)
        {
            DataService = dataService;
        }

        public async Task<string> GetToken()
        {
            return await DataService.GetToken();
        }

        public async Task<List<Vehicle>> GetVehicleCollectionData(string token, string date)
        {
            return await DataService.GetVehicleCollectionData(token, date);
        }

        public async Task<List<Vehicle>> GetVehicleCountData(string token, string date)
        {
            return await DataService.GetVehicleCountData(token, date);
        }
    }
}
