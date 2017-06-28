using System;

namespace TC.CustomTemplating.Example.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Property
    {
        private string name;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }
                char firstLetter = value.ToCharArray(0, 1)[0];
                if (!char.IsUpper(firstLetter))
                {
                    throw new ArgumentException("First digit of name should be upppercase!");
                }
                name = value;
            }
        }

        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        /// <value>The name.</value>
        public string FieldName
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return null;
                }
                return string.Format("{0}{1}",
                    Name.Substring(0, 1).ToLower(),
                    Name.Substring(1));
            }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type
        {
            get; set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        public Property()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        public Property(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}
