using Basekeeper.Entity;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Basekeeper.Repository.Yaml;

public class YamlInventoryRepository : InventoryRepository
{
    public YamlInventoryRepository()
    {
    }

    public List<LineItem> All()
    {
        try
        {
            using (StreamReader streamReader = new StreamReader("inventory.yaml"))
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
        Save(new List<LineItem>());
    }

    public void Save(List<LineItem> items)
    {
        using (StreamWriter streamWriter = new StreamWriter("inventory.yaml", false))
        {
            ISerializer serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            serializer.Serialize(streamWriter, items);
        }
    }
}
