using Api.User.Domain.Interfaces.Repository;
using Api.User.Infra.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.User.Infra.Test
{
    public class Test
    {
        private readonly IUserRepository _repository;

        public Test() : this(new UserRepository())
        {

        }

        internal Test(IUserRepository repository)
        {
            _repository = repository;
        }

        [Fact]
        public async Task GetUsersByKindOfService()
        {
            string kindOfService = "Eletricista";

            List<Domain.Entities.User> users;

            users = await _repository.Get(kindOfService: kindOfService);

            Assert.True(users.Count > 0);
            Assert.Contains(users, p => p.Address != null);
            Assert.Contains(users, p => p.Images != null);
            Assert.Contains(users, p => p.ProfessionalInformations != null);
            Assert.Contains(users, p => p.ProfessionalInformations.Services != null);
        }

        [Fact]
        public async Task GetUsersById()
        {
            int id = 1;

            List<Domain.Entities.User> users;

            users = await _repository.Get(id: id);

            Assert.True(users.Count > 0);
            Assert.Contains(users, p => p.Address != null);
            Assert.Contains(users, p => p.Images != null);
            Assert.Contains(users, p => p.ProfessionalInformations != null);
            Assert.Contains(users, p => p.ProfessionalInformations.Services != null);
        }

    }
}
