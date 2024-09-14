﻿namespace NStandard;

public static class TimePointValue
{
    public static TimePointValue<TValue> Create<TValue>(TValue value) => new(value);
}

public struct TimePointValue<TValue>
{
    public TimePointValue() : this(default) { }
    public TimePointValue(TValue? value)
    {
        TimePoint = DateTime.Now;
        _value = value;
    }

    public DateTime TimePoint { get; private set; }
    public TimeSpan TimeSpan => DateTime.Now - TimePoint;

    private TValue? _value;
    public TValue? Value
    {
        get => _value;
        set
        {
            if (!Equals(_value, value))
            {
                _value = value;
                TimePoint = DateTime.Now;
            }
        }
    }
}
