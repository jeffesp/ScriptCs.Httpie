using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Httpie
{
    public partial class Httpie
    {

        public void Get()
        {
            Execute();
        }
        public void Get(string url)
        {
            Url(url).Execute();
        }

        public Httpie Url(string url)
        {
            uri = new Uri(urlParser.ParseUrl(url), UriKind.Absolute);
            return this;
        }


    }
}
