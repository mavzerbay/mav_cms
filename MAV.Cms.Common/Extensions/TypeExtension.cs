using System;

namespace MAV.Cms.Common.Extensions
{
    public static class TypeExtension
    {
        public static bool HasValue(this string source)
        {
            return !string.IsNullOrEmpty(source) && !string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// null veya Guid.Empty eşitse true döner
        /// </summary>
        public static bool IsNullOrEmpty(this Guid? source)
        {
            return !source.HasValue || ( source.HasValue && source == Guid.Empty);
        }
    }
}
