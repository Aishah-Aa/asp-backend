using CodeCrafters_backend_teamwork.src.Entities;

public interface IOrderItemRepository
{
    public IEnumerable<OrderItem> FindMany();
    public OrderItem? FindOne(Guid orderItemId);
    public OrderItem CreateOne(OrderItem orderItem);
    public OrderItem UpdateOne(OrderItem updatedOrderItem);
    public bool DeleteOne(Guid orderItemId);
}
