// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;
using System.IO;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Handles templates stored as Embedded Resource.
    /// </summary>
    public static class TemplateResources
    {
        #region methods

        /// <summary>
        /// Gets a template from an embedded resource.
        /// </summary>
        /// <param name="streamName">The fulle name of the stream.</param>
        /// <param name="type">
        /// A type that is located in the assembly wherin 
        /// the template is stored as embedded resource.
        /// </param>
        /// <exception cref="InvalidOperationException">When the stream is not found</exception>
        /// <returns>The template loaded from the embedded resource.</returns>
        public static string Get(string streamName, Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            using (Stream stream = type.Assembly.GetManifestResourceStream(streamName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException("Stream '{0}' not found.".
                                                            FormatInvariant(streamName));
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        #endregion
    }
}