// CustomTemplating Library // www.timcools.net - Copyleft 2009 - Licensed under GNU LGPL v3.0
/////////////////////////////

using System;

namespace TC.CustomTemplating
{
    /// <summary>
    /// Event arguments containing the generated template class definition.
    /// </summary>
    [Serializable]
    public class ClassDefinitionEventArgs : EventArgs
    {
        #region private fields

        private readonly string _classDefinition;

        #endregion

        #region properties

        /// <summary>
        /// Get the generated template class definition code.
        /// </summary>
        public string ClassDefinition
        {
            get { return _classDefinition; }
        }

        #endregion

        #region constructor

        /// <summary>
        /// Initialize new ClassDefinitionEventArgs.
        /// </summary>
        /// <param name="classDefinition">The class definition.</param>
        public ClassDefinitionEventArgs(string classDefinition)
        {
            if (classDefinition == null) throw new ArgumentNullException("classDefinition");

            _classDefinition = classDefinition;
        }

        #endregion
    }
}