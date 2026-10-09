using NStandard.Design;

namespace NStandard.Analyzer.Test;

public interface IFieldFeatureLocationTestModel
{
    int Number { get; set; }
}

public partial class FieldFeatureClassWrapper
{
    [FieldFeature]
    public partial class Model : IFieldFeatureLocationTestModel
    {
        [FieldBackend]
        public int Number
        {
            get => GetValue();
            set => SetValue(value);
        }

        [FieldBackend]
        public int[] Numbers
        {
            get => GetValue();
            set => SetValue(value);
        }

        [FixedArray(2)]
        public partial int[] FixedSize2 { get; set; }
    }

    [FieldFeature]
    public partial struct ValueModel : IFieldFeatureLocationTestModel
    {
        [FieldBackend]
        public int Number
        {
            get => GetValue();
            set => SetValue(value);
        }

        [FieldBackend]
        public int[] Numbers
        {
            get => GetValue();
            set => SetValue(value);
        }

        [FixedArray(2)]
        public partial int[] FixedSize2 { get; set; }
    }
}

public partial struct FieldFeatureStructWrapper
{
    [FieldFeature]
    public partial class Model : IFieldFeatureLocationTestModel
    {
        [FieldBackend]
        public int Number
        {
            get => GetValue();
            set => SetValue(value);
        }

        [FieldBackend]
        public int[] Numbers
        {
            get => GetValue();
            set => SetValue(value);
        }

        [FixedArray(2)]
        public partial int[] FixedSize2 { get; set; }
    }

    [FieldFeature]
    public partial struct ValueModel : IFieldFeatureLocationTestModel
    {
        [FieldBackend]
        public int Number
        {
            get => GetValue();
            set => SetValue(value);
        }

        [FieldBackend]
        public int[] Numbers
        {
            get => GetValue();
            set => SetValue(value);
        }

        [FixedArray(2)]
        public partial int[] FixedSize2 { get; set; }
    }
}
