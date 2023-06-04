using System;

namespace NStandard
{
    public static class Ref
    {
        public static Ref<T> New<T>() where T : struct => new();
        public static Ref<T> Clone<T>(T value) where T : struct => new() { Value = value };
    }

    public class Ref<T> where T : struct
    {
        [Obsolete("Use Value instead.")]
        public T Struct => Value;
        public T Value;

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case Ref<T> _obj: return Value.Equals(_obj.Value);
                case T _obj: return Value.Equals(_obj);
                default: return Value.Equals(obj);
            }
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static implicit operator T(Ref<T> @ref) => @ref.Value;
        public static implicit operator Ref<T>(T @struct) => new() { Value = @struct };

        public override string ToString() => Value.ToString();
    }

}
