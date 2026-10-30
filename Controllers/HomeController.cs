using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _25DH110591_MyStore.Models;
using _25DH110591_MyStore.Models.ViewModel;
using PagedList;

namespace _25DH110591_MyStore.Controllers
{
    public class HomeController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();
        public ActionResult Index(string searchTerm, int? page)
        {
            var model = new HomeProductVM();

            var products = db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                model.SearchTerm = searchTerm;

                products = products.Where(p =>
                    p.ProductName.Contains(searchTerm) ||
                    p.ProductDecription.Contains(searchTerm) ||
                    p.Category.CategoryName.Contains(searchTerm));
            }

            int pageNumber = page ?? 1;
            int pageSize = 6;

            model.PageNumber = pageNumber;
            model.PageSize = pageSize;

            model.FeaturedProducts = products
                .OrderByDescending(p => p.OrderDetails.Count())
                .Take(10)
                .ToList();

            model.NewProducts = products
                .OrderBy(p => p.OrderDetails.Count())
                .Take(20)
                .ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        public ActionResult ProductDetail(int id, int quantity = 1, int? page = 1)
        {
            var product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            var model = new ProductDetailVM();

            model.Product = product;
            model.Quantity = quantity;
            model.RelatedProducts = db.Products
                .Where(p => p.CategoryID == product.CategoryID
                         && p.ProductID != product.ProductID)
                .Take(8)
                .ToList();

            int pageNumber = page ?? 1;
            int pageSize = 3;

            model.PageNumber = pageNumber;
            model.PageSize = pageSize;
            model.TopProducts = db.Products
                .Where(p => p.CategoryID == product.CategoryID
                         && p.ProductID != product.ProductID)
                .OrderByDescending(p => p.OrderDetails.Count())
                .Take(8)
                .ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}