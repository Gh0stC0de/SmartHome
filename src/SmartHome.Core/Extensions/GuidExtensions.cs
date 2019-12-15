using System;

namespace SmartHome.Core.Extensions
{
    /// <summary>
    ///     Extensions for <see cref="Guid"/>.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        ///     Checks if a <see cref="Guid"/> is null or empty.
        /// </summary>
        /// <param name="guid">The <see cref="Guid"/>.</param>
        /// <returns><c>True</c> if the guid is null or empty</returns>
        public static bool IsNullOrEmpty(this Guid? guid)
        {
            return !guid.HasValue || guid.Value == Guid.Empty;
        }
    }
}