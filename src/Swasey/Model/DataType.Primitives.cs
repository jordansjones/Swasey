
using System;
using System.Linq;

namespace Swasey.Model
{
    public sealed partial class DataType
    {

        public bool IsBool { get; private set; }
        public bool IsByte { get; private set; }
        public bool IsChar { get; private set; }
        public bool IsDecimal { get; private set; }
        public bool IsDouble { get; private set; }
        public bool IsFloat { get; private set; }
        public bool IsInt { get; private set; }
        public bool IsLong { get; private set; }
        public bool IsSByte { get; private set; }
        public bool IsShort { get; private set; }
        public bool IsString { get; private set; }
        public bool IsUInt { get; private set; }
        public bool IsULong { get; private set; }
        public bool IsUShort { get; private set; }
        public bool IsVoid { get; private set; }

        partial void InitializePrimitiveShortcuts(string type)
        {
            IsBool = Constants.DataType_Bool.Equals(type);
            IsByte = Constants.DataType_Byte.Equals(type);
            IsChar = Constants.DataType_Char.Equals(type);
            IsDecimal = Constants.DataType_Decimal.Equals(type);
            IsDouble = Constants.DataType_Double.Equals(type);
            IsFloat = Constants.DataType_Float.Equals(type);
            IsInt = Constants.DataType_Int.Equals(type);
            IsLong = Constants.DataType_Long.Equals(type);
            IsSByte = Constants.DataType_SByte.Equals(type);
            IsShort = Constants.DataType_Short.Equals(type);
            IsString = Constants.DataType_String.Equals(type);
            IsUInt = Constants.DataType_UInt.Equals(type);
            IsULong = Constants.DataType_ULong.Equals(type);
            IsUShort = Constants.DataType_UShort.Equals(type);
            IsVoid = Constants.DataType_Void.Equals(type);
        }

    }

}