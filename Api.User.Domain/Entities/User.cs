using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.User.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }

        [Required]
        public string Name { get; private set; }

        [Required]
        public string Email { get; private set; }

        [Required]
        public string Password { get; private set; }

        public Address Address { get; set; }

        public List<Phone> Phones { get; set; }

        public ProfessionalInformations ProfessionalInformations { get; set; }

        public List<Image> Images { get; set; }

        public List<Rating> Ratings { get; set; }
    }
}
