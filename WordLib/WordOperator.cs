using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Cpic.Cprs2010.Index;

namespace WordLib
{
    public class WordOperator
    {
        public static Dictionary<string, int> WordList = new Dictionary<string, int>();

        public static void Ini()
        {
            if (WordList.Count == 0)
            {
                lock (WordList)
                {
                    using (StreamReader sr = new StreamReader(System.Configuration.ConfigurationManager.AppSettings["WordFilePath"].ToString()))
                    {
                        while (!sr.EndOfStream)
                        {
                            string tmp = sr.ReadLine().Trim();
                            string[] arytmp = tmp.Split('\t');
                            string key = arytmp[0];
                            int value = Convert.ToInt32(arytmp[1]);
                            WordList.Add(key, value);
                        }
                    }
                }

            }
        }

        public static string GetWord(string strtmp)
        {
            Ini();

            List<string> Sentence = new List<string>();
            List<char> OneWord = new List<char>();
            List<char> tmpOneWord = new List<char>();
            List<string> TwoWord = new List<string>();
            List<string> ThreeWord = new List<string>();
            List<string> fourWord = new List<string>();
            List<string> fiveWord = new List<string>();

            //切成没有符号的短句
            Sentence = WordSplit.getSentenceList(strtmp);

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
                fourWord.AddRange(WordSplit.getFourWordList(tmpOneWord));
                fiveWord.AddRange(WordSplit.getFiveWordList(tmpOneWord));

            }
            string result = string.Empty;

            //先看5字词
            foreach (var s in fiveWord)
            {
                if (WordList.ContainsKey(s))
                    result += s + ";";
            }
            if (!string.IsNullOrEmpty(result))
            {
                return result.TrimEnd(';');
            }
            else
            {
                //再看4字词
                foreach (var s in fourWord)
                {
                    if (WordList.ContainsKey(s))
                    result += s + ";";
                }
                if (!string.IsNullOrEmpty(result))
                {
                    return result.TrimEnd(';');
                }
                else
                {
                    //再看3字词
                    foreach (var s in ThreeWord)
                    {
                        if (WordList.ContainsKey(s))
                        result += s + ";";
                    }
                    if (!string.IsNullOrEmpty(result))
                    {
                        return result.TrimEnd(';');
                    }
                    else
                    {

                        //最后看2字词
                        foreach (var s in TwoWord)
                        {
                            if (WordList.ContainsKey(s))
                            result += s + ";";
                        }
                        if (!string.IsNullOrEmpty(result))
                            return result.TrimEnd(';');
                        else
                            return string.Empty;
                    }

                }
            }


        }
    }
}
