// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Very simple ServiceLocator which will instantiate objects and their dependent objects. Each 
    /// object type is registred by a factory delegate that will be used to construct the object. 
    /// Arguments of the delegate are automaticly filled in.
    /// </summary>
    internal static class ServiceLocator
    {
        #region private fields

        private static readonly IDictionary<Type, Delegate> _types;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes the <see cref="ServiceLocator"/> class.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ServiceLocator()
        {
            _types = new Dictionary<Type, Delegate>();
            Register(new Func<IAppDomainManager>(() => new AppDomainManager()));
            Register(new Func<IFileSystem>(() => new FileSystem()));
            Register(new Func<IFileSystem, IPathResolver>(f => new PathResolver(f)));
            Register(new Func<IPathResolver, IFileSystem, IFileLoader>((r, f) => new FileLoader(r, f)));
            Register(new Func<ITextTransformer, IPathResolver, IFileLoader, ITextTransformerHost>((t, r, l) => new Host(t, r, l)));
        }

        #endregion

        #region methods

        /// <summary>
        /// Resolves a specific service and inject the arguments.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns></returns>
        internal static TContract Resolve<TContract>()
        {
            var type = typeof(TContract);
            return (TContract) Resolve(type, (object) null);
        }

        /// <summary>
        /// Resolves a specific service and inject the arguments.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TArgument">The type of the argument.</typeparam>
        /// <param name="argument">The argument.</param>
        /// <returns></returns>
        internal static TContract Resolve<TContract, TArgument>(TArgument argument) where TArgument : class
        {
            var type = typeof(TContract);
            return (TContract)Resolve(type, argument);
        }

        private static void Register(Delegate factory)
        {
            _types.Add(factory.Method.ReturnType, factory);
        }

        private static object Resolve<TArgument>(Type type, TArgument argument)
        {
            if (!_types.ContainsKey(type))
            {
                throw new InvalidOperationException("Type '{0}' not found".FormatInvariant(type));
            }
            var factory = _types[type];
            var parameters = factory.Method.GetParameters();
            var arguments = new object[parameters.Length];
            int i = 0;
            foreach (var info in parameters)
            {
                arguments[i++] = 
                    info.ParameterType == typeof(TArgument) 
                  ? argument 
                  : Resolve<object>(info.ParameterType, null);
            }

            return factory.DynamicInvoke(arguments);
        }

        #endregion
    }
}