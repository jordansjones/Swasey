using System;
using System.Linq;

namespace Swasey.Model
{
    public abstract class BasePath : IEquatable<BasePath>
    {

        protected readonly string Value;

        protected BasePath(string value, Predicate<string> validator)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            if (!validator(value))
            {
                throw new ArgumentException("Invalid " + GetType().Name + ".", "value");
            }

            Value = value;
        }

        public bool Equals(BasePath other)
        {
            if (ReferenceEquals(null, other)) { return false; }
            if (ReferenceEquals(this, other)) { return true; }
            return string.Equals(Value, other.Value);
        }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }
            if (obj.GetType() != GetType()) { return false; }
            return Equals((BasePath) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(BasePath left, BasePath right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BasePath left, BasePath right)
        {
            return !Equals(left, right);
        }


    }
}
