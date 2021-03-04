using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Model
{
    public class Product : BaseEntity
    {
        public const int NameMaxLength = 120;
        public const int CodeMaxLength = 250;
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal StockQty { get; set; }
        public bool InStock { get; set; }

        public decimal Price { get; set; }
        /*Να κοοτάξω ξανά πως θα βάλω το παρακάτω πεδίο, όχι string μάλλον*/
        public List<Image> Images { get; set; } = new List<Image>();
        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public void SetInStock(decimal stockQty)
        {
            if (stockQty <= 0)
            {
                InStock = false;
            }

            InStock = true;
        }

        //Error, για να παίξει σωστά πρέπει να πάρω το imagePath και να φέρω το image να το διαχειριστώ μάλλον
        public void SetImages(string titleAttribute,string imagePath)
        {
            Image image = new Image();
            image.ProductId = this.Id;
            image.TitleAttribute = titleAttribute;
            image.ImagePath = imagePath;

            Images.Add(image);
        }
    }
}