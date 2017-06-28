// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;
using Microsoft.VisualStudio.TextTemplating;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Defines a Host for a TextTransformer object.
    /// </summary>
    internal interface ITextTransformerHost : ITextTemplatingEngineHost
    {
        #region public events

        /// <summary>
        /// Event raised when the template class definition is genereated.
        /// </summary>
        event EventHandler<ClassDefinitionEventArgs> ClassDefinitionGenerated;

        #endregion

        #region methods

        /// <summary>
        /// Initializes the host with the specified argument dictionary. 
        /// This will populate the CallContext with the desired arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        void Initialize(TemplateArgumentCollection arguments);

        /// <summary>
        /// Remove arguments from CallContet and checks whether errors have 
        /// occured during transformation a throw exception
        /// when necesssary.
        /// </summary>
        /// <exception cref="TextTransformationException">when an error has occured during transformation.</exception>
        /// <param name="arguments">The arguments.</param>
        void Finish(TemplateArgumentCollection arguments);

        #endregion
    }
}