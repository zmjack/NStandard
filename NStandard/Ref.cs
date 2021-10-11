namespace NStandard
{
    public static class Ref
    {
        public static StructRef<T> New<T>() where T : struct => new();
        public static StructRef<T> Clone<T>(T value) where T : struct => new() { Ref = value };
        public static StructRef<T> Initialize<T>() where T : struct, IStructInitialize
        {
            var value = new T();
            value.InitializeStruct();
            return new() { Ref = value };
        }
    }

    public class StructRef<T> where T : struct
    {
        public T Ref;

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case StructRef<T> _obj: return Ref.Equals(_obj.Ref);
                case T _obj: return Ref.Equals(_obj);
                default: return Ref.Equals(obj);
            }
        }

        public override int GetHashCode() => Ref.GetHashCode();

        public static implicit operator T(StructRef<T> operand) => operand.Ref;
        public static implicit operator StructRef<T>(T operand) => new() { Ref = operand };

        public override string ToString() => Ref.ToString();
    }

}
