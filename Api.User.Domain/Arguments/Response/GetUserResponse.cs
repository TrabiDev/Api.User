using System.Collections.Generic;

namespace Api.User.Domain.Arguments.Response
{
    public class GetUserResponse
    {
        public List<Entities.User> Users { get; set; }
    }
}
