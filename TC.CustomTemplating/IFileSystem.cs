// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

namespace TC.CustomTemplating
{
    /// <summary>
    /// Responsible for accessing the FileSystem.
    /// </summary>
    internal interface IFileSystem
    {
        #region methods

        /// <summary>
        /// Reads all text from a text file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        string ReadAllText(string path);

        /// <summary>
        /// Combines the specified path with the name.
        /// </summary>
        /// <param name="path1">The path1.</param>
        /// <param name="path2">The path2.</param>
        /// <returns></returns>
        string Combine(string path1, string path2);

        /// <summary>
        /// Returns whether the file exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        bool FileExists(string path);

        /// <summary>
        /// Determines whether the path of the specified file is rooted.
        /// </summary>
        bool IsPathRooted(string path);

        /// <summary>
        /// Gets the name of the directory of the specified file name.
        /// </summary>
        string GetDirectoryName(string path);

        /// <summary>
        /// Gets the location of the entry assembly.
        /// </summary>
        /// <value>The entry assembly location.</value>
        string EntryAssemblyLocation { get; }

        #endregion
    }
}