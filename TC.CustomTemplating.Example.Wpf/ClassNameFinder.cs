using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TextTemplating;

namespace TC.CustomTemplating.Example.Wpf
{
    public class ClassNameFinder : MarshalByRefObject
    {
        /// <summary>
        /// Gets the transformation classes.
        /// </summary>
        /// <returns></returns>
        public IList<ClassName> GetTransformationClasses()
        {
            var types = (from ass in AppDomain.CurrentDomain.GetAssemblies()
                         from type in ass.GetTypes()
                         where type.BaseType != null 
                            && type.BaseType.Name == typeof(TextTransformation).Name
                         select new ClassName { Name = type.FullName }).ToList();
            return types;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ClassNameFinder"/> is reclaimed by garbage collection.
        /// </summary>
        ~ClassNameFinder()
        {
        }
    }
}
