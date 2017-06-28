// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;

namespace TC.CustomTemplating
{
    /// <summary>
    /// AppDomain managing object.
    /// </summary>
    internal class AppDomainManager : IAppDomainManager
    {
        #region methods

        /// <summary>
        /// Create a new AppDomain with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The newly created AppDomain</returns>
        public AppDomain Create(string name)
        {
            return AppDomain.CreateDomain(name);
        }

        /// <summary>
        /// Unloads the specified app domain.
        /// </summary>
        /// <param name="appDomain">The app domain that will be unloaded.</param>
        public void Unload(AppDomain appDomain)
        {
            AppDomain.Unload(appDomain);
        }

        #endregion
    }
}