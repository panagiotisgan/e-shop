using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace eShop.Model
{
    public class Country:BaseEntity
    {
        public const int NameMaxLength = 50;
        public string Name { get; set; }
        public ObservableCollection<State> States { get; set; }
        public ObservableCollection<User> Users { get; set; }

        //Test Comment
    }
}
