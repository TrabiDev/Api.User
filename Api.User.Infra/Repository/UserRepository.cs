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
                        user.ProfessionalInformations.Services = new List<Service>();
                        user.Images = new List<Image>();
                        user.Ratings = new List<Rating>();
                        user.Phones = new List<Phone>();

                        return user;
                    },
                    splitOn: "Id, Id, Id"
                )).ToList();
            }

            var listUserId = users.Select(p => p.Id);

            #region GetServices 

            var listProfessionalInformationsId = users.Select(p => p.ProfessionalInformations.Id);

            var services = await GetServices(listProfessionalInformationsId);

            foreach (var service in services)
            {
                users.FirstOrDefault(p => p.ProfessionalInformations.Id == service.ProfessionalInformationsId).ProfessionalInformations.Services.Add(service);
            }

            #endregion

            #region GetImages

            var images = await GetImages(listUserId);

            foreach (var image in images)
            {
                users.FirstOrDefault(p => p.Id == image.UserId).Images.Add(image);
            }

            #endregion

            #region GetRatings

            var ratings = await GetRatings(listUserId);

            foreach (var rating in ratings)
            {
                users.FirstOrDefault(p => p.Id == rating.UserId).Ratings.Add(rating);
            }

            #endregion

            #region GetPhones

            var phones = await GetPhones(listUserId);

            foreach (var phone in phones)
            {
                users.FirstOrDefault(p => p.Id == phone.UserId).Phones.Add(phone);
            }

            #endregion

            return users;
        }

        /// <summary>
        /// Busca os serviços de profissionais
        /// </summary>
        /// <param name="listProfessionalInformationsId">Lista de profissionais para consulta dos seus respectivos serviços</param>
        /// <returns></returns>
        private async Task<IEnumerable<Service>> GetServices(IEnumerable<int> listProfessionalInformationsId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    return await connection.QueryAsync<Service>(
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
        private async Task<IEnumerable<Image>> GetImages(IEnumerable<int> listUserId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return await connection.QueryAsync<Image>(
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
        /// Busca os ratings de usuários
        /// </summary>
        /// <param name="listUserId">Lista de usuários</param>
        /// <returns></returns>
        private async Task<IEnumerable<Rating>> GetRatings(IEnumerable<int> listUserId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return await connection.QueryAsync<Rating>(
                    @"SELECT
                        *
                      FROM Ratings
                      WHERE UserId IN @listUserId;",
                    param: new
                    {
                        listUserId
                    }
                );
            }
        }

        /// <summary>
        /// Busca os telefones de usuários
        /// </summary>
        /// <param name="listUserId">Lista de usuários</param>
        /// <returns></returns>
        private async Task<IEnumerable<Phone>> GetPhones(IEnumerable<int> listUserId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return await connection.QueryAsync<Phone>(
                    @"SELECT
                        *
                      FROM Phones
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
