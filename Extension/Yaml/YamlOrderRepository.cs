using Basekeeper.Command;
using Basekeeper.Entity;
using TelemRec;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

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
        try
        {
            using (StreamReader streamReader = new StreamReader(ORDERS_YAML))
            {
                IDeserializer deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                var items = deserializer.Deserialize<List<Order>>(streamReader);
                logger.Info($"Loaded {string.Join(",", items)}");
                return items;
            }
        }
        catch (FileNotFoundException)
        {
            logger.Info($"{ORDERS_YAML} doesn't exist");
            return new List<Order>();
        }
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
        using (StreamWriter streamWriter = new StreamWriter(File.Open(ORDERS_YAML, FileMode.Create)))
        {
            ISerializer serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            serializer.Serialize(streamWriter, lineItems);
        }
    }
}
