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

        //TODO: Telefone deve ser um array


        public string DDD { get; private set; }

        public string Phone { get; private set; }

        [Required]
        public string Password { get; private set; }

        public Address Address { get; set; }
        public ProfessionalInformations ProfessionalInformations { get; set; }
        public List<Images> Images { get; set; }
    }
}
