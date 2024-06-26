
using Basekeeper.Entity;

namespace Basekeeper.Repository;

public interface InventoryRepository
{
    void Save(List<LineItem> items);

    void Save(LineItem item);

    List<LineItem> All();

    void Reset();

    void Delete(string item);
}
