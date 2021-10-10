namespace NStandard.UnitValues
{
    public static class UnitValue
    {
        public static TUnitValue Create<TUnitValue>() where TUnitValue : IUnitValue, new()
        {
            var instance = new TUnitValue();
            instance.Init();
            return instance;
        }
    }
}
