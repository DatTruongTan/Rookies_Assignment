using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ServerBE.Models
{
    public class Customer: IdentityUser
    {
        public Customer() : base()
        {

        }

        public Customer(string userName) : base(userName)
        {

        }

        [PersonalData]
        public string FullName { get; set; }

        //[Key]
        //public string Id { get; set; }

        //[Required(ErrorMessage = "Please enter Name")]
        //[Display(Name = "Name")]
        //[StringLength(100)]
        //public string Name { get; set; }

        //[Required(ErrorMessage = "Please enter username")]
        //[Display(Name = "User Name")]
        //[StringLength(100)]
        //public string UserName { get; set; }

        //[Required(ErrorMessage = "Please enter password")]
        //[Display(Name = "Password")]
        //public int Password { get; set; }

        //[Required(ErrorMessage = "Please enter email")]
        //[Display(Name = "Email")]
        //[StringLength(100)]
        //public string Email { get; set; }
        
        //[Required(ErrorMessage = "Please choose gender")]
        //[Display(Name = "Gender")]
        //public string Gender { get; set; }
    }
}
