// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System.Collections.ObjectModel;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Collection that contains named template arguments.
    /// </summary>
    public class TemplateArgumentCollection : KeyedCollection<string, TemplateArgument>
    {
        #region methods

        /// <summary>
        /// When implemented in a derived class, extracts the key from the specified element.
        /// </summary>
        /// <param name="item">The element from which to extract the key.</param>
        /// <returns>The key for the specified element.</returns>
        protected override string GetKeyForItem(TemplateArgument item)
        {
            return item.Name;
        }

        #endregion
    }
}