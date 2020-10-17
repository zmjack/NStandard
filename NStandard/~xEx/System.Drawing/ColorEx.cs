namespace System.Drawing
{
    public static class ColorEx
    {
        /// <summary>
        /// Creates a color using Alpha(255) + HSV model.
        /// </summary>
        /// <param name="hue">[0,360)</param>
        /// <param name="saturation">[0,1]</param>
        /// <param name="value">[0,1]</param>
        /// <returns></returns>
        public static Color CreateFromAhsv(float hue, float saturation, float value) => CreateFromAhsv(255, hue, saturation, value);

        /// <summary>
        /// Creates a color using Alpha + HSV model.
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="hue">[0,360)</param>
        /// <param name="saturation">[0,1]</param>
        /// <param name="value">[0,1]</param>
        /// <returns></returns>
        public static Color CreateFromAhsv(int alpha, float hue, float saturation, float value)
        {
            if (hue < 0) throw new ArgumentException("The hue must be greater than 0.");
            if (saturation < 0 || saturation > 1) throw new ArgumentException("The saturation must be between 0 and 1.");
            if (value < 0 || value > 1) throw new ArgumentException("The value must be between 0 and 1.");

            int max = (int)Math.Round(255 * value);
            int min = (int)Math.Round(max - saturation * max);
            int d = max - min;

            int slide(int baseLine) => (int)Math.Round((hue - baseLine) * d / 60);

            switch (hue)
            {
                case float h when h == 0:
                    return Color.FromArgb(alpha, max, min, min);
                case float h when h < 60:
                    return Color.FromArgb(alpha, max, min + slide(0), min);
                case float h when h == 60:
                    return Color.FromArgb(alpha, max, max, min);
                case float h when h < 120:
                    return Color.FromArgb(alpha, min - slide(120), max, min);
                case float h when h == 120:
                    return Color.FromArgb(alpha, min, max, min);
                case float h when h < 180:
                    return Color.FromArgb(alpha, min, max, min + slide(120));
                case float h when h == 180:
                    return Color.FromArgb(alpha, min, max, max);
                case float h when h < 240:
                    return Color.FromArgb(alpha, min, min - slide(240), max);
                case float h when h == 240:
                    return Color.FromArgb(alpha, min, min, max);
                case float h when h < 300:
                    return Color.FromArgb(alpha, min + slide(240), min, max);
                case float h when h == 300:
                    return Color.FromArgb(alpha, max, min, max);
                case float h when h < 360:
                    return Color.FromArgb(alpha, max, min, min - slide(360));
                default:
                    throw new NotSupportedException();
            }
        }

    }
}
