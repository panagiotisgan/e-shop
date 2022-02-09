using System.Collections.ObjectModel;

namespace eShop.Model
{
    public class State:BaseEntity
    {
        public const int NameMaxLength = 120;
        public string Name { get; set; }
        public long CountryId { get; set; }
        public Country Country { get; set; }
        public ObservableCollection<City> Cities { get; set; }
    }
}