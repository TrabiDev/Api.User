using Api.User.Domain.Entities;
using Api.User.Domain.Interfaces.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.User.Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;

        private string connectionString;

        public UserRepository()
        {
            connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public UserRepository(IConfiguration configuration)
        {
            _config = configuration;
            connectionString = _config.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Busca usuários de acordo com os filtros enviados
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="kindOfService">Tipo de serviço</param>
        public async Task<List<Domain.Entities.User>> Get(int id = 0, string kindOfService = null)
        {
            List<Domain.Entities.User> users;

            string scriptSelect = CreateSqlGet(id, kindOfService);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                users = (await connection.QueryAsync<Domain.Entities.User, Address, ProfessionalInformations, Domain.Entities.User>(
                    scriptSelect,
                    param: new
                    {
                        Id = id,
                        KindOfService = kindOfService
                    },
                    map: (user, address, professionalInformations) =>
                    {
                        user.Address = address;
                        user.ProfessionalInformations = professionalInformations;

                        user.ProfessionalInformations.Services = new List<Services>();
                        user.Images = new List<Images>();

                        return user;
                    },
                    splitOn: "Id, Id"
                )).ToList();
            }

            #region GetServices 

            var listProfessionalInformationsId = users.Select(p => p.ProfessionalInformations.Id);

            var services = await GetServices(listProfessionalInformationsId);

            foreach (var service in services)
            {
                users.FirstOrDefault(p => p.ProfessionalInformations.Id == service.ProfessionalInformationsId).ProfessionalInformations.Services.Add(service);
            }

            #endregion

            #region GetImages

            var listUserId = users.Select(p => p.Id);

            var images = await GetImages(listUserId);

            foreach (var image in images)
            {
                users.FirstOrDefault(p => p.Id == image.UserId).Images.Add(image);
            }

            #endregion

            return users;
        }

        /// <summary>
        /// Busca os serviços de profissionais
        /// </summary>
        /// <param name="listProfessionalInformationsId">Lista de profissionais para consulta dos seus respectivos serviços</param>
        /// <returns></returns>
        private async Task<IEnumerable<Services>> GetServices(IEnumerable<int> listProfessionalInformationsId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    return await connection.QueryAsync<Services>(
                        @"SELECT
                            *
                          FROM Services
                          WHERE ProfessionalInformationsId IN @listProfessionalInformationsId",
                        param: new
                        {
                            listProfessionalInformationsId
                        }
                    );
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// <summary>
        /// Busca as imagens de usuários
        /// </summary>
        /// <param name="listUserId">Lista de usuários para consulta de suas respectivas imagens</param>
        /// <returns></returns>
        private async Task<IEnumerable<Images>> GetImages(IEnumerable<int> listUserId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return await connection.QueryAsync<Images>(
                    @"SELECT
                        *
                      FROM Images
                      WHERE UserId IN @listUserId;",
                    param: new
                    {
                        listUserId
                    }
                );
            }
        }

        /// <summary>
        /// Cria um script dinâmico para consulta dos usuários
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="kindOfService">Tipo de serviço</param>
        /// <returns></returns>
        private string CreateSqlGet(int id, string kindOfService)
        {
            string select = 
                    @"SELECT
	                    u.Id,
	                    u.Name,
	                    u.Email,
	                    u.DDD,
	                    u.Phone,
	                    u.Password,
	                    a.Id,
	                    a.UserId,
	                    a.AddressLine,
	                    a.Number,
	                    a.Complement,
	                    a.City,
	                    a.State,
	                    a.Country,
	                    a.ZipCode,
	                    a.Latitude,
	                    a.Longitude,
	                    p.Id,
	                    p.UserId,
	                    p.Description
                    FROM Users AS u
                    INNER JOIN Address AS a
                    ON u.Id = a.UserId
                    INNER JOIN ProfessionalInformations AS p
                    ON u.Id = p.UserId
                    INNER JOIN Services AS s
                    ON p.Id = s.ProfessionalInformationsId
                    WHERE 1 = 1";

            StringBuilder where = new StringBuilder();

            if(id > 0)
            {
                where.Append(" ");
                where.Append("AND u.Id = @Id");
            }

            if (!string.IsNullOrEmpty(kindOfService))
            {
                where.Append(" ");
                where.Append("AND s.Name LIKE CONCAT('%', @KindOfService, '%')");
            }

            return select + where;
        }

    }
}
