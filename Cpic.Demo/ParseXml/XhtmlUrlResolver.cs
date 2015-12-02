using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace ParseXml
{
    public class XhtmlUrlResolver : XmlUrlResolver
    {
        private static Dictionary<string, Uri> theDTDCache;
        private static string AppConfigPath = "";

        public  XhtmlUrlResolver(String sAppConfigPath)
        {
            AppConfigPath = sAppConfigPath;
        }

        public override Uri ResolveUri(Uri baseUri, string relativeUri)
        {
            if (DTDCache.ContainsKey(relativeUri))
            {
                Uri uri = DTDCache[relativeUri];
                return uri;
            }
            else
            {
                return base.ResolveUri(baseUri, relativeUri);
            }
        }

        private static Dictionary<string, Uri> DTDCache
        {
            get
            {
                if (theDTDCache == null)
                {
                    theDTDCache = new Dictionary<string, Uri>();
                    BuildDTDCache(theDTDCache);
                }
                return theDTDCache;
            }
        }

        private static void BuildDTDCache(Dictionary<string, Uri> dc)
        {
            //dwpi
            dc.Add("xhtml1-strict.dtd", new Uri(Path.Combine(AppConfigPath, "xhtml1-strict.dtd")));
            dc.Add("dataFeed.dtd", new Uri(Path.Combine(AppConfigPath, "dataFeed.dtd")));
            dc.Add("tsxmTextMarkup.dtd", new Uri(Path.Combine(AppConfigPath, "tsxmTextMarkup.dtd")));
            dc.Add("WPIMAX.dtd", new Uri(Path.Combine(AppConfigPath, "WPIMAX.dtd")));
            dc.Add("xhtml-qname-1.mod", new Uri(Path.Combine(AppConfigPath, "xhtml-qname-1.mod")));
        }
    }
}
