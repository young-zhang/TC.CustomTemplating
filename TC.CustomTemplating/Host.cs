// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.CodeDom;
using System.Runtime.Remoting.Messaging;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Custom T4 host that can pass arguments to the transformed
    /// template. Class is inheriting from MarshalByRefObject so 
    /// that it is not serialized to another AppDomain. 
    /// </summary>
    internal class Host : MarshalByRefObject, ITextTransformerHost
    {
        #region private const fields

        private const string OptionCacheAssemblies = "CacheAssemblies";
        private const string PropertyProcessorName = "PropertyProcessor";

        #endregion

        #region public events

        /// <summary>
        /// Event raised when the template class definition is genereated.
        /// </summary>
        public event EventHandler<ClassDefinitionEventArgs> ClassDefinitionGenerated;

        #endregion

        #region private fields

        private readonly ITextTransformer _transformer;
        private readonly IPathResolver _pathResolver;
        private readonly IFileLoader _fileLoader;
        private CompilerErrorCollection _compilationErrors;
        private string _templateClass;

        #endregion

        #region properties

        /// <summary>
        /// Gets the transformer.
        /// </summary>
        /// <value>The transformer.</value>
        public ITextTransformer Transformer
        {
            get { return _transformer; }
        }

        /// <summary>
        /// Gets the path resolver.
        /// </summary>
        /// <value>The path resolver.</value>
        public IPathResolver PathResolver
        {
            get { return _pathResolver; }
        }

        /// <summary>
        /// Gets the file loader.
        /// </summary>
        /// <value>The file loader.</value>
        public IFileLoader FileLoader
        {
            get { return _fileLoader; }
        }

        /// <summary>
        /// Gets the errors occured during last transformation.
        /// </summary>
        /// <value>The errors.</value>
        public CompilerErrorCollection Errors
        {
            get
            {
                return _compilationErrors;
            }
        }

        /// <summary>
        /// Gets the standard assembly referenced to compile the template.
        /// </summary>
        /// <value>The standard assembly references.</value>
        public IList<string> StandardAssemblyReferences
        {
            get
            {
                var locations = new List<string>();
                
                foreach (Assembly assembly in _transformer.AssemblyReferences)
                {
                    if (!assembly.IsDynamic())
                    {
                        locations.Add(assembly.Location);
                    }
                }

                return locations; 
            }
        }

        /// <summary>
        /// Gets the standard imports (usings) used to compile the template.
        /// </summary>
        /// <value>The standard imports.</value>
        public IList<string> StandardImports
        {
            get
            {
                return new List<string>
                {
                    typeof (String).Namespace,      //System
                    typeof (CodeObject).Namespace,  //System.CodeDom
                    typeof (Host).Namespace   //Current namespace
                };
            }
        }

        /// <summary>
        /// Gets the template file.
        /// </summary>
        /// <value>The template file.</value>
        public string TemplateFile
        {
            get
            {
                //Not used, template is not always comming from a file
                return string.Empty;  
            }
        }

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Host"/> class.
        /// </summary>
        /// <param name="transformer">The transformer.</param>
        /// <param name="pathResolver">The path resolver.</param>
        /// <param name="fileLoader">The file loader.</param>
        internal Host(ITextTransformer transformer, IPathResolver pathResolver, IFileLoader fileLoader)
        {
            if (transformer == null) throw new ArgumentNullException("transformer");
            if (pathResolver == null) throw new ArgumentNullException("pathResolver");
            if (fileLoader == null) throw new ArgumentNullException("fileLoader");

            _transformer = transformer;
            _pathResolver = pathResolver;
            _fileLoader = fileLoader;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Gets the host option. The enables communication from engine and
        /// template by passing object from the host to the called.
        /// </summary>
        /// <param name="optionName">Name of the option.</param>
        /// <returns></returns>
        public object GetHostOption(string optionName)
        {
            if (optionName == null) throw new ArgumentNullException("optionName");

            if (optionName == OptionCacheAssemblies)   // Asked by the engine
            {
                //This enables that each template is only compiled once.
                //Next time the template is generated, the same generated
                //class is used. 
                //The compiled classes are identified by using the MD5 hash
                //of the parsed template blocks
                return true;
            }

            return null;
        }

        /// <summary>
        /// Loads the include text.
        /// </summary>
        /// <param name="requestFileName">Name of the request file.</param>
        /// <param name="content">The content.</param>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        public bool LoadIncludeText(string requestFileName, out string content, out string location)
        {
            return _fileLoader.Load(requestFileName, _transformer.Path, out content, out location);
        }

        /// <summary>
        /// Log the compilation errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public void LogErrors(CompilerErrorCollection errors)
        {
            _compilationErrors = errors;
        }

        /// <summary>
        /// Provides the templating app domain.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public AppDomain ProvideTemplatingAppDomain(string content)
        {
            _templateClass = content;

            var eventArgs = new ClassDefinitionEventArgs(content);
            OnTemplateCompiled(eventArgs);

            return _transformer.AppDomain;
        }

        /// <summary>
        /// Resolves the assembly reference.
        /// </summary>
        /// <param name="assemblyReference">The assembly reference.</param>
        /// <returns></returns>
        public string ResolveAssemblyReference(string assemblyReference)
        {
            return ResolvePath(assemblyReference);
        }

        /// <summary>
        /// Resolves the property directive processor.
        /// </summary>
        /// <param name="processorName">Name of the processor.</param>
        /// <returns></returns>
        public Type ResolveDirectiveProcessor(string processorName)
        {
            if (string.Compare(processorName,
                               PropertyProcessorName,
                               StringComparison.OrdinalIgnoreCase) == 0)
            {
                return typeof(TemplateArgumentDirectiveProcessor);
            }

            throw new NotSupportedException(
                "Directive processor {0} not supported!".FormatInvariant(processorName));
        }

        /// <summary>
        /// Resolves the parameter value.
        /// </summary>
        /// <param name="directiveId">The directive id.</param>
        /// <param name="processorName">Name of the processor.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns></returns>
        public string ResolveParameterValue(string directiveId, string processorName, string parameterName)
        {
            return null;
        }

        /// <summary>
        /// Resolves the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public string ResolvePath(string path)
        {
            return _pathResolver.ResolvePath(path, _transformer.Path);
        }

        /// <summary>
        /// Sets the file extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        public void SetFileExtension(string extension)
        {
            //Setting the file extension is not supported by current host.
        }

        /// <summary>
        /// Sets the output encoding.
        /// </summary>
        public void SetOutputEncoding(Encoding encoding, bool fromOutputDirective)
        {
            //Setting output encoding is not supported by current host.
        }

        /// <summary>
        /// Initializes the host with the specified argument dictionary. 
        /// This will populate the CallContext with the desired arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public void Initialize(TemplateArgumentCollection arguments)
        {
            _compilationErrors = null;
            if (arguments != null)
            {
                foreach (var argument in arguments)
                {
                    CallContext.LogicalSetData(argument.Name, argument.Value);
                }
            }
        }

        /// <summary>
        /// Remove arguments from CallContet and checks whether errors have 
        /// occured during transformation a throw exception
        /// when necesssary.
        /// </summary>
        /// <exception cref="TextTransformationException">when an error has occured during transformation.</exception>
        public void Finish(TemplateArgumentCollection arguments)
        {
            if (arguments != null)
            {
                foreach (var argument in arguments)
                {
                    CallContext.FreeNamedDataSlot(argument.Name);
                }
            }

            if (_compilationErrors != null && _compilationErrors.HasErrors)
            {
                ThrowTransformationException();
            }
        }

        private void ThrowTransformationException()
        {
            var  messageBuilder = new StringBuilder();
            messageBuilder.AppendLine("Error occured while Transforming Template");
            foreach (CompilerError error in _compilationErrors)
            {
                messageBuilder.Append("({0}, {1}) [{2}] {3}".FormatInvariant(error.Line, error.Column, error.ErrorNumber, error.ErrorText));
                messageBuilder.AppendLine();
            }

            throw new TextTransformationException(messageBuilder.ToString(), _compilationErrors, _templateClass);
        }

        #endregion

        #region protected/internal methods

        /// <summary>
        /// Raises the ClassDefinitionGenerated event.
        /// </summary>
        /// <param name="eventArgs">The event argument containing the generated class.</param>
        protected void OnTemplateCompiled(ClassDefinitionEventArgs eventArgs)
        {
            var eventHandler = ClassDefinitionGenerated;
            if (eventHandler != null)
            {
                eventHandler(this, eventArgs);
            }
        }

        #endregion
    }
}
