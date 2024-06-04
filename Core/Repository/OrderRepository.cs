using Basekeeper.Entity;

namespace Basekeeper.Repository;

public interface OrderRepository
{
    List<Order> All();

    void Reset();

    void ReplaceAll(IEnumerable<Order> orders);

    void Create(Order order);
}
