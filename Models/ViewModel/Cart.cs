using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _25DH110591_MyStore.Models.ViewModel
{
    public class Cart
    {
        private List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void AddItem(
            int productId,
            string productName,
            int quantity,
            decimal unitPrice,
            string productImage)
        {
            var existingItem = items
                .FirstOrDefault(i => i.ProductID == productId);

            if (existingItem == null)
            {
                items.Add(new CartItem
                {
                    ProductID = productId,
                    ProductName = productName,
                    Quantity = quantity,
                    UnitPrice = unitPrice,
                    ProductImage = productImage
                });
            }
            else
            {
                existingItem.Quantity += quantity;
            }
        }
        public void RemoveItem(int productId)
        {
            items.RemoveAll(i => i.ProductID == productId);
        }
        public decimal TotalValue()
        {
            return items.Sum(i => i.TotalPrice);
        }
        public void Clear()
        {
            items.Clear();
        }
        public void UpdateQuantity(int productId, int quantity)
        {
            var item = items
                .FirstOrDefault(i => i.ProductID == productId);

            if (item != null)
            {
                item.Quantity = quantity;
            }
        }
    }
}