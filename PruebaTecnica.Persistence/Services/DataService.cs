using Newtonsoft.Json;
using PruebaTecnica.Domain.Abstraction.Shared;
using PruebaTecnica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Persistence.Services
{
    public class DataService : IDataService
    {
        public async Task<string> GetToken()
        {
            string? token = null;

            using (var client = new HttpClient())
            {
                var requestBody = new { Username = "user", Password = "1234" };
                var response = await client.PostAsJsonAsync("http://23.102.103.53:5200/api/Login", requestBody);
                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic? tokenResponse = JsonConvert.DeserializeObject(responseBody);

                token = tokenResponse?.token;
                return token;
            }
        }

        public async Task<List<Vehicle>> GetVehicleCountData(string token, string date)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync($"http://23.102.103.53:5200/api/ConteoVehiculos/{date}");
                var responseBody = await response.Content.ReadAsStringAsync();

                List<Vehicle> vehicleCountData = JsonConvert.DeserializeObject<List<Vehicle>>(responseBody);
                return vehicleCountData;
            }
        }

        public async Task<List<Vehicle>> GetVehicleCollectionData(string token, string date)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync($"http://23.102.103.53:5200/api/RecaudoVehiculos/{date}");
                var responseBody = await response.Content.ReadAsStringAsync();
                List<Vehicle> vehicleCollectionData = JsonConvert.DeserializeObject<List<Vehicle>>(responseBody);
                return vehicleCollectionData;
            }
        }

    }
}
