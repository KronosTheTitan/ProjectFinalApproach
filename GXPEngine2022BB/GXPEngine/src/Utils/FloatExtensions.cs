namespace GXPEngine
{
    public static class FloatExtensions
    {
        #region FloatExtensions calculations
        /// <summary>
        /// Clamp the float
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">Clamp minimum.</param>
        /// <param name="max">Clamp maximum.</param>
        /// <returns></returns>
        public static float Clamp(this float value, float min, float max)
        {
            return Mathf.Min(Mathf.Max(value, min), max);
        }

        /// <summary>
        /// Radius to degree
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToDegree(this float value)
        {
            return Vec2.Rad2Deg(value) % 360;
        }

        /// <summary>
        /// Degree to radius
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static float ToRadius(this float value)
        {
            return Vec2.Deg2Rad(value % 360);
        }
        #endregion
    }
}
