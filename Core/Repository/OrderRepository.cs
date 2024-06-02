using Basekeeper.Entity;

namespace Basekeeper.Repository;

public interface OrderRepository
{
    List<LineItem> All();

    void Reset();

    void Save(List<LineItem> lineItems);
}
