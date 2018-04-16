using Api.User.Domain.Entities;
using Api.User.Domain.Interfaces.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Slapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Api.User.Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<IEnumerable<Domain.Entities.User>> GetUsersByKindOfService(string kindOfService)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStrings.MeuLinkProd))
            {
                var dados = await connection.QueryAsync<dynamic>(
                    @"SELECT
	                    u.Id,
	                    u.Name,
	                    u.Email,
	                    u.DDD,
	                    u.Phone,
	                    u.Password,
	                    a.Id AS Address_Id,
	                    a.UserId AS Address_UserId,
	                    a.AddressLine AS Address_AddressLine,
	                    a.Number AS Address_Number,
	                    a.Complement AS Address_Complement,
	                    a.City AS Address_City,
	                    a.State AS Address_State,
	                    a.Country AS Address_Country,
	                    a.ZipCode AS Address_ZipCode,
	                    a.Latitude AS Address_Latitude,
	                    a.Longitude AS Address_Longitude,
	                    i.Id AS Images_Id,
	                    i.UserId AS Images_UserId,
	                    i.ImagesTypeId AS Images_ImagesTypeId,
	                    i.Value AS Images_Value,
	                    p.Id AS ProfessionalInformations_Id,
	                    p.UserId AS ProfessionalInformations_UserId,
	                    p.Description AS ProfessionalInformations_Description,
	                    s.Id AS Services_Id,
	                    s.ProfessionalInformationsId AS Services_ProfessionalInformationsId,
	                    s.Name AS Services_Name
                    FROM Users AS u
                    INNER JOIN Address AS a
                    ON u.Id = a.UserId
                    INNER JOIN Images AS i
                    ON u.Id = i.UserId
                    AND i.ImagesTypeId = 1
                    INNER JOIN ProfessionalInformations AS p
                    ON u.Id = p.UserId
                    INNER JOIN Services AS s
                    ON p.Id = s.ProfessionalInformationsId
                    WHERE s.Name = @kindOfService;",
                    kindOfService
                );

                AutoMapper.Configuration.AddIdentifier(
                    typeof(Domain.Entities.User), "Id");

                AutoMapper.Configuration.AddIdentifier(
                    typeof(Address), "Id");

                AutoMapper.Configuration.AddIdentifier(
                    typeof(Images), "Id");

                AutoMapper.Configuration.AddIdentifier(
                    typeof(ProfessionalInformations), "Id");

                AutoMapper.Configuration.AddIdentifier(
                    typeof(Services), "Id");

                IEnumerable<Domain.Entities.User> users = (AutoMapper.MapDynamic<Domain.Entities.User>(dados)
                    as IEnumerable<Domain.Entities.User>);

                return users;
            }
        }
    }
}
