using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NStandard
{
    public static partial class Any
    {
        public struct SharedChainItem<T>
        {
            public SeekOrigin Origin { get; set; }
            public T[] Layers { get; set; }
            public int Index { get; set; }
            public T Current => Layers[Index];
        }

        public static IEnumerable<SharedChainItem<T>> Chain<T>(IEnumerable<T>[] enumerables)
        {
            if (enumerables.Length == 0) throw new ArgumentException("The argument can not be empty.", nameof(enumerables));

            var length = enumerables.Length;
            var index = 0;
            var maxIndex = length - 1;

            var enumerators = new IEnumerator[length];
            for (int i = 0; i < enumerables.Length; i++)
            {
                enumerators[i] = enumerables[i].GetEnumerator();
            }
            var layers = new T[length];

            SharedChainItem<T> item = default;
            bool MoveNext()
            {
                var enumerator = enumerators[index];
                if (enumerator.MoveNext())
                {
                    var current = (T)enumerator.Current;
                    if (index < maxIndex)
                    {
                        layers[index] = current;
                        item.Origin = SeekOrigin.Begin;
                        item.Index = index;
                        item.Layers = layers;
                        index++;
                        enumerators[index].Reset();
                        return true;
                    }
                    else
                    {
                        layers[index] = current;
                        item.Origin = SeekOrigin.Current;
                        item.Index = index;
                        item.Layers = layers;
                        return true;
                    }
                }
                else
                {
                    layers[index] = default;
                    index--;
                    if (index >= 0)
                    {
                        item.Origin = SeekOrigin.End;
                        item.Index = index;
                        item.Layers = layers;
                        return true;
                    }
                    else return false;
                }
            }

            while (MoveNext())
            {
                yield return item;
            }
        }

        public static IEnumerable<SharedChainItem<T>> Chain<T>(T seed, Func<T, IEnumerable<T>>[] layerGenerators)
        {
            if (layerGenerators.Length == 0) throw new ArgumentException("The argument can not be empty.", nameof(layerGenerators));

            var length = layerGenerators.Length;
            var index = 0;
            var maxIndex = length - 1;

            var enumerators = new IEnumerator[length];
            enumerators[0] = layerGenerators[0](seed).GetEnumerator();
            var layers = new T[length];

            SharedChainItem<T> item = default;
            bool MoveNext()
            {
                var enumerator = enumerators[index];
                if (enumerator.MoveNext())
                {
                    var current = (T)enumerator.Current;
                    if (index < maxIndex)
                    {
                        layers[index] = current;
                        item.Origin = SeekOrigin.Begin;
                        item.Index = index;
                        item.Layers = layers;
                        index++;
                        enumerators[index] = layerGenerators[index](current).GetEnumerator();
                        return true;
                    }
                    else
                    {
                        layers[index] = current;
                        item.Origin = SeekOrigin.Current;
                        item.Index = index;
                        item.Layers = layers;
                        return true;
                    }
                }
                else
                {
                    layers[index] = default;
                    index--;
                    if (index >= 0)
                    {
                        item.Origin = SeekOrigin.End;
                        item.Index = index;
                        item.Layers = layers;
                        return true;
                    }
                    else return false;
                }
            }

            while (MoveNext())
            {
                yield return item;
            }
        }

    }
}
