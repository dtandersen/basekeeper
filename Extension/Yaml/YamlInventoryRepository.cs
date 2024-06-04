using Basekeeper.Entity;

namespace Basekeeper.Repository.Yaml;

public class YamlInventoryRepository : InventoryRepository
{
    public YamlInventoryRepository()
    {
    }

    public List<LineItem> All()
    {
        return YamlHelper.Read("inventory.yaml", new List<LineItem>());
    }

    public void Reset()
    {
        Save(new List<LineItem>());
    }

    public void Save(List<LineItem> items)
    {
        YamlHelper.Write("inventory.yaml", items);
    }
}
