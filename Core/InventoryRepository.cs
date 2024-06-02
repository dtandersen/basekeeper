using System.Xml.Linq;
using Core;

public interface InventoryRepository
{
    void Save(List<LineItem> items);
    List<LineItem> All();
}
