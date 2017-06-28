// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Resolves the path for a given file name.  
    /// </summary>
    internal class PathResolver : IPathResolver
    {
        #region private fields

        private readonly IFileSystem _fileSystem;

        #endregion

        #region properties

        /// <summary>
        /// Gets the file system.
        /// </summary>
        /// <value>The file system.</value>
        public IFileSystem FileSystem
        {
            get { return _fileSystem; }
        }

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PathResolver"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        internal PathResolver(IFileSystem fileSystem)
        {
            if (fileSystem == null) throw new ArgumentNullException("fileSystem");

            _fileSystem = fileSystem;
        }

        #endregion

        #region methods

        /// <summary>
        /// Resolves the path.
        /// </summary>
        /// <param name="fileName">The path.</param>
        /// <param name="hintPath">The transformer path.</param>
        /// <returns></returns>
        public string ResolvePath(string fileName, string hintPath)
        {
            if (_fileSystem.IsPathRooted(fileName))
            {
                return fileName;
            }

            if (!string.IsNullOrEmpty(hintPath))
            {
                string fullName = _fileSystem.Combine(hintPath, fileName);
                if (_fileSystem.FileExists(fullName))
                {
                    return fullName;
                }
            }

            if (!string.IsNullOrEmpty(_fileSystem.EntryAssemblyLocation))
            {
                var directoryName = _fileSystem.GetDirectoryName(_fileSystem.EntryAssemblyLocation);
                string fullName = _fileSystem.Combine(directoryName, fileName);
                if (_fileSystem.FileExists(fullName))
                {
                    return fullName;
                }
            }
            return fileName;
        }

        #endregion
    }
}