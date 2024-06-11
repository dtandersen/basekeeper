using Basekeeper.Entity;
using TelemRec;

namespace Basekeeper.Repository.Yaml;

public class YamlOrderRepository : OrderRepository
{
    private const string ORDERS_YAML = "orders.yaml";
    private Logger logger;

    public YamlOrderRepository()
    {
        logger = LogFactory.GetLogger(GetType());
    }

    public List<Order> All()
    {
        return YamlHelper.Read(ORDERS_YAML, new List<Order>());
    }

    public void Reset()
    {
        logger.Debug("Resetting...");
        File.Delete(ORDERS_YAML);
    }

    public void Create(Order order)
    {
        logger.Info($"Saving {order}...");
        var items = All();
        var newitems = items.Append(order);
        ReplaceAll(newitems);
    }

    public void ReplaceAll(IEnumerable<Order> lineItems)
    {
        logger.Info($"Saving {string.Join(",", lineItems)}...");
        YamlHelper.Write(ORDERS_YAML, lineItems.ToList());
    }

    public void Delete(string item)
    {
        var orders = All();
        orders.RemoveAll(o => o.Item == item);
        ReplaceAll(orders);
    }
}
