// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.VisualStudio.TextTemplating;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Represent a TextTransformer that will load the compiled templates in a 
    /// newly created AppDomain. When the DomainTextTransformer is disposed 
    /// the AppDomain is unloaded.
    /// </summary>
    /// <remarks>
    /// The transformation performed on the same DomainTextTransformer are synchronized. 
    /// </remarks>
    public sealed class DomainTextTransformer : TextTransformerBase, IDisposable
    {
        #region private fields

        private const int DefaultRecycleCount = 25;

        private static int _domainCounter;
        
        private int _recycleCount;
        private AppDomain _appDomain;
        private readonly IAppDomainManager _appDomainManager;

        private List<Assembly> _assemblyReferences = new List<Assembly>();
        private readonly object _assemblyReferencesLock = new object();

        #endregion

        #region properties

        /// <summary>
        /// Gets the assembly references used to compile the templates.
        /// </summary>
        /// <value>The assembly references.</value>
        public override IList<Assembly> AssemblyReferences
        {
            get
            {
                lock (_assemblyReferencesLock)
                {
                    return new List<Assembly>(_assemblyReferences);
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of tranformations that are performed before the App-Domain is recycled.
        /// To enable the automatic recycling feature the AutoRecycle property should be set to true. The
        /// default number of transformations is 20.
        /// </summary>
        /// <value>The number of tranformations that are performed before the App-Domain is recycled.</value>
        public int RecycleThreshold
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the automatic recycling feature is enabled. Enabling this feature
        /// eshures that the App-Domain is automatically recycled after a predefined number of 
        /// transformations. The number of transformations is defined by the RecycleThreshold property. 
        /// By default default value is false.
        /// </summary>
        /// <value>Whether the App-Domain is automatically recycled after a predefined number of transformations.</value>
        public bool AutoRecycle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the app domain used for the transformation.
        /// </summary>
        /// <value></value>
        public override AppDomain AppDomain
        {
            get { return _appDomain; }
        }

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainTextTransformer"/> class.
        /// </summary>
        public DomainTextTransformer()
        {
            _appDomainManager = ServiceLocator.Resolve<IAppDomainManager>();
            Initialize();
        }

#if TEST
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainTextTransformer"/> class.
        /// </summary>
        /// <param name="appDomainManager">The app domain manager.</param>
        /// <param name="host">The host.</param>
        /// <param name="engine">The engine.</param>
        internal DomainTextTransformer(IAppDomainManager appDomainManager, ITextTransformerHost host, ITextTemplatingEngine engine) : base(host, engine)
        {
            _appDomainManager = appDomainManager;
            Initialize();
        }
#endif

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="DomainTextTransformer"/> is reclaimed by garbage collection.
        /// </summary>
        ~DomainTextTransformer()
        {
            Dispose(false);
        }

        #endregion

        #region methods

        /// <summary>
        /// Recycles the domain wherein the transformations are preformed.
        /// </summary>
        public void Recycle()
        {
            _appDomainManager.Unload(AppDomain);

            _appDomain = CreateNewDomain();
            _recycleCount = 0;
        }

        #endregion

        #region internal/private methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            RecycleThreshold = DefaultRecycleCount;
            _appDomain = CreateNewDomain();

            _assemblyReferences = new List<Assembly>
                                     {
                                         typeof (CodeCompileUnit).Assembly, //System.dll
                                         typeof (string).Assembly,          //mscorelib
                                         typeof (TemplateArgument).Assembly //core assembly
                                     };
        }

        /// <summary>
        /// Start the transformation.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TextTransformationException">when an error has occured during transformation.</exception>
        protected override string StartTransformation(
            string template, 
            TemplateArgumentCollection arguments)
        {
            CheckAutoRecycle();

            if (arguments != null)
            {
                foreach (TemplateArgument pair in arguments)
                {
                    AddAssemblyReference(pair.Value);
                }
            }
            return base.StartTransformation(template, arguments);
        }

        /// <summary>
        /// Adds the assembly of the argument to the references.
        /// </summary>
        /// <param name="argument">The argument.</param>
        private void AddAssemblyReference(object argument)
        {
            if (argument == null)
            {
                return;
            }

            Assembly assembly = argument.GetType().Assembly;
            if (!_assemblyReferences.Contains(assembly))
            {
                lock (_assemblyReferencesLock)
                {
                    if (!_assemblyReferences.Contains(assembly))
                    {
                        _assemblyReferences.Add(assembly);
                    }
                }
            }
        }

        /// <summary>
        /// Checks the whther the domain need to be recycled.
        /// </summary>
        private void CheckAutoRecycle()
        {
            if (AutoRecycle && ++_recycleCount >= RecycleThreshold)
            {
                Recycle();
            }
        }

        /// <summary>
        /// Creates the new app-domain.
        /// </summary>
        private AppDomain CreateNewDomain()
        {
            string name = "Text Transformation ({0})"
                .FormatInvariant(
                    Interlocked.Increment(ref _domainCounter));
    
            return _appDomainManager.Create(name);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, 
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (disposing && _appDomain != null)
            {
                _appDomainManager.Unload(_appDomain);
                _appDomain = null;
            }
        }

        #endregion
    }
}