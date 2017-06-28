using System;
using Microsoft.VisualStudio.TextTemplating;

namespace TC.CustomTemplating.UnitTests.TextTransformerBaseSpecifications
{
    class TextTransformerImplementation : TextTransformerBase
    {
        public TextTransformerImplementation()
        {
        }

        public TextTransformerImplementation(ITextTransformerHost host, ITextTemplatingEngine engine) : base(host, engine)
        {
        }

        public override AppDomain AppDomain
        {
            get { return AppDomain.CurrentDomain; }
        }
    }
}
