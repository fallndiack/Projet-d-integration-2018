using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamb_Ji_ViewModel
{
   public class ContactViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Nom { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Sujet { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
