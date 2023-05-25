﻿using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<HasProduct> HasProducts { get; set; }
        public ICollection<WantProduct> WantProducts { get; set; }

    }
}
