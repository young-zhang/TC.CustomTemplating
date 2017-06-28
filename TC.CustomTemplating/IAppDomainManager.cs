// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;

namespace TC.CustomTemplating
{
    /// <summary>
    /// AppDomain managing object.
    /// </summary>
    internal interface IAppDomainManager
    {
        #region methods

        /// <summary>
        /// Unloads the specified app domain.
        /// </summary>
        /// <param name="appDomain">The app domain that will be unloaded.</param>
        void Unload(AppDomain appDomain);

        /// <summary>
        /// Creates the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The newly created AppDomain</returns>
        AppDomain Create(string name);

        #endregion
    }
}