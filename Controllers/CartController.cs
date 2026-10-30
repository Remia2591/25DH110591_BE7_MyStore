using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _25DH110591_MyStore.Models;
using _25DH110591_MyStore.Models.ViewModel;

namespace _25DH110591_MyStore.Controllers
{
    public class CartController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();
        private CartService GetCartService()
        {
            return new CartService(Session);
        }
        public ActionResult Index()
        {
            var cart = GetCartService().GetCart();
            return View(cart);
        }

        public ActionResult AddToCart(int id, int quantity = 1)
        {
            var product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            if (quantity < 1)
            {
                quantity = 1;
            }
            var cart = GetCartService().GetCart();
            cart.AddItem(
                product.ProductID,
                product.ProductName,
                quantity,
                product.ProductPrice,
                product.ProductImage
            );
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(int id)
        {
            var cart = GetCartService().GetCart();
            cart.RemoveItem(id);
            return RedirectToAction("Index");
        }
        public ActionResult ClearCart()
        {
            GetCartService().ClearCart();
            return RedirectToAction("Index");
        }
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            if (quantity < 1)
            {
                quantity = 1;
            }
            var cart = GetCartService().GetCart();
            cart.UpdateQuantity(id, quantity);
            return RedirectToAction("Index");
        }
    }
}