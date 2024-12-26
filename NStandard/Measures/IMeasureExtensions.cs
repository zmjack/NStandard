using NStandard.Measures;
using System.ComponentModel;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class IMeasureExtensions
{
    private static class Throws
    {
        public static InvalidOperationException UnableToAggregate(IMeasurable left, IMeasurable right) => new($"Unable to aggregate. ({left} + {right})");
    }

    public static TMeasure QSum<TMeasure>(this IEnumerable<IMeasureConvertible<TMeasure>> @this) where TMeasure : struct, IMeasurable, IAdditionMeasurable<TMeasure>
    {
        return QSum(from x in @this select x.Convert());
    }

    public static TMeasure QSum<TMeasure>(this IEnumerable<TMeasure> @this) where TMeasure : struct, IMeasurable, IAdditionMeasurable<TMeasure>
    {
        decimal sum = 0;

#if NET7_0_OR_GREATER
        var force = TMeasure.ForceAggregate;
        if (force)
        {
            foreach (var item in @this)
            {
                sum += item.Value;
            }

            return new TMeasure
            {
                Value = sum,
            };
        }
#endif
        var enumerator = @this.GetEnumerator();
        enumerator.MoveNext();
        var prev = enumerator.Current;

        while (enumerator.MoveNext())
        {
            var item = enumerator.Current;
            if (!prev.CanAggregate(item)) throw Throws.UnableToAggregate(prev, item);

            sum += enumerator.Current.Value;
            prev = item;
        }

        return new TMeasure
        {
            Value = sum,
        };
    }

    public static TMeasure QSum<TMeasure>(this IEnumerable<TMeasure?> @this) where TMeasure : struct, IMeasurable, IAdditionMeasurable<TMeasure>
    {
        decimal sum = 0;

#if NET7_0_OR_GREATER
        var force = TMeasure.ForceAggregate;

        if (force)
        {
            foreach (var item in @this)
            {
                if (!item.HasValue) continue;
                sum += item.Value.Value;
            }

            return new TMeasure
            {
                Value = sum,
            };
        }
#endif
        var enumerator = @this.GetEnumerator();
        TMeasure prev = new();
        while (enumerator.MoveNext())
        {
            var item = enumerator.Current;
            if (!item.HasValue) continue;

            prev = item.Value;
            break;
        }

        while (enumerator.MoveNext())
        {
            var item = enumerator.Current;
            if (!item.HasValue) continue;
            if (!prev.CanAggregate(item.Value)) throw Throws.UnableToAggregate(prev, item.Value);

            sum += item.Value.Value;
            prev = item.Value;
        }

        return new TMeasure
        {
            Value = sum,
        };
    }

    public static TMeasure QAverage<TMeasure>(this IEnumerable<IMeasureConvertible<TMeasure>> @this) where TMeasure : struct, IMeasurable, IAdditionMeasurable<TMeasure>
    {
        return QAverage(from x in @this select x.Convert());
    }

    public static TMeasure QAverage<TMeasure>(this IEnumerable<TMeasure> @this) where TMeasure : struct, IMeasurable, IAdditionMeasurable<TMeasure>
    {
        if (!@this.Any()) throw new InvalidOperationException("Sequence contains no elements");

        decimal sum = 0;
        decimal count = 0;

#if NET7_0_OR_GREATER
        var force = TMeasure.ForceAggregate;
        if (force)
        {
            foreach (var item in @this)
            {
                sum += item.Value;
                count++;
            }

            return new TMeasure
            {
                Value = sum / count,
            };
        }
#endif
        var enumerator = @this.GetEnumerator();
        enumerator.MoveNext();
        var prev = enumerator.Current;
        count++;

        while (enumerator.MoveNext())
        {
            var item = enumerator.Current;
            if (!prev.CanAggregate(item)) throw Throws.UnableToAggregate(prev, item);

            sum += enumerator.Current.Value;
            count++;
            prev = item;
        }

        return new TMeasure
        {
            Value = sum / count,
        };
    }

    public static TMeasure? QAverage<TMeasure>(this IEnumerable<TMeasure?> @this) where TMeasure : struct, IMeasurable, IAdditionMeasurable<TMeasure>
    {
        if (!@this.Any()) return default;

        decimal sum = 0;
        decimal count = 0;

#if NET7_0_OR_GREATER
        var force = TMeasure.ForceAggregate;

        if (force)
        {
            foreach (var item in @this)
            {
                if (!item.HasValue) continue;
                sum += item.Value.Value;
                count++;
            }

            if (count > 0)
            {
                return new TMeasure
                {
                    Value = sum / count,
                };
            }
            else return default;
        }
#endif
        var enumerator = @this.GetEnumerator();
        TMeasure prev = new();
        while (enumerator.MoveNext())
        {
            var item = enumerator.Current;
            if (!item.HasValue) continue;

            prev = item.Value;
            count++;
            break;
        }

        while (enumerator.MoveNext())
        {
            var item = enumerator.Current;
            if (!item.HasValue) continue;
            if (!prev.CanAggregate(item.Value)) throw Throws.UnableToAggregate(prev, item);

            sum += item.Value.Value;
            count++;
            prev = item.Value;
        }

        if (count > 0)
        {
            return new TMeasure
            {
                Value = sum / count,
            };
        }
        else return default;
    }

    public static TMeasure QAverageOrDefault<TMeasure>(this IEnumerable<IMeasureConvertible<TMeasure>> @this, TMeasure @default = default) where TMeasure : struct, IMeasurable, IAdditionMeasurable<TMeasure>
    {
        return QAverageOrDefault(from x in @this select x.Convert());
    }

    public static TMeasure QAverageOrDefault<TMeasure>(this IEnumerable<TMeasure> @this, TMeasure @default = default) where TMeasure : struct, IMeasurable, IAdditionMeasurable<TMeasure>
    {
        if (!@this.Any()) return @default;
        return QAverage(@this);
    }

    public static TMeasure? QAverageOrDefault<TMeasure>(this IEnumerable<TMeasure?> @this, TMeasure? @default = default) where TMeasure : struct, IMeasurable, IAdditionMeasurable<TMeasure>
    {
        return QAverage(@this) ?? @default;
    }
}
