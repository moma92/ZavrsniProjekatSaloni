using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZavrsniProjekatSaloni.DTOs
{
    public class UserDTO
    {
        

        [Required(ErrorMessage = "Polje je obavezno!")]
        [Display(Name = "Korisnicko ime")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }
    }
}