using APPLICATION_DEMO.DAL.Models;
using APPLICATION_DEMO.DAL.Models.interfaces;
using Microsoft.VisualBasic;

namespace APPLICATION_DEMO.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FoodDBContext _foodDBContext;
        private readonly SalesCart _salesCart;

        public OrderRepository(FoodDBContext foodDBContext, SalesCart salesCart)
        {
            _foodDBContext = foodDBContext;
            _salesCart = salesCart;
            
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _foodDBContext.orders.Add(order);

            var addCartItems = _salesCart.AddCartItems;

            foreach( var item in addCartItems )
            {
                var OrderD = new OrderDetail()
                {
                    Amount = item.amount,
                    FoodId = item.food.FoodID,
                    OrderId = order.OrderId,
                    Price = item.food.Price
                };
                _foodDBContext.ordersDetail.Add(OrderD);
            }
            _foodDBContext.SaveChanges();

        }
        public void Add(Order order)
        {
            _foodDBContext.orders.Add(order); // _context هو كائن DbContext

        }
    }
}
