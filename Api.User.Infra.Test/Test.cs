using Api.User.Domain.Interfaces.Repository;
using Api.User.Infra.Repository;
using System.Collections.Generic;
using System.Linq;
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
        public async void ReturnListOfProfessionalsByKindOfService()
        {
            string kindOfService = "Eletricista";

            IEnumerable<Domain.Entities.User> users;

            users = await _repository.GetUsersByKindOfService(kindOfService);

            Assert.True(users.Count() > 0);
        }
    }
}
