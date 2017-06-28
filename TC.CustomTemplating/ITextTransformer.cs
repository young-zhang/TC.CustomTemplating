// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;
using System.Collections.Generic;
using System.Reflection;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Defines a TextTransformer used to transform templates.
    /// </summary>
    public interface ITextTransformer
    {
        #region public events

        /// <summary>
        /// Event raised when the template class definition is genereated.
        /// </summary>
        event EventHandler<ClassDefinitionEventArgs> ClassDefinitionGenerated;

        #endregion

        #region properties

        /// <summary>
        /// Gets the app domain used for the transformation.
        /// </summary>
        AppDomain AppDomain { get; }

        /// <summary>
        /// Gets the assembly references used to compile the templates.
        /// </summary>
        /// <value>The assembly references.</value>
        IList<Assembly> AssemblyReferences { get; }

        /// <summary>
        /// Gets the path wherin the assemblies and or included templates can be found.
        /// </summary>
        /// <value>The path.</value>
        string Path { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Transforms the specified template without using an argument.
        /// </summary>
        /// <param name="templateSource">The template.</param>
        /// <exception cref="TextTransformationException">Text transformation has failed.</exception>
        /// <exception cref="ArgumentNullException">template is null or empty.</exception>
        /// <returns>The output of the text transformation.</returns>
        string Transform(string templateSource);

        /// <summary>
        /// Transforms the specified template using a single named argument.
        /// </summary>
        /// <param name="templateSource">The template.</param>
        /// <param name="argumentName">The argument name used to transform the text.</param>
        /// <param name="argumentValue">The argument value used to transform the text. Can be null.</param>
        /// <exception cref="TextTransformationException">Text transformation has failed.</exception>
        /// <exception cref="ArgumentNullException">template is null or empty.</exception>
        /// <returns>The output of the text transformation.</returns>
        string Transform(string templateSource, string argumentName, object argumentValue);

        /// <summary>
        /// Transforms the specified template using a dictionary of named arguments.
        /// </summary>
        /// <param name="templateSource">The template.</param>
        /// <param name="arguments">A dictionary containing named arguments used to transform the text.</param>
        /// <exception cref="TextTransformationException">Text transformation has failed.</exception>
        /// <exception cref="ArgumentNullException">template is null or empty.</exception>
        /// <returns>The output of the text transformation.</returns>
        string Transform(string templateSource, TemplateArgumentCollection arguments);

        #endregion
    }
}