using Core;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class YamlInventoryRepository : InventoryRepository
{
    // private List<LineItem> Items;

    public YamlInventoryRepository()
    {
        // Items = new List<LineItem>    {
        //     new LineItem(Item: "Iron", Quantity: 1),
        //     new LineItem(Item: "Bronze", Quantity: 2),
        //     new LineItem(Item: "Gold", Quantity: 3),
        //     new LineItem(Item: "Diamond", Quantity: 4),
        // };
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
        catch (FileNotFoundException e)
        {
            return new List<LineItem>();
        }
    }

    public void Save(List<LineItem> items)
    {
        // items = new List<LineItem>
        // {
        //     new LineItem(Item: "Iron", Quantity: 1),
        //     new LineItem(Item: "Bronze", Quantity: 2),
        //     new LineItem(Item: "Gold", Quantity: 3),
        //     new LineItem(Item: "Diamond", Quantity: 4),
        // };
        // Console.WriteLine($"{items}");
        using (StreamWriter streamWriter = new StreamWriter("inventory.yaml", false))
        {
            ISerializer serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            serializer.Serialize(streamWriter, items);
        }
        // this.Items = items;
    }
}
