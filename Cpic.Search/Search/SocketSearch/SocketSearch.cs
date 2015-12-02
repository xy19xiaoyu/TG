#region Copyright (c) dev Soft All rights reserved
/*****************文件信息*****************************
* 创始信息:
*	Copyright(c) 2008-2010 @ CPIC Corp
*	CLR 版本: 2.0.50727.3603
*	文 件 名: SocketSearch.cs
*	创 建 人: xiwenlei(xiwl) $ chenxiaoyu(xy)
*	创建日期: 2010-9-27 13:45:35
*	版    本: V1.0
*	备注描述: $Myparameter1$           
*
* 修改历史: 
*   ****NO_1:
*	修 改 人: 
*	修改日期: 
*	描    述: $Myparameter1$           
******************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Reflection;
using log4net;
using System.IO;
[assembly: log4net.Config.DOMConfigurator()]

/// <summary>
/// 为CPRS2010组件，提供对检索、检索历史及用户的相关操作类
/// </summary>
namespace Cpic.Cprs2010.Search.SocketSearch
{
    /// <summary>
    ///SocketSearch 的摘要说明
    /// </summary>
    public class SocketSearch : Search.ISearch
    {

        #region "字段"
        private int _id;
        private Search.SearchStatus _Status;
        private string _Ip;
        private string _Port;
        private Socket socket;
        private SearchDbType _Type;
        private ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region 构造函数
        public SocketSearch(int id, string Ip, string Port, SearchDbType type)
        {
            _id = id;
            _Ip = Ip;
            _Port = Port;
            _Type = type;
            Ini();
        }
        #endregion

        #region 属性
        public string IP
        {
            get
            {
                return _Ip;
            }
            set
            {
                _Ip = value;
            }
        }
        public string Port
        {
            get
            {
                return _Port;
            }
            set
            {
                _Port = value;
            }
        }
        #endregion


        #region ISearch 成员

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public Search.SearchStatus Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }

        public bool Ini()
        {
            try
            {
                IPAddress serverIp = IPAddress.Parse(IP);
                int serverPort = Convert.ToInt32(Port);
                IPEndPoint iep = new IPEndPoint(serverIp, serverPort);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(iep);
                socket.ReceiveTimeout = CprsConfig.TimeOut;
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                Status = SearchStatus.Error;
                return false;
            }
        }

        public bool logIn()
        {
            log.Debug(string.Format("IP:{0},Port{1} logIn", IP, Port));
            try
            {
                SendMessage(string.Format("({0}++ MachineID (20) ++):Has Logged In", Id.ToString().PadLeft(4, '0')));
                string resive = Receive();
                Status = SearchStatus.LogIn;
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                Status = SearchStatus.Error;
                return false;
            }
        }

        public bool logOut()
        {
            log.Debug(string.Format("IP:{0},Port{1} logOut", IP, Port));
            try
            {
                SendMessage(string.Format("({0}++ MachineID (20) ++):Has Logged Out", Id.ToString().PadLeft(4, '0')));
                string resive = Receive();
                Status = SearchStatus.LogOut;
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                Status = SearchStatus.Error;
                return false;
            }


        }

        public ResultInfo Search(SearchPattern _searchPattern)
        {
            ResultInfo re = new ResultInfo();
            re.SearchPattern = _searchPattern;

            //string strSearch = string.Format("({0}++{1}++):({2}){3}", Id.ToString().PadLeft(4, '0'), _searchPattern.UserId.ToString().PadLeft(7, '0').PadRight(16, ' '), _searchPattern.SearchNo, _searchPattern.Pattern);
            string strSearch = string.Format("({0}++{1}{2}++):({3}){4}", Id.ToString().PadLeft(4, '0'),
                _searchPattern.UserId.ToString().PadLeft(7, '0'), _searchPattern.GroupName.PadRight(9, ' '), _searchPattern.SearchNo, _searchPattern.Pattern);

            try
            {
                SendMessage(strSearch);
                Status = SearchStatus.Searching;
                socket.Blocking = true;
                re.HitMsg = Receive();
                Status = SearchStatus.Free;
                del_OldCnpFile(_searchPattern);
            }
            catch (SocketException se)
            {
                log.Error("检索式：" + strSearch + ".请求已经超时!" + Environment.NewLine + se.ToString());
                re.HitMsg = "请求已经超时!";
                Status = SearchStatus.Free;
            }
            catch (Exception ex)
            {
                log.Error("检索式：" + strSearch + ".检索时发生错误!" + Environment.NewLine + ex.ToString());
                re.HitMsg = "检索时发生错误!";
                Status = SearchStatus.Free;
            }
            return re;
        }

        /// <summary>
        /// 删除老CNP文件
        /// </summary>
        private void del_OldCnpFile(SearchPattern _searchPattern)
        {
            try
            {
                ResultServices re = new ResultServices();
                string strFile = re.getResultFilePath(_searchPattern, false);
                FileInfo fileInfor = new FileInfo(strFile);

                string[] strArryFile = Directory.GetFiles(fileInfor.DirectoryName, fileInfor.Name.ToLower().Replace(".cnp", "") + ".*.*.cnp", SearchOption.TopDirectoryOnly);

                foreach (string strItme in strArryFile)
                {
                    try
                    {
                        File.Delete(strItme);
                    }
                    catch (Exception er)
                    {
                        log.Error(er.ToString());
                    }
                }

                fileInfor.Delete();
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
        }
        #endregion

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        private void SendMessage(string msg)
        {


            //if (string.GetData()->nDataLength < 255)
            //{
            //    ar << (BYTE)string.GetData()->nDataLength;
            //}
            //else if (string.GetData()->nDataLength < 0xfffe)
            //{
            //    ar << (BYTE)0xff;
            //    ar << (WORD)string.GetData()->nDataLength;
            //}
            //else
            //{
            //    ar << (BYTE)0xff;
            //    ar << (WORD)0xffff;
            //    ar << (DWORD)string.GetData()->nDataLength;
            //}
            //ar.Write(string.m_pchData, string.GetData()->nDataLength * sizeof(TCHAR));
            //return ar;

            //ar << (WORD)m_bClose;
            ushort begin = 0;
            // <255
            char length1;
            //255<= length < 65534
            ushort length2;
            // length >=65534
            uint length3;

            short end = 1;


            byte[] bytBegin = BitConverter.GetBytes(begin);
            byte[] bytContent = System.Text.Encoding.GetEncoding("gb2312").GetBytes(msg);
            byte[] bytEnd = BitConverter.GetBytes(end);
            byte[] buff;
            byte[] blength1 = new byte[1];
            byte[] blength2;
            byte[] blength3;

            if (bytContent.Length < 255)
            {
                length1 = (char)bytContent.Length;
                blength1[0] = (byte)length1;
                //定义整个2进制数组的长度
                buff = new byte[bytBegin.Length + blength1.Length + bytContent.Length + 2];
                bytBegin.CopyTo(buff, 0); //begin
                blength1.CopyTo(buff, bytBegin.Length); //消息内容长度 
                bytContent.CopyTo(buff, bytBegin.Length + +blength1.Length); //消息内容
            }
            else if (bytContent.Length < 0xfffe)
            {
                //length < 65534
                length1 = (char)0xff;
                blength1[0] = (byte)length1;
                length2 = (ushort)bytContent.Length;
                blength2 = BitConverter.GetBytes(length2);
                //定义整个2进制数组的长度
                buff = new byte[bytBegin.Length + blength1.Length + blength2.Length + bytContent.Length + 2];
                bytBegin.CopyTo(buff, 0); //begin
                blength1.CopyTo(buff, bytBegin.Length); //消息内容长度 
                blength2.CopyTo(buff, bytBegin.Length + +blength1.Length);
                bytContent.CopyTo(buff, bytBegin.Length + +blength1.Length + blength2.Length); //消息内容
            }
            else
            {
                length1 = (char)0xff;
                blength1[0] = (byte)length1;
                length2 = (ushort)0xffff;
                blength2 = BitConverter.GetBytes(length2);
                length3 = (uint)bytContent.Length;
                blength3 = BitConverter.GetBytes(length3);
                //定义整个2进制数组的长度
                buff = new byte[bytBegin.Length + blength1.Length + blength2.Length + blength3.Length + bytContent.Length + 2];
                bytBegin.CopyTo(buff, 0); //begin
                blength1.CopyTo(buff, bytBegin.Length); //消息内容长度 
                blength2.CopyTo(buff, bytBegin.Length + +blength1.Length);
                blength2.CopyTo(buff, bytBegin.Length + +blength1.Length + blength2.Length);
                bytContent.CopyTo(buff, bytBegin.Length + +blength1.Length + blength2.Length + blength3.Length); //消息内容
            }

            buff[buff.Length - 2] = 0;  // 消息内容结束
            buff[buff.Length - 1] = 0; // 最后m_msgList
            socket.Send(buff, buff.Length, 0); //发送
        }
        /// <summary>
        /// 受到从Main 返回的信息
        /// </summary>
        /// <returns></returns>
        private string Receive()
        {
            byte[] bytesReceived = new byte[255];
            byte[] byteAll = null;
            int bytes = 0;
            do
            {

                bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
                if (bytes == 0)
                {
                    return "";
                }

                byte[] bytetmp = new byte[((byteAll == null) ? 0 : byteAll.Length) + bytes];
                if (byteAll != null)
                {
                    byteAll.CopyTo(bytetmp, 0);
                    if (bytes < 255)
                    {
                        for (int i = 0; i < bytes; i++)
                        {
                            bytetmp[byteAll.Length + i] = bytesReceived[i];
                        }
                    }
                    else
                    {
                        bytesReceived.CopyTo(bytetmp, byteAll.Length);
                    }
                }
                else
                {
                    if (bytes < 255)
                    {
                        for (int i = 0; i < bytes; i++)
                        {
                            bytetmp[i] = bytesReceived[i];
                        }
                    }
                    else
                    {
                        bytesReceived.CopyTo(bytetmp, 0);
                    }

                }
                byteAll = bytetmp;
            }
            while (bytes >= 255);
            byte[] bytbClose = new byte[2];
            bytbClose[0] = byteAll[0];
            bytbClose[1] = byteAll[1];

            ushort BClose = BitConverter.ToUInt16(byteAll, 0);
            short StringLong = (short)byteAll[2];
            string stringContent = BitConverter.ToString(byteAll, 3, StringLong);
            string msg = System.Text.Encoding.GetEncoding("gb2312").GetString(byteAll, 5, byteAll.Length - 5);
            string regptat = "(.\\d{2}\\]\\(\\d{4}\\):)|^\\d";

            int index = msg.IndexOf(":(");
            if (index > 0)
            {
                return msg.Substring(index + 1);
            }
            else
            {
                return System.Text.RegularExpressions.Regex.Replace(msg, regptat, "");
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        #endregion

        #region ISearch 成员


        public SearchDbType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                this._Type = value;
            }
        }

        #endregion
    }

}

