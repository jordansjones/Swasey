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
        }

        public bool IsVoidType { get; private set; }

        public bool Equals(DataType other)
        {
            if (ReferenceEquals(null, other)) { return false; }
            if (ReferenceEquals(this, other)) { return true; }
            return string.Equals(_value, other._value);
        }

        public static implicit operator string(DataType dataType)
        {
            return dataType._value;
        }

        public static implicit operator DataType(string candidate)
        {
            return new DataType(candidate);
        }

        public override string ToString()
        {
            return _value;
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

    }
}
