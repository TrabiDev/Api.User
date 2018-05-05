using Api.User.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Api.User.Domain.Entities
{
    public class Rating
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        public int UserId { get; private set; }

        public ERatingType RatingType  { get;set; }



    }
}
