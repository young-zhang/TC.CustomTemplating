// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

namespace TC.CustomTemplating
{
    /// <summary>
    /// Holds information about an argument used during 
    /// initialization of transformation.
    /// </summary>
    internal class ArgumentInfo
    {
        #region private fields

        private string _name;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                FieldName = "_{0}".FormatInvariant(_name);
            }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        /// <value>The field name.</value>
        public string FieldName { get; private set; }

        /// <summary>
        /// Gets or sets the type of the converter.
        /// </summary>
        /// <value>The type of the converter.</value>
        public string ConverterType { get; set; }

        /// <summary>
        /// Gets or sets the type of the editor.
        /// </summary>
        /// <value>The type of the editor.</value>
        public string EditorType { get; set; }

        #endregion
    }
}