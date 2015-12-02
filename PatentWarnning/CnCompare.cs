using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace PatentWarnning
{
    public class CnpCompare
    {


        /// <summary>
        /// 比较两个结果文件CnpA、CnpB，并将比较结果存到第一个文件所在的目录下
        /// CnpAAndCnpB.Cnp,CnpAOrCnpB.Cnp,CnpANotCnpB.Cnp,CnpBNotCnpA.Cnp
        /// return value: hashtable, values list as follows
        ///  key:"lst1AndLst2", value:CnpAAndCnpB中的结果数 
        ///  key:"lst1OrLst2", value:CnpAOrCnpB中的结果数   totalNum
        ///  key:"lst1NotLst2", value:CnpANotCnpB中的结果数 addNum
        ///  key:"lst2NotLst1", value:CnpBNotCnpA中的结果数 deleteNum
        /// </summary>


        public Hashtable Compare(String filePath1, byte[] file)
        {
            List<int> lst1 = GetResultList(filePath1);
            List<int> lst2 = new List<int>();
            
            List<int> lst1AndLst2 = new List<int>();
            List<int> lst1OrLst2 = new List<int>();
            List<int> lst1NotLst2 = new List<int>();
            List<int> lst2NotLst1 = new List<int>();

            int lstLength1 = lst1.Count;
            int lstLength2 = lst2.Count;
            int i = 0; // lst1的指针
            int j = 0; // lst2的指针
            for (; i < lstLength1 && j < lstLength2; )
            {
                if (lst1[i] == lst2[j])
                {
                    lst1AndLst2.Add(lst1[i]);
                    lst1OrLst2.Add(lst1[i]);
                    i++;
                    j++;
                }
                else if (lst1[i] > lst2[j])
                {
                    lst2NotLst1.Add(lst2[j]);
                    lst1OrLst2.Add(lst2[j]);
                    j++;
                }
                else if (lst1[i] < lst2[j])
                {
                    lst1NotLst2.Add(lst1[i]);
                    lst1OrLst2.Add(lst1[i]);
                    i++;
                }
            }

            // 将lst2的剩下的内容存入list
            if (lstLength1 == i)
            {
                lst2NotLst1.AddRange(lst2.GetRange(j, lstLength2 - j));
                lst1OrLst2.AddRange(lst2.GetRange(j, lstLength2 - j));
            }
            // 将lst1的剩下的内容存入list
            else if (lstLength2 == j)
            {
                lst1NotLst2.AddRange(lst1.GetRange(i, lstLength1 - i));
                lst1OrLst2.AddRange(lst1.GetRange(i, lstLength1 - i));
            }

            Hashtable resultNum = new Hashtable();
            //resultNum.Add("lst1AndLst2", lst1AndLst2.Count);
            //resultNum.Add("lst1OrLst2", lst1OrLst2.Count);
            //resultNum.Add("lst1NotLst2", lst1NotLst2.Count);
            //resultNum.Add("lst2NotLst1", lst2NotLst1.Count);
            lst1OrLst2.AddRange(lst2NotLst1);
            resultNum.Add("Num", lst1NotLst2.Count);
            resultNum.Add("File", lst1NotLst2.ToArray());
            return resultNum;

        }

        /// <summary>
        /// 比较两个结果文件CnpA、CnpB，并将比较结果存到第一个文件所在的目录下
        /// CnpAAndCnpB.Cnp,CnpAOrCnpB.Cnp,CnpANotCnpB.Cnp,CnpBNotCnpA.Cnp
        /// return value: hashtable, values list as follows
        ///  key:"lst1AndLst2", value:CnpAAndCnpB中的结果数
        ///  key:"lst1OrLst2", value:CnpAOrCnpB中的结果数 
        ///  key:"lst1NotLst2", value:CnpANotCnpB中的结果数 
        ///  key:"lst2NotLst1", value:CnpBNotCnpA中的结果数 
        /// </summary>
        public Hashtable Compare(String filePath1, String filePath2, String desDirPath)
        {
            List<int> lst1 = GetResultList(filePath1);
            List<int> lst2 = GetResultList(filePath2);
            List<int> lst1AndLst2 = new List<int>();
            List<int> lst1OrLst2 = new List<int>();
            List<int> lst1NotLst2 = new List<int>();
            List<int> lst2NotLst1 = new List<int>();

            int lstLength1 = lst1.Count;
            int lstLength2 = lst2.Count;
            int i = 0; // lst1的指针
            int j = 0; // lst2的指针
            for (; i < lstLength1 && j < lstLength2; )
            {
                if (lst1[i] == lst2[j])
                {
                    lst1AndLst2.Add(lst1[i]);
                    lst1OrLst2.Add(lst1[i]);
                    i++;
                    j++;
                }
                else if (lst1[i] > lst2[j])
                {
                    lst2NotLst1.Add(lst2[j]);
                    lst1OrLst2.Add(lst2[j]);
                    j++;
                }
                else if (lst1[i] < lst2[j])
                {
                    lst1NotLst2.Add(lst1[i]);
                    lst1OrLst2.Add(lst1[i]);
                    i++;
                }
            }

            // 将lst2的剩下的内容存入list
            if (lstLength1 == i)
            {
                lst2NotLst1.AddRange(lst2.GetRange(j, lstLength2 - j));
                lst1OrLst2.AddRange(lst2.GetRange(j, lstLength2 - j));
            }
            // 将lst1的剩下的内容存入list
            else if (lstLength2 == j)
            {
                lst1NotLst2.AddRange(lst1.GetRange(i, lstLength1 - i));
                lst1OrLst2.AddRange(lst1.GetRange(i, lstLength1 - i));
            }

            // 将四个list存到四个文件中
            String desDir = desDirPath;
            String fileName1 = Path.GetFileName(filePath1);
            fileName1 = fileName1.Substring(0, fileName1.ToUpper().IndexOf(".CNP"));
            String fileName2 = Path.GetFileName(filePath2);
            fileName2 = fileName2.Substring(0, fileName2.ToUpper().IndexOf(".CNP"));
            String CnpAAndCnpB = desDir + "\\" + fileName1 + "And" + fileName2 + ".Cnp";
            String CnpAOrCnpB = desDir + "\\" + fileName1 + "Or" + fileName2 + ".Cnp";
            String CnpANotCnpB = desDir + "\\" + fileName1 + "Not" + fileName2 + ".Cnp";
            String CnpBNotCnpA = desDir + "\\" + fileName2 + "Not" + fileName1 + ".Cnp";


            try
            {
                File.WriteAllBytes(CnpAAndCnpB, GetListBytes(lst1AndLst2));
                File.WriteAllBytes(CnpAOrCnpB, GetListBytes(lst1OrLst2));
                File.WriteAllBytes(CnpANotCnpB, GetListBytes(lst1NotLst2));
                File.WriteAllBytes(CnpBNotCnpA, GetListBytes(lst2NotLst1));
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }


            Hashtable resultNum = new Hashtable();
            resultNum.Add("lst1AndLst2", lst1AndLst2.Count);
            resultNum.Add("lst1AndLst2", lst1OrLst2.Count);
            resultNum.Add("lst1NotLst2", lst1NotLst2.Count);
            resultNum.Add("lst2NotLst1", lst2NotLst1.Count);
            return resultNum;

        }

        /// <summary>
        /// 比较两个结果文件CnpA、CnpB，并将比较结果存到第一个文件所在的目录下
        /// CnpAAndCnpB.Cnp,CnpAOrCnpB.Cnp,CnpANotCnpB.Cnp,CnpBNotCnpA.Cnp
        /// return value: hashtable, values list as follows
        ///  key:"lst1AndLst2", value:CnpAAndCnpB中的结果数
        ///  key:"lst1OrLst2", value:CnpAOrCnpB中的结果数 
        ///  key:"lst1NotLst2", value:CnpANotCnpB中的结果数 
        ///  key:"lst2NotLst1", value:CnpBNotCnpA中的结果数 
        /// </summary>
        public Hashtable CompareWithWriteFile(String filePath1, String filePath2)
        {
            List<int> lst1 = GetResultList(filePath1);
            List<int> lst2 = GetResultList(filePath2);
            List<int> lst1AndLst2 = new List<int>();
            List<int> lst1OrLst2 = new List<int>();
            List<int> lst1NotLst2 = new List<int>();
            List<int> lst2NotLst1 = new List<int>();

            int lstLength1 = lst1.Count;
            int lstLength2 = lst2.Count;
            int i = 0; // lst1的指针
            int j = 0; // lst2的指针
            for (; i < lstLength1 && j < lstLength2; )
            {
                if (lst1[i] == lst2[j])
                {
                    lst1AndLst2.Add(lst1[i]);
                    lst1OrLst2.Add(lst1[i]);
                    i++;
                    j++;
                }
                else if (lst1[i] > lst2[j])
                {
                    lst2NotLst1.Add(lst2[j]);
                    lst1OrLst2.Add(lst2[j]);
                    j++;
                }
                else if (lst1[i] < lst2[j])
                {
                    lst1NotLst2.Add(lst1[i]);
                    lst1OrLst2.Add(lst1[i]);
                    i++;
                }
            }

            // 将lst2的剩下的内容存入list
            if (lstLength1 == i)
            {
                lst2NotLst1.AddRange(lst2.GetRange(j, lstLength2 - j));
                lst1OrLst2.AddRange(lst2.GetRange(j, lstLength2 - j));
            }
            // 将lst1的剩下的内容存入list
            else if (lstLength2 == j)
            {
                lst1NotLst2.AddRange(lst1.GetRange(i, lstLength1 - i));
                lst1OrLst2.AddRange(lst1.GetRange(i, lstLength1 - i));
            }

            // 将四个list存到四个文件中
            String desDir = Path.GetDirectoryName(filePath1);
            String fileName1 = Path.GetFileName(filePath1);
            fileName1 = fileName1.Substring(0, fileName1.ToUpper().IndexOf(".CNP"));
            String fileName2 = Path.GetFileName(filePath2);
            fileName2 = fileName2.Substring(0, fileName2.ToUpper().IndexOf(".CNP"));
            String CnpAAndCnpB = desDir + "\\" + fileName1 + "And" + fileName2 + ".Cnp";
            String CnpAOrCnpB = desDir + "\\" + fileName1 + "Or" + fileName2 + ".Cnp";
            String CnpANotCnpB = desDir + "\\" + fileName1 + "Not" + fileName2 + ".Cnp";
            String CnpBNotCnpA = desDir + "\\" + fileName2 + "Not" + fileName1 + ".Cnp";


            try
            {
                File.WriteAllBytes(CnpAAndCnpB, GetListBytes(lst1AndLst2));
                File.WriteAllBytes(CnpAOrCnpB, GetListBytes(lst1OrLst2));
                File.WriteAllBytes(CnpANotCnpB, GetListBytes(lst1NotLst2));
                File.WriteAllBytes(CnpBNotCnpA, GetListBytes(lst2NotLst1));
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }

            Hashtable resultNum = new Hashtable();
            resultNum.Add("lst1AndLst2", lst1AndLst2.Count);
            resultNum.Add("lst1OrLst2", lst1OrLst2.Count);
            resultNum.Add("lst1NotLst2", lst1NotLst2.Count);
            resultNum.Add("lst2NotLst1", lst2NotLst1.Count);
            return resultNum;

        }

        /// <summary>
        /// 比较两个结果文件CnpA、CnpB，并将比较结果存到第一个文件所在的目录下
        /// CnpAAndCnpB.Cnp,CnpAOrCnpB.Cnp,CnpANotCnpB.Cnp,CnpBNotCnpA.Cnp
        /// return value: hashtable, values list as follows
        ///  key:"lst1AndLst2", value:CnpAAndCnpB中的结果数
        ///  key:"lst1OrLst2", value:CnpAOrCnpB中的结果数 
        ///  key:"lst1NotLst2", value:CnpANotCnpB中的结果数 
        ///  key:"lst2NotLst1", value:CnpBNotCnpA中的结果数 
        /// </summary>
        public Hashtable CompareWithWriteFile2(String filePath1, String filePath2)
        {
            List<int> lst1 = GetResultList(filePath1);
            List<int> lst2 = GetResultList(filePath2);
            List<int> lst1AndLst2 = new List<int>();
            List<int> lst1OrLst2 = new List<int>();
            List<int> lst1NotLst2 = new List<int>();
            List<int> lst2NotLst1 = new List<int>();

            int lstLength1 = lst1.Count;
            int lstLength2 = lst2.Count;
            int i = 0; // lst1的指针
            int j = 0; // lst2的指针
            for (; i < lstLength1 && j < lstLength2; )
            {
                if (lst1[i] == lst2[j])
                {
                    lst1AndLst2.Add(lst1[i]);
                    lst1OrLst2.Add(lst1[i]);
                    i++;
                    j++;
                }
                else if (lst1[i] > lst2[j])
                {
                    lst2NotLst1.Add(lst2[j]);
                    lst1OrLst2.Add(lst2[j]);
                    j++;
                }
                else if (lst1[i] < lst2[j])
                {
                    lst1NotLst2.Add(lst1[i]);
                    lst1OrLst2.Add(lst1[i]);
                    i++;
                }
            }

            // 将lst2的剩下的内容存入list
            if (lstLength1 == i)
            {
                lst2NotLst1.AddRange(lst2.GetRange(j, lstLength2 - j));
                lst1OrLst2.AddRange(lst2.GetRange(j, lstLength2 - j));
            }
            // 将lst1的剩下的内容存入list
            else if (lstLength2 == j)
            {
                lst1NotLst2.AddRange(lst1.GetRange(i, lstLength1 - i));
                lst1OrLst2.AddRange(lst1.GetRange(i, lstLength1 - i));
            }

            // 将四个list存到四个文件中
            String desDir = Path.GetDirectoryName(filePath1);
            String fileName1 = Path.GetFileName(filePath1);
            fileName1 = fileName1.Substring(0, fileName1.ToUpper().IndexOf(".CNP"));
            String fileName2 = Path.GetFileName(filePath2);
            fileName2 = fileName2.Substring(0, fileName2.ToUpper().IndexOf(".CNP"));
            String CnpAAndCnpB = desDir + "\\" + fileName1 + "And" + fileName2 + ".Cnp";
            String CnpAOrCnpB = desDir + "\\" + fileName1 + "Or" + fileName2 + ".Cnp";
            String CnpANotCnpB = desDir + "\\" + fileName1 + "Not" + fileName2 + ".Cnp";
            String CnpBNotCnpA = desDir + "\\" + fileName2 + "Not" + fileName1 + ".Cnp";


            try
            {
                File.WriteAllBytes(CnpAAndCnpB, GetListBytes(lst1AndLst2));
                File.WriteAllBytes(CnpAOrCnpB, GetListBytes(lst1OrLst2));
                File.WriteAllBytes(CnpANotCnpB, GetListBytes(lst1NotLst2));
                File.WriteAllBytes(CnpBNotCnpA, GetListBytes(lst2NotLst1));
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }

            Hashtable resultNum = new Hashtable();
            resultNum.Add("lst1AndLst2", lst1AndLst2);
            resultNum.Add("lst1OrLst2", lst1OrLst2);
            resultNum.Add("lst1NotLst2", lst1NotLst2);
            resultNum.Add("lst2NotLst1", lst2NotLst1);
            return resultNum;

        }

        /// <summary>
        /// 将List<int>转换成为byte[]
        /// </summary>
        public byte[] GetListBytes(List<int> lst)
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
        public List<int> GetResultList(String filePath)
        {

            List<int> lstfml = new List<int>();
            byte[] fmlNo = new byte[4];
            int readlength;
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                while (fs.Position != fs.Length)
                {
                    readlength = fs.Read(fmlNo, 0, 4);
                    if (readlength <= 0)
                    {
                        break;
                    }
                    lstfml.Add(BitConverter.ToInt32(fmlNo, 0));
                }

            }
            return lstfml;
        }



    }
}
