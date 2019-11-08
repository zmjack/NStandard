using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XColor
    {
        public static float GetHueOfHsv(this Color @this) => @this.GetHue();

        public static float GetSaturationOfHsv(this Color @this)
        {
            var rgb = new[] { @this.R, @this.G, @this.B };
            double min = rgb.Min();
            double max = rgb.Max();

            if (max == 0) return 0;
            else return (float)((max - min) / max);
        }

        public static float GetValueOfHsv(this Color @this)
        {
            var rgb = new[] { @this.R, @this.G, @this.B };
            double max = rgb.Max();
            return (float)(max / 255);
        }
    }
}
