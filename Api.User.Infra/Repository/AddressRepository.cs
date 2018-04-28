using Api.User.Domain.Entities;
using Api.User.Domain.Interfaces.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Api.User.Infra.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IConfiguration _config;

        private string connectionString;

        public AddressRepository(IConfiguration configuration)
        {
            _config = configuration;
            connectionString = _config.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Busca o endereço de um usuário
        /// </summary>
        /// <param name="userId">Id do usuário</param>
        public async Task<Address> Get(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return await connection.QueryFirstAsync<Address>(
                    @"SELECT 
                        * 
                      FROM Address
                      WHERE UserId = @UserId",
                    new
                    {
                        UserId = userId
                    }
                );
            }
        }

    }
}
