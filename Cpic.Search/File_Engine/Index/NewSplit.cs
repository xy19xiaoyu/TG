using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cpic.Cprs2010.Index;
using Cpic.Cprs2010.Search;
namespace Cpic.Cprs2010.Index
{   
    public class NewSplit : IDisposable
    {
        private Dictionary<string, System.IO.FileStream> filelist = new Dictionary<string, System.IO.FileStream>();
        private DataInterfaceConfig config;
        private List<SearchEnter> lstSearchEnter;
        private string OutPutPath = string.Empty;
        private int Sept;
        private Search.SearchDbType _Type; 
        public string ErrorStringLog;
        public FileMode FileMode;
        public List<string> DirList;
        public bool isDone = false;
        public log4net.ILog log;
        public log lg;
        
        public Search.SearchDbType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        /// <summary>
        /// 文件路径
        /// </summary>
        private string filepath = "{0}\\{1}\\{2}.001";
        /// <summary>
        /// 当前处理进度
        /// </summary>
        public int ExIndex = 0;

        private string DataFileName;
        private string DataPath;     

        #region 构造函数

        /// <summary>
        /// 构造函数 初始或所有的001文件
        /// </summary>
        /// <param name="DataType">数据类型,只有中文CN|引文EN</param>
        /// <param name="configFilePath">配置文件路径</param>
        /// <param name="strOutPutPath">输出目录</param>
        public NewSplit(string configFilePath, string strOutPutPath, string strDataPath,log4net.ILog log)
        {            
            DataPath = strDataPath;
            DataFileName = Path.GetFileNameWithoutExtension(configFilePath).Replace(".cfg", "");
            config = new DataInterfaceConfig(configFilePath);
            lstSearchEnter = config.SearchEnters;
            OutPutPath = strOutPutPath;
            this.Type = config.Type;
            this.log = log;
            Sept = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Sept"].ToString());
            this.FileMode = FileMode.CreateNew;          
        }

        /// <summary>
        /// 构造函数 初始或所有的001文件
        /// </summary>
        /// <param name="DataType">数据类型,只有中文CN|引文EN</param>
        /// <param name="configFilePath">配置文件路径</param>
        /// <param name="strOutPutPath">输出目录</param>
        public NewSplit(string configFilePath, string strOutPutPath, string strDataPath, log log)
        {
            DataPath = strDataPath;
            DataFileName = Path.GetFileNameWithoutExtension(configFilePath).Replace(".cfg", "");
            config = new DataInterfaceConfig(configFilePath);
            lstSearchEnter = config.SearchEnters;
            OutPutPath = strOutPutPath;
            this.Type = config.Type;
            this.lg = log;
            Sept = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Sept"].ToString());
            this.FileMode = FileMode.CreateNew;
        }

        #endregion

