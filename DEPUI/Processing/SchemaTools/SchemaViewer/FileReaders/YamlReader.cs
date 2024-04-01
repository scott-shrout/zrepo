using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
namespace DEPUI.Processing.SchemaTools.SchemaViewer.FileReaders
{
    public static class YamlReader
    {
        public static IDictionary<string,object?>? GetYamlContents(Stream inputStream)
        {
            var reader = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            using var yamlReader = new StreamReader(inputStream);
            inputStream.Seek(0, SeekOrigin.Begin);
            var yamlContents = yamlReader.ReadToEnd();

            var objectArray = reader.Deserialize<Dictionary<object, object?>>(yamlContents);
            return GetConvertedObject(objectArray) as IDictionary<string, object?>;
        }

      
        private static object? GetConvertedObject(object? inputObject)
        {
            return inputObject switch
            {
                IDictionary<object, object?> inputDictionary => inputDictionary.ToDictionary(dictionary => dictionary.Key.ToString()!, dictionary => GetConvertedObject(dictionary.Value)),
                object[] objectArray => objectArray.Select(item => GetConvertedObject(item)).ToArray(),
                List<object> objectList => objectList.Select(item => GetConvertedObject(item)).ToArray(),
                _ => inputObject
            };
            }
        }
    
}
