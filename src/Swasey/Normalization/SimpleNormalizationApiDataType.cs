using System;
using System.Linq;

namespace Swasey.Normalization
{
    internal class SimpleNormalizationApiDataType : NormalizationApiDataType
    {

        public SimpleNormalizationApiDataType(dynamic jObject)
        {
            JObject = jObject;
        }

        #region Utilities

        internal static SimpleNormalizationApiDataType ParseFromJObject(object obj)
        {
            dynamic prop = obj;
            var type = new SimpleNormalizationApiDataType(prop);

            type.TryParseTypeFromJObject(obj);
            type.TryParseEnumFromJObject(obj);

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

            if (string.IsNullOrWhiteSpace(type.TypeName))
            {
                type.TypeName = "object";
            }

            return type;
        }

        public void TryParseTypeFromJObject(object obj)
        {
            dynamic prop = obj;

            var propType = (string) prop.type;
            if (!String.IsNullOrWhiteSpace(propType)) { propType = propType.ToLowerInvariant(); }

            switch (propType)
            {
                case "array":
                    ParseArrayFromJObject(obj);
                    break;
                case "boolean":
                    IsPrimitive = true;
                    TypeName = propType;
                    break;
                case "integer":
                    ParseIntegerFromJObject(obj);
                    break;
                case "number":
                    ParseNumberFromJObject(obj);
                    break;
                case "string":
                    ParseStringFromJObject(obj);
                    break;
                case "void":
                    TypeName = propType;
                    break;
            }

            if (prop.ContainsKey("$ref"))
            {
                TypeName = (string) prop["$ref"];
                IsModelType = true;
            }
            else if (string.IsNullOrEmpty(TypeName) && !string.IsNullOrWhiteSpace(propType))
            {
                // Assume that the property is a model type
                TypeName = (string) prop.type;
                IsModelType = true;
            }
        }

        public void TryParseEnumFromJObject(object obj)
        {
            dynamic prop = obj;
            if (!prop.ContainsKey("enum")) return;

            var count = prop["enum"].Count;
            var values = new string[count];
            for (var i = 0; i < count; i++)
            {
                values[i] = (string) prop["enum"][i];
            }

            IsEnum = true;
            EnumValues = values;
        }

        public void ParseIntegerFromJObject(dynamic prop)
        {
            const string int32Format = "int32";
            const string int64Format = "int64";

            var format = prop.ContainsKey("format") ? (string) prop.format : int64Format;

            format = int32Format.Equals(format, StringComparison.InvariantCultureIgnoreCase)
                ? "int"
                : "long";

            TypeName = format;
            IsPrimitive = true;
        }

        public void ParseNumberFromJObject(dynamic prop)
        {
            const string floatFormat = "float";
            const string doubleFormat = "double";

            var format = prop.ContainsKey("format") ? (string) prop.format : doubleFormat;

            format = floatFormat.Equals(format, StringComparison.InvariantCultureIgnoreCase)
                ? "float"
                : "double";

            TypeName = format;
            IsPrimitive = true;
        }

        public void ParseStringFromJObject(dynamic prop)
        {
            var propType = (string) prop.type;
            var normalPropType = propType;
            if (!string.IsNullOrWhiteSpace(normalPropType)) { normalPropType = normalPropType.ToLowerInvariant(); }

            var isPrimitive = false;

            if (!"string".Equals(normalPropType) && !prop.ContainsKey("format"))
            {
                TypeName = propType;
            }
            if (!prop.ContainsKey("format"))
            {
                TypeName = "string";
            }
            else
            {
                var format = (string) prop.format;
                if (!string.IsNullOrWhiteSpace(format)) { format = format.ToLowerInvariant(); }

                switch (format)
                {
                    case "date":
                    case "date-time":
                        format = "System.DateTime";
                        isPrimitive = true;
                        break;
                    default:
                        // Otherwise just use the format
                        break;
                }
                TypeName = format;
            }

            IsPrimitive = isPrimitive;
        }

        public void ParseArrayFromJObject(dynamic prop)
        {
            IsEnumerable = true;
            IsEnumerableUnique = prop.ContainsKey("uniqueItems") && ((bool) prop.uniqueItems);


            if (!prop.ContainsKey("items")) return;

            object items = prop.items;
            var type = ParseFromJObject(items);

            TypeName = type.TypeName;
        }

        #endregion
    }
}
