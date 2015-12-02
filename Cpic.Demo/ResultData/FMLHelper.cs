using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cpic.Cprs2010.Search.ResultData
{
    public class FMLHelper
    {
        //
        public List<int> isfml = new List<int>();
        public List<string> checkedpr = new List<string>();
        public List<int> novilidefml = new List<int>();
        public List<datainfo> fmlSource;
        public List<fmlinfo> roots = new List<fmlinfo>();
        public List<fmlinfo> getfml(int fmlid)
        {
            //得到家族所有的专利信息
            getdbfml(fmlid);

            //循环每个专利，他们的最早优先权
            foreach (var item in fmlSource)
            {
                getRoots(item);

            }
            //循环每个最早的优先权，得到这些在先申请后的所有 被引用专利。
            foreach (var root in roots)
            {
                getChirden(root);
            }


            return roots;
        }

        public void getRoots(datainfo item)
        {
            if (isfml.Contains(item.id)) return;
            isfml.Add(item.id);
            string[] arypr = item.pr.Split(';');
            foreach (var tmp in arypr)
            {
                if (tmp.Trim() == "") continue;
                string s = "";
                if ("TD".IndexOf(tmp.Substring(tmp.Length - 1)) >= 0)
                {
                    s = tmp.Substring(0, tmp.Length - 1);
                }
                else
                {
                    s = tmp;
                }
                var parents = from y in fmlSource
                              where y.an == s
                              select y;               
                if (parents.Count() > 0)
                {
                 
                    //找到父节点,继续往上找
                    foreach (var parent in parents)
                    {
                        getRoots(parent);
                        
                    }
                }
                else
                {
                    var ex = from rt in roots 
                             where rt.an == s
                             select rt;
                    if (ex.Count() > 0) continue;
                    roots.Add(new fmlinfo() { an = s, id = -1, pn = "" });
                }
            }
        }

        public void getChirden(fmlinfo node)
        {
            
            //得打这个节点的申请号，判断优先权信息里有这些说句的 就是这个节点的子节点
            var an = node.an;
            if ("TD".IndexOf(an.Substring(an.Length - 1)) >= 0)
            {
                an = an.Substring(0, an.Length - 1);
            }
            var res = from y in fmlSource
                          where y.pr.IndexOf(an) >=0
                          select y;
            List<fmlinfo> chirden = new List<fmlinfo>();
            foreach (var tmp in res)
            {
                //如果这个号的优先权是它自己 退出；
                if (tmp.pr.IndexOf(tmp.an) >= 0) continue;
                chirden.Add(new fmlinfo() { id = tmp.id, an = tmp.an, pn = tmp.pn });
            }
            foreach (var tmp in chirden)
            {
                getChirden(tmp);
            }
            node.Chirden = chirden;

        }

        public void getdbfml(int fmlid)
        {
            ResultDataManagerDataContext en = new ResultDataManagerDataContext();
            List<datainfo> res = (from tmp in en.DocdbDocInfo
                                 where tmp.CPIC == fmlid
                                 select new datainfo()
                                 {
                                     id  = tmp.ID,
                                     pn = tmp.PubID,
                                     an = tmp.AppNo,
                                     pr = tmp.PR
                                 }).ToList<datainfo>();
            this.fmlSource = res;
            foreach (var tmp in fmlSource)
            {
                if (tmp.pr.Trim() == "") continue;
                tmp.pr = tmp.pr.Replace(tmp.an,"");
            }
        }
        public bool checkfml(int fmlid)
        {
            getdbfml(fmlid);
            foreach (var x in fmlSource)
            {
                //if (x.an.Trim() == "") continue;
                //lstan.Add(new clsan() { id = x.id, an = x.an });
                //if (x.pr.Trim() == "") continue;
                //string[] arypr = x.pr.Split(';');
                //foreach (var s in arypr)
                //{
                //    listpr.Add(new clspr() { id = x.id, pr = s });
                //}
                novilidefml.Add(x.id);

            }
            //循环每个号；判断
            foreach (var x in fmlSource)
            {
                Getfml(x);
                isfml.Add(x.id);
                novilidefml.Remove(x.id);
            }
            if (novilidefml.Count == 0)
            {

                return true;
            }
            else
            {
                return false;
            }

        }
        private void Getfml(datainfo item)
        {            
            //判断是否已经在验证后的家族列表中，如果已经在，进行下一个
            if (isfml.Contains(item.id)) return;
            //找父亲 判断在先申请是否在这个族里，如果有 找到它 的ID
            getParent(item);
            //找兄弟 判断是否有同样的 在先申请 如果有，则是兄弟，并判断兄弟是否有 父亲；
            getBrothers(item);
        }
        private void getParent(datainfo item)
        {
            string[] arypr = item.pr.Split(';');
            foreach (var tmp in arypr)
            {
                if (tmp.Trim() == "") continue;
                string s = "";
                if ("TD".IndexOf(tmp.Substring(tmp.Length - 1)) >= 0)
                {
                    s = tmp.Substring(0, tmp.Length - 1);
                }
                else
                {
                    s = tmp;
                }

                var parents = from y in fmlSource
                        where y.an == s
                        select y;
                if (parents.Count() > 0)
                {
                    //把找到的在先申请加入到 已经验证成功的号
                    foreach (var parent in parents)
                    {
                        isfml.Add(parent.id);
                        novilidefml.Remove(parent.id);
                    }
                    foreach (var m in parents)
                    {
                        Getfml(m);
                    }
                }
            }

        }
        private void getBrothers(datainfo item)
        {
            string[] arypr = item.pr.Split(';');
            foreach (var s in arypr)
            {
                var brothers = from y in fmlSource
                              where y.pr.IndexOf(s) >0 && y.id != item.id 
                              select y;
                if (brothers.Count() > 0)
                {
                    //把找到的在先申请加入到 已经验证成功的号
                    foreach (var brother in brothers)
                    {
                        isfml.Add(brother.id);
                        novilidefml.Remove(brother.id);
                    }
                    foreach (var m in brothers)
                    {
                        Getfml(m);
                    }
                }
            }
        }


    }
    [Serializable]   
    public class fmlinfo
    {
        public int id;
        public string pn;
        public string an;
        public List<fmlinfo> Chirden;
    }

    public class datainfo
    {
        public int id;
        public string pn;
        public string an;
        public string pr;
    }
    public class clsan
    {
        public int id;
        public string an;
    }

    public class clspr
    {
        public int id;
        public string pr;
    }
}
