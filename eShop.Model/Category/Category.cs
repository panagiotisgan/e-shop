using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace eShop.Model
{
    public class Category : BaseEntity
    { 
        public const int MaxLength = 25;
        public string Name { get; set; }
        public ObservableCollection<Product> Products { get; set; }
    }
}
