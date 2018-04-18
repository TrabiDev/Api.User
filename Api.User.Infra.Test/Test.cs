using Api.User.Domain.Interfaces.Repository;
using Api.User.Infra.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public void ReturnListOfProfessionalsByKindOfService()
        {
            AsyncLocal<string> kindOfService = new AsyncLocal<string>();
            kindOfService.Value = "Eletricista";

            IEnumerable<Domain.Entities.User> users;

            users = _repository.GetUsersByKindOfService(kindOfService.Value);

            Assert.True(users.Count() > 0);
        }
    }
}
