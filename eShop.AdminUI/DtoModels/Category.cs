using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.AdminUI.DtoModels
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ObservableCollection<Product> Products { get; set; } 
    }
}
