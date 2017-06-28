using System;
using System.Collections.Generic;

namespace TC.CustomTemplating.IntegrationTests.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ClassDefinition : MarshalByRefObject
    {
        private readonly List<Property> _properties = new List<Property>();

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        public string Namespace
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text1.
        /// </summary>
        /// <value>The text1.</value>
        public List<Property> Properties
        {
            get
            {
                return _properties;
            }
        }
    }
}