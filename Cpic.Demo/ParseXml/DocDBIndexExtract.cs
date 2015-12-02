using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Collections;
using Util;
using System.Text.RegularExpressions;

namespace ParseXml
{
    public class DocDBIndexExtract
    {

        private XmlParser xmlParser;
        public XNamespace exch = "http://www.epo.org/exchange";

        public DocDBIndexExtract(XmlParser p)
        {
            this.xmlParser = p;
        }


        //根据公开号获得xml文件的路径
        private String getXmlPathByPublicNo(String publicno)
        {
            String xmlpath = Common.DocDB_File_Root;
            String xmlpath_16 = publicno.PadLeft(16, '0');
            xmlpath = xmlpath + publicno.Substring(0, 2) + "//" + xmlpath_16.Substring(0, 4) + "//" + xmlpath_16.Substring(4, 4) + "//"
                + xmlpath_16.Substring(8, 4) + "//" + xmlpath_16.Substring(12, 4) + "//" + publicno + ".xml";
            return xmlpath;
        }

        //获得一篇专利文献的英文摘要，如果没有英文摘要，返回null。
        private String getAbstract(String xmlpath)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(xmlpath, xmlParser.Settings, xmlParser.Context))
                {
                    XDocument xRoot = XDocument.Load(reader, LoadOptions.None);
                    var nodes =
                                 from el in xRoot.Descendants(exch + "patent-family").Descendants(exch + "abstract")
                                 where (el.Attribute("lang") != null && el.Attribute("lang").Value.Trim().ToLower() == "en")
                                 select el;
                    String value = String.Empty;
                    if (nodes.Count() != 0)//在当前文献中含有英文摘要
                    {
                        foreach (var node in nodes)
                        {
                            value = value + Common.Index_Spliter + node.Value.Trim();
                        }
                        value = StringUtil.RemoveFirstSpliter(value);
                        value = value.Replace("|", " ");
                        value = value.Replace("\n", " ");
                        return value;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(xmlpath + "提取专利文献摘要出错:" + e.Message);
            }
        }


        /// <summary>
        /// 获得专利文献摘要,如为空通过家族号查找
        /// </summary>
        /// <param name="xRoot"></param>
        /// <param name="pubIDs"></param>
        /// <returns></returns>
        public String getAbstract(XDocument xRoot, String[] pubIDs)
        {
            try
            {
                var nodes =
                        from el in xRoot.Descendants(exch + "abstract")
                        where (el.Attribute("lang") != null && el.Attribute("lang").Value.Trim().ToLower() == "en")
                        select el;
                String value = String.Empty;
                if (nodes == null || nodes.Count() == 0)
                {
                    nodes =
                        from el in xRoot.Descendants(exch + "patent-family").Descendants(exch + "abstract")
                        where (el.Attribute("lang") != null && el.Attribute("lang").Value.Trim().ToLower() == "en")
                        select el;
                }
                foreach (var node in nodes)
                {
                    value = value + Common.Index_Spliter + node.Value.Trim();
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = value.Replace("|", " ");
                value = value.Replace("\n", " ");
                value = value.Replace(Common.Index_Spliter, " ");
                return value;
                //else//当前文献中没有英文摘要，则取同族专利中获得英文文献摘要
                //{
                //    //for (int i = 0; pubIDs != null && i < pubIDs.Length; i++)
                //    //{
                //    //    String publicno = pubIDs[i].ToString().Trim();
                //    //    String xmlpath = getXmlPathByPublicNo(publicno);//获得文献的路径
                //    //    String abs = getAbstract(xmlpath);
                //    //    if (abs != null)
                //    //    {
                //    //        return abs;
                //    //    }
                //    //}
                //    return String.Empty;
                //}
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献摘要出错:" + e.Message);
            }
        }


        public string getAbstract(XDocument xRoot)
        {
            string value = "";
            try
            {
                var nodes =
                        from el in xRoot.Descendants(exch + "abstract")
                        where (el.Attribute("lang") != null && el.Attribute("lang").Value.Trim().ToLower() == "en")
                        select el;

                if (nodes == null || nodes.Count() == 0)
                {
                    nodes =
                        from el in xRoot.Descendants(exch + "patent-family").Descendants(exch + "abstract")
                        where (el.Attribute("lang") != null && el.Attribute("lang").Value.Trim().ToLower() == "en")
                        select el;
                }

                if (nodes == null || nodes.Count() == 0)
                {
                    nodes = from el in xRoot.Descendants(exch + "abstract")
                            select el;
                }

                foreach (var node in nodes)
                {
                    value = value + Common.Index_Spliter + node.Value.Trim();
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = value.Replace("|", " ");
                value = value.Replace("\n", " ");
                value = value.Replace(Common.Index_Spliter, " ");
            }
            catch (Exception ex)
            { }
            return value;
        }

        public string getFmyAbstract(XDocument xRoot)
        {
            string result = "";
            try
            {
                var results = from e in xRoot.Elements(exch + "patent-family").Elements(exch + "abstract")
                              select e;
                foreach (var item in results)
                {
                    result = item.Value;
                }

            }
            catch (Exception ex)
            { }
            return result;
        }


        //获取专利文献发明名称
        public String getInventionTitle(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes = from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "invention-title")
                            where (el.Attribute("lang") != null && el.Attribute("lang").Value.Trim().ToLower() == "en")
                            select el;
                if (nodes == null || nodes.Count() == 0)
                {
                    nodes = from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "invention-title")
                            select el;
                }

                foreach (var node in nodes)
                {
                    value = value + Common.Index_Spliter + node.Value.Trim();
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = value.Replace("|", " ");
                value = value.Replace("\n", " ");
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献发明名称出错:" + e.Message);
            }
            return value;
        }

