using CodeCrafters_backend_teamwork.src.Abstractions;
using CodeCrafters_backend_teamwork.src.Databases;
using CodeCrafters_backend_teamwork.src.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeCrafters_backend_teamwork.src.Reository
{
    public class OrderCheckoutRepo : IOrderCheckoutRepository
    {
        private DbSet<OrderCheckout> _checkouts;

        private DatabaseContext _databaseContext;

        public OrderCheckoutRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _checkouts = databaseContext.OrderCheckouts;
        }

        public IEnumerable<OrderCheckout> CreateOne(OrderCheckout newOrderCheckout)
        {
            _checkouts.Add(newOrderCheckout);
            _databaseContext.SaveChanges();
            return _checkouts;
        }

        public OrderCheckout? FindOne(Guid orderCheckoutId)
        {
            return _checkouts.FirstOrDefault((orderCheckout) => orderCheckout.Id == orderCheckoutId);
        }

        public IEnumerable<OrderCheckout> FindMany()
        {
            return _checkouts;
        }

        public IEnumerable<OrderCheckout>? DeleteOne(Guid orderCheckoutId)
        {
            var foundOrderCheckout = FindOne(orderCheckoutId);

            if (foundOrderCheckout != null)
            {
                _checkouts.Remove(foundOrderCheckout);
            }
            _databaseContext.SaveChanges();
            return _checkouts;

        }


        public OrderCheckout UpdateOne(Guid orderCheckoutId, OrderCheckout updatedCheckout)
        {

            OrderCheckout? orderCheckout = _checkouts.FirstOrDefault(orderCheckout => orderCheckout.Id == orderCheckoutId);
            if (orderCheckout != null)
            {

                _checkouts.Update(orderCheckout);
            }
            _databaseContext.SaveChanges();

            return updatedCheckout;

        }
    }
}