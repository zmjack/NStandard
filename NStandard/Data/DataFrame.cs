using NStandard;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NStandard.Data;

public class DataFrame<T>
{
    private static ArgumentException IndexLengthNotMatch(string argument, int valueLength, int indexLength) => new($"Length of values ({valueLength}) does not match length of index ({indexLength}).", argument);
    private static ArgumentException ColumnLengthNotMatch(string argument, int valueColumns, int columnLength) => new($"{columnLength} columns passed, passed data had {valueColumns} columns.", argument);
    private static ArgumentException InvalidIndexName(string argument, string name) => new($"Invalid index name ({name}).", argument);

    public class Row
    {
        public string Index { get; set; }
        public T[] Values { get; set; }
    }

    public DataFrame(Dictionary<string, T[]> source, IEnumerable<string>? index = null)
    {
        var rowLength = source.Values.Max(x => x.Length);
        var colLength = source.Keys.Count;

        Index = NormalizeIndex(index, rowLength);
        Columns = NormalizeColumns(source.Keys, colLength);

        var values = new T[rowLength, colLength];
        foreach (var (col, arr) in source.Values.Pairs())
        {
            foreach (var (row, value) in arr.Pairs())
            {
                values[row, col] = value;
            }
        }
        Values = values;
    }

    public DataFrame(IEnumerable<T> values, IEnumerable<string>? index = null, IEnumerable<string>? columns = null)
        : this([.. from v in values select new T[] { v }], index, columns)
    {
    }

    public DataFrame(IEnumerable<IEnumerable<T>> values, IEnumerable<string>? index = null, IEnumerable<string>? columns = null)
        : this([.. from v in values select v.ToArray()], index, columns)
    {
    }

    public DataFrame(T[][] values, IEnumerable<string>? index = null, IEnumerable<string>? columns = null)
    {
        var rowLength = values.Length;
        var colLength = values.Max(x => x.Length);

        Index = NormalizeIndex(index, rowLength);
        Columns = NormalizeColumns(columns, colLength);
        Values = NormalizeValues(values, rowLength, colLength);
    }

    public DataFrame(T[,] values, IEnumerable<string>? index = null, IEnumerable<string>? columns = null)
    {
        var rowLength = values.GetLength(0);
        var colLength = values.GetLength(1);

        Index = NormalizeIndex(index, rowLength);
        Columns = NormalizeColumns(columns, colLength);
        Values = values;
    }

    private static string[] GetSequenceIndex(int count)
    {
        return [.. from n in Enumerable.Range(0, count) select n.ToString()];
    }

    private static string[] NormalizeIndex(IEnumerable<string>? index, int length)
    {
        if (index is not null)
        {
            if (index.Count() != length) throw IndexLengthNotMatch(nameof(index), length, index.Count());
            return index.ToArray();
        }
        else return GetSequenceIndex(length);
    }
    private static string[] NormalizeColumns(IEnumerable<string>? columns, int length)
    {
        if (columns is not null)
        {
            if (columns.Count() != length) throw ColumnLengthNotMatch(nameof(columns), length, columns.Count());
            return columns.ToArray();
        }
        else return GetSequenceIndex(length);
    }
    private static T[,] NormalizeValues(T[][] values, int rowLength, int colLength)
    {
        var _values = new T[rowLength, colLength];
        for (int row = 0; row < rowLength; row++)
        {
            var source = values[row];
            var sourceLength = source!.Length;
            for (int col = 0; col < colLength; col++)
            {
                if (col < sourceLength)
                {
                    _values[row, col] = source[col];
                }
            }
        }
        return _values;
    }

    public T[,] Values { get; }
    public string[] Index { get; }
    public string[] Columns { get; }

    public int RowLength => Values.GetLength(0);
    public int ColumnLength => Values.GetLength(1);

    public IEnumerable<IEnumerable<T>> RowValues()
    {
        for (int i = 0; i < RowLength; i++)
        {
            yield return RowValues(i);
        }
    }

    public IEnumerable<IEnumerable<T>> ColumnValues()
    {
        for (int i = 0; i < ColumnLength; i++)
        {
            yield return ColumnValues(i);
        }
    }

    public IEnumerable<T> RowValues(int i)
    {
        for (int col = 0; col < ColumnLength; col++)
        {
            yield return Values[i, col];
        }
    }

    public IEnumerable<T> ColumnValues(int i)
    {
        for (int row = 0; row < RowLength; row++)
        {
            yield return Values[row, i];
        }
    }

    public IEnumerable<T> RowValues(string name)
    {
        var i = Index.IndexOf(name);
        if (i < 0) throw InvalidIndexName(nameof(name), name);

        for (int col = 0; col < ColumnLength; col++)
        {
            yield return Values[i, col];
        }
    }

    public IEnumerable<T> ColumnValues(string column)
    {
        var i = Columns.IndexOf(column);
        if (i < 0) throw InvalidIndexName(nameof(column), column);

        for (int col = 0; col < RowLength; col++)
        {
            yield return Values[col, i];
        }
    }

    private static IEnumerable<Row> InnerRows(IEnumerable<string> index, IEnumerable<IEnumerable<T>> values)
    {
        return
            from pair in Any.Zip(index, values)
            select new Row
            {
                Index = pair.Item1,
                Values = [.. pair.Item2],
            };
    }

    public IEnumerable<Row> Rows() => InnerRows(Index, RowValues());
    public IEnumerable<Row> Head(int count) => InnerRows(Index.Take(count), RowValues().Take(count));
#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
    public IEnumerable<Row> Tail(int count) => InnerRows(Index.TakeLast(count), RowValues().TakeLast(count));
#else
    public IEnumerable<Row> Tail(int count) => InnerRows(Index.Skip(RowLength - count).Take(count), RowValues().Skip(RowLength - count).Take(count));
#endif
}
