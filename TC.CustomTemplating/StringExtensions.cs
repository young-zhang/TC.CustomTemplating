// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System.Globalization;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Extension methods defined for the string type.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Replaces the format item in a specified System.String with the text equivalent of the value of a corresponding System.Object instance in a specified array. A specified parameter supplies culture-specific formatting information. 
        /// Invariant culture supplies culture-specific formatting information.
        /// </summary>
        /// <param name="value">A composite format string.</param>
        /// <param name="arguments">An System.Object array containing zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the System.String equivalent of the corresponding instances of System.Object in args.</returns>
        /// <exception cref="System.ArgumentNullException">format or args is null.</exception>
        /// <exception cref="System.FormatException">format is invalid.-or- The number indicating an argument to format is less than zero, or greater than or equal to the length of the args array.</exception>
        internal static string FormatInvariant(this string value, params object[] arguments)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                value, 
                arguments);
        }
    }
}
