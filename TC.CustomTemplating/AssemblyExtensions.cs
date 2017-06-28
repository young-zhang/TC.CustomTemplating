// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;
using System.Reflection;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Extension methods for Assembly.
    /// </summary>
    internal static class AssemblyExtensions
    {
        #region methods

        /// <summary>
        /// Determines whether the specified assembly is dynamic.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>
        /// 	<c>true</c> if the specified assembly is dynamic; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsDynamic(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            
            return assembly.ManifestModule.Name.StartsWith("<", StringComparison.Ordinal);
        }

        #endregion
    }
}