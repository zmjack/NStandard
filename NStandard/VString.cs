using System;

namespace NStandard
{
    public struct VString
    {
        public string String;

        public VString(string str)
        {
            String = str;
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case VString _: return String.Equals(((VString)obj).String);
                case string _obj: return String.Equals(_obj);
                default: return Equals(String, obj);
            }
        }

        public override int GetHashCode() => String.GetHashCode();
        public static bool operator ==(VString left, VString right) => left.Equals(right);
        public static bool operator !=(VString left, VString right) => !left.Equals(right);

        public static implicit operator string(VString operand) => operand.String;
        public static implicit operator VString(string operand) => new(operand);

        public override string ToString() => String.ToString();

        public static string GetString<T>(T obj) where T : struct
        {
            if (obj is VString)
                return obj.GetReflector().Field<string>(nameof(String)).Value;
            else return null;
        }
    }
}