        #region 文件创建 关闭
        /// <summary>
        /// 如果按数据数量划分
        /// </summary>
        /// <param name="Index"></param>
        private void AddSearchEnterFile(int Index)
        {
            string filename = string.Empty;
            string filefullPath = string.Empty;
           

            //处理检索入口 有多少个检索入口要有对应的切词后的文件
            foreach (var enter in lstSearchEnter)
            {
                ///如果是唯一索引列
                if (enter.Index == config.SerialIndex)
                {
                    continue;
                }
                //判断是否切词,如果需要切词
                if (enter.WordSplit != WordSplitType.None && enter.SingleFile == false)
                {
                    //判断是否是中文切词
                    if (enter.WordSplit == WordSplitType.Cn)
                    {
                        #region 中文切词
                        //建立1字词,2字词,3字词 的文件
                        for (int j = 1; j <= 3; j++)
                        {
                            filename = enter.Name + j.ToString();  //文件名
                            filefullPath = string.Format(filepath, OutPutPath, Index.ToString().PadLeft(5, '0'), filename); //文件全路径
                            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filefullPath)); //创建文件所在目录
                            if (File.Exists(filefullPath))
                            {
                                File.Delete(filefullPath);
                            }
                            filelist.Add(Index + filename, new System.IO.FileStream(filefullPath, FileMode, FileAccess.Write)); //IO 文件流
                        }
                        #endregion
                    }
                    else
                    {
                        #region 英文切词

                        for (int j = 1; j <= 26; j++)
                        {
                            filename = enter.Name + "_" + (char)(j + 64);  //文件名
                            filefullPath = string.Format(filepath, OutPutPath, Index.ToString().PadLeft(5, '0'), filename); //文件全路径
                            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filefullPath)); //创建文件所在目录
                            if (File.Exists(filefullPath))
                            {
                                File.Delete(filefullPath);
                            }
                            filelist.Add(Index + filename, new System.IO.FileStream(filefullPath, FileMode.OpenOrCreate, FileAccess.Write)); //IO 文件流
                        }
                        //todo:添加_0 的 文件
                        filename = enter.Name + "_0"; //文件名
                        filefullPath = string.Format(filepath, OutPutPath, Index.ToString().PadLeft(5, '0'), filename); //文件全路径
                        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filefullPath)); //创建文件所在目录
                        if (File.Exists(filefullPath))
                        {
                            File.Delete(filefullPath);
                        }
                        filelist.Add(Index + filename, new System.IO.FileStream(filefullPath, FileMode, FileAccess.Write)); //IO 文件流

                        #endregion
                    }
                }
                else //不需要切词 只留一个检索入口的文件
                {
                    #region 不需要切词
                    filename = enter.Name;  //文件名
                    filefullPath = string.Format(filepath, OutPutPath, Index.ToString().PadLeft(5, '0'), filename); //文件全路径
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filefullPath)); //创建文件所在目录
                    if (File.Exists(filefullPath))
                    {
                        File.Delete(filefullPath);
                    }
                    filelist.Add(Index + filename, new System.IO.FileStream(filefullPath, FileMode, FileAccess.Write)); //IO 文件流

                    // 判断是否需要建立子索引
                    if (enter.SubKey.Count > 0)
                    {
                        foreach (SubSearchEnter subenter in enter.SubKey)
                        {

                            filename = subenter.Name;  //文件名
                            filefullPath = string.Format(filepath, OutPutPath, Index.ToString().PadLeft(5, '0'), filename); //文件全路径
                            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filefullPath)); //创建文件所在目录
                            if (File.Exists(filefullPath))
                            {
                                File.Delete(filefullPath);
                            }
                            filelist.Add(Index + filename, new System.IO.FileStream(filefullPath, FileMode, FileAccess.Write)); //IO 文件流
                        }

                    }
                    #endregion
                }
            }

        }

        /// <summary>
        /// 关闭某一个段的所有文件流
        /// </summary>
        /// <param name="Index"></param>
        private void CloseSearchEnterFile()
        {
            System.Threading.Thread.Sleep(1000 * 10);
            foreach (System.Collections.Generic.KeyValuePair<string, System.IO.FileStream> item in filelist)
            {
                if (item.Value.CanWrite)
                {
                    Error(item.Key.ToString() + "\t" + item.Value.Length);
                    item.Value.Flush();
                    item.Value.Close();
                }

            }
            filelist.Clear();          
        }

        ~NewSplit()
        {
            CloseSearchEnterFile();          
        }
        #endregion

        #region 中文
        /// <summary>
        /// 中文数据拆分
        /// </summary>
        public void CnSplitDataFile(List<string> lstDir)
        {

            List<string[]> lines = new List<string[]>();
            string tmpline;
            int FmlId;
            int PreFmlId = -1;
            int dirNum;
            string[] tmpary;
            string DataFilePath;

            foreach (string dir in lstDir)
            {
                if(string.IsNullOrEmpty(dir))
                {
                    continue;
                }
              
                dirNum = Convert.ToInt32(dir);             

                DataFilePath = DataPath +"\\" + dir+"\\" + DataFileName + ".txt";
                if (!System.IO.File.Exists(DataFilePath))
                {
                    Info("文件：" + DataFilePath + "不存在！");
                    Error("文件："+DataFilePath + "不存在！");
                    continue;
                }

              
                AddSearchEnterFile(dirNum);
               
                TimeSpan tsp;
                DateTime start = DateTime.Now;  
                #region 读数据切词
                
                using (System.IO.StreamReader sr = new System.IO.StreamReader(DataFilePath, Encoding.GetEncoding(config.Encoding)))
                {

                    while (!sr.EndOfStream)
                    {
                        ExIndex += 1; //当前处理的进度
                        tmpline = sr.ReadLine();
                        tmpary = tmpline.Split(config.Split.ToCharArray());
                        tmpary[0] = tmpary[0].Trim();
                        FmlId = Convert.ToInt32(tmpary[0]);

                        if (PreFmlId == FmlId || PreFmlId == -1)
                        {
                            lines.Add(tmpary);
                            PreFmlId = FmlId;
                        }
                        else
                        {
                            try
                            {

                                if (PreFmlId % 10000 == 1)
                                {
                                    Info(String.Format("正在处理 段：{0} 家族：{1}", dirNum, PreFmlId));
                                }

                                CnSplitLines(lines, dirNum);
                                lines.Clear();
                                lines.Add(tmpary);
                                PreFmlId = FmlId;

                            }
                            catch (Exception ex)
                            {
                                Error(string.Format("正在处理 段：{0} 家族：{1}\n错误信息：{2}", dirNum, PreFmlId, ex));
                                lines.Clear();
                                lines.Add(tmpary);
                                PreFmlId = FmlId;
                            }
                        }

                    }
                    if (lines.Count > 0)
                    {
                        Info(String.Format("正在处理 段：{0} 家族：{1}", dirNum, lines[0][0].ToString()));                        
                        CnSplitLines(lines, dirNum);
                    }
                }
                #endregion
                tsp = DateTime.Now - start;
                Error("数据总数：" + ExIndex);
                Error("用时：" + com.FormatTimeSpan(tsp));
                ExIndex = 0;
                CloseSearchEnterFile();
                lines.Clear();            

            }           


        }
        public void Info(string str)
        {
            if (log == null)
            {
                lg.error(str);
            }
            else
            {
                log.Info(str);
            }
            Console.WriteLine(str);
        }
        public void Error(string str)
        {
            if (log == null)
            {
                lg.error(str);
            }
            else
            {
                log.Error(str);
            }
            Console.WriteLine(str);
        }
        public void Debug(string str)
        {
            if (log == null)
            {
                lg.error(str);
            }
            else
            {
                log.Debug(str);
            }
            Console.WriteLine(str);
        }
      

        /// <summary>
        /// 拆分一条数据
        /// </summary>
        /// <param name="strLine"></param>
        /// <param name="se"></param>
        /// <returns></returns>
        private bool CnSplitLines(List<string[]> strLines, int Index)
        {
            List<string[]> arysp = strLines;
            List<string[]> arytmp = new List<string[]>();
            List<string> lsttmp;


            foreach (var se in lstSearchEnter)
            {
                lsttmp = new List<string>();
                ///如果是唯一索引列
                if (se.Index == config.SerialIndex)
                {
                    continue;
                }

                //判断是否需要二次拆分
                if (!string.IsNullOrEmpty(se.Split))
                {
                    foreach (var tmp in arysp)
                    {
                        lsttmp.AddRange(tmp[se.Index].Split(se.Split.ToCharArray()).ToList<string>());
                    }
                    CnExSearchEnter(lsttmp, se, Index, (int)Convert.ToInt32(arysp[0][config.SerialIndex]));

                }
                else
                {

                    foreach (var tmp in arysp)
                    {
                        lsttmp.Add(tmp[se.Index]);
                    }
                    //不需要拆分                                     
                    CnExSearchEnter(lsttmp, se, Index, (int)Convert.ToInt32(arysp[0][config.SerialIndex]));
                }

            }

            return true;

        }
        /// <summary>
        /// 处理一条检索项的数据
        /// </summary>
        /// <param name="strData">一个家族检索项数据</param>
        /// <param name="se">检索项配置信息</param>
        /// <param name="Year">数据所在年份</param>
        /// <param name="SerialNo">公开号或者唯一的序列号</param>
        /// <returns></returns>        
        private bool CnExSearchEnter(List<string> strData, SearchEnter se, int Index, int SerialNo)
        {
            byte[] by;
            byte[] byserial = BitConverter.GetBytes(SerialNo);
            strData = strData.Distinct<string>().ToList<string>();
            Encoding cd =System.Text.Encoding.GetEncoding(se.Encoding);
            //需要切词
            if (se.WordSplit == WordSplitType.Cn)
            {
                List<string> Sentence = new List<string>();
                List<char> OneWord = new List<char>();
                List<char> tmpOneWord = new List<char>();
                List<string> TwoWord = new List<string>();
                List<string> ThreeWord = new List<string>();
                foreach (var str in strData)
                {
                    //切成没有符号的短句
                    Sentence = WordSplit.getSentenceList(str.Trim());

                    //循环每个短句
                    foreach (string s in Sentence)
                    {
                        //如果为空进行下次循环
                        if (string.IsNullOrEmpty(s))
                        {
                            continue;
                        }

                        //短句中的一字词
                        tmpOneWord = WordSplit.getOneWordList(s);
                        OneWord.AddRange(tmpOneWord);
                        //短句中的二字词
                        TwoWord.AddRange(WordSplit.getTwoWordList(tmpOneWord));
                        //短句中的三字词
                        ThreeWord.AddRange(WordSplit.getThreeWordList(tmpOneWord));

                    }
                }
                OneWord = OneWord.Distinct<char>().OrderBy(x => x.ToString()).ToList<char>();
                TwoWord = TwoWord.Distinct<string>().OrderBy(x => x).ToList<string>();
                ThreeWord = ThreeWord.Distinct<string>().OrderBy(x => x).ToList<string>();

                if (se.SingleFile == true)
                {
                    //写三字词
                    foreach (string w in ThreeWord)
                    {
                        by = cd.GetBytes(w.ToString());
                        if (by.Length < 6)
                        {
                            Error(SerialNo + "\t" + w.ToString());
                            continue;

                        }
                        filelist[Index.ToString() + se.Name].Write(byserial, 0, byserial.Length);
                        filelist[Index.ToString() + se.Name].Write(by, 0, 6);
                    }
                }
                else
                {
                    //写一字词
                    foreach (char w in OneWord)
                    {
                        by = cd.GetBytes(w.ToString());
                        if (by.Length < 2)
                        {
                            Error(SerialNo + "\t" + w.ToString());
                            continue;

                        }
                        filelist[Index.ToString() + se.Name + "1"].Write(byserial, 0, byserial.Length);
                        filelist[Index.ToString() + se.Name + "1"].Write(by, 0, 2);
                    }
                    //写二字词
                    foreach (string w in TwoWord)
                    {
                        by = cd.GetBytes(w.ToString());
                        if (by.Length < 4)
                        {
                            Error(SerialNo + "\t" + w.ToString());
                            continue;

                        }
                        filelist[Index.ToString() + se.Name + "2"].Write(byserial, 0, byserial.Length);
                        filelist[Index.ToString() + se.Name + "2"].Write(by, 0, 4);
                    }
                    //写三字词
                    foreach (string w in ThreeWord)
                    {
                        by = cd.GetBytes(w.ToString());
                        if (by.Length < 6)
                        {
                            Error(SerialNo + "\t" + w.ToString());
                            continue;

                        }
                        filelist[Index.ToString() + se.Name + "3"].Write(byserial, 0, byserial.Length);
                        filelist[Index.ToString() + se.Name + "3"].Write(by, 0, 6);
                    }
                }



            }
            else
            {
                strData = strData.OrderBy(x => x).ToList<string>();
                foreach (string tmp in strData)
                {
                    if (string.IsNullOrEmpty(tmp.Trim()))
                    {
                        continue;
                    }
                    filelist[Index.ToString() + se.Name].Write(byserial, 0, byserial.Length);
                    by = System.Text.Encoding.GetEncoding(config.Encoding).GetBytes(tmp.ToString().PadRight(se.Length, ' '));
                    if (by.Length < se.Length)
                    {
                        Error(SerialNo + "\t" + tmp.ToString());
                        continue;
                    }

                    filelist[Index.ToString() + se.Name].Write(by, 0, se.Length);
                }

                if (se.SubKey.Count > 0)
                {
                    foreach (SubSearchEnter sbenter in se.SubKey)
                    {
                        List<string> tmp = new List<string>();
                        foreach (string s in strData)
                        {
                            if (s.Length >= sbenter.Length)
                            {
                                tmp.Add(s.Substring(0, sbenter.Length));
                            }
                            else
                            {
                                tmp.Add(s);
                            }
                        }

                        tmp = tmp.Distinct<string>().OrderBy(x => x).ToList<string>();
                        foreach (string t in tmp)
                        {
                            if (string.IsNullOrEmpty(t.Trim()))
                            {
                                continue;
                            }
                            filelist[Index.ToString() + sbenter.Name].Write(byserial, 0, byserial.Length);
                            by = System.Text.Encoding.GetEncoding(config.Encoding).GetBytes(t.ToString().PadRight(sbenter.Length, ' '));
                            if (by.Length < sbenter.Length)
                            {
                                Error(SerialNo + "\t" + tmp.ToString());
                                continue;
                            }
                            filelist[Index.ToString() + sbenter.Name].Write(by, 0, sbenter.Length);

                        }
                    }
                }
            }

            return true;
        }
        #endregion

        #region 英文

        /// <summary>
        /// 英文数据拆分
        /// </summary>
        public void EnSplitDataFile(List<string> lstDir)
        {
            List<string[]> lines = new List<string[]>();
            string tmpline;
            int FmlId;
            int PreFmlId = -1;
            int dirNum;
            string[] tmpary;
            string DataFilePath;

            foreach (string dir in lstDir)
            {
                if (string.IsNullOrEmpty(dir))
                {
                    continue;
                }

                dirNum = Convert.ToInt32(dir);
              
                DataFilePath = DataPath + "\\" + dir + "\\" + DataFileName + ".txt";
                if (!System.IO.File.Exists(DataFilePath))
                {
                    Info("文件：" + DataFilePath + "不存在！");
                    Error("文件：" + DataFilePath + "不存在！");
                    continue;
                }
                
                AddSearchEnterFile(dirNum);


                TimeSpan tsp;
                DateTime start = DateTime.Now;   
                #region 切词
                using (System.IO.StreamReader sr = new System.IO.StreamReader(DataFilePath, Encoding.GetEncoding("utf-8")))
                {

                    while (!sr.EndOfStream)
                    {
                        ExIndex += 1; //当前处理的进度
                        tmpline = sr.ReadLine();
                        tmpary = tmpline.Split(config.Split.ToCharArray());
                        tmpary[0] = tmpary[0].Trim();
                        FmlId = Convert.ToInt32(tmpary[0]);


                        if (PreFmlId == FmlId || PreFmlId == -1)
                        {
                            lines.Add(tmpary);
                            PreFmlId = FmlId;
                        }
                        else
                        {
                            try
                            {


                                if (PreFmlId % 10000 == 1)
                                {
                                    Info(String.Format("正在处理 段：{0} 家族：{1}", dirNum, PreFmlId));
                                }
                                EnSplitLines(lines, dirNum);
                                lines.Clear();
                                lines.Add(tmpary);
                                PreFmlId = FmlId;

                            }
                            catch (Exception ex)
                            {
                                Error(string.Format("正在处理 段：{0} 家族：{1}\n错误信息：{2}", dirNum, PreFmlId, ex));
                                lines.Clear();
                                lines.Add(tmpary);
                                PreFmlId = FmlId;
                            }
                        }

                    }
                    if (lines.Count > 0)
                    {
                        Info(String.Format("正在处理 段：{0} 家族：{1}", dirNum, lines[0][0].ToString()));
                        EnSplitLines(lines, dirNum);
                    }                  
                }
#endregion
                tsp = DateTime.Now - start;
                Error("数据总数：" + ExIndex);               
                Error("用时：" + com.FormatTimeSpan(tsp));
                ExIndex = 0;

                CloseSearchEnterFile();
                lines.Clear();
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strLines"></param>
        /// <param name="tmpIndex"></param>
        /// <returns></returns>
        private bool EnSplitLines(List<string[]> strLines, int tmpIndex)
        {
            List<string[]> arysp = strLines;
            List<string[]> arytmp = new List<string[]>();
            List<string> lsttmp;

            foreach (var se in lstSearchEnter)
            {
                lsttmp = new List<string>();
                ///如果是唯一索引列
                if (se.Index == config.SerialIndex)
                {
                    continue;
                }

                //判断是否需要二次拆分
                if (!string.IsNullOrEmpty(se.Split))
                {
                    foreach (var tmp in arysp)
                    {
                        lsttmp.AddRange(tmp[se.Index].Split(se.Split.ToCharArray()).ToList<string>());
                    }
                    EnExSearchEnter(lsttmp, se, tmpIndex, (int)Convert.ToInt32(arysp[0][config.SerialIndex]));

                }
                else
                {

                    foreach (var tmp in arysp)
                    {
                        lsttmp.Add(tmp[se.Index]);
                    }
                    //不需要拆分                                     
                    EnExSearchEnter(lsttmp, se, tmpIndex, (int)Convert.ToInt32(arysp[0][config.SerialIndex]));
                }

            }

            return true;
        }

        /// <summary>
        /// 处理一条检索项的数据
        /// </summary>
        /// <param name="strData">一个家族检索项数据</param>
        /// <param name="se">检索项配置信息</param>
        /// <param name="Year">数据所在年份</param>
        /// <param name="SerialNo">公开号或者唯一的序列号</param>
        /// <returns></returns>        
        private bool EnExSearchEnter(List<string> strData, SearchEnter se, int Index, int SerialNo)
        {
            byte[] by;
            byte[] byserial = BitConverter.GetBytes(SerialNo);
            byte[] byfloat;
            strData = strData.Distinct<string>().ToList<string>();
            //需要切词
            if (se.WordSplit == WordSplitType.English)
            {
                List<string> OneWord = new List<string>();

                char fchar;
                string str;
                for (int i = 0; i < strData.Count; i++)
                {
                    str = strData[i].Trim();
                    //如果为空进行下次循环
                    if (string.IsNullOrEmpty(str))
                    {
                        continue;
                    }

                    //短句中的一字词
                    OneWord = WordSplit.getEnglistWordList(str);

                    if (se.SingleFile == true)
                    {
                        //写单词
                        for (int j = 0; j < OneWord.Count; j++)
                        {
                            by = Encoding.UTF8.GetBytes(OneWord[j].PadRight(16, ' '));
                            if (by.Length < 16)
                            {
                                Error(SerialNo + "\t" + OneWord[j].ToString());
                                continue;
                            }
                            filelist[Index.ToString() + se.Name].Write(byserial, 0, byserial.Length);
                            if (se.WordLocation == true)
                            {
                                byfloat = BitConverter.GetBytes((short)(i + 1));
                                filelist[Index.ToString() + se.Name].Write(byfloat, 0, 2);
                                byfloat = BitConverter.GetBytes((short)(j + 1));
                                filelist[Index.ToString() + se.Name].Write(byfloat, 0, 2);
                            }
                            filelist[Index.ToString() + se.Name].Write(by, 0, 16);
                        }
                    }
                    else
                    {
                        //写单词
                        for (int j = 0; j < OneWord.Count; j++)
                        {
                            fchar = OneWord[j][0];
                            if ((fchar >= '0' && fchar <= '9') || (fchar >= '０' && fchar <= '９'))
                            {
                                fchar = '0';
                            }
                            if (fchar == '_')
                            {
                                continue;
                            }

                            by = Encoding.UTF8.GetBytes(OneWord[j].PadRight(16, ' '));
                            if (by.Length < 16)
                            {
                                Error(SerialNo + "\t" + OneWord[j].ToString());
                                continue;

                            }
                            filelist[Index.ToString() + se.Name + "_" + fchar].Write(byserial, 0, byserial.Length);
                            if (se.WordLocation == true)
                            {
                                byfloat = BitConverter.GetBytes((short)(i + 1));
                                filelist[Index.ToString() + se.Name + "_" + fchar].Write(byfloat, 0, 2);
                                byfloat = BitConverter.GetBytes((short)(j + 1));
                                filelist[Index.ToString() + se.Name + "_" + fchar].Write(byfloat, 0, 2);
                            }
                            filelist[Index.ToString() + se.Name + "_" + fchar].Write(by, 0, 16);

                        }
                    }

                }

            }
            else
            {
                strData = strData.OrderBy(x => x).ToList<string>();
                foreach (string tmp in strData)
                {
                    if (string.IsNullOrEmpty(tmp.Trim()))
                    {
                        continue;
                    }
                    if (se.DataType.ToUpper() == "INT")
                    {
                        by = BitConverter.GetBytes(Convert.ToInt32(tmp));
                    }
                    else
                    {
                        by = Encoding.UTF8.GetBytes(tmp.ToUpper().PadRight(se.Length, ' '));
                    }
                    if (by.Length < se.Length)
                    {
                        Error(SerialNo + "\t" + tmp.ToString());
                        continue;
                    }
                    filelist[Index.ToString() + se.Name].Write(byserial, 0, byserial.Length);
                    filelist[Index.ToString() + se.Name].Write(by, 0, se.Length);
                }


                if (se.SubKey.Count > 0)
                {
                    foreach (SubSearchEnter sbenter in se.SubKey)
                    {
                        List<string> tmp = new List<string>();
                        foreach (string s in strData)
                        {
                            if (s.Length >= sbenter.Length)
                            {
                                tmp.Add(s.Substring(0, sbenter.Length));
                            }
                            else
                            {
                                tmp.Add(s);
                            }
                        }

                        tmp = tmp.OrderBy(x => x).ToList<string>();
                        foreach (string t in tmp)
                        {
                            if (string.IsNullOrEmpty(t.Trim()))
                            {
                                continue;
                            }
                            if (se.DataType.ToUpper() == "INT")
                            {
                                by = BitConverter.GetBytes(Convert.ToInt32(tmp));
                            }
                            else
                            {
                                by = Encoding.UTF8.GetBytes(t.ToUpper().PadRight(sbenter.Length, ' '));
                            }
                            if (by.Length < sbenter.Length)
                            {
                                Error(SerialNo + "\t" + tmp.ToString());
                                continue;

                            }
                            filelist[Index.ToString() + sbenter.Name].Write(byserial, 0, byserial.Length);
                            filelist[Index.ToString() + sbenter.Name].Write(by, 0, sbenter.Length);
                        }
                    }
                }
            }

            return true;
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            foreach (System.Collections.Generic.KeyValuePair<string, System.IO.FileStream> item in filelist)
            {
                item.Value.Close();
                item.Value.Dispose();
            }

        }

        #endregion        

        public void Split()
        {
           // System.Threading.Thread.Sleep(1000 * 10);

            if (Type == SearchDbType.Cn)
            {
                CnSplitDataFile(DirList);
            }
            else
            {
                EnSplitDataFile(DirList);
            }

            CloseSearchEnterFile();
            isDone = true;
        }
    }
}

