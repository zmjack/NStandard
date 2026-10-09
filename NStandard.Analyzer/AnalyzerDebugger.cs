namespace NStandard.Analyzer
{
    internal static class AnalyzerDebugger
    {
        private static bool _confirmed = false;
        internal static void DebugAvailable()
        {
#if DEBUG
            return;
            if (!_confirmed && !Debugger.IsAttached)
            {
                Debugger.Launch();
                _confirmed = true;
            }
#endif
        }
    }
}
