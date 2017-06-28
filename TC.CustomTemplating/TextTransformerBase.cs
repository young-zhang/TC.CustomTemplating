// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TextTemplating;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Basses class for text transformer implementations.
    /// </summary>
    public abstract class TextTransformerBase : ITextTransformer
    {
        #region private fields

        private readonly object _transformationLock = new object();
        private readonly ITextTransformerHost _host;
        private readonly ITextTemplatingEngine _engine;

        #endregion

        #region events

        /// <summary>
        /// Event raised when the template class definition is genereated.
        /// </summary>
        public event EventHandler<ClassDefinitionEventArgs> ClassDefinitionGenerated;

        #endregion

        #region properties

#if TEST

        /// <summary>
        /// Gets the host.
        /// </summary>
        /// <value>The host.</value>
        internal ITextTransformerHost Host
        {
            get { return _host; }
        }

#endif

        /// <summary>
        /// Gets the app domain used for the transformation.
        /// </summary>
        public abstract AppDomain AppDomain
        {
            get;
        }

        /// <summary>
        /// Gets the assembly references used to compile the templates.
        /// </summary>
        /// <value>The assembly references.</value>
        public virtual IList<Assembly> AssemblyReferences
        {
            get
            {
                return AppDomain.GetAssemblies();
            }
        }

        /// <summary>
        /// Gets the path wherin the assemblies and or included templates can be found.
        /// </summary>
        /// <value>The path.</value>
        public string Path
        {
            get;
            set;
        }

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TextTransformerBase"/> class.
        /// </summary>
        protected internal TextTransformerBase()
        {
            _host = ServiceLocator.Resolve<ITextTransformerHost, ITextTransformer>(this);
            _host.ClassDefinitionGenerated += HostClassDefinitionGeneratedHandler;
            _engine = new Engine();
        }

#if TEST
        /// <summary>
        /// Initializes a new instance of the <see cref="TextTransformerBase"/> class.
        /// </summary>
        internal TextTransformerBase(ITextTransformerHost host, ITextTemplatingEngine engine)
        {
            _host = host;
            _host.ClassDefinitionGenerated += HostClassDefinitionGeneratedHandler;
            _engine = engine;
        }
#endif

        #endregion

        #region methods

        /// <summary>
        /// Transforms the specified template without using an argument.
        /// </summary>
        /// <param name="templateSource">The template.</param>
        /// <exception cref="TextTransformationException">Text transformation has failed.</exception>
        /// <exception cref="ArgumentNullException">template is null or empty.</exception>
        /// <returns>The output of the text transformation.</returns>
        public string Transform(string templateSource)
        {
            lock (_transformationLock)
            {
                return StartTransformation(templateSource, null);
            }
        }

        /// <summary>
        /// Transforms the specified template using a single named argument.
        /// </summary>
        /// <param name="templateSource">The template.</param>
        /// <param name="argumentName">The argument name used to transform the text.</param>
        /// <param name="argumentValue">The argument value used to transform the text. Can be null.</param>
        /// <exception cref="TextTransformationException">Text transformation has failed.</exception>
        /// <exception cref="ArgumentNullException">template is null or empty.</exception>
        /// <returns>The output of the text transformation.</returns>
        public string Transform(string templateSource, string argumentName, object argumentValue)
        {
            if (string.IsNullOrEmpty(argumentName))
            {
                throw new ArgumentException("Provided argument name should not be null or empty!", "argumentName");
            }

            lock (_transformationLock)
            {
                return StartTransformation(templateSource, new TemplateArgumentCollection
                                                               {
                                                                   new TemplateArgument(argumentName, argumentValue)
                                                               });
            }
        }

        /// <summary>
        /// Transforms the specified template using a dictionary of named arguments.
        /// </summary>
        /// <param name="templateSource">The template.</param>
        /// <param name="arguments">A dictionary containing named arguments used to transform the text.</param>
        /// <exception cref="TextTransformationException">Text transformation has failed.</exception>
        /// <exception cref="ArgumentNullException">template is null or empty.</exception>
        /// <returns>The output of the text transformation.</returns>
        public string Transform(string templateSource, TemplateArgumentCollection arguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

            lock (_transformationLock)
            {
                return StartTransformation(templateSource, arguments);
            }
        }

        /// <summary>
        /// Called when the host has generated the Hosts the class definition.
        /// </summary>
        private void HostClassDefinitionGeneratedHandler(object source, ClassDefinitionEventArgs eventArgs)
        {
            OnClassDefinitionGenerated(eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="ClassDefinitionGenerated"/> event.
        /// </summary>
        /// <param name="e">The <see cref="TC.CustomTemplating.ClassDefinitionEventArgs"/> instance containing the event data.</param>
        protected virtual void OnClassDefinitionGenerated(ClassDefinitionEventArgs e)
        {
            var eventHandler = ClassDefinitionGenerated;
            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
        }

        /// <summary>
        /// Start the transformation.
        /// </summary>
        /// <param name="templateSource">The template source.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TextTransformationException">when an error has occured during transformation.</exception>
        protected virtual string StartTransformation(string templateSource, TemplateArgumentCollection arguments)
        {

            _host.Initialize(arguments);

            string result = _engine.ProcessTemplate(templateSource, _host);

            _host.Finish(arguments);
            return result;
        }

        #endregion
    }
}