// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

namespace TC.CustomTemplating
{
    /// <summary>
    /// Named argument who's value can be passed to a template.
    /// </summary>
    public class TemplateArgument
    {
        #region properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value { get; set; }

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateArgument"/> class.
        /// </summary>
        public TemplateArgument( )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateArgument"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public TemplateArgument(string name, object value)
        {
            Name = name;
            Value = value;
        }

        #endregion
    }
}