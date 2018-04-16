using System.ComponentModel.DataAnnotations;

namespace Api.User.Domain.Entities
{
    public class Services
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        public int ProfessionalInformationsId { get; private set; }

        [Required]
        public string Name { get; private set; }
    }
}
