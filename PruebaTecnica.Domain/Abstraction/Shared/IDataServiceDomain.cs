using PruebaTecnica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Domain.Abstraction.Shared
{
    public interface IDataServiceDomain
    {
        Task<string> GetToken();
        Task<List<Vehicle>> GetVehicleCountData(string token, string date);
        Task<List<Vehicle>> GetVehicleCollectionData(string token, string date);
    }
}
