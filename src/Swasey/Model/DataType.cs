using System;
using System.Linq;

namespace Swasey.Model
{
    public sealed class DataType : IEquatable<DataType>
    {

        private readonly string _value;

        public DataType(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            _value = value;
            IsVoidType = Constants.DataType_Void.ToUpper().Equals(_value.ToUpper());
            IsEnumerable = false;
        }

        public string DefaultValue { get; set; }

        public bool HasDefaultValue { get { return !string.IsNullOrWhiteSpace(DefaultValue); } }

        public bool HasMaximumValue { get { return !string.IsNullOrWhiteSpace(MaximumValue); } }

        public bool HasMinimumValue { get { return !string.IsNullOrWhiteSpace(MinimumValue); } }

        public bool IsEnum { get; private set; }

        public bool IsEnumerable { get; set; }

        public bool IsEnumerableUnique { get; set; }

        public bool IsNullable { get; set; }

        public bool IsVoidType { get; private set; }

        public string[] EnumValues { get; private set; }

        public string MinimumValue { get; set; }

        public string MaximumValue { get; set; }

        public override string ToString()
        {
            return _value;
        }

        #region Equality

        public bool Equals(DataType other)
        {
            if (ReferenceEquals(null, other)) { return false; }
            if (ReferenceEquals(this, other)) { return true; }
            return string.Equals(_value, other._value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }
            return obj is DataType && Equals((DataType) obj);
        }

        public override int GetHashCode()
        {
            return (_value != null ? _value.GetHashCode() : 0);
        }

        public static bool operator ==(DataType left, DataType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DataType left, DataType right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Operators

        public static implicit operator string(DataType dataType)
        {
            return dataType._value;
        }

        public static implicit operator DataType(string candidate)
        {
            return new DataType(candidate);
        }

        #endregion

        #region Utilities

        internal static DataType ParseFromPropertyJObject(dynamic prop)
        {
            if (prop.ContainsKey("$ref"))
            {
                return new DataType((string) prop["$ref"]);
            }
            DataType type = null;

            var propType = (string) prop.type;
            if (!string.IsNullOrWhiteSpace(propType)) propType = propType.ToLowerInvariant();

            switch (propType)
            {
                case "array":
                    type = ParseArrayFromJObject(prop);
                    break;
                case "boolean":
                    type = new DataType(propType);
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

        internal static DataType ParseIntegerFromJObject(dynamic prop)
        {
            const string int32Format = "int32";

            var format = (string) prop.format;

            format = int32Format.Equals(format, StringComparison.InvariantCultureIgnoreCase)
                ? "int"
                : "long";

            return new DataType(format);
        }

        internal static DataType ParseNumberFromJObject(dynamic prop)
        {
            return new DataType((string) prop.format);
        }

        internal static DataType ParseStringFromJObject(dynamic prop)
        {
            if (!prop.ContainsKey("format"))
            {
                return new DataType("string");
            }

            var format = (string) prop.format;
            if (!string.IsNullOrWhiteSpace(format)) format = format.ToLowerInvariant();

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

            return new DataType(format);
        }

        internal static DataType ParseArrayFromJObject(dynamic prop)
        {
            if (!prop.ContainsKey("items"))
            {
                throw new SwaseyException("Expected 'items' property, but none found: '{0}'", prop);
            }
            DataType type = ParseFromPropertyJObject(prop.items);
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
