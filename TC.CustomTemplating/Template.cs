// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;
using System.Collections.Generic;
using System.Reflection;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Static class that gives direct access to the Text Transformation functionality. 
    /// All transformations are performed within the current App-Domain.
    /// </summary>
    public static class Template
    {
        #region private fields

#if TEST
        private static ITextTransformer _transformer = new TextTransformer();
#else
        private static readonly ITextTransformer _transformer = new TextTransformer();
#endif

        #endregion

        #region properties

        /// <summary>
        /// Gets the path wherin the assemblies and or included templates can be found.
        /// </summary>
        /// <value>The path.</value>
        public static string Path
        {
            get { return _transformer.Path; }
            set { _transformer.Path = value; }
        }

        /// <summary>
        /// Gets the assembly references used to compile the templates.
        /// </summary>
        /// <value>The assembly references.</value>
        public static IList<Assembly> AssemblyReferences
        {
            get
            {
                return _transformer.AssemblyReferences;
            }
        }

        /// <summary>
        /// Gets and sets the used TextTransfomer.
        /// </summary>
        /// <value>The assembly references.</value>
        public static ITextTransformer TextTransformer
        {
            get { return _transformer; }
#if TEST
            internal set { _transformer = value; }
#endif
        }

        #endregion

        #region methods

        /// <summary>
        /// Transforms the specified template without using an argument.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <exception cref="TextTransformationException">Text transformation has failed.</exception>
        /// <exception cref="ArgumentNullException">template is null or empty.</exception>
        /// <returns>The output of the text transformation.</returns>
        public static string Transform(string template)
        {
            return _transformer.Transform(template);
        }

        /// <summary>
        /// Transforms the specified template using a single named argument.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="argumentName">The argument name used to transform the text.</param>
        /// <param name="argumentValue">The argument value used to transform the text. Can be null.</param>
        /// <exception cref="TextTransformationException">Text transformation has failed.</exception>
        /// <exception cref="ArgumentNullException">template is null or empty.</exception>
        /// <returns>The output of the text transformation.</returns>
        public static string Transform(string template, string argumentName, object argumentValue)
        {
            return _transformer.Transform(template, argumentName, argumentValue);
        }

        /// <summary>
        /// Transforms the specified template using a dictionary of named arguments.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="arguments">A dictionary containing named arguments used to transform the text.</param>
        /// <exception cref="TextTransformationException">Text transformation has failed.</exception>
        /// <exception cref="ArgumentNullException">template is null or empty.</exception>
        /// <returns>The output of the text transformation.</returns>
        public static string Transform(string template, TemplateArgumentCollection arguments)
        {
            return _transformer.Transform(template, arguments);
        }

        #endregion
    }
}