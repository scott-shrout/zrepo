using DEPUI.Extensions;
using DEPUI.Interfaces.Schema;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace DEPUI.Models.SchemaNode
{
    public abstract class SchemaNode
    {
        public string? Name { get; set; }
        public object? ExampleValue { get; set; }
        public object? DefaultValue { get; set; }
        public string? Description { get; set; }
        public abstract string? TypeDisplayName { get; }
        private SchemaNode? parent;
        public SchemaNode? Parent
        {
            get
            {
                return parent;
            }
            set
            {
                if (value != null)
                {
                    NodePath = value.NodePath + "." + Name;
                }
                parent = value;
            }
        }
        public string? NodePath { get; protected set; }

        public virtual SchemaNode[] TreeNodes => new SchemaNode[] { this };

        private static Dictionary<string, Type>? schemaSpecificationTypeMappings;
        public static Dictionary<string, Type> SchemaSpecificationTypeMappings
        {
            get
            {
                if (schemaSpecificationTypeMappings == null)
                {
                    schemaSpecificationTypeMappings = typeof(SchemaNode)
                        .Assembly
                        .GetTypes()
                        .Where(type => type.GetInterface("IHasSpecificationTypeName") != null && !type.IsAbstract)
                        .Select(type => new { typeName = type.GetProperty("SpecificationTypeName")!.GetValue(Activator.CreateInstance(type)), type })
                        .ToDictionary(item => item.typeName!.ToString()!, item => item.type);
                };

                return schemaSpecificationTypeMappings;
            }
        }

        private static Dictionary<string, Type>? typeDisplayMappings;
        public static Dictionary<string, Type> TypeDisplayMappings
        {
            get
            {
                if (typeDisplayMappings == null)
                {
                    typeDisplayMappings = typeof(SchemaNode)
                    .Assembly
                    .GetTypes()
                    .Where(type => type.BaseType == typeof(SchemaNode) && !type.IsAbstract)
                    .Select(type => new { typeName = type.GetProperty("TypeDisplayName")!.GetValue(Activator.CreateInstance(type)), type })
                    .ToDictionary(item => item.typeName!.ToString()!, item => item.type);
                };

                return typeDisplayMappings;
            }
        }


         public virtual bool HasDisplayProperties => ExampleValue != null;

        public static SchemaNode? FromPropertySpecification(string name, SchemaNode parent, IDictionary<string, object?> propertyValues, Func<IDictionary<string, object?>, SchemaNode, SchemaNode[]> childrenBuilder)
        {
            if (propertyValues.TryGetValue("oneOf", out var oneOfObject) && oneOfObject is object[] objectArrayValue) {
                return new OneOfSchemaNode(name, objectArrayValue, parent, childrenBuilder);
            }
            else if (propertyValues.TryGetValue("type", out var typeObject))
            {
                var typeValue = typeObject as string;
                Type newObjectType;
                if (typeValue == "array")
                {
                    var items = propertyValues["items"] as IDictionary<string, object?>;
                    if (items!.TryGetValue("type", out var itemTypeValue) && itemTypeValue!.ToString() == "string")
                    {
                        newObjectType = typeof(PrimitiveArraySchemaNode);
                    }
                    else
                    {
                        newObjectType = typeof(ObjectArraySchemaNode);
                    }
                }
                else
                {
                    newObjectType = SchemaSpecificationTypeMappings[typeValue!];
                }
                return Activator.CreateInstance(newObjectType, name, parent, propertyValues, childrenBuilder) as SchemaNode;
            }
            else if (propertyValues.TryGetValue("allOf", out var allOfObject) && allOfObject is object[] objectArray)
            {
                var enumItem = objectArray[0] as IDictionary<string?, object?>;
                var enumValues = enumItem?["enum"] as object[] ?? Array.Empty<object>();
                return new StringSchemaNode() { Name = name, Parent = parent, EnumerationValues = enumValues!.Select(enumValue => enumValue.ToString()).ToArray()! };
            }

            return null;

        }

        public SchemaNode(string name, SchemaNode parent, IDictionary<string, object?> propertyValues)
        {
            Name = name;
            Parent = parent;
            Description = propertyValues.GetValueOrDefault("description")?.ToString();
            DefaultValue = propertyValues.GetValueOrDefault("default")?.ToString();
            ExampleValue = propertyValues.GetValueOrDefault("example");
        }

        public SchemaNode(string name, SchemaNode parent)
        {
            Name = name;
            Parent = parent;
        }
            public SchemaNode() { }

        public abstract IDictionary<string,object?> GetAvroObject();
    }
}
