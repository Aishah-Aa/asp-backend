using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCrafters_backend_teamwork.src.Abstrations;
using CodeCrafters_backend_teamwork.src.Entities;

namespace CodeCrafters_backend_teamwork.src.Service.cs
{
    public class OrderItemService : IOrderItemService
    {

        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public IEnumerable<OrderItemReadDto> FindMany()
        {
            var orderItems = _orderItemRepository.FindMany();
            var orderItemsRead = orderItems.Select(_mapper.Map<OrderItemReadDto>);
            return orderItemsRead;
        }


        public OrderItemReadDto? FindOne(Guid orderItemId)
        {

            OrderItem? orderItem = _orderItemRepository.FindOne(orderItemId);
            OrderItemReadDto? orderItemRead = _mapper.Map<OrderItemReadDto>(orderItem);
            return orderItemRead;
        }

        public OrderItemReadDto CreateOne(OrderItemReadDto OrderItem)

        {
            var newOrderItem = _mapper.Map<OrderItem>(OrderItem);
            var createdOrderItem = _orderItemRepository.CreateOne(newOrderItem);
            var createdOrderItemRead = _mapper.Map<OrderItemReadDto>(createdOrderItem);
            return createdOrderItemRead;
        }

        public OrderItemReadDto? UpdateOne(Guid id, OrderItem orderItem)

        {
            var updatOrderItem = _orderItemRepository.FindOne(id);
            if (updatOrderItem != null)
            {
                updatOrderItem.Quantity = orderItem.Quantity;
                var updatedOrderItemAllInfo = _orderItemRepository.UpdateOne(updatOrderItem);
                var updatedOrderItemRead = _mapper.Map<OrderItemReadDto>(updatedOrderItemAllInfo);
                return updatedOrderItemRead;
            }
            else return null;

        }

        public bool DeleteOne(Guid id)
        {
            return _orderItemRepository.DeleteOne(id);
        }
    }

    public class OrderItemReadDto
    {
    }
}



