using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ParseXml
{
    public class DwpiIndexExtract
    {
        private XNamespace defaultNSP = "http://schemas.thomson.com/ts/20041221/tsip";
        private XNamespace dwpi = "http://schemas.thomson.com/ts/20041221/dwpi";
        private XNamespace tsip = "http://schemas.thomson.com/ts/20041221/tsip";
        private XNamespace wila = "http://schemas.thomson.com/ts/20041221/wila";
        private XNamespace xhtml = "http://www.w3.org/1999/xhtml";

        //获得专利文献发明名称
        public String getInventionTitle(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "titleEnhanced").Descendants(defaultNSP + "titleAscii").Descendants(defaultNSP + "titlePartAscii")
                             select el;

                foreach (var node in nodes)
                {
                    value = value + ";" + node.Value.Trim();
                }
                value = RemoveFirstSpliter(value);
                value = value.Replace("|", " ");
                value = value.Replace("\n", " ");
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献发明名称出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利文献申请号（countryCode+applicationYear+number 例子：JP19930345308）
        public String getApplyNo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "applications").Descendants(defaultNSP + "application").Descendants(defaultNSP + "applicationId")
                             select el;
                foreach (var node in nodes)
                {
                    bool exit_dwpi = true;
                    var number =
                                    from el in node.Descendants(defaultNSP + "number")
                                    where el.Attribute(tsip + "form").Value.Trim().ToLower() == "dwpi"
                                    select el;
                    if (number.Count() == 0)//如果没有dwpi格式的number，则取tis格式p的number
                    {
                        exit_dwpi = false;
                        number =
                                    from el in node.Descendants(defaultNSP + "number")
                                    where el.Attribute(tsip + "form").Value.Trim().ToLower() == "tsip"
                                    select el;
                    }
                    var countryCode = node.Descendants(defaultNSP + "countryCode");
                    var applicationYear = node.Descendants(defaultNSP + "applicationYear");
                    if (countryCode.Count() > 0)
                    {
                        value = value + ";" + countryCode.First().Value.Trim();
                    }
                    if (applicationYear.Count() > 0)
                    {
                        if (countryCode.Count() == 0)
                        {
                            value = value + ";" + applicationYear.First().Value.Trim();
                        }
                        else
                        {
                            value = value + applicationYear.First().Value.Trim();
                        }
                    }
                    if (number.Count() > 0)
                    {
                        if (exit_dwpi)//含有dwpi格式的number
                        {
                            if (applicationYear.Count() == 0 && countryCode.Count() == 0)
                            {
                                value = value + ";" + number.First().Value.Trim();
                            }
                            else
                            {
                                value = value + number.First().Value.Trim();
                            }
                        }
                        else//不含dwpi格式的number，则取tisp格式的number
                        {
                            if (applicationYear.Count() == 0 && countryCode.Count() == 0)
                            {
                                value = value + ";" + DwpiFormatAppno(number.First().Value.Trim());
                            }
                            else
                            {
                                value = value + DwpiFormatAppno(number.First().Value.Trim());
                            }
                        }
                    }
                }
                value =  RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献申请号出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利文献摘要
        public String getAbstract(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                        from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "abstractEnhanced").Descendants(defaultNSP + "abstractCoreAscii")
                        select el;
                if (nodes == null || nodes.Count() == 0)
                {
                    nodes =
                             from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "abstractEnhanced").Descendants(defaultNSP + "abstractTechFocusAscii")
                             select el;
                }
                if (nodes == null || nodes.Count() == 0)
                {
                    nodes =
                             from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "abstractEnhanced").Descendants(defaultNSP + "abstractExtensionAscii")
                             select el;
                }

                foreach (var node in nodes)
                {
                    value = value + ";" + node.Value.Trim();
                }
                value =  RemoveFirstSpliter(value);
                value = value.Replace("|", " ");
                value = value.Replace("\n", " ");
            }
            catch (Exception e)
            {
                throw new Exception("获取专利文献摘要出错:" + e.Message);
            }

            return value.Trim(); ;
        }

        //获得专利文献入藏号
        public String getAccession(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "accessions").Descendants(defaultNSP + "accession")
                         where el.Attribute(tsip + "type").Value == "pan"
                         select el;
                foreach (var node in nodes)
                {
                    value = value + ";" + node.Value.Trim();
                }
                value =  RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献入藏号出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利文献申请人
        public String getAssignees(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "assignees").Descendants(defaultNSP + "assignee").Descendants(defaultNSP + "assigneeTotal")
                             where el.Attribute(tsip + "form").Value == "dwpi"
                             select el;

                foreach (var node in nodes)
                {
                    value = value + ";" + node.Value.Trim();
                }
                value =  RemoveFirstSpliter(value);
                value = value.Replace("|", " ");
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献申请人出错:" + e.Message);
            }

            return value.Trim();
        }

        //获得专利文献公告号（样例：JP7173670）
        public String getPublicNo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "publications").Descendants(defaultNSP + "publication").Descendants(defaultNSP + "documentId")
                             select el;

                foreach (var node in nodes)
                {
                    var number =
                                    from el in node.Descendants(defaultNSP + "number")
                                    where el.Attribute(tsip + "form").Value == "dwpi"
                                    select el;
                    var countryCode = node.Descendants(defaultNSP + "countryCode");
                    var kind = node.Descendants(defaultNSP + "kindCode");

                    if (countryCode.Count() > 0)
                    {
                        value = value + ";" + countryCode.First().Value.Trim();
                    }
                    if (number.Count() > 0)
                    {
                        if (countryCode.Count() == 0)
                        {
                            value = value + ";" + number.First().Value.Trim();
                        }
                        else
                        {
                            value = value + number.First().Value.Trim();
                        }
                    }
                    if (kind.Count() > 0)
                    {
                        if (countryCode.Count() == 0 && number.Count() == 0)
                        {
                            value = value + ";" + kind.First().Value.Trim();
                        }
                        else
                        {
                            value = value + kind.First().Value.Trim();
                        }
                    }
                }
                value =  RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献公告号出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利文献IPC号
        public String getIPC(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "classificationIpcCurrent").Descendants(defaultNSP + "ipc")
                             select el;

                foreach (var node in nodes)
                {
                    String ipc = node.Value.Trim();
                    value = value + ";" + ipc;
                }
                value =  RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献IPC号出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利文献德温特分类号
        public String getDWPINo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var ChemicalNodes =
                        from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "classificationDwpi").Descendants(defaultNSP + "classesChemical").Descendants(defaultNSP + "classChemical")
                        select el;
                var EngineeringNodes =
                            from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "classificationDwpi").Descendants(defaultNSP + "classesEngineering").Descendants(defaultNSP + "classEngineering")
                            select el;
                foreach (var node in ChemicalNodes)
                {
                    value = value + ";" + node.Value.Trim();
                }
                foreach (var node in EngineeringNodes)
                {
                    value = value + ";" + node.Value.Trim();
                }
                value =  RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献德温特分类号出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利文献优先权号(申请号)（JP19930345308）
        public String getPriorities(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "priorities").Descendants(defaultNSP + "priority").Descendants(defaultNSP + "applicationId")
                             select el;

                foreach (var node in nodes)
                {
                    var number =
                                    from el in node.Descendants(defaultNSP + "number")
                                    where el.Attribute(tsip + "form").Value == "dwpi"
                                    select el;
                    var countryCode = node.Descendants(defaultNSP + "countryCode");
                    var applicationYear = node.Descendants(defaultNSP + "applicationYear");

                    if (countryCode.Count() > 0)
                    {
                        value = value + ";" + countryCode.First().Value.Trim();
                    }
                    if (applicationYear.Count() > 0)
                    {
                        if (countryCode.Count() == 0)
                        {
                            value = value + ";" + applicationYear.First().Value.Trim();
                        }
                        else
                        {
                            value = value + applicationYear.First().Value.Trim();
                        }
                    }
                    if (number.Count() > 0)
                    {
                        if (countryCode.Count() == 0 && applicationYear.Count() == 0)
                        {
                            value = value + ";" + number.First().Value.Trim();
                        }
                        else
                        {
                            value = value + number.First().Value.Trim();
                        }
                    }
                }
                value =  RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献优先权号出错:" + e.Message);
            }
            return value.Trim();
        }

        public String getManualCode(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                String Chemical = "";
                var nodes =
                             from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "manualCodesChemical")
                             select el;

                foreach (var node in nodes)
                {
                    Chemical = Chemical +"  "+ node.Value.Trim();
                }
                if (Chemical.Trim() != "")
                {
                    Chemical = "Chemical:" + Chemical;
                }

                String Engineering = "";
                nodes =
                             from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "manualCodesEngineering")
                             select el;

                foreach (var node in nodes)
                {
                    Engineering = Engineering + "  " + node.Value.Trim();
                }
                if (Engineering.Trim() != "")
                {
                    Engineering = "Engineering:" + Chemical;
                }

                String Electrical = "";
                nodes =
                             from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "manualCodesElectrical")
                             select el;

                foreach (var node in nodes)
                {
                    Electrical = Electrical + "  " + node.Value.Trim();
                }
                if (Electrical.Trim() != "")
                {
                    Electrical = "Electrical:" + Electrical;
                }
                value = Chemical + Electrical + Engineering;
            }
            catch (Exception e)
            {
                throw new Exception("获得手工代码出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利发明人
        public String getInventors(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                             from el in xRoot.Descendants(defaultNSP + "invention").Descendants(defaultNSP + "inventors").Descendants(defaultNSP + "inventor").Descendants(defaultNSP + "inventorTotal")
                             where el.Attribute(tsip + "form").Value == "dwpi"
                             select el;

                foreach (var node in nodes)
                {
                    value = value + ";" + node.Value.Trim();
                }
                value =  RemoveFirstSpliter(value);
                value = value.Replace("|", " ");
            }
            catch (Exception e)
            {
                throw new Exception("获得专利发明人出错:" + e.Message);
            }
            return value.Trim();
        }

        //去掉字符串的第一个分隔符
        public String RemoveFirstSpliter(String str)
        {
            if (str.Trim() == "" || str == String.Empty)
            {
                return str;
            }
            if (str.StartsWith(";"))
            {
                return str.Substring(1);
            }
            else
            {
                throw new Exception(str + "第一个字符不是分隔符");
            }
        }

       

        //格式化dwpi中tisp格式的申请号
        public  String DwpiFormatAppno(String appno)
        {
            try
            {
                if (appno == null)
                {
                    throw new Exception("申请号为null");
                }
                else
                {
                    if (appno.Length <= 7)//申请号长度小于7，直接返回
                    {
                        return appno;
                    }
                    else
                    {
                        int pos = appno.LastIndexOf('0');
                        if (appno.Length - pos - 1 >= 7)//最后7位不包含0,则返回最后7位
                        {
                            return appno.Substring(appno.Length - 7);
                        }
                        else
                        {
                            String tail = appno.Substring(pos + 1);//申请号中最后一个0之后的字符串
                            String head = appno.Substring(0, 7 - tail.Length);//申请号中前7 - tail.Length个字符
                            return (head + tail);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("格式化申请号：" + appno + "出错:" + e.Message);
            }
        }
    }
}
