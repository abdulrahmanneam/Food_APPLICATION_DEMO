using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APPLICATION_DEMO.DAL.Models
{
    public class SalesCart
    {
        public string SaleId { get; set; }
        public List<addCartItem> AddCartItems { get; set; }

        private readonly FoodDBContext _context;

        // Constructor
        public SalesCart(FoodDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            AddCartItems = new List<addCartItem>();
        }

        public static SalesCart GetCart(IServiceProvider serviceProvider)
        {
            var session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            var context = serviceProvider.GetService<FoodDBContext>();
            if (session == null || context == null)
            {
                throw new InvalidOperationException("Session or FoodDBContext not found.");
            }

            string cartID = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartID);

            return new SalesCart(context) { SaleId = cartID };
        }

        public decimal GetTotal()
        {
            // Ensure the AddCartItems is populated
            var items = getSalesCartItems();
            return items.Sum(item => item.amount);
        }

        public void AddToCart(Food food, int amount)
        {
            if (food == null) throw new ArgumentNullException(nameof(food));

            var cartItem = _context.addCartItems.SingleOrDefault(
                s => s.food.FoodID == food.FoodID && s.addCartId == this.SaleId);

            if (cartItem == null)
            {
                cartItem = new addCartItem
                {
                    addCartId = this.SaleId,
                    food = food,
                    amount = amount
                };

                _context.addCartItems.Add(cartItem);
            }
            else
            {
                cartItem.amount += amount;
            }

            _context.SaveChanges();
        }

        public int RemoveFromCart(Food food)
        {
            if (food == null) throw new ArgumentNullException(nameof(food));

            var cartItem = _context.addCartItems.SingleOrDefault(
                s => s.food.FoodID == food.FoodID && s.addCartId == this.SaleId);

            int currentAmount = 0;

            if (cartItem != null)
            {
                if (cartItem.amount > 1)
                {
                    cartItem.amount--;
                    currentAmount = cartItem.amount;
                }
                else
                {
                    _context.addCartItems.Remove(cartItem);
                }

                _context.SaveChanges();
            }

            return currentAmount;
        }

        public List<addCartItem> getSalesCartItems()
        {
            return AddCartItems ?? (AddCartItems =
                _context.addCartItems.Where(c => c.addCartId == SaleId)
                .Include(s => s.food)
                .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _context.addCartItems.Where(cart => cart.addCartId == SaleId);

            _context.addCartItems.RemoveRange(cartItems);

            _context.SaveChanges();
        }
    }
}
