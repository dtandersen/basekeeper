
using Basekeeper.Entity;

namespace Basekeeper.Repository;

public interface InventoryRepository
{
    void Save(List<LineItem> items);

    List<LineItem> All();

    void Reset();
}
