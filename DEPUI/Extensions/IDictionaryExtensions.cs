namespace DEPUI.Extensions
{
    public static class IDictionaryExtensions
    {

        public static object? GetValueOrDefault(this IDictionary<string,object?> dictionary, string key)
        {
            return dictionary.TryGetValue(key, out var objectValue) ? objectValue : null;
        }
        public static object? GetNestedValue(this IDictionary<string, object?> dictionary, string keyPath)
        {
            return GetObjectValue(dictionary, keyPath.Split("."));
        }

        private static object? GetObjectValue(object inputObject, string[] remainingKeys)
        {
            if (inputObject is not IDictionary<string, object?> dictionaryValue || remainingKeys.Length == 0)
            {
                return inputObject;
            }

            var currentKey = remainingKeys[0];

            if (!dictionaryValue.ContainsKey(currentKey))
            {
                return null;
            }

            return GetObjectValue(dictionaryValue[currentKey]!, remainingKeys[1..]);

        }
    }
}
