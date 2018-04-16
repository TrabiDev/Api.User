using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.User.Domain.Entities
{
    public class ProfessionalInformations
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        public int UserId { get; private set; }

        public string Description { get; set; }

        public IEnumerable<Services> Services { get; set; }
    }
}
