using System.Collections.Generic;

namespace Api.User.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string DDD { get; private set; }
        public string Phone { get; private set; }
        public string Password { get; private set; }
        public Address Address { get; set; }
        public ProfessionalInformations ProfessionalInformations { get; set; }
        public List<Images> Images { get; set; }
    }
}
