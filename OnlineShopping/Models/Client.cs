using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Display(Name = "Име")]
        public string FirstName { get; set; }
        [Display(Name = "Презиме")]
        public string LastName { get; set; }
        [Display(Name = "Име и презиме")]
        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName);  }
        }
        public ICollection<ShoppingCart> ProductSizes { get; set; }
    }
}
