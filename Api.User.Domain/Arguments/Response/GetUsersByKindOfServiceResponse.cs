using System;
using System.Collections.Generic;
using System.Text;

namespace Api.User.Domain.Arguments.Response
{
    public class GetUsersByKindOfServiceResponse
    {
        public List<Entities.User> Users { get; set; }
    }
}
