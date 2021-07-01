using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class Size
    {
        public int Id { get; set; }
        [Display (Name = "Величина")]
        public int ProductSize { get; set; }
        public ICollection<ProductSize> Products { get; set; }
    }
}
