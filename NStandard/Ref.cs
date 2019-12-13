using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
    public static class Ref
    {
        public static Ref<T> Bind<T>(T value) where T : struct => new Ref<T>(value);
    }

    public class Ref<T>
        where T : struct
    {
        public object RefValue;
        public T Value => (T)RefValue;

        public Ref(T value) => RefValue = value;

        public static bool operator ==(Ref<T> left, T right) => false;
        public static bool operator !=(Ref<T> left, T right) => true;

        public static bool operator ==(Ref<T> left, Ref<T> right) => left.RefValue == right.RefValue;
        public static bool operator !=(Ref<T> left, Ref<T> right) => left.RefValue != right.RefValue;

        public static implicit operator T(Ref<T> @this) => @this.Value;

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case Ref<T> _obj: return Value.Equals(_obj.Value);
                case T _obj: return Value.Equals(_obj);
                default: return obj.Return(x => Value.Equals(x), x => false);
            }
        }

        public override int GetHashCode() => RefValue.GetHashCode();
        public override string ToString() => RefValue.ToString();
    }
}
