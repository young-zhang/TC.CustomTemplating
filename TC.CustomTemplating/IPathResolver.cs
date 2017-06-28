// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

namespace TC.CustomTemplating
{
    /// <summary>
    /// Resolves the path for a given file name.  
    /// </summary>
    internal interface IPathResolver
    {
        #region methods

        /// <summary>
        /// Resolves the path for a given file name.  
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="hintPath">The transformer path.</param>
        /// <returns>The full path to the file. Fill name file is not found.</returns>
        string ResolvePath(string fileName, string hintPath);

        #endregion
    }
}