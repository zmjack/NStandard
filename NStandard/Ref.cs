namespace NStandard
{
    public static class Ref
    {
        public static Ref<T> New<T>() where T : struct => new();
        public static Ref<T> Clone<T>(T value) where T : struct => new() { Struct = value };
    }

    public class Ref<T> where T : struct
    {
        public T Struct;

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case Ref<T> _obj: return Struct.Equals(_obj.Struct);
                case T _obj: return Struct.Equals(_obj);
                default: return Struct.Equals(obj);
            }
        }

        public override int GetHashCode() => Struct.GetHashCode();

        public static implicit operator T(Ref<T> @ref) => @ref.Struct;
        public static implicit operator Ref<T>(T @struct) => new() { Struct = @struct };

        public override string ToString() => Struct.ToString();
    }

}
