using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserWebAPI.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The UserName value cannot exceed 200 characters. ")]
        public string UserName { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The Name value cannot exceed 300 characters. ")]
        public string Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Email value cannot exceed 200 characters. ")]
        public string Email { get; set; }

        [StringLength(200, ErrorMessage = "Phone value cannot exceed 200 characters. ")]
        public string Phone { get; set; }
    }
}
