using System;
using System.Linq;

namespace Swasey.Model
{
    public sealed class ParameterName : IEquatable<ParameterName>
    {

        private readonly string _value;

        public ParameterName(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (!IsValid(value))
            {
                throw new ArgumentException("Invalid " + typeof (QualifiedName).Name + ".", "value");
            }

            if (Char.IsUpper(value[0]))
            {
                value = Char.ToLower(value[0]) + value.Substring(1);
            }
            _value = value;
        }

        public bool Equals(ParameterName other)
        {
            if (ReferenceEquals(null, other)) { return false; }
            if (ReferenceEquals(this, other)) { return true; }
            return string.Equals(_value, other._value);
        }

        public static bool IsValid(string candidate)
        {
            return !string.IsNullOrWhiteSpace(candidate) && Char.IsLetter(candidate[0]);
        }

        public static bool TryParse(string candidate, out ParameterName parameterName)
        {
            parameterName = null;

            if (!IsValid(candidate))
            {
                return false;
            }

            parameterName = new ParameterName(candidate);
            return true;
        }

        public static implicit operator string(ParameterName parameterName)
        {
            return parameterName._value;
        }

        public static implicit operator ParameterName(string candidate)
        {
            return new ParameterName(candidate);
        }

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }
            return obj is ParameterName && Equals((ParameterName) obj);
        }

        public override int GetHashCode()
        {
            return (_value != null ? _value.GetHashCode() : 0);
        }

        public static bool operator ==(ParameterName left, ParameterName right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ParameterName left, ParameterName right)
        {
            return !Equals(left, right);
        }

    }
}
