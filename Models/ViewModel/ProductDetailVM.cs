using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace _25DH110591_MyStore.Models.ViewModel
{
    public class ProductDetailVM
    {
        public Product Product { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal EstimatedValue
        {
            get
            {
                if (Product == null)
                    return 0;

                return Quantity * Product.ProductPrice;
            }
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 3;
        public List<Product> RelatedProducts { get; set; }
        public IPagedList<Product> TopProducts { get; set; }
    }
}