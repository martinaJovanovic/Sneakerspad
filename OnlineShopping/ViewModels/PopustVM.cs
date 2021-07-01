using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModels
{
    public class PopustVM
    {
        public Product Product { get; set; }
        [Display(Name = "Попуст во %")]
        public int Popust { get; set; }
    }
}
