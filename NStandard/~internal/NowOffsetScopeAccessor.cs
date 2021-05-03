using System;
using System.Linq;

namespace NStandard
{
    public class NowOffsetScopeAccessor
    {
        public static readonly NowOffsetScopeAccessor Instance = new();

        internal NowOffsetScopeAccessor() { }

        public bool HasAnyScope => NowOffsetScope.Scopes.Count > 0;

        public DateTimeOffset Current
        {
            get
            {
                var scope = NowOffsetScope.Current;
                if (scope is null) throw new InvalidOperationException($"No {nameof(NowOffsetScope)} found.");
                return scope.Now;
            }
        }
        public DateTimeOffset? CurrentOrDefault => NowOffsetScope.Current?.Now;

        public DateTimeOffset Outermost
        {
            get
            {
                var scope = NowOffsetScope.Scopes.LastOrDefault();
                if (scope is null) throw new InvalidOperationException($"No {nameof(NowOffsetScope)} found.");
                return scope.Now;
            }
        }
        public DateTimeOffset? OutermostOrDefault => NowOffsetScope.Scopes.LastOrDefault()?.Now;

    }
}
