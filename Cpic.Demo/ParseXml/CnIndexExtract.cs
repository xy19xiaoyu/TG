using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace ParseXml
{
    public class CnIndexExtract
    {
        //获得专利文献申请号
        public String getApplyNo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                if (xRoot.Root.Name == "Patent")
                {
                    var nodes =
                              from el in xRoot.Descendants("APNNO")
                              select el;
                    value = nodes.First().Value.Trim();
                }
                else
                {
                    var nodes =
                              from el in xRoot.Descendants("doc-filing_no")
                              select el;
                    value = nodes.First().Value.Trim().Substring(0, 12);
                }
                if (value == String.Empty)
                {
                    throw new Exception("获得专利文献申请号出错");
                }
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献申请号出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利文献申请日
        public String getApplyDate(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("APD")
                         select el;
                foreach (var node in nodes)
                {
                    String ApplyDate = node.Value.Trim();
                    if (ApplyDate != "")
                    {
                        value = value + ";" + ApplyDate;
                    }
                }
                value = RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献申请日出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利文献联系地址
        public String getAddress(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("ADDRESS")
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
                throw new Exception("获得专利文献联系地址出错:" + e.Message);
            }
            return value.Trim();
        }

        ////获得专利文献公开公告号
        //public String getPublicAnnouncementNo(XDocument xRoot)
        //{
        //    String value = String.Empty;
        //    try
        //    {
        //        var PUBNR =
        //                 from el in xRoot.Descendants("PUBNR")//公开号
        //                 select el;
        //        var APPNR =
        //                 from el in xRoot.Descendants("APPNR")//公告号
        //                 select el;
        //        if (PUBNR.Count() > 0)
        //        {
        //            String pubno = PUBNR.First().Value.Trim();
        //            if (pubno!="0000000")
        //            {
        //                value = pubno;
        //            }
        //        }
        //        if (APPNR.Count() > 0)
        //        {
        //            String appno = APPNR.First().Value.Trim();
        //            if (appno != "0000000")
        //            {
        //                if (value == String.Empty)
        //                {
        //                    value = appno;
        //                }
        //                else
        //                {
        //                    value = value + ";" + appno;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("获得专利文献公开号出错:" + e.Message);
        //    }
        //    return value.Trim();
        //}


        //获得专利文献公开号
        public String getPublicNo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var PUBNR =
                         from el in xRoot.Descendants("PUBNR")//公开号
                         select el;

                if (PUBNR.Count() > 0)
                {
                    String pubno = PUBNR.First().Value.Trim();
                    if (!Regex.IsMatch(pubno, @"^0{1,}$"))
                    {
                        value = pubno;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献公开号出错:" + e.Message);
            }
            return value.Trim();
        }


        //获得专利文献公告号
        public String getAnnouncementNo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var APPNR =
                         from el in xRoot.Descendants("APPNR")//公告号
                         select el;
                if (APPNR.Count() > 0)
                {
                    String ann = APPNR.First().Value.Trim();
                    if (!Regex.IsMatch(ann, @"^0{1,}$"))
                    {
                        value = ann;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献公告号出错:" + e.Message);
            }
            return value.Trim();
        }




        ////获得专利文献公开公告日
        //public String getPublicAnnouncementDate(XDocument xRoot)
        //{
        //    String value = String.Empty;
        //    try
        //    {
        //        String publicDate = String.Empty;
        //        var PUD =
        //                 from el in xRoot.Descendants("PUD")//公开日
        //                 select el;
        //        var APPD =
        //                 from el in xRoot.Descendants("APPD")//公告日
        //                 select el;
        //        if (PUD.Count() > 0)
        //        {
        //            publicDate = PUD.First().Value.Trim();
        //            if (publicDate != "")
        //            {
        //                publicDate = FormatUtil.FormatDate(publicDate);
        //                value = publicDate;
        //            }
        //        }
        //        if (APPD.Count() > 0)
        //        {
        //            String announceDate = APPD.First().Value.Trim();
        //            if (announceDate != "")
        //            {
        //                announceDate = FormatUtil.FormatDate(announceDate);
        //                if (value == String.Empty)
        //                {
        //                    value = announceDate;
        //                }
        //                else
        //                {
        //                    value = value + ";" + announceDate;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("获得专利文献公开公告日出错:" + e.Message);
        //    }
        //    return value.Trim();
        //}



        //获得专利文献公开日
        public String getPublicDate(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                String publicDate = String.Empty;
                var PUD =
                         from el in xRoot.Descendants("PUD")//公开日
                         select el;

                if (PUD.Count() > 0)
                {
                    publicDate = PUD.First().Value.Trim();
                    if (publicDate != "")
                    {
                        value = publicDate;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献公开日出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利文献公告日
        public String getAnnouncementDate(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                String annDate = String.Empty;

                var APPD =
                         from el in xRoot.Descendants("APPD")//公告日
                         select el;
                if (APPD.Count() > 0)
                {
                    annDate = APPD.First().Value.Trim();
                    if (annDate != "")
                    {
                        value = annDate;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献公告日出错:" + e.Message);
            }
            return value.Trim();
        }


        //获得专利文献优先权号
        public String getPriorNo(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("PRI")
                         select el;
                foreach (var node in nodes)
                {
                    var country = node.Descendants("CO");
                    var no = node.Descendants("NR");
                    if (country.Count() > 0)
                    {
                        value = value + ";" + country.First().Value.Trim();
                    }
                    if (no.Count() > 0)
                    {
                        if (country.Count() == 0)
                        {
                            value = value + ";" + no.First().Value.Trim();
                        }
                        else
                        {
                            value = value + no.First().Value.Trim();
                        }
                    }
                }
                value = RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献优先权号出错:" + e.Message);
            }
            return value.Trim();
        }



        //获得专利文献优先权号
        public String getPriorNo1(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("PRI")
                         select el;
                foreach (var node in nodes)
                {
                    var country = node.Descendants("CO");
                    var no = node.Descendants("NR");
                    var date = node.Descendants("DATE");
                    if (country.Count() > 0)
                    {
                        value = value + ";" + country.First().Value.Trim();
                    }

                    if (date.Count() > 0)
                    {
                        if (country.Count() == 0)
                        {
                            value = value + ";" + date.First().Value.Trim();
                        }
                        else
                        {
                            value = value + " " +date.First().Value.Trim();
                        }
                    }

                    if (no.Count() > 0)
                    {
                        value = value + " [" + no.First().Value.Trim() + "]";
                    }
                }
                value = RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献优先权号出错:" + e.Message);
            }
            return value.Trim();
        }

        /// <summary>
        /// PCT进入国家日期
        /// </summary>
        /// <param name="xRoot"></param>
        /// <returns></returns>
        public String getPCT_PstDA(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                       from el in xRoot.Descendants("PCT")
                       select el;
                foreach (var node in nodes)
                {
                    var PSTDA = node.Descendants("PSTDA");

                    if (PSTDA.Count() > 0)
                    {
                        value =  PSTDA.First().Value.Trim();
                    }
                }
                value = RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("PCT进入国家日期:" + e.Message);
            }
            return value.Trim();
        }

        /// <summary>
        /// PCT国际申请日
        /// </summary>
        /// <param name="xRoot"></param>
        /// <returns></returns>
        public String getPCT_PCTDA(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                       from el in xRoot.Descendants("PCT")
                       select el;
                foreach (var node in nodes)
                {
                    var PSTDA = node.Descendants("PCTDA");

                    if (PSTDA.Count() > 0)
                    {
                        value = PSTDA.First().Value.Trim();
                    }
                }
                value = RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("PCT国际申请日:" + e.Message);
            }
            return value.Trim();
        }

        /// <summary>
        /// PCT国际申请号
        /// </summary>
        /// <param name="xRoot"></param>
        /// <returns></returns>
        public String getPCT_PCTNO(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                       from el in xRoot.Descendants("PCT")
                       select el;
                foreach (var node in nodes)
                {
                    var PSTDA = node.Descendants("PCTNO");

                    if (PSTDA.Count() > 0)
                    {
                        value = PSTDA.First().Value.Trim();
                    }
                }
                value = RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("PCT国际申请:" + e.Message);
            }
            return value.Trim();
        }

        /// <summary>
        /// PCT国际公布号
        /// </summary>
        /// <param name="xRoot"></param>
        /// <returns></returns>
        public String getPCT_PPBDO(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                       from el in xRoot.Descendants("PCT")
                       select el;
                foreach (var node in nodes)
                {
                    var PSTDA = node.Descendants("PPBDO");

                    if (PSTDA.Count() > 0)
                    {
                        value = PSTDA.First().Value.Trim();
                    }
                }
                value = RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("PCT国际公布号:" + e.Message);
            }
            return value.Trim();
        }

        /// <summary>
        /// PCT国际公布日
        /// </summary>
        /// <param name="xRoot"></param>
        /// <returns></returns>
        public String getPCT_PPBDA(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                       from el in xRoot.Descendants("PCT")
                       select el;
                foreach (var node in nodes)
                {
                    var PSTDA = node.Descendants("PPBDA");

                    if (PSTDA.Count() > 0)
                    {
                        value = PSTDA.First().Value.Trim();
                    }
                }
                value = RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("PCT国际公布日:" + e.Message);
            }
            return value.Trim();
        }

        /// <summary>
        /// PCT公布语言
        /// </summary>
        /// <param name="xRoot"></param>
        /// <returns></returns>
        public String getPCT_PLANG(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                       from el in xRoot.Descendants("PCT")
                       select el;
                foreach (var node in nodes)
                {
                    var PSTDA = node.Descendants("PLANG");

                    if (PSTDA.Count() > 0)
                    {
                        value = PSTDA.First().Value.Trim();
                    }
                }
                value = RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("PCT公布语言:" + e.Message);
            }
            return value.Trim();
        }


        //获得专利文献主权利要求
        public String getMainClaim(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("CLAIM")
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
                throw new Exception("获得专利文献主权利要求出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利文献权利要求
        public String getClaims(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("claims").Descendants("claim").Descendants("claim-text")
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
                throw new Exception("获得专利文献权利要求出错:" + e.Message);
            }
            return value.Trim();
        }
        //获得专利文献说明书
        public String getDescription(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("description").Descendants("p")
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
                throw new Exception("获得专利文献说明书出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得邮政编码
        public String getZip(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                       from el in xRoot.Descendants("ZIP")
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
                throw new Exception("获得邮政编码出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得授权日
        public String getGrantDate(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                       from el in xRoot.Descendants("GRD")
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
                throw new Exception("获得授权日出错:" + e.Message);
            }
            return value.Trim();
        }
        //获得授权公告日
        public String getGrantPubDate(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                       from el in xRoot.Descendants("GRPD")
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
                throw new Exception("获得授权公告日出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得代理机构代码
        public String getAgentCode(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                       from el in xRoot.Descendants("AGENCY")
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
                throw new Exception("获得代理机构代码出错:" + e.Message);
            }
            return value.Trim();
        }

        /// <summary>
        /// 代理人信息
        /// </summary>
        /// <param name="xRoot"></param>
        /// <returns></returns>
        public String getAgents(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                       from el in xRoot.Descendants("AGENT")
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
                throw new Exception("获得代理人出错:" + e.Message);
            }
            return value.Trim();
        }




        //获得专利文献申请人
        public String getApply(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("APPL")
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
                throw new Exception("获得专利文献申请人出错:" + e.Message);
            }
            return value.Trim();
        }
        //获得专利文献发明人
        public String getInventor(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("INVENTOR")
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
                throw new Exception("获得专利文献发明人出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利文献关键字
        public String getKeyWord(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("KEYWORD")
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
                throw new Exception("获得专利文献关键字出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利文献主IPC
        public String getMainIPC(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("IPC")
                         select el;
                if (nodes.Count() > 0)
                {
                    value = nodes.First().Value;
                }
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献主IPC出错:" + e.Message);
            }
            value = value.PadRight(12, ' ');
            return value.Trim();
        }
        //获得专利文献IPC
        public String getIPC(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("IPC")
                         select el;
                foreach (var node in nodes)
                {
                    String ipc = node.Value.Trim();
                    value = value + ";" + ipc;
                }
                value = RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献IPC出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利文献发明名称
        public String getTitle(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("TITLE")
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

        //获得专利文献代理机构代码
        public String getAgency(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("AGENCY")
                         select el;
                foreach (var node in nodes)
                {
                    value = value + ";" + node.Value.Trim();
                }
                value = RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献代理机构代码出错:" + e.Message);
            }
            return value.Trim();
        }


        //获得专利文献范畴分类号
        public String getField(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("FIELDC")
                         select el;
                foreach (var node in nodes)
                {
                    value = value + ";" + node.Value.Trim();
                }
                value = RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献范畴分类号出错:" + e.Message);
            }
            return value.Trim();
        }

        //获得专利文献省市代码
        public String getProvinceCode(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                var nodes =
                         from el in xRoot.Descendants("NC")
                         select el;
                foreach (var node in nodes)
                {
                    value = value + ";" + node.Value.Trim();
                }
                value = RemoveFirstSpliter(value);
            }
            catch (Exception e)
            {
                throw new Exception("获得专利文献省市代码出错:" + e.Message);
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
                         from el in xRoot.Descendants("ABSTR")
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
                throw new Exception("获得专利文献摘要出错:" + e.Message);
            }
            return value.Trim();
        }


       

      


        /// <summary>
        /// 获得中文全文索引信息（nData_Index_Full1_YYMMDD）
        /// </summary>
        /// <param name="xRoot"></param>
        /// <returns>权利要求</returns>
        public String getFullIndex_Claim(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                String claim = getClaims(xRoot);
                claim = claim.Replace("\r", " ");
                claim = claim.Replace("\n", " ");
                return claim;
            }
            catch (Exception e)
            {
                throw new Exception("获得中文全文索引信息（nData_Index_Full1_YYMMDD）出错:" + e.Message);
            }
        }
        /// <summary>
        /// 获得中文全文索引信息（nData_Index_Full2_YYMMDD）
        /// </summary>
        /// <param name="xRoot"></param>
        /// <returns>说明书</returns>
        public String getFullIndex_Description(XDocument xRoot)
        {
            String value = String.Empty;
            try
            {
                String des = getDescription(xRoot);
                des = des.Replace("\r", " ");
                des = des.Replace("\n", " ");
                return des;
            }
            catch (Exception e)
            {
                throw new Exception("获得中文全文索引信息（nData_Index_Full2_YYMMDD）出错:" + e.Message);
            }
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
    }
}
