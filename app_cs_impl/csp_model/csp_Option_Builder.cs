using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csp_model
{
   public sealed class CspOptionsBuilder
    {
        private readonly CspOptions options = new CspOptions();
        public CspOptionsBuilder() { }
        public CspDirectiveBuilder Defaults { get; set; } = new CspDirectiveBuilder();
        public CspDirectiveBuilder Scripts { get; set; } = new CspDirectiveBuilder();
        public CspDirectiveBuilder Styles { get; set; } = new CspDirectiveBuilder();
        public CspDirectiveBuilder Images { get; set; } = new CspDirectiveBuilder();
        public CspDirectiveBuilder Fonts { get; set; } = new CspDirectiveBuilder();
        public CspDirectiveBuilder Media { get; set; } = new CspDirectiveBuilder();
        public CspOptions Build()
        {
            this.options.Defaults = this.Defaults.Sources;
            this.options.Scripts = this.Scripts.Sources;
            this.options.Styles = this.Styles.Sources;
            this.options.Images = this.Images.Sources;
            this.options.Fonts = this.Fonts.Sources;
            this.options.Media = this.Media.Sources;
            return this.options;
        }
    }

    public sealed class CspDirectiveBuilder
    {
        internal CspDirectiveBuilder() { }

        internal List<string> Sources { get; set; } = new List<string>();

        public CspDirectiveBuilder AllowSelf() => Allow("'self'");
        public CspDirectiveBuilder AllowNone() => Allow("none");
        public CspDirectiveBuilder AllowAny() => Allow("*");

        public CspDirectiveBuilder Allow(string source)
        {
            this.Sources.Add(source);
            return this;
        }
    }
}
