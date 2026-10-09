using System.Text;

namespace NStandard.IO;

public class Scanner(string s) : StringReader(s)
{
    public T Next<T>() where T : IConvertible
    {
        var sb = new StringBuilder();
        int current;
        int pos = 0;
        do
        {
            current = Read();
            if (current < 0) throw new InvalidOperationException($"No more characters are available.");
            pos++;
        } while (char.IsWhiteSpace((char)current));

        do
        {
            sb.Append((char)current);
            var next = (char)Peek();
            if (char.IsWhiteSpace(next)) break;
            current = Read();
        } while (current != -1);

        var value = sb.ToString();
        var type = typeof(T);
        try
        {
            if (type.IsEnum) return (T)Enum.Parse(type, value);
            else return (T)Convert.ChangeType(value, typeof(T));
        }
        catch (Exception ex)
        {
            throw new InvalidCastException($"\"{value}\" cannot be converted to {type}.", ex);
        }
    }

    public bool TryNext<T>(out T ret) where T : IConvertible
    {
        try
        {
            ret = Next<T>();
            return true;
        }
        catch
        {
            ret = default!;
            return false;
        }
    }
}
