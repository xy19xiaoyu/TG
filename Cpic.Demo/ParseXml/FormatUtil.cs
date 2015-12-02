using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using System.Collections;

namespace Util
{
    public class FormatUtil
    {
        //去掉公开号最后的双字母 如DE69902957DD1 格式化为DE69902957D1， 这是因为xml文件路径是按照单字母存放的
        public static String FormatPubNo(String PubNo)
        {
            if (PubNo.Length < 3)
            {
                return PubNo;
            }
            String kind = PubNo.Substring(PubNo.Length - 3, 2);
            try
            {
                Convert.ToInt32(PubNo.Substring(PubNo.Length - 1));
            }
            catch (Exception ex)
            {
                kind = PubNo.Substring(PubNo.Length - 2);
            }
            if (kind[0] == kind[1])
            {
                char ch = kind[0];
                int pos = PubNo.LastIndexOf(ch);
                PubNo = PubNo.Substring(0, pos) + PubNo.Substring(pos + 1);
            }
            return PubNo;
        }
        //格式化IPC，去掉空格，补0变成11位
        public static String FormatIPC(String ipc)
        {
            //新数据IPC加入版本号H04L  1/18(2006.01)
            if (ipc.Contains('('))
            {
                ipc = ipc.Substring(0, ipc.IndexOf('(')).Trim();
            }

            String tem = ipc.Replace("-", "");
            String[] split = tem.Split('/');
            if (split.Length == 2)//如果IPC中包含/
            {
                String start4 = split[0].Substring(0, 4);//开始4位不变
                String mid3 = split[0].Substring(4).Trim().PadLeft(3, '0');//中间不足3位则左补0
                String last4 = split[1].Trim().PadRight(4, '0');//最后不足4为则右补0
                return start4 + mid3 + last4;
            }
            else
            {
                return tem.PadRight(11, '0');
            }
        }

        //格式化ECLA，
        public static String FormatECLA(String ECLA)
        {
            return ECLA.Replace(' ', '0');
        }

        //返回IPC中/前的所有字符串加/后的4个字符
        public static String DocDbFormatIPC(String ipc)
        {
            if (ipc.Trim() == "")
            {
                return ipc.Trim();
            }
            else if (!ipc.Contains('/'))
            {
                return ipc.Split(' ')[0].Trim();
            }
            else
            {
                String[] tem = ipc.Split('/');
                String result = ipc.Split('/')[0].Trim() + "/";
                if (tem[1].Length > 4)//只取/后的4个字符
                {
                    result = result + tem[1].Substring(0, 4).Trim();
                }
                else
                {
                    result = result + tem[1].Trim();
                }
                return result;
            }
        }


        //格式化范畴分类号。每3位为一个范畴分类号，中间用；间隔
        public static String FormatField(String field)
        {
            StringBuilder fields = new StringBuilder();
            for (int i = 0; i < field.Length; i++)
            {
                fields.Append(field[i]);
                if ((i + 1) % 3 == 0 && i != field.Length - 1)
                {
                    fields.Append(Common.Index_Spliter);
                }
            }
            return fields.ToString();
        }

        //取ipc的80个字符
        public static String PartIPC(String ipc)
        {
            try
            {
                String tem_ipc = String.Empty;
                if (ipc.Length >= 81)//ipc最后取80个字符
                {
                    if (ipc[80].ToString() == Common.Index_Spliter)//如果第81个字符是分隔符,则取前80个字符
                    {
                        tem_ipc = ipc.Substring(0, 80);
                    }
                    else//取前81个字符中完整的ipc
                    {
                        String[] tem = ipc.Substring(0, 81).Split(Common.Index_Spliter[0]);
                        for (int i = 0; i < tem.Length - 1; i++)
                        {
                            if (i == tem.Length - 2)
                            {
                                tem_ipc = tem_ipc + tem[i];
                            }
                            else
                            {
                                tem_ipc = tem_ipc + tem[i] + ";";
                            }
                        }
                    }
                }
                else
                {
                    tem_ipc = ipc;
                }
                return tem_ipc;
            }
            catch (Exception e)
            {
                throw new Exception("取IPC的前80个字符出错" + e.Message);
            }
        }



        //DWPI格式化IPC,去掉空格和-
        public static String FormatIPC_RemoveSpace(String ipc)
        {
            char[] ch = ipc.ToCharArray();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ch.Length; i++)
            {
                if (ch[i] != '-' && ch[i] != ' ')
                {
                    sb.Append(ch[i]);
                }
            }
            return sb.ToString();
        }


        //dwpi的申请号取500个字符
        public static String FormatApno_dwpi(String apno)
        {
            if (apno.Length <= 500)
            {
                return apno;
            }
            else
            {
                String tem = apno.Substring(0, 500);
                tem = tem.Substring(0, tem.LastIndexOf(';'));
                return tem;
            }
        }

        //dwpi的公开号取500个字符
        public static String FormatPubno_dwpi(String pubno)
        {
            if (pubno.Length <= 500)
            {
                return pubno;
            }
            else
            {
                String tem = pubno.Substring(0, 500);
                tem = tem.Substring(0, tem.LastIndexOf(';'));
                return tem;
            }
        }

        //格式化日期
        public static String FormatDate(String date)
        {
            DateTime tmpdt;
            // string to Datetime
            if (!DateTime.TryParse(date, out tmpdt))
            {
                try
                {
                    //转不了格式如同20010101
                    tmpdt = new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(date.Substring(4, 2)), Convert.ToInt32(date.Substring(6, 2)));
                }
                catch (Exception)
                {
                    return String.Empty;
                }
            }
            return tmpdt.ToString("yyyyMMdd");
        }

        //格式化dwpi中tisp格式的申请号
        public static String DwpiFormatAppno(String appno)
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

        //去重，如果aaa,bbb,aaa 返回结果为aaa,bbb
        public static String Distinct(String value)
        {
            String result = "";
            if (value.Trim() == "")
            {
                result = "";
            }
            else
            {
                String[] item = value.Split(Common.Index_Spliter[0]);
                IEnumerable list = item.Distinct();
                foreach (String s in list)
                {
                    result = result + Common.Index_Spliter + s;
                }
                result = result.Trim().Substring(1);
                if (result[result.Length - 1] == Common.Index_Spliter[0])
                {
                    result = result.Substring(0, result.Length - 1);
                }
            }
            return result;
        }

        //去重，如果aaa,bbb,aaa 返回结果为aaa,bbb
        public static String Distinct(String value, char spliter)
        {
            String result = "";
            if (value.Trim() == "")
            {
                result = "";
            }
            else
            {
                String[] item = value.Split(spliter);
                IEnumerable list = item.Distinct();
                foreach (String s in list)
                {
                    result = result + Common.Index_Spliter.ToString() + s;
                }
                result = result.Trim().Substring(1);
                if (result[result.Length - 1] == spliter)
                {
                    result = result.Substring(0, result.Length - 1);
                }
            }
            return result;
        }
    }
}
