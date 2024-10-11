using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASI.Basecode.Data.Models
{
    public class Admin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [MaxLength(255)]
        public string Role { get; set; } = "Admin"; // Default value set here
        public string Password { get; set; }
    }
}
