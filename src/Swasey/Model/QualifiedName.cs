using System;
using System.Linq;

namespace Swasey.Model
{
    public sealed class QualifiedName : IEquatable<QualifiedName>
    {

        private readonly string _value;

        public QualifiedName(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (!IsValid(value))
            {
                throw new ArgumentException("Invalid " + typeof (QualifiedName).Name + ".", "value");
            }

            if (Char.IsLower(value[0]))
            {
                value = Char.ToUpper(value[0]) + value.Substring(1);
            }
            _value = value;
        }

        public bool Equals(QualifiedName other)
        {
            if (ReferenceEquals(null, other)) { return false; }
            if (ReferenceEquals(this, other)) { return true; }
            return string.Equals(_value, other._value);
        }

        public static bool IsValid(string candidate)
        {
            return !string.IsNullOrWhiteSpace(candidate) && Char.IsLetter(candidate[0]);
        }

        public static bool TryParse(string candidate, out QualifiedName qualifiedName)
        {
            qualifiedName = null;

            if (!IsValid(candidate))
            {
                return false;
            }

            qualifiedName = new QualifiedName(candidate);
            return true;
        }

        public static implicit operator string(QualifiedName qualifiedName)
        {
            return qualifiedName._value;
        }

        public static implicit operator QualifiedName(string candidate)
        {
            return new QualifiedName(candidate);
        }

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }
            if (obj.GetType() != GetType()) { return false; }
            return Equals((QualifiedName) obj);
        }

        public override int GetHashCode()
        {
            return (_value != null ? _value.GetHashCode() : 0);
        }

        public static bool operator ==(QualifiedName left, QualifiedName right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(QualifiedName left, QualifiedName right)
        {
            return !Equals(left, right);
        }


    }
}
