using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace TC.CustomTemplating.Example.Wpf
{
    public class Controller : INotifyPropertyChanged
    {
        #region events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region private const fields

        private const string DefaultTemplate =
            "<#@ template hostspecific=\"true\" debug=\"true\" #>\r<#@ property processor=\"PropertyProcessor\" name=\"Argument\" type=\"String\" #>\r<#= DateTime.Now #>\rArgument: <#= Argument #>";
        private const string TemplateDomainActionCreate = "Create";
        private const string TemplateDomainActionUnload = "Unload";

        #endregion

        #region private fields

        private string template;
        private string output;
        private CompilerErrorCollection compilerErrors;
        private string compiledTemplate;
        private string templateDomainAction;
        private bool templateDomainAvailable;
        private string templateDomainName;
        private IList<ClassName> currentDomainClasses;
        private IList<ClassName> templateDomainClasses;

        private TextTransformerBase transformer;
        private DomainTextTransformer domain;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>The template.</value>
        public string Template
        {
            get
            {
                return template;
            }
            set
            {
                Change(ref template, value);
            }
        }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public TemplateArgumentCollection Arguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the compiler errors.
        /// </summary>
        /// <value>The compiler errors.</value>
        public CompilerErrorCollection CompilerErrors
        {
            get
            {
                return compilerErrors;
            }
            private set
            {
                Change(ref compilerErrors, value);
            }
        }

        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        /// <value>The output.</value>
        public string Output
        {
            get
            {
                return output;
            }
            private set
            {
                Change(ref output, value);
            }
        }

        /// <summary>
        /// Gets or sets the compiled template.
        /// </summary>
        /// <value>The output.</value>
        public string CompiledTemplate
        {
            get
            {
                return compiledTemplate;
            }
            private set
            {
                Change(ref compiledTemplate, value);
            }
        }

        /// <summary>
        /// Gets the template domain action.
        /// </summary>
        /// <value>The template domain action.</value>
        public string TemplateDomainAction
        {
            get
            {
                return templateDomainAction;
            }
            private set
            {
                Change(ref templateDomainAction, value);
            }
        }

        /// <summary>
        /// Gets or sets the number of ITextTransformer classes template domain.
        /// </summary>
        /// <value>The template domain action.</value>
        public bool TemplateDomainAvailable
        {
            get
            {
                return templateDomainAvailable;
            }
            private set
            {
                Change(ref templateDomainAvailable, value);
            }
        }

        public string TemplateDomainName
        {
            get
            {
                return templateDomainName;
            }
            private set
            {
                Change(ref templateDomainName, value);
            }
        }

        /// <summary>
        /// Gets or sets the number of ITextTransformer classes current domain.
        /// </summary>
        /// <value>The current domain classes.</value>
        public IList<ClassName> CurrentDomainClasses
        {
            get
            {
                return currentDomainClasses;
            }
            private set
            {
                Change(ref currentDomainClasses, value);
            }
        }

        /// <summary>
        /// Gets or sets the number of ITextTransformer classes template domain.
        /// </summary>
        /// <value>The template domain action.</value>
        public IList<ClassName> TemplateDomainClasses
        {
            get
            {
                return templateDomainClasses;
            }
            private set
            {
                Change(ref templateDomainClasses, value);
            }
        }

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        public Controller()
        {
            TemplateDomainAction = TemplateDomainActionCreate;
            Template = DefaultTemplate;
            Arguments = new TemplateArgumentCollection
                            {
                                new TemplateArgument("Argument", "Test")
                            };

            transformer = new TextTransformer();
            transformer.ClassDefinitionGenerated += Host_ClassDefinitionGenerated;

            RefreshDomainProperties();
        }

        #endregion

        #region methods

        /// <summary>
        /// Transforms this temamplet.
        /// </summary>
        internal bool Transform()
        {
            try
            {
                if (Arguments == null || Arguments.Count == 0)
                {
                    Output = transformer.Transform(template);
                }
                else if (Arguments.Count == 1)
                {
                    TemplateArgument argument = Arguments.First();
                    Output = transformer.Transform(template, argument.Name, argument.Value);
                }
                else
                {
                    Output = transformer.Transform(template, Arguments);
                }

                RefreshDomainProperties();

                CompilerErrors = null;
                return true;
            }
            catch (TextTransformationException ex)
            {
                CompilerErrors = ex.CompilationErrors;
                Output = null;
                return false;
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        internal void Clear()
        {
            Template = DefaultTemplate;
        }

        /// <summary>
        /// Toggles the template domain.
        /// </summary>
        internal void ToggleTemplateDomain()
        {
            transformer.ClassDefinitionGenerated -= Host_ClassDefinitionGenerated;

            if (domain == null)
            {
                domain = new DomainTextTransformer();
                domain.AutoRecycle = true;
                domain.RecycleThreshold = 5;
                TemplateDomainAction = TemplateDomainActionUnload;
                transformer = domain;
            }
            else
            {
                domain.Dispose();
                domain = null;
                TemplateDomainAction = TemplateDomainActionCreate;
                transformer = new TextTransformer();
            }

            transformer.ClassDefinitionGenerated += Host_ClassDefinitionGenerated;

            RefreshDomainProperties();
        }

        /// <summary>
        /// Recyles the template domain.
        /// </summary>
        internal void RecyleTemplateDomain()
        {
            if (domain == null)
            {
                return;
            }

            domain.Recycle();

            RefreshDomainProperties();
        }

        /// <summary>
        /// Handles the TemplateCompiled event of the Host control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TC.CustomTemplating.ClassDefinitionEventArgs"/> instance containing the event data.</param>
        void Host_ClassDefinitionGenerated(object sender, ClassDefinitionEventArgs e)
        {
            CompiledTemplate = e.ClassDefinition;
        }

        /// <summary>
        /// Refreshes the domain properties.
        /// </summary>
        private void RefreshDomainProperties()
        {
            if (domain == null)
            {
                TemplateDomainAvailable = false;
                TemplateDomainName = "<null>";
                TemplateDomainClasses = null;
            }
            else
            {
                TemplateDomainAvailable = true;
                TemplateDomainName = domain.AppDomain.FriendlyName;
                TemplateDomainClasses = GetDomainClasses(domain.AppDomain);
            }
            CurrentDomainClasses = GetDomainClasses(AppDomain.CurrentDomain);
        }

        /// <summary>
        /// Gets the domain classes.
        /// </summary>
        /// <param name="appDomain">The app domain.</param>
        /// <returns></returns>
        private static IList<ClassName> GetDomainClasses(AppDomain appDomain)
        {
            if (appDomain == null)
            {
                return null;
            }

            //Count TextTransformation classes in appDomain
            var assembly = Assembly.GetExecutingAssembly();

            if (assembly == null || string.IsNullOrEmpty(assembly.FullName))
            {
                throw new InvalidOperationException("assembly is null");
            }

            ClassNameFinder finder = appDomain.CreateInstanceAndUnwrap(assembly.FullName, typeof(ClassNameFinder).FullName) as ClassNameFinder;
            Debug.Assert(finder != null);

            return finder.GetTransformationClasses();
        }

        /// <summary>
        /// Changes the specified local variable and raised the PropertyChanged event
        /// . This method NEEDS to be called from the property setter. NO other usage allowed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="newValue">The new value.</param>
        private void Change<T>(ref T source, T newValue)
        {
            source = newValue;

            //Get calling property name
            var frameMethod = new StackFrame(1, false).GetMethod();
            var propertyName = frameMethod.Name.Substring(4);

            PropertyChangedEventArgs eventArgs = new PropertyChangedEventArgs(propertyName);
            OnPropertyChanged(eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, eventArgs);
            }
        }

        #endregion
    }
}
