using System;
using System.IO;
using System.Text;

namespace NStandard;

public class ConsoleContext : Scope<ConsoleContext>
{
    public int CursorLeft { get; }
    public int CursorSize { get; }
    public int CursorTop { get; }
    public bool CursorVisible { get; }
    public string Title { get; }
    public TextReader In { get; }
    public TextWriter Out { get; }
    public TextWriter Error { get; }
    public Encoding InputEncoding { get; }
    public Encoding OutputEncoding { get; }
    public ConsoleColor ForegroundColor { get; }
    public ConsoleColor BackgroundColor { get; }

    private readonly bool _hasCursor;
    private readonly bool _restoreCursor;

    protected ConsoleContext(bool restoreCursor)
    {
        _restoreCursor = restoreCursor;

        try
        {
            CursorLeft = Console.CursorLeft;
            CursorTop = Console.CursorTop;
            CursorSize = Console.CursorSize;
            CursorVisible = Console.CursorVisible;
            _hasCursor = true;
        }
        catch { }

        Title = Console.Title;
        ForegroundColor = Console.ForegroundColor;
        BackgroundColor = Console.BackgroundColor;

        In = Console.In;
        Out = Console.Out;
        Error = Console.Error;
        InputEncoding = Console.InputEncoding;
        OutputEncoding = Console.OutputEncoding;
    }

    public static ConsoleContext Begin(bool restoreCursor = false) => new(restoreCursor);

    public override void Disposing()
    {
        if (_hasCursor && _restoreCursor)
        {
            if (Console.CursorLeft != CursorLeft) Console.CursorLeft = CursorLeft;
            if (Console.CursorTop != CursorTop) Console.CursorTop = CursorTop;
            if (Console.CursorSize != CursorSize) Console.CursorSize = CursorSize;
            if (Console.CursorVisible != CursorVisible) Console.CursorVisible = CursorVisible;
        }

        if (Console.Title != Title) Console.Title = Title;
        if (Console.ForegroundColor != ForegroundColor) Console.ForegroundColor = ForegroundColor;
        if (Console.BackgroundColor != BackgroundColor) Console.BackgroundColor = BackgroundColor;

        if (Console.In != In) Console.SetIn(In);
        if (Console.Out != Out) Console.SetOut(Out);
        if (Console.Error != Error) Console.SetError(Error);
        if (Console.InputEncoding != InputEncoding) Console.InputEncoding = InputEncoding;
        if (Console.OutputEncoding != OutputEncoding) Console.OutputEncoding = OutputEncoding;
    }

}
