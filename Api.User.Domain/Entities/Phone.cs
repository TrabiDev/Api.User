using System.ComponentModel.DataAnnotations;

namespace Api.User.Domain.Entities
{
    public class Phone
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        public int UserId { get; private set; }

        [Required]
        public string DDD { get; private set; }

        [Required]
        public string PhoneNumber { get; private set; }
    }
}
