using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ParseXml
{
    public class XmlParser
    {
        private XmlParserContext m_context = null;

        public XmlParserContext Context
        {
            get { return m_context; }
            set { m_context = value; }
        }
        private XmlReaderSettings m_settings = null;



        public XmlReaderSettings Settings
        {
            get { return m_settings; }
            set { m_settings = value; }
        }

        //DTD文件路径
        private String m_DTDPath = String.Empty;
        public String DTDPath
        {
            get { return m_DTDPath; }
            set { m_DTDPath = value; }
        }

        public XmlParser(String DTDPath, String xmlEncoding)
        {
            m_settings = new XmlReaderSettings();
            m_settings.ProhibitDtd = false;
            m_settings.ValidationType = ValidationType.DTD;

            if (DTDPath == String.Empty)
            {
                m_DTDPath = String.Empty;
            }
            else
            {
                m_DTDPath = "file:///" + DTDPath;
            }
            NameTable nt = new NameTable();
            XmlNamespaceManager xnm = new XmlNamespaceManager(nt);
            // m_context = new XmlParserContext(nt, new XmlNamespaceManager(nt), "exchange-document", "", m_DTDPath, "", "", "en", XmlSpace.None);
            m_context = new XmlParserContext(nt, new XmlNamespaceManager(nt), "exchange-document", "", "", "", "", "en", XmlSpace.None, Encoding.GetEncoding(xmlEncoding));
        }
    }
}
