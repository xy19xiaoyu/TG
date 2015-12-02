using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public class StringUtil
    {

        //判断首字符是不是字母，是字母返回true，是数字返回false
        public static bool StartWithString(String s)
        {
            String lower = s.ToLower();
            String upper = s.ToUpper();
            if (lower == upper)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        //去掉xml文件中包含的低位非打印字符（中文部分文献含有这个错误）
        public static string ReplaceLowOrderASCIICharacters(string tmp)
        {
            StringBuilder info = new StringBuilder();
            foreach (char cc in tmp)
            {
                int ss = (int)cc;
                if (((ss >= 0) && (ss <= 8)) || ((ss >= 11) && (ss <= 12)) || ((ss >= 14) && (ss <= 32)))
                    info.AppendFormat(" ", ss);//&#x{0:X};
                else if (ss == 38)
                {
                    info.AppendFormat("＆", ss);
                }
                else if (ss == 127)
                {
                    info.AppendFormat(" ", ss);
                }
                else
                {
                    info.Append(cc);
                }
            }
            return info.ToString();
        }
        
        /// <summary>
        /// 返回字符串的指定字节数，如字符串的字节数小于指定字节数，则在后面补空格
        /// </summary>
        /// <param name="str"></param>
        /// <param name="byteNum"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        static public String SubStringByByte(String str, int byteNum,Encoding encode)
        {
            byte[] bytes = new byte[byteNum];
            for(int i =0;i<byteNum;i++)//初始值都是空格
            {
                bytes[i] = System.Convert.ToByte(' ');
            }
            byte[] tem = encode.GetBytes(str);
            if (tem.Length <= byteNum)//如果字节数小于要求返回的字节数，则后面补空格
            {
                for (int i = 0; i < tem.Length; i++)
                {
                    bytes[i] = tem[i];
                }
            }
            else
            {
                for (int i = 0; i < byteNum; i++)
                {
                    bytes[i] = tem[i];
                }
            }
            return encode.GetString(bytes);
        }

        //去掉字符串的第一个分隔符
        static public String RemoveFirstSpliter(String str)
        {
            if (str.Trim() == "" || str == String.Empty)
            {
                return str;
            }
            if (str.StartsWith(Common.Index_Spliter))
            {
                return str.Substring(1);
            }
            else
            {
                throw new Exception(str+"第一个字符不是分隔符");
            }
        }

        //获得字符串中分隔符的个数
        static public int GetSpliterNum(String str, char spliter)
        {
            char[] chs = str.ToCharArray();
            int num = 0;
            for (int i = 0; i < chs.Count(); i++)
            {
                if (chs[i].CompareTo(spliter) == 0)
                {
                    num++;
                }
            }
            return num;
        }
    }
}
