using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Basekeeper.Repository.Yaml;

class YamlHelper
{
    public static T Read<T>(String path, T defaultValue)
    {
        try
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                IDeserializer deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                var Items = deserializer.Deserialize<T>(streamReader);
                return Items;
            }
        }
        catch (FileNotFoundException)
        {
            return defaultValue;
        }
    }

    public static void Write<T>(String path, T value)
    {
        using (StreamWriter streamWriter = new StreamWriter(path, false))
        {
            ISerializer serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            serializer.Serialize(streamWriter, value);
        }
    }
}