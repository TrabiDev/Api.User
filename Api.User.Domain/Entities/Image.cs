using Api.User.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Api.User.Domain.Entities
{
    public class Image
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        public int UserId { get; private set; }

        [Required]
        public EImageType ImageTypeId { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
