using System;
using System.Drawing;
using Xunit;

namespace NStandard.Test
{
    public class ColorExTests
    {
        [Fact]
        public void AhsvTest()
        {
            var random = new Random();
            int step(int i) => random.Next(256 - i) % 3 + 1;

            for (var r = 0; r < 256; r += step(r))
            {
                for (var g = 0; g < 256; g += step(g))
                {
                    for (var b = 0; b < 256; b += step(b))
                    {
                        var color = Color.FromArgb(r, g, b);
                        var ashvColor = ColorEx.CreateFromAhsv
                            (color.GetHueOfHsv(), color.GetSaturationOfHsv(), color.GetValueOfHsv());

                        Assert.Equal(color, ashvColor);
                    }
                }
            }
        }

    }
}
