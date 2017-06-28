// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Transforms templates in the current AppDomain.
    /// </summary>
    public class TextTransformer : TextTransformerBase
    {
        #region properties

        /// <summary>
        /// Gets the app domain used for the transformation.
        /// </summary>
        /// <value></value>
        public override AppDomain AppDomain
        {
            get { return AppDomain.CurrentDomain; }
        }

        #endregion
    }
}