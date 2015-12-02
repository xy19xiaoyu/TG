using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace PatentWarnning
{
    public class ConvertLstByte
    {

        /// <summary>
        /// 将List<int>转换成为byte[]
        /// </summary>
        public static byte[] GetListBytes(List<int> lst)
        {
            List<byte> lstByte = new List<byte>();
            foreach (int item in lst)
            {
                lstByte.AddRange(System.BitConverter.GetBytes(item));
            }
            return lstByte.ToArray();
        }



        /// <summary>
        /// 得到某一结果文件的list
        /// </summary>
        public static List<int> GetCnpList(byte[] bteCnp)
        {
            List<int> lstfml = new List<int>();
            try
            {
                for (int i = 0; i < bteCnp.Length; i += 4)
                {
                    lstfml.Add(BitConverter.ToInt32(bteCnp, i));
                }
            }
            catch (Exception ex)
            {
            }
            lstfml.Reverse();
            return lstfml;
        }
    }
}
