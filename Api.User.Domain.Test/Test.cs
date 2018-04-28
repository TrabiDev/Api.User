using Api.User.Domain.Interfaces.Services;
using Api.User.Domain.Services;
using Api.User.Infra.Repository;
using System.Threading.Tasks;
using Xunit;

namespace Api.User.Domain.Test
{
    public class Test
    {
        private readonly IUserService _service;

        public Test() : this(new UserService(new UserRepository()))
        {

        }

        internal Test(IUserService service)
        {
            _service = service;
        }

        [Fact]
        public async Task GetUsersByKindOfService()
        {
            string kindOfService = "Eletricista";

            var response = await _service.Get(kindOfService: kindOfService);

            Assert.True(response.Users.Count > 0);
            Assert.Contains(response.Users, p => p.Address != null);
            Assert.Contains(response.Users, p => p.Images != null);
            Assert.Contains(response.Users, p => p.ProfessionalInformations != null);
            Assert.Contains(response.Users, p => p.ProfessionalInformations.Services != null);
        }

        [Fact]
        public async Task GetUsersById()
        {
            int id = 1;

            var response = await _service.Get(id: id);

            Assert.True(response.Users.Count > 0);
            Assert.Contains(response.Users, p => p.Address != null);
            Assert.Contains(response.Users, p => p.Images != null);
            Assert.Contains(response.Users, p => p.ProfessionalInformations != null);
            Assert.Contains(response.Users, p => p.ProfessionalInformations.Services != null);
        }

    }
}
