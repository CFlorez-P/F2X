using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PruebaTecnica.Domain.Abstraction.Shared;
using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Domain.Request;
using PruebaTecnica.Domain.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Persistence.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IConfiguration Configuration;
        public VehicleRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Vehicle> GetAllVehicleData(VehicleDataRequest request)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.uspGetAllData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PageIndex", request.PageIndex);
                    command.Parameters.AddWithValue("@PageSize", request.PageSize);
                    command.Parameters.AddWithValue("@FechaDesde", !string.IsNullOrEmpty(request.FechaDesde) ? DateTime.Parse(request.FechaDesde) : null);
                    command.Parameters.AddWithValue("@FechaHasta", !string.IsNullOrEmpty(request.FechaHasta) ? DateTime.Parse(request.FechaHasta): null);
                    command.Parameters.AddWithValue("@Estacion", request.Estacion);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Vehicle> vehicles = new List<Vehicle>();

                        while (reader.Read())
                        {
                            Vehicle vehicle = new Vehicle();
                            vehicle.Estacion = reader.GetString(reader.GetOrdinal("Estacion"));
                            vehicle.Sentido = reader.GetString(reader.GetOrdinal("Sentido"));
                            vehicle.Hora = reader.GetInt32(reader.GetOrdinal("Hora"));
                            vehicle.Categoria = reader.GetString(reader.GetOrdinal("Categoria"));
                            vehicle.Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad"));
                            vehicle.ValorTabulado = reader.GetInt32(reader.GetOrdinal("ValorTabulado"));
                            vehicle.Fecha = reader.GetString(reader.GetOrdinal("Fecha"));

                            vehicles.Add(vehicle);
                        }

                        return vehicles;
                    }
                }
            }
        }

        public List<VehicleValueResponse> GetVehicleValueData()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.uspGetValueData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<VehicleValueResponse> vehicles = new List<VehicleValueResponse>();

                        while (reader.Read())
                        {
                            VehicleValueResponse vehicle = new VehicleValueResponse();
                            vehicle.Estacion = reader.GetString(reader.GetOrdinal("Estacion"));
                            vehicle.TotalCantidad = reader.GetInt32(reader.GetOrdinal("TotalCantidad"));
                            vehicle.TotalValorTabulado = reader.GetInt32(reader.GetOrdinal("TotalValorTabulado"));
                            vehicle.Fecha = reader.GetString(reader.GetOrdinal("Fecha"));

                            vehicles.Add(vehicle);
                        }

                        return vehicles;
                    }
                }
            }
        }

        public bool SaveVehicleData(List<Vehicle> vehicleCollections)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var vehicleCollection in vehicleCollections)
                {
                    using (SqlCommand command = new SqlCommand("dbo.uspSaveRecaudos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Estacion", vehicleCollection.Estacion);
                        command.Parameters.AddWithValue("@Sentido", vehicleCollection.Sentido);
                        command.Parameters.AddWithValue("@Hora", vehicleCollection.Hora);
                        command.Parameters.AddWithValue("@Categoria", vehicleCollection.Categoria);
                        command.Parameters.AddWithValue("@ValorTabulado", vehicleCollection.ValorTabulado);
                        command.Parameters.AddWithValue("@Fecha", vehicleCollection.Fecha);
                        command.Parameters.AddWithValue("@Cantidad", vehicleCollection.Cantidad);

                        command.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }
            return true;
        }

        public List<string> GetAllStations()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.uspGetAllStations", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> stations = new List<string>();

                        while (reader.Read())
                        {
                            stations.Add(reader.GetString(0));
                        }

                        return stations;
                    }
                }
            }
        }

        public List<string> GetAllDates()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.uspGetAllDates", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> dates = new List<string>();

                        while (reader.Read())
                        {
                            dates.Add(reader.GetString(0));
                        }

                        return dates;
                    }
                }
            }
        }

    }
}
