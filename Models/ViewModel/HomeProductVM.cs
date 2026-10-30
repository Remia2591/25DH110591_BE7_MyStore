using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;


namespace _25DH110591_MyStore.Models.ViewModel
{
    public class HomeProductVM
    {
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<Product> FeaturedProducts { get; set; }
        public IPagedList<Product> NewProducts { get; set; }
    }
}