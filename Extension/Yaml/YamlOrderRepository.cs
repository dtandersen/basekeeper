using Basekeeper.Command;
using Basekeeper.Entity;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Basekeeper.Repository.Yaml;

public class YamlOrderRepository : OrderRepository
{
    private const string ORDERS_YAML = "orders.yaml";

    public List<LineItem> All()
    {
        try
        {
            using (StreamReader streamReader = new StreamReader(ORDERS_YAML))
            {
                IDeserializer deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                var Items = deserializer.Deserialize<List<LineItem>>(streamReader);
                return Items;
            }
        }
        catch (FileNotFoundException)
        {
            return new List<LineItem>();
        }
    }

    public void Reset()
    {
        var items = new List<LineItem>();
        Save(items);
    }

    public void Save(List<LineItem> lineItems)
    {
        Console.WriteLine($"Saving {string.Join(", ", lineItems)}");
        using (StreamWriter streamWriter = new StreamWriter(ORDERS_YAML, false))
        {
            ISerializer serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            serializer.Serialize(streamWriter, lineItems);
        }
    }
}
