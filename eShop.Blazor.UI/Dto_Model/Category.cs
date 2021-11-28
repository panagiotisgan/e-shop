﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Dto_Model
{
    public class Category
    {
        public string Name { get; set; }
        public ObservableCollection<Product> Products { get; set; }
    }
}
