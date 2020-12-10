using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class AccountViewModel
    {
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(255, ErrorMessage = "Must be between 4 and 255 characters", MinimumLength = 4)]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 6 and 255 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
