namespace NStandard
{
    public class NowScopeAccessor
    {
        public static readonly NowScopeAccessor Instance = new();

        internal NowScopeAccessor() { }

        public bool HasAnyScope => NowScope.Scopes.Count > 0;

        public DateTime Current
        {
            get
            {
                var scope = NowScope.Current;
                if (scope is null) throw new InvalidOperationException($"No {nameof(NowScope)} found.");
                return scope.Now;
            }
        }
        public DateTime? CurrentOrDefault => NowScope.Current?.Now;

        public DateTime Outermost
        {
            get
            {
                var scope = NowScope.Scopes.LastOrDefault();
                if (scope is null) throw new InvalidOperationException($"No {nameof(NowScope)} found.");
                return scope.Now;
            }
        }
        public DateTime? OutermostOrDefault => NowScope.Scopes.LastOrDefault()?.Now;

    }
}
