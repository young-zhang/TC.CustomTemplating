// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace TC.CustomTemplating
{
    /// <summary>
    /// The exception that is thrown when a text transformation has failed.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors"), Serializable]
    public class TextTransformationException : InvalidOperationException
    {
        #region private fields

        private readonly CompilerErrorCollection _compilationError;
        private readonly string _templateClass;

        #endregion

        #region properties
      
        /// <summary>
        /// Gets the compiler error collection.
        /// </summary>
        /// <value>The compiler error collection.</value>
        public CompilerErrorCollection CompilationErrors
        {
            get { return _compilationError; }
        }

        /// <summary>
        /// Gets the template class.
        /// </summary>
        /// <value>The template class.</value>
        public string TemplateClass
        {
            get { return _templateClass; }
        }

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TextTransformationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="compilerErrorCollection">The compiler error collection.</param>
        /// <param name="templateClass">The template class.</param>
        internal TextTransformationException(string message, CompilerErrorCollection compilerErrorCollection, string templateClass) : 
            base(message)
        {
            _compilationError = compilerErrorCollection;
            _templateClass = templateClass;
        }     

        /// <summary>
        /// Initializes a new instance of the <see cref="TextTransformationException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        protected TextTransformationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _compilationError = info.GetValue("compilationError", typeof(CompilerErrorCollection)) as CompilerErrorCollection;
            _templateClass = info.GetString("templateClass");
        }

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("compilationError", _compilationError);
            info.AddValue("templateClass", _templateClass);
        }

        #endregion
    }
}