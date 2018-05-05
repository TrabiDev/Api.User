using Api.User.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Api.User.Domain.Entities
{
    public class Rating
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        public int UserId { get; private set; }

        [Required]
        public ERatingType RatingType  { get; private set; }

        [Required]
        public float RatingValue { get; private set; }
    }
}
