using System;
using System.Linq;

namespace Swasey.Normalization
{
    internal class SimpleNormalizationApiDataType : NormalizationApiDataType
    {

        public SimpleNormalizationApiDataType(string typeName, dynamic jObject)
        {
            TypeName = typeName;
            JObject = jObject;
        }

        #region Utilities

        internal static SimpleNormalizationApiDataType ParseFromJObject(dynamic prop)
        {
            if (prop.ContainsKey("$ref"))
            {
                return new SimpleNormalizationApiDataType((string) prop["$ref"], prop);
            }
            SimpleNormalizationApiDataType type = null;

            var propType = (string) prop.type;
            if (!String.IsNullOrWhiteSpace(propType)) { propType = propType.ToLowerInvariant(); }

            switch (propType)
            {
                case "array":
                    type = ParseArrayFromJObject(prop);
                    break;
                case "boolean":
                    type = new SimpleNormalizationApiDataType(propType, prop);
                    break;
                case "integer":
                    type = ParseIntegerFromJObject(prop);
                    break;
                case "number":
                    type = ParseNumberFromJObject(prop);
                    break;
                default: // "string"
                    type = ParseStringFromJObject(prop);
                    break;
            }

            if (type == null)
            {
                throw new SwaseyException("Failed to parse property type: '{0}'", propType);
            }

            if (prop.ContainsKey("enum"))
            {
                var count = prop["enum"].Count;
                var values = new string[count];
                for (var i = 0; i < count; i++)
                {
                    values[i] = (string) prop["enum"][i];
                }
                type.IsEnum = true;
                type.EnumValues = values;
            }

            if (prop.ContainsKey("defaultValue"))
            {
                type.DefaultValue = (string) prop.defaultValue;
            }
            if (prop.ContainsKey("minimum"))
            {
                type.MinimumValue = (string) prop.minimum;
            }
            if (prop.ContainsKey("maximum"))
            {
                type.MaximumValue = (string) prop.maximum;
            }
            if (prop.ContainsKey("nullable"))
            {
                type.IsNullable = (bool) prop.nullable;
            }

            return type;
        }

        internal static SimpleNormalizationApiDataType ParseIntegerFromJObject(dynamic prop)
        {
            const string int32Format = "int32";

            var format = (string) prop.format;

            format = int32Format.Equals(format, StringComparison.InvariantCultureIgnoreCase)
                ? "int"
                : "long";

            return new SimpleNormalizationApiDataType(format, prop);
        }

        internal static SimpleNormalizationApiDataType ParseNumberFromJObject(dynamic prop)
        {
            return new SimpleNormalizationApiDataType((string) prop.format, prop);
        }

        internal static SimpleNormalizationApiDataType ParseStringFromJObject(dynamic prop)
        {
            if (!prop.ContainsKey("format"))
            {
                return new SimpleNormalizationApiDataType("string", prop);
            }

            var format = (string) prop.format;
            if (!String.IsNullOrWhiteSpace(format)) { format = format.ToLowerInvariant(); }

            switch (format)
            {
                case "date":
                case "date-time":
                    format = "System.DateTime";
                    break;
                default:
                    // Otherwise just use the format
                    break;
            }

            return new SimpleNormalizationApiDataType(format, prop);
        }

        internal static SimpleNormalizationApiDataType ParseArrayFromJObject(dynamic prop)
        {
            if (!prop.ContainsKey("items"))
            {
                throw new SwaseyException("Expected 'items' property, but none found: '{0}'", prop);
            }
            var type = ParseFromJObject(prop.items);
            type.IsEnumerable = true;

            if (prop.ContainsKey("uniqueItems"))
            {
                type.IsEnumerableUnique = (bool) prop.uniqueItems;
            }

            return type;
        }

        #endregion
    }
}
