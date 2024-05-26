using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCrafters_backend_teamwork.src.DTO
{
    public class OrderItemCreateDto
    {
        public int Quantity { get; set; }
        public Guid StockId { get; set; }
        public double Price { get; set; }
    }
}