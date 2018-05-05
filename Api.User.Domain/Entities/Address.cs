using System.ComponentModel.DataAnnotations;

namespace Api.User.Domain.Entities
{
    public class Address
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        public int UserId { get; private set; }

        [Required]
        [MaxLength(100)]
        public string AddressLine { get; private set; }
        public int Number { get; private set; }

        [MaxLength(100)]
        public string Complement { get; private set; }

        [Required]
        [MaxLength(100)]
        public string City { get; private set; }

        [Required]
        [MaxLength(2)]
        public string State { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; private set; }

        [MaxLength(100)]
        public string ZipCode { get; private set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }
    }
}
