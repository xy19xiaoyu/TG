using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Cpic.Cprs2010.Index
{
    public class WordSplit
    {
        private static readonly string regFuHao = "[\u0020-\u002f]|[\u003a-\u0040]|[\u005b-\u0060]|[\u007b-\u007e]|[\uff01-\uff0f]|[\uff1a-\uff20]|[\uff3b-\uff40]|[\uff5b-\uff5e]|\\s|\\n|、|。";
        private static readonly System.Text.RegularExpressions.RegexOptions options = (((System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace | System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                                | System.Text.RegularExpressions.RegexOptions.Multiline)
                                | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //private static readonly string regEnWord = "(?<word>[A-Za-z]{1}([A-Za-z]|\\d){0,15})";
        private static readonly string regEnWord = "(?<word>[A-Z0-9]{1,16})";
        #region 切句子
        public static List<string> getSentenceList(string str)
        {
            List<string> Sentence = new List<string>();
            //正则表达式 用来得到所有的1字词 以及英文单词 以及C# &nbsp; 等单词
            Regex reg = new Regex(regFuHao, options);
            Sentence = reg.Split(str).ToList<string>();
            return Sentence;

        }
        #endregion

        #region 一字词
        /// <summary>
        /// 切词 得到一个短句的1字词
        /// </summary>
        /// <param name="str">这个短句中不能包含标点符号 ，。？！空格 以便于用切出来的1字词进行组合生成2字词3字词</param>
        /// <returns></returns>
        public static List<char> getOneWordList(string str)
        {
            str = str.ToUpper();
            str = ToSBC(str);
            List<char> charlist = new List<char>();        
            charlist = str.ToCharArray().ToList(); ;
            return charlist;
        }
        #endregion

        #region 二字词
        /// <summary>
        /// 根据得到的一字词生成2字词
        /// </summary>
        /// <param name="lstOnWord">一字词列表</param>
        /// <returns></returns>
        public static List<string> getTwoWordList(List<char> lstOnWord)
        {
            List<string> TwoWord = new List<string>();
            for (int i = 0; i < lstOnWord.Count - 1; i++)
            {               
                TwoWord.Add(lstOnWord[i].ToString() + lstOnWord[i + 1].ToString());
            }
            return TwoWord;
        }
        #endregion

        #region 三字词
        /// <summary>
        /// 根据得到的一字词生成3字词
        /// </summary>
        /// <param name="lstOnWord">一字词列表</param>
        /// <returns></returns>
        public static List<string> getThreeWordList(List<char> lstOnWord)
        {
            List<string> ThreeWord = new List<string>();
            for (int i = 0; i < lstOnWord.Count - 2; i++)
            {
                //如果是数字开头的不进行组词              
                ThreeWord.Add(lstOnWord[i].ToString() + lstOnWord[i + 1].ToString() + lstOnWord[i + 2].ToString());
            }
            switch (lstOnWord.Count)
            {
                case 0:
                    break;
                case 1:
                    ThreeWord.Add(lstOnWord[lstOnWord.Count - 1].ToString().PadRight(3, '　'));
                    break;
                case 2:
                    ThreeWord.Add(lstOnWord[0].ToString() + lstOnWord[1].ToString() + "　");
                    ThreeWord.Add(lstOnWord[1].ToString().PadRight(3, '　'));
                    break;
                default:
                    ThreeWord.Add(lstOnWord[lstOnWord.Count - 2] + lstOnWord[lstOnWord.Count - 1].ToString() + "　");
                    ThreeWord.Add(lstOnWord[lstOnWord.Count - 1].ToString().PadRight(3, '　'));
                    break;
            }          
            
            return ThreeWord;
        }

        /// <summary>
        /// 根据得到的一字词生成3字词
        /// </summary>
        /// <param name="lstOnWord">一字词列表</param>
        /// <returns></returns>
        public static List<string> getThreeWordList1(List<char> lstOnWord)
        {
            List<string> ThreeWord = new List<string>();
            for (int i = 0; i < lstOnWord.Count - 2; i++)
            {
                //如果是数字开头的不进行组词              
                ThreeWord.Add(lstOnWord[i].ToString() + lstOnWord[i + 1].ToString() + lstOnWord[i + 2].ToString());
            }          
            return ThreeWord;
        }
        #endregion

        #region 四字词
        /// <summary>
        /// 根据得到的一字词生成3字词
        /// </summary>
        /// <param name="lstOnWord">一字词列表</param>
        /// <returns></returns>
        public static List<string> getFourWordList(List<char> lstOnWord)
        {
            List<string> FourWord = new List<string>();
            for (int i = 0; i < lstOnWord.Count - 3; i++)
            {
                //如果是数字开头的不进行组词              
                FourWord.Add(string.Format("{0}{1}{2}{3}", lstOnWord[i], lstOnWord[i + 1], lstOnWord[i + 2], lstOnWord[i + 3]));
            }

            return FourWord;
        }
        #endregion

        #region 五字词
        /// <summary>
        /// 根据得到的一字词生成3字词
        /// </summary>
        /// <param name="lstOnWord">一字词列表</param>
        /// <returns></returns>
        public static List<string> getFiveWordList(List<char> lstOnWord)
        {
            List<string> FiveWord = new List<string>();
            for (int i = 0; i < lstOnWord.Count - 4; i++)
            {
                //如果是数字开头的不进行组词              
                FiveWord.Add(string.Format("{0}{1}{2}{3}{4}", lstOnWord[i], lstOnWord[i + 1], lstOnWord[i + 2], lstOnWord[i + 3], lstOnWord[i + 4]));
            }


            return FiveWord;
        }
        #endregion

        #region 英文切词
        /// <summary>
        /// 切词 得到一个英文短句或文章的英文切词
        /// </summary>
        /// <param name="str">这个短句中不能包含标点符号 ，。？！空格 以便于用切出来的1字词进行组合生成2字词3字词</param>
        /// <returns></returns>
        public static List<string> getEnglistWordList(string str)
        {
            str = str.ToUpper();
            List<string> Word = new List<string>();
            System.Text.RegularExpressions.RegexOptions options = (((System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace | System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                        | System.Text.RegularExpressions.RegexOptions.Multiline)
                        | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //正则表达式 用来得到所有的1字词 以及英文单词 以及C# &nbsp; 等单词
            Regex reg = new Regex(regEnWord, options);
            MatchCollection matches = reg.Matches(str);
            // Report on each match.
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                Word.Add(groups["word"].Value.ToUpper());
            }

            return Word;

        }
        #endregion

        #region 转全角的函数
        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>        
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)

                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        #endregion  

        #region 转半角的函数
        /// <summary>
        /// 转半角的函数(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;

                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)

                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

    }
}