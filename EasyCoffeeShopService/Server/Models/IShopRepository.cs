namespace Server;

public interface IShopRepository
{
    List<Order> GetAllOrders();
    void AddOrder(Order order);
    void DeleteOrder(int id);

}