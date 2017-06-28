// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

namespace TC.CustomTemplating
{
    /// <summary>
    /// Responsible for loading the full content of a file.
    /// </summary>
    internal interface IFileLoader
    {
        #region methods

        /// <summary>
        /// Loads the specified file name.
        /// </summary>
        /// <param name="requestFileName">Name of the request file.</param>
        /// <param name="hintPath">A hint path wherin the file is searched.</param>
        /// <param name="content">The content of the file when the loading succeeded. Null otherwise.</param>
        /// <param name="location">The location where the file is loaded. Null when file is not found.</param>
        /// <returns>True when the file was loaded successfully</returns>
        bool Load(string requestFileName, string hintPath, out string content, out string location);

        #endregion
    }
}