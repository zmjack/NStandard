namespace NStandard
{
    public struct ValueWrapper<T> where T : class
    {
        private readonly T _value;
        public T Value => _value;

        public ValueWrapper(T value)
        {
            _value = value;
        }

        public override bool Equals(object other)
        {
            return other switch
            {
                ValueWrapper<T> wrapper => _value?.Equals(wrapper.Value) ?? false,
                T otherValue => _value?.Equals(otherValue) ?? false,
                _ => Equals(_value, other),
            };
        }

        public override int GetHashCode() => Value.GetHashCode();
        public static bool operator ==(ValueWrapper<T> left, ValueWrapper<T> right) => left.Equals(right);
        public static bool operator !=(ValueWrapper<T> left, ValueWrapper<T> right) => !left.Equals(right);

        public static implicit operator T(ValueWrapper<T> operand) => operand._value;
        public static implicit operator ValueWrapper<T>(T operand) => new(operand);
        public override string ToString() => _value.ToString();
    }
}
