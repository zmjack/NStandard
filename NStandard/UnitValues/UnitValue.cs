namespace NStandard.UnitValues
{
    public static class UnitValue
    {
        public static TUnitValue Default<TUnitValue>() where TUnitValue : IUnitValue, new()
        {
            var instance = new TUnitValue();
            instance.InitializeStruct();
            return instance;
        }
    }
}
