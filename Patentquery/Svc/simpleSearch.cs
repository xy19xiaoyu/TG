using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Patentquery.Svc
{
    public class simpleSearch
    {

        //------------- 中文分类
        //一、	数字字母混合类
        //1、	IPC（IC）
        //■	带”/”： 
        //规则：1位字母+2位数字+1位字母+0个或者多个空格+1到2位数字+”/”+0到2位数字
        //处理：识别出来送引擎时候，处理的时候，中文空格需要将空格换成0；英文多个空格换成一个空格；反斜杠一起送引擎；
        //例子：比如对于A01B 43/00，对于中文引擎，应该送A01B043/00/IC，对于英文引擎，应该送A01B 43/00/IC。
        //■	不带”/”:
        //规则： 1位字母+2位数字+1位字母+“0”+1到2位数字，至少3位。
        //2、	优先权（PR）
        //规则： 2位字母+2位数字+0到10位数字；至少4位。
        //送所有汉字入口，最多20个汉字
        //二、	纯数字类
        //3、	公开日、公告日、申请日、申请号（AD,PD,GD,AN） 
        //为6位和8位时，并且以19、20开头才可检索，否则不送引擎；超过8位自动截取
        //4、	申请号（AN）
        //只有达到4位已上并且以19、20开头才可检索，否则不送引擎；超过12位自动截取；
        //5、	公告号（GN）：
        //只有达到2位已上并且以1、2、3开头才可检索，否则不送引擎；超过9位自动截取；
        //6、	公开号（PN）
        //只有达到2位已上并且以1、2开头才可检索，否则不送引擎；超过9位自动截取；
        //三、	其他类
        //7、	发明人、名称、申请人、主权利要求、摘要（AB,CL,TI,IN,PA,）
        //凡不属于以上类型的，都归入其它类入口

        //------------- 英文分类
        //一、	数字字母混合类
        //1、	IPC（IC、EC）
        //■	带”/”： 
        //规则：1位字母+2位数字+1位字母+0个或者多个空格+1到2位数字+”/”+0到2位数字
        //处理：识别出来送引擎时候，处理的时候，中文空格需要将空格换成0；英文多个空格换成一个空格；反斜杠一起送引擎；
        //例子：比如对于A01B 43/00，对于中文引擎，应该送A01B043/00/IC，对于英文引擎，应该送A01B 43/00/IC。
        //■	不带”/”:
        //规则： 1位字母+2位数字+1位字母+“0”+1到2位数字，至少3位。
        //2、	优先权（PR）
        //规则： 2位字母+2位数字+0到10位数字；至少4位。
        //送所有汉字入口，最多20个汉字
        //二、	纯数字类
        //3、	公开日、公告日、申请日、申请号（AD,PD, AN） 
        //为6位和8位时，并且以19、20开头才可检索，否则不送引擎；超过8位自动截取
        //4、	申请号（AN）
        //只有达到4位已上并且以19、20开头才可检索，否则不送引擎；超过12位自动截取；
        //5、	公开号（PN）
        //只有达到2位已上并且以1、2开头才可检索，否则不送引擎；超过9位自动截取；
        //三、	其他类
        //6、	发明人、名称、申请人、摘要（AB,TI,IN,PA,）
        //凡不属于以上类型的，都归入其它类入口

        //------------- 忽略的检索入口
        //中文专利以下字段不用检索： TX, CO, DZ, CT, AG,
        //世界专利以下字段不用检索： CT,EC,

        //------------- 处理步骤：
        //1. 清除除”/”之外所有的特殊字符(clearSpecialChar)：仅保留数字、字母、汉字、空格和/， 对于连续的/，仅保留一个/； 
        //2. 如果仅为1-2位数字或者英文字母，仅送AB入口(shortLen())；
        //3. 切词： 按照空格切分输入字符串，生成检索项数组Words；
        //   1、处理“/”(extractIpc)： 
        //        a)	如果包含有ipc，则根据规则，摘取ipc，置入Words数组；
        //        b)	如果不包含ipc，则去掉/；
        //   2、 按照空格切词，置入Words数组；
        //4. 针对单个检索项，生成检索式。
        //    判断数组中每个检索项，判断类型(getType)：
        //    a)	如果不属于其它类，则根据类型和检索项，生成检索式(generateQueryBySingleWord)； 
        //    b)	如果属于其它类，则首先从字符串中进行依次摘取连续的数字字母混合串、数字串进行判断（按照PR、IPC、（AD,PD,GD,AN）、AN、GN、PN、（AB,CL,TI,IN,PA）的顺序），
        //        然后根据分类及摘取的检索项生成检索式（生成检索式时，以+号连接摘取的各项）；
        //5. 针对检索项数组，合成检索式：第4步生成的检索式按照*号连接(getAnalysisQuery)；
        //6. 送检索；
        //7. 根据检索结果，将各项全局变量还原（resetGlobalParams:errorTips ）；

        //------------- 优化处理
        //注意： 为了减轻引擎的压力，提供两个参数限制检索式长度：
        //1、 经过步骤1处理后，长度最长限制为40个字符
        //2、 检索项数组最长为3； 
        //3、 针对单个检索项生成的检索式中+号最多连接8个子项。即，检索式总长最多24项。

        //------------- 命名
        //1. OriginQuery 用户输入的检索式
        //2. Query: 清除特殊字符后的检索式
        //3. ClearQuery: 检索式过长，切割后的检索式
        //4. Words: 步骤4切词后生成的数组
        //5. Word:  数组中的单项
        //6. itemsOfWord: 单项摘取后中的子项数组
        //7. itemOfWord: 单项摘取后中的子项
        //*/

        /*---------------------------------------------Global params begin---------------------------------------------*/
        /*--------------性能参数--------------*/
        int maxQueryLength = 20; // 30;
        int maxWordsLength = 3;
        int maxItemsOfWordLength = 8;
        int maxOtherEntranceLength = 20;
        /*--------------错误提示--------------*/
        public string errorTips = "";
        string inputLengthExceedTips = "";
        string keywordsNumberExceedTips = "";
        string useTips = "";

        public simpleSearch()
        {
            string inputLengthExceedTips = "输入长度超过" + maxQueryLength + "个字词，后面字词已被忽略。";
            string keywordsNumberExceedTips = "输入关键字个数超过" + maxWordsLength + "，后面的关键字已被忽略。";
            string useTips = "请您在不同的检索关键字间加上空格";
        }
        /*--------------检索入口: 检索入口之间要以,号分隔--------------*/
        // 中文各类检索入口，共6类
        string[] cnEntrances = new string[]{
    "IC",
    "PR",
    "AD,PD,GD,AN",
    "AN",
    "GN",
    "PN",
    "AB,CL,TI,IN,PA,AT,DZ"
};
        // 英文各类检索入口
        string[] wdEntrances = new string[]{
    "IC",
    "PR,AN,PN",
    "AD,PD,AN",
    "AB,TI,IN,PA",
    "PN"
};

        /*---------------------------------------------Global params end---------------------------------------------*/

        public string getClearQuery(string _strtype, string strScontent)
        {
            string strOriginalQuery = strScontent.Trim();
            if (strOriginalQuery == "" || strOriginalQuery == useTips) { return ""; }

            var strQuery = clearSpecialChar(strOriginalQuery, _strtype);
            string strClearQuery = strQuery;
            if (strQuery.Length > maxQueryLength)
            { // 检索式超长
                errorTips += inputLengthExceedTips;
                strClearQuery = strQuery.Substring(0, maxQueryLength);
            }
            return strClearQuery;

        }

        public string clearSpecialChar(string strQuery, string type)
        {
            Regex reg = null;
            if (type == "cn")
            {
                reg = new Regex(@"[^\u4e00-\u9fa5aa-zA-Z0-9\/\s\.]");

            }
            else if (type == "wd")
            {
                reg = new Regex(@"[^a-zA-Z0-9\/\s]");
            }
            if (reg == null) return strQuery;

            string _strQuery = reg.Replace(strQuery, "");

            _strQuery = Regex.Replace(_strQuery, @"(\/)+", @"\/");
            return _strQuery;
        }



        // 处理输入太短的情况
        public bool shortLen(string strQuery)
        {

            strQuery = Regex.Replace(strQuery, @"(\/)+", "");
            if (Regex.IsMatch(strQuery, @"^\w{0,2}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        // 将ipc/前的空格换成0，以便进行空格切分
        public string transeformIpc(string strQuery)
        {
            string strLocalQuery = strQuery;
            //var reg = /[a-zA-Z]\d{2}[a-zA-Z](\s*)(\d{1,2})\/\d{0,2}/g;
            //string ipcRes="";
            //while ((ipcRes = reg.exec(strQuery)) != null) {
            //    var itemIpc = ipcRes[0];
            //    if (RegExp.$2.length == 1) itemIpc = ipcRes[0].replace(RegExp.$1, "00");  // “/”前为1位数字，将数字前的空格替换成为2个0
            //    if (RegExp.$2.length == 2) itemIpc = ipcRes[0].replace(RegExp.$1, "0");   // “/”前为2位数字，将数字前的空格替换成为1个0
            //    strLocalQuery = strLocalQuery.replace(ipcRes[0], itemIpc);
            //}
            return strLocalQuery;
        }

        // 将ipc/前的空格换成0，以便进行空格切分
        public string transeformIpcBack(string strQuery, string type)
        {
            var strLocalQuery = strQuery;
            //if (type == "wd") { // 世界专利检索时，中间空格要去掉
            //    Regex reg =new Regex(@"/([a-zA-Z]\d{2}[a-zA-Z])(0+)(\d{1,2})\/\d{0,2}/g");
            //    string  ipcRes;
            //    Match regrs=reg.Match(strQuery);
            //    while (regrs.Success) {
            //        var itemIpc =regrs.Value;
            //        if (RegExp.$3.length == 1) itemIpc = ipcRes[0].replace(RegExp.$1 + RegExp.$2, RegExp.$1 + "  ");  // “/”前为1位数字，将数字前的空格替换成为2个空格
            //        if (RegExp.$3.length == 2) itemIpc = ipcRes[0].replace(RegExp.$1 + RegExp.$2, RegExp.$1 + " ");   // “/”前为2位数字，将数字前的空格替换成为1个空格
            //        strLocalQuery = strLocalQuery.replace(ipcRes[0], itemIpc);
            //    }
            //}
            return strLocalQuery;
        }

        public string[] getCutWords(string strTranseformIpc)
        {
            // 根据空格切词
            string[] words = strTranseformIpc.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return words;
        }

        private bool isIC(string keyword)
        {
            Regex regIpc = new Regex(@"^[a-zA-Z]\d{2}[a-zA-Z](\d{3}\/\d{0,2}){0,1}$");
            if (regIpc.IsMatch(keyword)) return true;
            else return false;
        }
        // 规则： 2位字母+2位数字+0到10位数字；至少4位。
        private bool isPR(string keyword)
        {
            Regex regPR = new Regex(@"^[a-zA-Z]{2}\d{2}\d{0,10}$");
            if (regPR.IsMatch(keyword)) return true;
            else return false;
        }
        private bool isDate(string keyword)
        {
            Regex reg = new Regex(@"^(\d{4})(\d{2})?(\d{2})?$");
            Match RegRs = reg.Match(keyword);

            if (RegRs.Success)
            {
                int intYear = int.Parse(RegRs.Groups[1].Value);
                // 判断年份
                if (intYear < 1800 || intYear > 2500) return false;
                if (!string.IsNullOrEmpty(RegRs.Groups[2].Value))
                {
                    int intMonth = int.Parse(RegRs.Groups[2].Value);

                    // 判断月份
                    if (intMonth > 12 || intMonth < 1) return false;

                    if (!string.IsNullOrEmpty(RegRs.Groups[3].Value))
                    {
                        int intDay = int.Parse(RegRs.Groups[3].Value);
                        // 判断日期
                        if (intDay < 1 || intDay > DateTime.DaysInMonth(intYear, intMonth)) return false;
                    }
                }
                return true;

            }
            else { return false; }
        }

        //输入 ZL2010 2 0643383.1 或ZL201020643383.1 、CN201601818U检索为零。
        //而这对初学者而言是很可能的输入方式，而不会删除”ZL”、“CN”和空格。
        //单一入口项(AN/PN)
        private string[] analyFullSingle_Enter(string keyword)
        {
            string[] itemsOfWord = new string[] { "", "", "", "", "", "" };

            keyword = keyword.Replace(" ", "");
            Regex regFullAn12 = new Regex(@"^(ZL|CN)((19|20)\d{2}[12389]\d{7})\.?[\d|X|x]?$", RegexOptions.IgnoreCase);
            Regex regFullAn8 = new Regex(@"^(ZL|CN)([089]\d[12389]\d{5})\.?[\d|X|x]?$", RegexOptions.IgnoreCase);
            Regex regFullPNGN = new Regex(@"^(CN)?((1|2|3|8|9)(\d{8}|\d{6}))[A-Z]?$", RegexOptions.IgnoreCase);

            Match rsReg;
            //var arr = new Array();

            if (regFullAn12.IsMatch(keyword))
            {
                rsReg = regFullAn12.Match(keyword);
                //arr.push(rsReg.Groups[2].Value);
                itemsOfWord[3] = rsReg.Groups[2].Value;
            }
            else if (regFullAn8.IsMatch(keyword))
            {
                rsReg = regFullAn8.Match(keyword);
                //arr.push(getApplyNo(rsReg.Groups[2].Value));
                itemsOfWord[3] = rsReg.Groups[2].Value;
            }
            else if (regFullPNGN.IsMatch(keyword))
            {
                rsReg = regFullPNGN.Match(keyword);
                //arr.push(rsReg.Groups[2].Value);
                itemsOfWord[4] = rsReg.Groups[2].Value;
                itemsOfWord[5] = rsReg.Groups[2].Value;
            }
            else
            {
                itemsOfWord = null;
            }

            return itemsOfWord;
        }


        //单一入口项(PN)
        private string[] analyFullSingle_Enter_En(string keyword)
        {
            string[] itemsOfWord = new string[] { "", "", "", "", "", "" };
            keyword = keyword.Replace(" ", "");
            Regex regFullPN = new Regex(@"^([a-z]{2}[0-9]{2,}[0-9A-Z]+[A-Z].?)$", RegexOptions.IgnoreCase);
            Match RegMs;
            //var arr = new Array();
            if (regFullPN.IsMatch(keyword))
            {
                RegMs = regFullPN.Match(keyword);
                //arr.push(rsReg[1]);
                itemsOfWord[4] = RegMs.Groups[1].Value;
            }
            else
            {
                itemsOfWord = null;
            }
            return itemsOfWord;
        }


        // 中文的三个数字入口
        private bool isANGNPN(string keyword)
        {

            string apNo = getApplyNo(keyword);
            Regex regFullAN = new Regex(@"^(19|20)[123]\d{9}\.?[\d|X|x]?$");
            if (apNo != null && regFullAN.IsMatch(apNo))
            {
                return false;
            }

            //var reg = /^\d{2,}(\.\d)$/;
            Regex reg = new Regex(@"^\d{4,}$");
            if (reg.IsMatch(keyword))
            {
                return true;
            }
            return false;
        }

        private bool isAN(string keyword)
        {
            var apNo = getApplyNo(keyword);
            if (apNo == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool isGN(string keyword)
        {
            Regex reg = new Regex(@" ^(1|2|3)\d\d{0,7}$");
            if (reg.IsMatch(keyword)) { return true; }

            else { return false; }
        }
        private bool isPN(string keyword)
        {
            Regex reg = new Regex(@" ^(1|2)\d\d{0,7}$");
            if (reg.IsMatch(keyword)) { return true; }
            else { return false; }
        }
        private bool isChinese(string keyword)
        {
            Regex regChinese = new Regex(@"^[\u4e00-\u9fa5]{1,}$");
            if (regChinese.IsMatch(keyword))
            {
                return true;
            }
            return false;
        }
        // 英文的pr、an、pn三个入口采用同一个规则
        private bool isPRANPN(string keyword)
        {
            Regex reg = new Regex(@"^[A-Za-z]{2}\d{2,12}$");
            if (reg.IsMatch(keyword))
            {
                return true;
            }
            return false;
        }
        private bool isEnglish(string keyword)
        {
            Regex regEnglish = new Regex(@"^[A-Za-z]{1,}$");   // 纯英文入口
            if (regEnglish.IsMatch(keyword))
            {
                return true;
            }
            return false;
        }


        private string[] getIC(string keyword)
        {

            List<string> keywords = new List<string>();

            Regex reg = new Regex(@"[a-zA-Z]\d{2}[a-zA-Z](\d{3}\/\d{0,2}){0,1}");
            Match res = reg.Match(keyword);
            while (res.Success)
            {
                keywords.Add(res.Groups[0].Value);
                res = res.NextMatch();
            }
            if (keywords.Count != 0) return keywords.ToArray();
            else return null;
        }

        private string[] getPR(string keyword)
        {
            List<string> keywords = new List<string>();
            Regex reg = new Regex(@"[a-zA-Z]{2}\d{2}\d{0,10}");
            Match res = reg.Match(keyword);
            while (res.Success)
            {
                keywords.Add(res.Groups[0].Value);
                res = res.NextMatch();
            }
            if (keywords.Count != 0) return keywords.ToArray();
            else return null;

        }
        // 英文的pr、an、pn三个入口采用同一个规则
        private string[] getPRANPN(string keyword)
        {
            List<string> keywords = new List<string>();
            Regex reg = new Regex(@"[A-Za-z]{2}\d{2,12}");
            Match res = reg.Match(keyword);
            while (res.Success)
            {
                keywords.Add(res.Groups[0].Value);
                res = res.NextMatch();
            }
            if (keywords.Count != 0) return keywords.ToArray();
            else return null;
        }
        private string[] getDate(string keyword)
        {
            List<string> keywords = new List<string>();
            Regex reg = new Regex(@"(1|2)(\d{3})(\d{2})(\d{2})?");
            Match res = reg.Match(keyword);
            while (res.Success)
            {
                if (isDate(res.Groups[0].Value))
                    keywords.Add(res.Groups[0].Value);
                res = res.NextMatch();
            }
            if (keywords.Count != 0) return keywords.ToArray();
            else return null;

        }

        private string[] getANGNPN(string keyword)
        {
            List<string> keywords = new List<string>();
            Regex reg = new Regex(@"\d{2,12}");
            Match res = reg.Match(keyword);
            while (res.Success)
            {
                keywords.Add(res.Groups[0].Value);
                res = res.NextMatch();
            }
            if (keywords.Count != 0) return keywords.ToArray();
            else return null;
        }




        // 中文AN，验证并修改，不循环获取
        private string[] getAN(string keyword)
        {
            List<string> apNos = new List<string>();
            Regex reg = new Regex(@"\d{2,12}(\.\d)*");
            Match res = reg.Match(keyword);
            //while ((res = reg.exec(keyword)) != null) { apNos.push(res[0]); }
            if (res.Success)
            {
                apNos.Add(res.Groups[0].Value);
            }
            if (apNos.Count == 0) return null;

            // 进一步获取
            List<string> keywords = new List<string>();
            for (var i = 0; i < apNos.Count; i++)
            {
                var _apNo = getApplyNo(apNos[i]);
                if (_apNo != null)
                {
                    keywords.Add(_apNo);
                }
            }

            if (keywords.Count == 0)
            {
                return null;
            }
            else
            {
                return keywords.ToArray();
            }
        }
        // 验证和修改申请号
        private string getApplyNo(string apNo)
        {
            try
            {
                string _apNo = apNo;
                string year1 = apNo.Substring(0, 2);
                if (int.Parse(year1) > 50)
                {
                    _apNo = "19" + _apNo;
                    if (_apNo.Length > 5)
                        _apNo = _apNo.Substring(0, 5) + "00" + _apNo.Substring(5);
                }
                var year2 = apNo.Substring(0, 1);
                if (year2 == "0")
                {
                    _apNo = "20" + _apNo;
                    if (_apNo.Length > 5)
                        _apNo = _apNo.Substring(0, 5) + "00" + _apNo.Substring(5);
                }
                Regex reg = new Regex(@"^\d{4,12}(\.[\d|X|x])*$");
                Regex regFullAN = new Regex(@"^\d{12}\.?[\d|X|x]?$");
                if (!regFullAN.IsMatch(_apNo))
                {
                    if (!reg.IsMatch(_apNo))
                    {
                        return null;
                    }
                }
                return _apNo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // 中文GN，不循环获取
        private string[] getGN(string keyword)
        {
            List<string> keywords = new List<string>();
            Regex reg = new Regex(@"(1|2|3)\d\d{0,7}");
            Match res = reg.Match(keyword);
            //while ((res = reg.exec(keyword)) != null) { apNos.push(res[0]); }
            if (res.Success)
            {
                //keywords.push(res[0]); 
                keywords.Add(res.Groups[0].Value);
            }
            if (keywords.Count != 0) return keywords.ToArray();
            else return null;
        }
        // 中文PN，不循环获取
        private string[] getPN(string keyword)
        {
            List<string> keywords = new List<string>();
            Regex reg = new Regex(@"(1|2)\d\d{0,7}");
            Match res = reg.Match(keyword);
            //while ((res = reg.exec(keyword)) != null) { apNos.push(res[0]); }
            if (res.Success)
            {
                //keywords.push(res[0]); 
                keywords.Add(res.Groups[0].Value);
            }
            if (keywords.Count != 0) return keywords.ToArray();
            else return null;

        }
        private string[] getChinese(string keyword)
        {
            List<string> keywords = new List<string>();
            Regex regChinese = new Regex(@"[\u4e00-\u9fa5]{1,}");
            Match res = regChinese.Match(keyword);

            while (res.Success)
            {
                //keywords.push(res[0]); 
                keywords.Add(res.Groups[0].Value);

                res = res.NextMatch();
            }
            if (keywords.Count != 0) return keywords.ToArray();
            else return null;
        }
        private string[] getOther(string keyword)
        {
            List<string> keywords = new List<string>();
            Regex regEnglish = new Regex(@"[0-9A-Za-z]{2,}");
            Match res = regEnglish.Match(keyword);
            while (res.Success)
            {
                //keywords.push(res[0]); 
                keywords.Add(res.Groups[0].Value);
                res = res.NextMatch();
            }
            if (keywords.Count != 0) return keywords.ToArray();
            else return null;
        }

        // 将数组中的字符串从给定字符串中替换成空格
        private string keywordgetridofitems(string keyword, string[] items)
        {
            for (var i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                    keyword = keyword.Replace(items[i], " ");
            }
            return keyword;
        }



        public int gettype(string keyword, string type)
        {
            if (type == "cn")
            {
                if (isIC(keyword)) return 0;
                else if (isPR(keyword)) return 1;
                else if (isDate(keyword)) return 2;
                else if (isANGNPN(keyword)) return 3;
                else if (isPN(keyword)) return 3;
                else if (isGN(keyword)) return 3;
                else if (isAN(keyword)) return 4;
                else if (isChinese(keyword) || isEnglish(keyword)) return 6;
                else return -1;
            }
            else if (type == "wd")
            {
                if (isIC(keyword)) return 0;
                else if (isAN(keyword)) return 4;
                else if (isPRANPN(keyword)) return 1;
                else if (isDate(keyword)) return 2;
                else if (isEnglish(keyword)) return 3;
                else return -1;
            }
            else
            {
                return -1;
            }

        }

        public List<string[]> extractItemsFromWord(string keyword, string srctype)
        {
            List<string[]> itemsOfWord = new List<string[]>();
            //string[] itemsOfWord = new string[6]; // 该数组的单个元素仍然还是数组
            itemsOfWord.Add(null);
            itemsOfWord.Add(null);
            itemsOfWord.Add(null);
            itemsOfWord.Add(null);
            itemsOfWord.Add(null);
            itemsOfWord.Add(null);
            itemsOfWord.Add(null);

            if (srctype == "cn")
            {
                // 首先获取an，然后获取日期
                // 在数字匹配结束的地方再去掉an
                itemsOfWord[0] = getIC(keyword);
                if (itemsOfWord[0] != null) { keyword = keywordgetridofitems(keyword, itemsOfWord[0]); }

                itemsOfWord[1] = getPR(keyword);
                if (itemsOfWord[1] != null) { keyword = keywordgetridofitems(keyword, itemsOfWord[1]); }

                itemsOfWord[3] = getAN(keyword);

                itemsOfWord[2] = getDate(keyword);
                if (itemsOfWord[2] != null) { keyword = keywordgetridofitems(keyword, itemsOfWord[2]); }

                itemsOfWord[4] = getGN(keyword);
                if (itemsOfWord[4] != null) { keyword = keywordgetridofitems(keyword, itemsOfWord[4]); }
                itemsOfWord[5] = getPN(keyword);
                if (itemsOfWord[5] != null) { keyword = keywordgetridofitems(keyword, itemsOfWord[5]); }

                if (itemsOfWord[3] != null) { keyword = keywordgetridofitems(keyword, itemsOfWord[3]); }

                itemsOfWord[6] = getChinese(keyword);
                if (itemsOfWord[6] != null)
                {
                    keyword = keywordgetridofitems(keyword, itemsOfWord[6]);
                    itemsOfWord[6] = itemsOfWord[6].Concat(getOther(keyword)).ToArray();
                }
                else itemsOfWord[6] = getOther(keyword);
                if (itemsOfWord[6] != null) { keyword = keywordgetridofitems(keyword, itemsOfWord[6]); }


            }
            else if (srctype == "wd")
            {
                itemsOfWord[0] = getIC(keyword);
                if (itemsOfWord[0] != null) { keyword = keywordgetridofitems(keyword, itemsOfWord[0]); }

                itemsOfWord[1] = getPRANPN(keyword);
                if (itemsOfWord[1] != null) { keyword = keywordgetridofitems(keyword, itemsOfWord[1]); }

                itemsOfWord[2] = getDate(keyword);
                if (itemsOfWord[2] != null) { keyword = keywordgetridofitems(keyword, itemsOfWord[2]); }

                itemsOfWord[3] = getOther(keyword);
                if (itemsOfWord[3] != null) { keyword = keywordgetridofitems(keyword, itemsOfWord[3]); }
            }
            return itemsOfWord;
        }

        private void reduceItemsOfWord(string[] itemsOfWord)
        {
            //Array tempItemsOfWord = new Array();
            var length = 0;
            for (var i = 0; i < itemsOfWord.Length; i++)
            {
                var items = itemsOfWord[i];
                length += items.Length;
            }
        }


        private string combileItemsAndEntrance(string[] items, string entrance)
        {
            string query = "";
            for (var i = 0; i < items.Length; i++)
            {
                if (items[i] == "" || items[i] == null) continue;
                if (i == 0)
                {
                    query = query + items[i] + "/" + entrance;
                }
                else
                {
                    query = query + "+" + items[i] + "/" + entrance;
                }
            }
            return query;
        }

        //获取检索式
        private string analysisWord(string keyword, string srctype)
        {
            List<string[]> itemsOfWord = new List<string[]>();   //  new Array("", "", "", "", "", "");
            itemsOfWord.Add(null);
            itemsOfWord.Add(null);
            itemsOfWord.Add(null);
            itemsOfWord.Add(null);
            itemsOfWord.Add(null);
            itemsOfWord.Add(null);
            itemsOfWord.Add(null);

            string strWordQuery = "";

            if (srctype == "cn")
            {
                itemsOfWord[0] = analyFullSingle_Enter(keyword);
            }
            else
            {
                itemsOfWord[0] = analyFullSingle_Enter_En(keyword);
            }

            if (itemsOfWord[0] == null)
            {
                //itemsOfWord = new string[] { "", "", "", "", "", "" };
                var type = gettype(keyword, srctype);
                if (type != -1)
                {  // 该检索项为某个纯类
                    if (type == 3 && srctype == "cn")
                    { // 对于中文的三个检索入口AN,GN,PN，需要进一步判断后加入数组
                        // AN
                        var arr3 = getAN(keyword);
                        if (arr3 != null)
                        {
                            itemsOfWord[3] = arr3;
                        }
                        // GN
                        var arr4 = getGN(keyword);
                        if (arr4 != null)
                        {
                            itemsOfWord[4] = arr4;
                        }
                        // PN
                        var arr5 = getPN(keyword);
                        if (arr5 != null)
                        {
                            itemsOfWord[5] = arr5;
                        }
                    }
                    else if (type == 4)
                    {//新增对申请号的精确检索
                        var arr3 = getAN(keyword);
                        if (arr3 != null)
                        {
                            itemsOfWord[3] = arr3;
                        }
                    }
                    else
                    {
                        var arr = new string[] { "" };
                        arr[0] = keyword;
                        itemsOfWord[type] = arr;
                    }
                }
                else
                {
                    // 该检索项为混合类，需要进一步摘取
                    itemsOfWord = extractItemsFromWord(keyword, srctype);
                }
            }


            var arrEntrace = srctype == "cn" ? cnEntrances : wdEntrances;
            for (var i = 0; i < arrEntrace.Length; i++)
            {
                if (itemsOfWord[i] != null && itemsOfWord[i][0] != "")
                {
                    // 切割检索入口
                    string[] entrances;
                    if (srctype == "cn") entrances = cnEntrances[i].Split(',');
                    else entrances = wdEntrances[i].Split(',');
                    // 合成检索式
                    for (var j = 0; j < entrances.Length; j++)
                    {
                        var itemQuery = combileItemsAndEntrance(itemsOfWord[i], entrances[j]);
                        if (itemQuery == "") continue;
                        if (strWordQuery == "")
                            strWordQuery = strWordQuery + "" + itemQuery + "";
                        else
                            strWordQuery = strWordQuery + "+" + itemQuery + "";
                    }
                }
            }

            // 检索式单项超长处理，保留检索项最长的项
            var items = strWordQuery.Split('+');
            if (items.Length > maxItemsOfWordLength)
            {
                var arrTemp = items.OrderBy(p => p.Length).ToArray();
                strWordQuery = arrTemp[0];
                for (var i = 1; i < maxItemsOfWordLength; i++)
                {
                    strWordQuery = strWordQuery + "+" + arrTemp[i];
                }
            }

            // 将ipc转换回去
            strWordQuery = transeformIpcBack(strWordQuery, srctype);
            return strWordQuery;
        }

        private int sortLength(string a, string b)
        {
            return b.Length - a.Length;
        }

        //Array.prototype.delRepeat = function () {
        //    var newArray = [];
        //    var provisionalTable = {};
        //    for (var i = 0, item; (item = this[i]) != null; i++) {
        //        if (!provisionalTable[item]) {
        //            newArray.push(item);
        //            provisionalTable[item] = true;
        //        }
        //    }
        //    return newArray;
        //}

        private string getAnalysisQuery(string strClearQuery, string type)
        {
            // 转换带/的ipc前的空格为0
            string strTranseformIpc = transeformIpc(strClearQuery);
            // 切词
            string[] Arrykeywords = getCutWords(strTranseformIpc);
            // 去重
            List<string> keywords = new List<string>();
            foreach (string strItem in Arrykeywords)
            {
                if (keywords.Contains(strItem))
                {
                    continue;
                }
                keywords.Add(strItem);
            }

            // 检索关键字过多
            if (keywords.Count > maxWordsLength)
            {
                errorTips += keywordsNumberExceedTips;

                keywords.RemoveRange(maxWordsLength, keywords.Count - maxWordsLength);
            }
            // 处理数组中的每个单项
            var wordQuery = "";
            foreach (string strItem in keywords)
            {
                string word = analysisWord(strItem, type);
                if (!string.IsNullOrEmpty(word))
                {
                    if (wordQuery == "")
                    {
                        wordQuery = wordQuery + "(" + word + ")";
                    }
                    else
                    {
                        wordQuery = wordQuery + "*(" + word + ")";
                    }
                }
            }
            return wordQuery;
        }

        private string getOneItemQuery(string strTxt, string dbType)
        {
            string strUserInputTxt = strTxt;
            strUserInputTxt = strUserInputTxt.Replace("“", "\"");
            strUserInputTxt = strUserInputTxt.Replace("”", "\"");
            string strRs = "";
            if (strUserInputTxt.EndsWith("\"") && strUserInputTxt.StartsWith("\""))
            {
                strRs = strUserInputTxt.Trim('\"'); // strUserInputTxt.Substring(1, strUserInputTxt.Length - 1);
                strRs = Regex.Replace(strRs, @"[-+*\/]", " ");
                if (dbType.ToUpper() == "CN")
                {
                    strRs = strRs + "/TI+" + strRs + "/AB+" + strRs + "/CL+" + strRs + "/IN+" + strRs + "/PA";
                }
                else
                {
                    strRs = strRs + "/TI+" + strRs + "/AB+" + strRs + "/IN+" + strRs + "/PA";
                }
            }

            return strRs;
        }

        ///dbType[cn|wd]
        public string simpleSearchNew(string strDbTp, string strShTxt)
        {
            string strRs = "";

            // 获取检索式
            string dbType = strDbTp.ToLower();
            if (dbType == "en")
            {
                dbType = "wd";
            }
            string strClearQuery = getOneItemQuery(strShTxt, dbType);
            string strFinalQuery = "F YY ";

            if (strClearQuery == "")
            {
                strClearQuery = getClearQuery(dbType, strShTxt);
                if (strClearQuery == "")
                {
                    strRs = "请输入检索条件";
                }
                else if (strClearQuery == "请您在不同的检索关键字之间加上空格")
                {
                    strRs = strClearQuery;
                }

                // 检索式长度判断
                if (shortLen(strClearQuery) == true)
                {
                    strClearQuery = Regex.Replace(strClearQuery, @"(\/)+", "");
                    if (strClearQuery == "")
                    {
                        strRs = "请输入有效检索内容";
                    }
                    else
                    {
                        strFinalQuery = strFinalQuery + getOneItemQuery("\"" + strClearQuery + "\"", dbType);  //"(" + strClearQuery + "/AB)"; getOneItemQuery("\"" + strClearQuery + "\"", dbType)
                        strRs = strFinalQuery;
                    }
                }
                else
                {
                    // 分析检索式
                    string analysisQuery = getAnalysisQuery(strClearQuery, dbType);
                    if (!string.IsNullOrEmpty(analysisQuery))
                    {
                        strFinalQuery += analysisQuery;   // getAnalysisQuery(strClearQuery, dbType);
                        strRs = strFinalQuery;
                    }
                    else
                    {
                        strRs = "请输入有效检索内容";
                    }
                }
            }
            else
            {
                strFinalQuery = "F XX ";
                strFinalQuery += strClearQuery;
                strRs = strFinalQuery;
            }

            return strRs;
        }
    }
}