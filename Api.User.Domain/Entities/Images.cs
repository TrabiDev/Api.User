using Api.User.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Api.User.Domain.Entities
{
    public class Images
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        public int UserId { get; private set; }

        [Required]
        public EImagesType ImagesTypeId { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
