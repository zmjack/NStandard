using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
    public delegate TDelegate FuncConvertDelegate<TDelegate>(TDelegate @delegate) where TDelegate : Delegate;
}
