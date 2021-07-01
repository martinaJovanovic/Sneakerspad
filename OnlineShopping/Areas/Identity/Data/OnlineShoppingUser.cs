using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineShopping.Models;

namespace OnlineShopping.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the OnlineShoppingUser class
    public class OnlineShoppingUser : IdentityUser
    {
        [Display(Name = "Улога")]
        public string Role { get; set; }
        public int? ManagerId { get; set; }
        [Display(Name = "Менаџер")]
        [ForeignKey("ManagerId")]
        public Manager Manager { get; set; }
        public int? ClientId { get; set; }
        [Display(Name = "Клиент")]
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        [Display(Name = "Име")]
        public String FirstName { get; set; }
        [Display(Name = "Презиме")]
        public String LastName { get; set; }
        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }
    }
}
