namespace NStandard
{
    public static class Ref
    {
        public static Ref<T> Bind<T>(T value) where T : struct => new Ref<T>(value);
    }

    public class Ref<T> where T : struct
    {
        public object RefValue;
        public T Value => (T)RefValue;

        public Ref(T value) => RefValue = value;
        public Ref(object value) => RefValue = value;

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case Ref<T> _obj: return Value.Equals(_obj.Value);
                case T _obj: return Value.Equals(_obj);
                default: return obj.Return(x => Value.Equals(x), @default: false);
            }
        }

        public override int GetHashCode() => RefValue.GetHashCode();
        public static bool operator ==(Ref<T> left, Ref<T> right) => left.RefValue == right.RefValue;
        public static bool operator !=(Ref<T> left, Ref<T> right) => left.RefValue != right.RefValue;

        public static implicit operator T(Ref<T> operand) => operand.Value;
        public static implicit operator Ref<T>(T operand) => new Ref<T>(operand);

        public override string ToString() => RefValue.ToString();
    }
}
