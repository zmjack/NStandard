using NStandard.Design;

namespace NStandard.Analyzer.Test;

public partial class ClassWrapper
{
    [FieldFeature]
    public partial class Model
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
    }

    [FieldFeature]
    public partial struct ValueModel
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
    }
}

public partial struct StructWrapper
{
    [FieldFeature]
    public partial class Model
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
    }

    [FieldFeature]
    public partial struct ValueModel
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
    }
}

#region ModelWithNoNS
[FieldFeature]
public partial class Model
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
}
#endregion

[FieldFeature]
public partial struct ValueModel
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
}
