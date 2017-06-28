// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using Microsoft.VisualStudio.TextTemplating;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Custom T4 directive processor that makes the template arguments accessible
    /// from within the template. The argument are accesible through a
    /// generated property. The argument is initialized by asking the 
    /// host for the object by invoking the GetArgument method. There is
    /// is also a property generated called Host that allows to
    /// access the custom host.
    /// </summary>
    internal sealed class TemplateArgumentDirectiveProcessor : DirectiveProcessor
    {
        #region private const fields

        private const string ArgumentNameType = "type";
        private const string ArgumentNameName = "name";
        private const string ArgumentNameConverter = "converter";
        private const string ArgumentNameEditor = "editor";
        private const string MethodGetArgument = "LogicalGetData";

        #endregion

        #region private fields

        private static readonly CodeGeneratorOptions _options = new CodeGeneratorOptions
                          {
                              BlankLinesBetweenMembers = true,
                              IndentString = "        ",
                              VerbatimOrder = true,
                              BracingStyle = "C"
                          };

        private readonly Dictionary<string, ArgumentInfo> _argumentInfos = new Dictionary<string, ArgumentInfo>();

        private CodeDomProvider _provider;
        private bool _editorUsed;

        #endregion

        #region properties

        /// <summary>
        /// Gets the code provider.
        /// </summary>
        /// <value>The code provider.</value>
        public CodeDomProvider CodeProvider
        {
            get { return _provider; }
        }

        #endregion

        #region methods

        /// <summary>
        /// Starts the processing run.
        /// </summary>
        /// <param name="languageProvider">The language provider.</param>
        /// <param name="templateContents">The template contents.</param>
        /// <param name="errors">The errors.</param>
        public override void StartProcessingRun(CodeDomProvider languageProvider, string templateContents, CompilerErrorCollection errors)
        {
            base.StartProcessingRun(languageProvider, templateContents, errors);
            _editorUsed = false;
            _provider = languageProvider;
        }

        /// <summary>
        /// Processes the directive.
        /// </summary>
        /// <param name="directiveName">Name of the directive.</param>
        /// <param name="arguments">The arguments.</param>
        public override void ProcessDirective(string directiveName, IDictionary<string, string> arguments)
        {
            var info = new ArgumentInfo
                                    {
                                        Name = GetArgument(arguments, ArgumentNameName),
                                        Type = GetTypeArgument(arguments, ArgumentNameType),
                                        ConverterType = GetTypeArgument(arguments, ArgumentNameConverter),
                                        EditorType = GetTypeArgument(arguments, ArgumentNameEditor)
                                    };

            if (string.IsNullOrEmpty(info.Name))
            {
                throw new InvalidOperationException("Property directive type name null or empty.");
            }

            if (_argumentInfos.ContainsKey(info.Name))
            {
                throw new InvalidOperationException(
                    "Object directive '{0}' already exists."
                        .FormatInvariant(directiveName));
            }

            if (string.IsNullOrEmpty(info.Type))
            {
                throw new InvalidOperationException(
                    "Property directive '{0}' type is null or empty."
                        .FormatInvariant(info.Name));
            }

            if (!string.IsNullOrEmpty(info.EditorType))
            {
                _editorUsed = true;
            }
            _argumentInfos.Add(info.Name, info);
        }

        /// <summary>
        /// Generates the code that will be added to the generated template. For
        /// each argument a private field is generated that will hold the argument 
        /// value. Also a readonly public property is generated to access the argument 
        /// value. When a convertor of editor is defined there is a attribute genereted 
        /// for each of them.
        /// </summary>
        /// <returns>The fields and arguments</returns>
        public override string GetClassCodeForProcessingRun()
        {
            using (var writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                foreach (ArgumentInfo argument in _argumentInfos.Values)
                {
                    //Create field
                    var field = new CodeMemberField(argument.Type, argument.FieldName)
                                    {
                                        Attributes = MemberAttributes.Private
                                    };

                    _provider.GenerateCodeFromMember(field, writer, _options);

                    //Create the property for each argument
                    var property = new CodeMemberProperty
                                       {
                                           Name = argument.Name,
                                           Type = new CodeTypeReference(argument.Type),
                                           Attributes = MemberAttributes.Public,
                                           HasGet = true,
                                           HasSet = false
                                       };
                    property.GetStatements.Add(
                        new CodeMethodReturnStatement(new CodeFieldReferenceExpression(
                                                          new CodeThisReferenceExpression(),
                                                          argument.FieldName)));
                    //Add editor attribute
                    if (!string.IsNullOrEmpty(argument.EditorType))
                    {
                        property.CustomAttributes.Add(new CodeAttributeDeclaration(
                                                          new CodeTypeReference(typeof(EditorAttribute)),
                                                          new CodeAttributeArgument(
                                                              new CodeTypeOfExpression(argument.EditorType)),
                                                          new CodeAttributeArgument(
                                                              new CodeTypeOfExpression(typeof(UITypeEditor)))));
                    }

                    //Add convertor attribute
                    if (!string.IsNullOrEmpty(argument.ConverterType))
                    {
                        property.CustomAttributes.Add(new CodeAttributeDeclaration(
                                                          new CodeTypeReference(typeof(TypeConverter)),
                                                          new CodeAttributeArgument(
                                                              new CodeTypeOfExpression(argument.ConverterType))));
                    }

                    _provider.GenerateCodeFromMember(property, writer, _options);
                }

                return writer.ToString();
            }
        }

        /// <summary>
        /// Gets the post initialization code for processing run. This will 
        /// generate a call to the GetArgument method of the host for each 
        /// private field generated. 
        /// </summary>
        /// <returns></returns>
        public override string GetPostInitializationCodeForProcessingRun()
        {
            using (var writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                foreach (ArgumentInfo argument in _argumentInfos.Values)
                {
                    //Generate initialization code for each argument
                    //_FieldName = (FieldType) this.GetHostOption("_FieldName");
                    var assignment = new CodeAssignStatement(
                        new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), argument.FieldName),
                        new CodeCastExpression(
                            new CodeTypeReference(argument.Type),
                            new CodeMethodInvokeExpression(
                                new CodeTypeReferenceExpression(typeof(CallContext)),
                                MethodGetArgument,
                                new CodePrimitiveExpression(argument.Name))));

                    _provider.GenerateCodeFromStatement(assignment, writer, _options);
                }

                return writer.ToString();
            }
        }

        /// <summary>
        /// Finishes the processing run.
        /// </summary>
        public override void FinishProcessingRun()
        {
            return;
        }

        /// <summary>
        /// Gets the imports for processing run.
        /// </summary>
        /// <returns></returns>
        public override string[] GetImportsForProcessingRun()
        {
            return null;
        }

        /// <summary>
        /// Gets the pre initialization code for processing run.
        /// </summary>
        /// <returns></returns>
        public override string GetPreInitializationCodeForProcessingRun()
        {
            return null;
        }

        /// <summary>
        /// Gets the references for processing run.
        /// </summary>
        /// <returns></returns>
        public override string[] GetReferencesForProcessingRun()
        {
            if (_editorUsed)
            {
                return new[] { typeof(UITypeEditor).Assembly.Location };
            }
            return null;
        }

        /// <summary>
        /// Determines whether the directive with the specific name is supported.
        /// </summary>
        /// <param name="directiveName">Name of the directive.</param>
        /// <returns>
        /// 	<c>true</c> always returns true.
        /// </returns>
        public override bool IsDirectiveSupported(string directiveName)
        {
            return true;
        }

        /// <summary>
        /// Get the argument with the specified name.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private static string GetArgument(IDictionary<string, string> arguments, string name)
        {
            return !arguments.ContainsKey(name) ? null : arguments[name];
        }

        /// <summary>
        /// Gets a type argument and normalize it.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private static string GetTypeArgument(IDictionary<string, string> arguments, string name)
        {
            var argument = GetArgument(arguments, name);
            return NormalizeType(argument);
        }

        /// <summary>
        /// Normalizes the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        internal static string NormalizeType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return null;
            }
            var charArray = type.ToCharArray();
            var genericTypes = 0;
            var commaIndex = -1;
            for (int i = 0; i < charArray.Length; i++)
            {
                var c = charArray[i];
                if (c == '<')
                {
                    genericTypes++;
                }
                else if (c == '>')
                {
                    genericTypes--;
                }
                else if (c == ',' && genericTypes == 0)
                {
                    commaIndex = i;
                    break;
                }
            }
            if (genericTypes != 0)
            {
                var message = "Invalid generic type specified: '{0}'".FormatInvariant(type);
                throw new InvalidOperationException(message);
            }
            if (commaIndex >= 0)
            {
                return type.Substring(0, commaIndex).Trim();
            }
            return type;
        }

        #endregion
    }
}