        //获得专利文献公开号
        public String getPublicDocNo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var exchange_document =
                               from el in xRoot.Descendants(exch + "exchange-document")
                               select el;

                foreach (var node in exchange_document)
                {
                    String country = node.Attribute("country") == null ? "" : node.Attribute("country").Value.Trim();
                    String doc_number = node.Attribute("doc-number") == null ? "" : node.Attribute("doc-number").Value.Trim();
                    String kind = node.Attribute("kind") == null ? "" : node.Attribute("kind").Value.Trim();
                    value = value + Common.Index_Spliter + country + doc_number + kind;
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献公开号出错:" + e.Message);
            }
            return value;
        }


        //获得专利文献公开号
        public String getEpoPublicDocNo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "publication-reference").Descendants("document-id")
                         where (el.Parent.Attribute("data-format") != null && el.Parent.Attribute("data-format").Value.Trim().ToLower() == "epodoc")
                         select el;

                foreach (var node in nodes)
                {
                    var country = node.Descendants("country");
                    var doc_number = node.Descendants("doc-number");
                    var kind = node.Descendants("kind");
                    if (country.Count() > 0)
                    {
                        value = value + Common.Index_Spliter + country.First().Value.Trim();
                    }
                    if (doc_number.Count() > 0)
                    {
                        if (country.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + doc_number.First().Value.Trim();
                        }
                        else
                        {
                            value = value + doc_number.First().Value.Trim();
                        }
                    }
                    if (kind.Count() > 0)
                    {
                        if (country.Count() == 0 && doc_number.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + kind.First().Value.Trim();
                        }
                        else
                        {
                            value = value + kind.First().Value.Trim();
                        }
                    }
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献EPO公开号出错:" + e.Message);
            }
            return value;
        }

        //获得专利文献公开号DODB格式
        public String getDocdbPublicDocNo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "publication-reference").Descendants("document-id")
                         where (el.Parent.Attribute("data-format") != null && el.Parent.Attribute("data-format").Value.Trim().ToLower() == "docdb")
                         select el;

                foreach (var node in nodes)
                {
                    var country = node.Descendants("country");
                    var doc_number = node.Descendants("doc-number");
                    var kind = node.Descendants("kind");
                    if (country.Count() > 0)
                    {
                        value = value + Common.Index_Spliter + country.First().Value.Trim();
                    }
                    if (doc_number.Count() > 0)
                    {
                        if (country.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + doc_number.First().Value.Trim();
                        }
                        else
                        {
                            value = value + doc_number.First().Value.Trim();
                        }
                    }
                    if (kind.Count() > 0)
                    {
                        if (country.Count() == 0 && doc_number.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + kind.First().Value.Trim();
                        }
                        else
                        {
                            value = value + kind.First().Value.Trim();
                        }
                    }
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献EPO公开号出错:" + e.Message);
            }
            return value;
        }

        //获得专利文献公开号-原始格式
        public String getOriginalPublicDocNo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "publication-reference").Descendants("document-id")
                         where (el.Parent.Attribute("data-format") != null && el.Parent.Attribute("data-format").Value.Trim().ToLower() == "original")
                         select el;

                foreach (var node in nodes)
                {
                    var country = node.Descendants("country");
                    var doc_number = node.Descendants("doc-number");
                    var kind = node.Descendants("kind");
                    if (country.Count() > 0)
                    {
                        value = value + Common.Index_Spliter + country.First().Value.Trim();
                    }
                    if (doc_number.Count() > 0)
                    {
                        if (country.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + getCountry(xRoot).Trim() + doc_number.First().Value.Trim();
                        }
                        else
                        {
                            value = value + doc_number.First().Value.Trim();
                        }
                    }
                    if (kind.Count() > 0)
                    {
                        if (country.Count() == 0 && doc_number.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + kind.First().Value.Trim();
                        }
                        else
                        {
                            value = value + kind.First().Value.Trim();
                        }
                    }
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献EPO公开号出错:" + e.Message);
            }
            return value;
        }

        public String getKind(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var exchange_document =
                               from el in xRoot.Descendants(exch + "exchange-document")
                               select el;

                foreach (var node in exchange_document)
                {
                    String kind = node.Attribute("kind") == null ? "" : node.Attribute("kind").Value.Trim();
                    value = value + Common.Index_Spliter + kind;
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献类型出错:" + e.Message);
            }
            return value;
        }


        public String getStatus(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var exchange_document =
                               from el in xRoot.Descendants(exch + "exchange-document")
                               select el;

                foreach (var node in exchange_document)
                {
                    String kind = node.Attribute("status") == null ? "" : node.Attribute("status").Value.Trim();
                    value = value + Common.Index_Spliter + kind;
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献属性出错:" + e.Message);
            }
            return value;
        }


        public String getLastExchangeDate(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var exchange_document =
                               from el in xRoot.Descendants(exch + "exchange-document")
                               select el;

                foreach (var node in exchange_document)
                {
                    String date = node.Attribute("date-of-last-exchange") == null ? "" : node.Attribute("date-of-last-exchange").Value.Trim();
                    value = value + Common.Index_Spliter + date;
                }
                value = StringUtil.RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献最后交换日期出错:" + e.Message);
            }
            return value;
        }


        public String getSerialNo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var exchange_document =
                               from el in xRoot.Descendants(exch + "exchange-document")
                               select el;

                foreach (var node in exchange_document)
                {
                    String doc_number = node.Attribute("doc-number") == null ? "" : node.Attribute("doc-number").Value.Trim();
                    value = value + Common.Index_Spliter + doc_number;
                }
                value = StringUtil.RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献流水号出错:" + e.Message);
            }
            return value;
        }

        public String getCountry(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var exchange_document =
                               from el in xRoot.Descendants(exch + "exchange-document")
                               select el;

                foreach (var node in exchange_document)
                {
                    String country = node.Attribute("country") == null ? "" : node.Attribute("country").Value.Trim();
                    value = value + Common.Index_Spliter + country;
                }
                value = StringUtil.RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献国别出错:" + e.Message);
            }
            return value;
        }

        //获取专利文献公开日期
        public String getPublicDate(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var exchange_document =
                              from el in xRoot.Descendants(exch + "exchange-document")
                              select el;

                foreach (var node in exchange_document)
                {
                    String date_publ = node.Attribute("date-publ") == null ? getPublicDateByBody(xRoot) : node.Attribute("date-publ").Value.Trim();
                    date_publ = FormatUtil.FormatDate(date_publ);
                    value = value + Common.Index_Spliter + date_publ;
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献公开日期出错:" + e.Message);
            }
            return value;
        }

        //获得专利文献公开日期
        private string getPublicDateByBody(XDocument xRoot)
        {
            String value = "";
            try
            {
                var nodes =
                             from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "publication-reference").Descendants("document-id").Descendants("date")
                             where (el.Parent.Parent.Attribute("data-format") != null && el.Parent.Parent.Attribute("data-format").Value.Trim().ToLower() == "docdb")
                             select el;
                foreach (var node in nodes)
                {
                    String Date = node.Value.Trim();
                    Date = FormatUtil.FormatDate(Date);
                    value = value + Common.Index_Spliter + Date;
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献开日期出错:" + e.Message);
            }
            return value;
        }

        //获取专利文献申请号
        public String getApplyNo(XDocument xRoot)
        {
            return getDocdbApplyNo(xRoot);
        }

        //获取专利文献申请号
        public String getDocdbApplyNo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "application-reference").Descendants("document-id")
                         where (el.Parent.Attribute("data-format") != null && el.Parent.Attribute("data-format").Value.Trim().ToLower() == "docdb")
                         select el;

                foreach (var node in nodes)
                {
                    var country = node.Descendants("country");
                    var doc_number = node.Descendants("doc-number");
                    var kind = node.Descendants("kind");
                    if (country.Count() > 0)
                    {
                        value = value + Common.Index_Spliter + country.First().Value.Trim();
                    }
                    if (doc_number.Count() > 0)
                    {
                        if (country.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + doc_number.First().Value.Trim();
                        }
                        else
                        {
                            value = value + doc_number.First().Value.Trim();
                        }
                    }
                    if (kind.Count() > 0)
                    {
                        if (country.Count() == 0 && doc_number.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + kind.First().Value.Trim();
                        }
                        else
                        {
                            value = value + kind.First().Value.Trim();
                        }
                    }
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献申请号出错:" + e.Message);
            }
            return value;
        }

        //获取专利文献申请号EPO格式
        public String getEpoApplyNo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "application-reference").Descendants("document-id")
                         where (el.Parent.Attribute("data-format") != null && el.Parent.Attribute("data-format").Value.Trim().ToLower() == "epodoc")
                         select el;

                foreach (var node in nodes)
                {
                    var country = node.Descendants("country");
                    var doc_number = node.Descendants("doc-number");
                    var kind = node.Descendants("kind");
                    if (country.Count() > 0)
                    {
                        value = value + Common.Index_Spliter + country.First().Value.Trim();
                    }
                    if (doc_number.Count() > 0)
                    {
                        if (country.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + doc_number.First().Value.Trim();
                        }
                        else
                        {
                            value = value + doc_number.First().Value.Trim();
                        }
                    }
                    if (kind.Count() > 0)
                    {
                        if (country.Count() == 0 && doc_number.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + kind.First().Value.Trim();
                        }
                        else
                        {
                            value = value + kind.First().Value.Trim();
                        }
                    }
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献申请号出错:" + e.Message);
            }
            return value;
        }

        //获取专利文献申请号原始格式
        public String getOriginalApplyNo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "application-reference").Descendants("document-id")
                         where (el.Parent.Attribute("data-format") != null && el.Parent.Attribute("data-format").Value.Trim().ToLower() == "original")
                         select el;

                foreach (var node in nodes)
                {
                    var country = node.Descendants("country");
                    var doc_number = node.Descendants("doc-number");
                    var kind = node.Descendants("kind");
                    if (country.Count() > 0)
                    {
                        value = value + Common.Index_Spliter + country.First().Value.Trim();
                    }
                    if (doc_number.Count() > 0)
                    {
                        if (country.Count() == 0)
                        {
                            //value = value + Common.Index_Spliter + doc_number.First().Value.Trim();
                            value = value + Common.Index_Spliter + getCountry(xRoot).Trim() + doc_number.First().Value.Trim();
                        }
                        else
                        {
                            value = value + doc_number.First().Value.Trim();
                        }
                    }
                    if (kind.Count() > 0)
                    {
                        if (country.Count() == 0 && doc_number.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + kind.First().Value.Trim();
                        }
                        else
                        {
                            value = value + kind.First().Value.Trim();
                        }
                    }
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献申请号出错:" + e.Message);
            }
            return value;
        }

        //获取专利文献申请日
        public String getApplyDate(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "application-reference").Descendants("document-id").Descendants("date")
                             where (el.Parent.Parent.Attribute("data-format") != null && el.Parent.Parent.Attribute("data-format").Value.Trim().ToLower() == "docdb")
                             select el;
                foreach (var node in nodes)
                {
                    String Date = node.Value.Trim();
                    Date = FormatUtil.FormatDate(Date);
                    value = value + Common.Index_Spliter + Date;
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献申请日出错:" + e.Message);
            }
            return value;
        }

        //获取专利文献IPC号
        public String getIPC(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "classifications-ipcr").Descendants("classification-ipcr").Descendants("text")
                             select el;
                foreach (var node in nodes)
                {
                    var IPC = node.Value.Trim();
                    IPC = FormatUtil.DocDbFormatIPC(IPC);
                    IPC = FormatUtil.FormatIPC_RemoveSpace(IPC);
                    value = value + Common.Index_Spliter + IPC;
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献IPC号出错:" + e.Message);
            }
            return value;
        }

        //获取专利文献ECLA号
        public String getECLA(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "classification-ecla").Descendants("classification-symbol")
                             where (el.Parent.Attribute("classification-scheme") != null && el.Parent.Attribute("classification-scheme").Value.Trim().ToUpper() == "EC")
                             select el;
                foreach (var node in nodes)
                {
                    var ECLA = node.Value.Trim();
                    ECLA = FormatUtil.FormatECLA(ECLA);
                    value = value + Common.Index_Spliter + ECLA;
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献ECLA号出错:" + e.Message);
            }
            return value;
        }

        //获取专利文献申请人
        public String getApplies(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "parties").Descendants(exch + "applicants").Descendants(exch + "applicant").Descendants(exch + "applicant-name").Descendants("name")
                             where (el.Parent.Parent.Attribute("data-format") != null && el.Parent.Parent.Attribute("data-format").Value.Trim().ToLower() == "docdb")
                             select el;

                foreach (var node in nodes)
                {
                    String text = node.Value.Trim();
                    if (text.Contains("["))//如果是EPO的数据，则把名字后面的国别去掉
                    {
                        text = text.Substring(0, text.IndexOf('[')).Trim();
                    }
                    value = value + Common.Index_Spliter + text;
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = value.Replace("|", " ");
                value = value.Replace("\n", " ");
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献申请人出错:" + e.Message);
            }
            return value;
        }

        //获取专利文献申请人+[国别]
        public String getAppliesAndCC(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "parties").Descendants(exch + "applicants").Descendants(exch + "applicant").Descendants(exch + "applicant-name").Descendants("name")
                             where (el.Parent.Parent.Attribute("data-format") != null && el.Parent.Parent.Attribute("data-format").Value.Trim().ToLower() == "docdb")
                             select el;

                foreach (var node in nodes)
                {
                    String text = node.Value.Trim();
                    value = value + Common.Index_Spliter + text;
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = value.Replace("|", " ");
                value = value.Replace("\n", " ");
                value = FormatUtil.Distinct(value);

            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献申请人出错:" + e.Message);
            }
            return value;
        }

        //获取专利文献申请人国别
        public String getAppliesCountry(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "parties").Descendants(exch + "applicants").Descendants(exch + "applicant").Descendants("residence").Descendants("country")
                             where (el.Parent.Parent.Attribute("data-format") != null && el.Parent.Parent.Attribute("data-format").Value.Trim().ToLower() == "docdb")
                             select el;

                if (nodes == null || nodes.Count() == 0)//如果是EPO的数据，申请人国别从申请人名字中取，例如 James [US]
                {
                    nodes =
                            from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "parties").Descendants(exch + "applicants").Descendants(exch + "applicant").Descendants(exch + "applicant-name").Descendants("name")
                            where (el.Parent.Parent.Attribute("data-format") != null && el.Parent.Parent.Attribute("data-format").Value.Trim().ToLower() == "docdb")
                            select el;
                }
                foreach (var node in nodes)
                {
                    String text = node.Value.Trim();
                    if (text.Contains("["))
                    {
                        text = text.Substring(text.IndexOf('[') + 1, text.IndexOf(']') - text.IndexOf('[') - 1).Trim();
                        value = value + Common.Index_Spliter + text;
                    }
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = value.Replace("|", " ");
                value = value.Replace("\n", " ");
                value = FormatUtil.Distinct(value);

            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献申请人国别出错:" + e.Message);
            }
            return value;
        }


        //获取专利文献优先权信息
        public String getPrior(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "priority-claims").Descendants(exch + "priority-claim").Descendants("document-id")
                             where (el.Parent.Attribute("data-format") != null && el.Parent.Attribute("data-format").Value.Trim().ToLower() == "docdb")
                             select el;

                foreach (var node in nodes)
                {
                    var country = node.Descendants("country");
                    var doc_number = node.Descendants("doc-number");
                    var kind = node.Descendants("kind");
                    if (country.Count() > 0)
                    {
                        value = value + Common.Index_Spliter + country.First().Value.Trim();
                    }
                    if (doc_number.Count() > 0)
                    {
                        if (country.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + doc_number.First().Value.Trim();
                        }
                        else
                        {
                            value = value + doc_number.First().Value.Trim();
                        }
                    }
                    if (kind.Count() > 0)
                    {
                        if (country.Count() == 0 && doc_number.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + kind.First().Value.Trim();
                        }
                        else
                        {
                            value = value + kind.First().Value.Trim();
                        }
                    }
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献优先权信息出错:" + e.Message);
            }
            return value;
        }

        //获取专利文献优先权信息
        public String getEpoPrior(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "priority-claims").Descendants(exch + "priority-claim").Descendants("document-id")
                             where (el.Parent.Attribute("data-format") != null && el.Parent.Attribute("data-format").Value.Trim().ToLower() == "epodoc")
                             select el;

                foreach (var node in nodes)
                {
                    var country = node.Descendants("country");
                    var doc_number = node.Descendants("doc-number");
                    var kind = node.Descendants("kind");
                    if (country.Count() > 0)
                    {
                        value = value + Common.Index_Spliter + country.First().Value.Trim();
                    }
                    if (doc_number.Count() > 0)
                    {
                        if (country.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + doc_number.First().Value.Trim();
                        }
                        else
                        {
                            value = value + doc_number.First().Value.Trim();
                        }
                    }
                    if (kind.Count() > 0)
                    {
                        if (country.Count() == 0 && doc_number.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + kind.First().Value.Trim();
                        }
                        else
                        {
                            value = value + kind.First().Value.Trim();
                        }
                    }
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献优先权信息出错:" + e.Message);
            }
            return value;
        }

        //获取专利文献优先权信息+优先权日
        public String getEpoPriorAndPrd(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "priority-claims").Descendants(exch + "priority-claim").Descendants("document-id")
                             where (el.Parent.Attribute("data-format") != null && el.Parent.Attribute("data-format").Value.Trim().ToLower() == "epodoc")
                             select el;

                foreach (var node in nodes)
                {
                    var country = node.Descendants("country");
                    var doc_number = node.Descendants("doc-number");
                    var kind = node.Descendants("kind");
                    var date = node.Descendants("date");

                    value = value + Common.Index_Spliter;

                    if (country.Count() > 0)
                    {
                        value = value + country.First().Value.Trim();
                    }
                    if (doc_number.Count() > 0)
                    {
                        value = value + doc_number.First().Value.Trim();
                    }
                    if (kind.Count() > 0)
                    {
                        value = value + kind.First().Value.Trim();
                    }

                    if (date.Count() > 0)
                    {
                        value = value + "   " + date.First().Value.Trim();
                    }
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献优先权信息出错:" + e.Message);
            }
            return value;
        }

        //获得专利发明人
        public String getInventors(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                               from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "parties").Descendants(exch + "inventors").Descendants(exch + "inventor").Descendants(exch + "inventor-name").Descendants("name")
                               where (el.Parent.Parent.Attribute("data-format") != null && el.Parent.Parent.Attribute("data-format").Value.Trim().ToLower() == "docdb")
                               select el;

                foreach (var node in nodes)
                {
                    String text = node.Value.Trim();
                    if (text.Contains("["))//如果是EPO的数据，则把名字后面的国别去掉
                    {
                        text = text.Substring(0, text.IndexOf('[')).Trim();
                    }
                    value = value + Common.Index_Spliter + text;
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = value.Replace("|", " ");
                value = value.Replace("\n", " ");
                value = FormatUtil.Distinct(value);

            }
            catch (Exception e)
            {
                throw new Exception("获得专利发明人出错:" + e.Message);
            }
            return value;
        }

        //获得专利发明人+[国别]
        public String getInventorsAndCC(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                               from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "parties").Descendants(exch + "inventors").Descendants(exch + "inventor").Descendants(exch + "inventor-name").Descendants("name")
                               where (el.Parent.Parent.Attribute("data-format") != null && el.Parent.Parent.Attribute("data-format").Value.Trim().ToLower() == "docdb")
                               select el;

                foreach (var node in nodes)
                {
                    String text = node.Value.Trim();
                    value = value + Common.Index_Spliter + text;
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = value.Replace("|", " ");
                value = value.Replace("\n", " ");
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利发明人出错:" + e.Message);
            }
            return value;
        }

        //获得专利发明人国别
        public String getInventorCountry(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                               from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "parties").Descendants(exch + "inventors").Descendants(exch + "inventor").Descendants("residence").Descendants("country")
                               where (el.Parent.Parent.Attribute("data-format") != null && el.Parent.Parent.Attribute("data-format").Value.Trim().ToLower() == "docdb")
                               select el;

                if (nodes == null || nodes.Count() == 0)//如果是EPO的数据，申请人国别从申请人名字中取，例如 James [US]
                {
                    nodes =
                               from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "parties").Descendants(exch + "inventors").Descendants(exch + "inventor").Descendants(exch + "inventor-name").Descendants("name")
                               where (el.Parent.Parent.Attribute("data-format") != null && el.Parent.Parent.Attribute("data-format").Value.Trim().ToLower() == "docdb")
                               select el;
                }

                foreach (var node in nodes)
                {
                    String text = node.Value.Trim();
                    if (text.Contains("["))
                    {
                        text = text.Substring(text.IndexOf('[') + 1, text.IndexOf(']') - text.IndexOf('[') - 1).Trim();
                        value = value + Common.Index_Spliter + text;
                    }

                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = value.Replace("|", " ");
                value = value.Replace("\n", " ");
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利发明人国别出错:" + e.Message);
            }
            return value;
        }

        //获得文献引用号，只取专利形式的引用文献，用公开号表示的，如果是EOP格式的公考号，则转化成docdb格式
        public String getCited(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                               from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "references-cited").Descendants(exch + "citation").Descendants("patcit").Descendants("document-id")
                               select el;
                foreach (var node in nodes)
                {
                    var country = node.Descendants("country");
                    var doc_number = node.Descendants("doc-number");
                    var kind = node.Descendants("kind");
                    if (country.Count() > 0)
                    {
                        value = value + Common.Index_Spliter + country.First().Value.Trim();
                    }
                    if (doc_number.Count() > 0)
                    {
                        String text = doc_number.First().Value.Trim();
                        if (text.Contains('['))
                        {
                            text = text.Substring(0, text.IndexOf('['));
                        }
                        text = text.Replace(" ", "");
                        if (StringUtil.StartWithString(text))//EPO格式的公开号要转成docdb格式的
                        {
                            text = FormatUtil.FormatPubNo(text);
                        }
                        if (country.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + text;
                        }
                        else
                        {
                            value = value + text;
                        }
                    }
                    if (kind.Count() > 0)
                    {
                        if (country.Count() == 0 && doc_number.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + kind.First().Value.Trim();
                        }
                        else
                        {
                            value = value + kind.First().Value.Trim();
                        }
                    }
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得文献引用号出错:" + e.Message);
            }
            return value;
        }

        public String getCited(XDocument xRoot, String srepPhase)
        {
            String value = String.Empty;
            try
            {
                var nodes = from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "references-cited").Descendants(exch + "citation").Descendants("patcit").Descendants("document-id")
                            where (el.Parent.Parent.Attribute("srep-phase") != null && el.Parent.Parent.Attribute("srep-phase").Value.Trim().ToUpper().Equals(srepPhase))
                            select el;
                foreach (var node in nodes)
                {
                    var country = node.Descendants("country");
                    var doc_number = node.Descendants("doc-number");
                    var kind = node.Descendants("kind");
                    var date = node.Descendants("date");
                    if (country.Count () > 0)
                    {
                        value = value + Common.Index_Spliter + country.First().Value.Trim();
                    }
                    if (doc_number.Count () > 0)
                    {
                        String text = doc_number.First().Value.Trim();
                        if (text.Contains('['))
                        {
                            text = text.Substring(0, text.IndexOf('['));
                        }
                        text = text.Replace(" ", "");
                        if (StringUtil.StartWithString(text))//EPO格式的公开号要转成docdb格式的
                        {
                            text = FormatUtil.FormatPubNo(text);
                        }
                        if (country.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + text;
                        }
                        else
                        {
                            value = value + text;
                        }
                    }
                    if (kind.Count() > 0)
                    {
                        if (country.Count() == 0 && doc_number.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + kind.First().Value.Trim();
                        }
                        else
                        {
                            value = value + kind.First().Value.Trim();
                        }
                    }
                    if (date.Count() > 0)
                    {
                        string strDate = date.First().Value.Trim();
                        strDate = strDate.Substring(0, 4) + "." + strDate.Substring(4, 2) + "." + strDate.Substring(6, 2);
                        if (country.Count() == 0 && doc_number.Count() == 0 && kind.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + strDate;
                        }
                        else
                        {
                            value = value + ", " + strDate;
                        }
                    }
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得文献引用号出错:" + e.Message);
            }
            return value;
        }

        public String getCitedNotContain(XDocument xRoot, String srepPhase)
        {
            String value = String.Empty;
            try
            {
                var nodes = from el in xRoot.Descendants(exch + "bibliographic-data").Descendants(exch + "references-cited").Descendants(exch + "citation").Descendants("patcit").Descendants("document-id")
                            where (el.Parent.Parent.Attribute("srep-phase") != null && !el.Parent.Parent.Attribute("srep-phase").Value.Trim().ToUpper().Equals(srepPhase))
                            select el;
                foreach (var node in nodes)
                {
                    var country = node.Descendants("country");
                    var doc_number = node.Descendants("doc-number");
                    var kind = node.Descendants("kind");
                    var date = node.Descendants("date");
                    if (country.Count () > 0)
                    {
                        value = value + Common.Index_Spliter + country.First().Value.Trim();
                    }
                    if (doc_number.Count() > 0)
                    {
                        String text = doc_number.First().Value.Trim();
                        if (text.Contains('['))
                        {
                            text = text.Substring(0, text.IndexOf('['));
                        }
                        text = text.Replace(" ", "");
                        if (StringUtil.StartWithString(text))//EPO格式的公开号要转成docdb格式的
                        {
                            text = FormatUtil.FormatPubNo(text);
                        }
                        if (country.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + text;
                        }
                        else
                        {
                            value = value + text;
                        }
                    }
                    if (kind.Count() > 0)
                    {
                        if (country.Count() == 0 && doc_number.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + kind.First().Value.Trim();
                        }
                        else
                        {
                            value = value + kind.First().Value.Trim();
                        }
                    }
                    if (date.Count() > 0)
                    {
                        string strDate = date.First().Value.Trim();
                        strDate = strDate.Substring(0, 4) + "." + strDate.Substring(4, 2) + "." + strDate.Substring (6, 2);
                        if (country.Count() == 0 && doc_number.Count() == 0 && kind.Count() == 0)
                        {
                            value = value + Common.Index_Spliter + strDate;
                        }
                        else
                        {
                            value = value + ", " + strDate;
                        }
                    }
                }
                value = StringUtil.RemoveFirstSpliter(value);
                value = FormatUtil.Distinct(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得文献引用号出错:" + e.Message);
            }
            return value;
        }

        /// <summary>
        /// 提取摘要信息
        /// </summary>
        /// <param name="xRoot"></param>
        /// <returns>发明名称|摘要</returns>
        public String AbstractExtract(XDocument xRoot, String[] pubIDs)
        {
            String Data_Index_abs = String.Empty;
            try
            {
                String inventionTitle = getInventionTitle(xRoot).Trim();
                String abs = getAbstract(xRoot, pubIDs).Trim();
                Data_Index_abs = inventionTitle + "|" + abs;
                Data_Index_abs = Data_Index_abs.Replace('\r', ' ');
                Data_Index_abs = Data_Index_abs.Replace('\n', ' ');
                return Data_Index_abs;
            }
            catch (Exception e)
            {
                throw new Exception("提取摘要信息出错：" + e.Message);
            }
        }

        //提取内容信息
        //申请号|申请日|公开公告日|IPC号（第八版）| ECLA |申请人|发明人| 引用文献号|申请人国别|发明人国别
        public String ContentExtract(XDocument xRoot)
        {
            String Data_Index_Content = String.Empty;
            try
            {
                string DocdbPN = getDocdbPublicDocNo(xRoot);
                string EpoPN = getEpoPublicDocNo(xRoot);
                string OriginalPN = getOriginalPublicDocNo(xRoot);
                if (DocdbPN == "")
                {
                    DocdbPN = getPublicDocNo(xRoot);
                }
                String PN = string.Format("{0};{1};{2}", DocdbPN, EpoPN, OriginalPN);
                PN = PN.Replace(";;", ";").Trim(';');

                ///////---------------
                String applyNo = getEpoApplyNo(xRoot).Trim();    //申请号
                string strDocDbAN = getDocdbApplyNo(xRoot).Trim();
                string strOriginalAN = getOriginalApplyNo(xRoot).Trim();
                string AN = string.Format("{0};{1};{2}", applyNo, strDocDbAN, strOriginalAN);
                AN = AN.Replace(";;", ";").Trim(';');


                String applyDate = getApplyDate(xRoot).Trim();
                String publicDate = getPublicDate(xRoot).Trim();
                String IPC = getIPC(xRoot).Trim();
                String ECLA = getECLA(xRoot).Trim();
                String applies = getApplies(xRoot).Trim();
                String inventors = getInventors(xRoot).Trim();
                String cites = getCited(xRoot).Trim();
                String applyCountry = getAppliesCountry(xRoot);
                String inventorCountry = getInventorCountry(xRoot);

                //Data_Index_Content = applyNo + "|" + applyDate + "|" + publicDate + "|" + IPC + "|" + ECLA + "|" + applies + "|" + inventors + "|" + cites + "|" + applyCountry + "|" + inventorCountry;

                Data_Index_Content = PN + "|" + AN + "|" + applyDate + "|" + publicDate + "|" + IPC + "|" + ECLA + "|" + applies + "|" + inventors + "|" + cites + "|" + applyCountry + "|" + inventorCountry;


                Data_Index_Content = Data_Index_Content.Replace('\r', ' ');
                Data_Index_Content = Data_Index_Content.Replace('\n', ' ');
                return Data_Index_Content;
            }
            catch (Exception e)
            {
                throw new Exception("提取内容信息出错：" + e.Message);
            }
        }

        //提取优先权信息
        //优先权号
        public String PrioExtract(XDocument xRoot)
        {
            String Data_Index_Prio = String.Empty;
            try
            {
                String prior = getEpoPrior(xRoot).Trim();
                Data_Index_Prio = prior;
                Data_Index_Prio = Data_Index_Prio.Replace('\r', ' ');
                Data_Index_Prio = Data_Index_Prio.Replace('\n', ' ');
                return Data_Index_Prio;
            }
            catch (Exception e)
            {
                throw new Exception("提取优先权索引信息出错：" + e.Message);
            }
        }


        //static void Main(string[] args)
        //{
        //    DocDBIndexExtract docDBIndexExtract = new DocDBIndexExtract(@"F://docdb-entities.dtd");
        //    docDBIndexExtract.PropertyData_Index_abs_path = @"F://CPRS//Data_Index_abs.txt";
        //    docDBIndexExtract.PropertyData_Index_Content_path = @"F://CPRS//Data_Index_content.txt";
        //    docDBIndexExtract.PropertyData_Index_Prio_path = @"F://CPRS//Data_Index_prior.txt";
        //    //docDBIndexExtract.Test();
        //    for (int i = 0; i < 120000; i++)
        //    {
        //        docDBIndexExtract.PropertyXMLPath = @"F://AP200503209D0.xml";
        //        docDBIndexExtract.InformationExtract();
        //    }
        //}
    }
}
