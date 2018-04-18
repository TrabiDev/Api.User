using Api.User.Domain.Entities;
using Api.User.Domain.Interfaces.Repository;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Api.User.Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<Domain.Entities.User> GetUsersByKindOfService(string kindOfService)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStrings.MeuLinkProd))
            {
                return connection.Query<Domain.Entities.User, Address, ProfessionalInformations, Domain.Entities.User>(
                    @"SELECT
	                    u.Id,
	                    u.Name,
	                    u.Email,
	                    u.DDD,
	                    u.Phone,
	                    u.Password,
	                    a.Id AS Address_Id,
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
	                    p.Id AS ProfessionalInformations_Id,
	                    p.UserId,
	                    p.Description
                    FROM Users AS u
                    INNER JOIN Address AS a
                    ON u.Id = a.UserId
                    INNER JOIN ProfessionalInformations AS p
                    ON u.Id = p.UserId
                    INNER JOIN Services AS s
                    ON p.Id = s.ProfessionalInformationsId
                    WHERE s.Name = @kindOfService;",
                    param: new
                    {
                        kindOfService
                    },
                    map: (user, address, professionalInformations) =>
                    {
                        user.Address = address;
                        user.ProfessionalInformations = professionalInformations;

                        return user;
                    },
                    splitOn: "Password, Address_Id, Longitude, ProfessionalInformations_Id"
                );
            }
        }
    }
}
