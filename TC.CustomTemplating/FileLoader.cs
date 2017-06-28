// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

namespace TC.CustomTemplating
{
    /// <summary>
    /// Responsible for loading the full content of a file.
    /// </summary>
    internal class FileLoader : IFileLoader
    {
        #region private fields

        private readonly IFileSystem _fileSystem;
        private readonly IPathResolver _resolver;

        #endregion

        #region properties

#if TEST
        /// <summary>
        /// Gets the file system.
        /// </summary>
        /// <value>The file system.</value>
        internal IFileSystem FileSystem
        {
            get { return _fileSystem; }
        }
#endif

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLoader"/> class.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <param name="fileSystem">The file system.</param>
        public FileLoader(IPathResolver resolver, IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _resolver = resolver;
        }

        #endregion

        #region methods

        /// <summary>
        /// Loads the file with the file name when the file exists. The used filename is resolved
        /// by the FileResolver.
        /// </summary>
        /// <param name="requestFileName">Name of the request file.</param>
        /// <param name="hintPath">A hint path wherin the file is searched.</param>
        /// <param name="content">The content of the file when the loading succeeded. Null otherwise.</param>
        /// <param name="location">The location where the file is loaded. Null when file is not found.</param>
        /// <returns>
        /// True when the file was loaded successfully
        /// </returns>
        public bool Load(string requestFileName, string hintPath, out string content, out string location)
        {
            content = null;
            location = null;

            var resolverdPath = _resolver.ResolvePath(requestFileName, hintPath);

            if (!_fileSystem.FileExists(resolverdPath))
            {
                return false;
            }

            content = _fileSystem.ReadAllText(resolverdPath);
            location = resolverdPath;
            return true;
        }

        #endregion
    }
}