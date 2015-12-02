using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Cpic.Cprs2010.Search.ResultData;
using TLC.BusinessLogicLayer;

public partial class My_ComparePatent : System.Web.UI.Page
{
    private string type = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Ids"] != null && Request.QueryString["Ids"] != "")
            {
                if (Request.QueryString["type"] == null)
                {
                    type = "CN";
                }
                else
                {
                    type = Request.QueryString["type"].ToString();
                }
                string[] arrayId = Request.QueryString["Ids"].Split(new string[]{"|"}, StringSplitOptions.RemoveEmptyEntries);
                List<xmlDataInfo> lst = new List<xmlDataInfo>();
                foreach (string strId in arrayId)
                {
                    SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
                    //if (search.GetResultByAppNo(strId).Count > 0)
                    //{
                    xmlDataInfo currentXmlDataInfo = new xmlDataInfo();
                    if (type == "CN")
                    {
                        currentXmlDataInfo = search.GetCnxmlDataInfo(strId);
                        gonggao.Visible = true;
                    }
                    else
                    {
                        currentXmlDataInfo = search.GetEnxmlDataInfo(strId);
                        gonggao.Visible = false;
                    }
                    lst.Add(currentXmlDataInfo);
                    string titleA = currentXmlDataInfo.StrTitle;
                    if (titleA.Length > 30)
                    {
                        titleA = titleA.Substring(0, 30) + "……";
                    }
                    DropDownListPatentA.Items.Add(new ListItem(titleA, strId));
                    DropDownListPatentB.Items.Add(new ListItem(titleA, strId));
                    //}
                }
                ViewState["List"] = lst;
                if (DropDownListPatentA.Items.Count > 0)
                {
                    DropDownListPatentA.SelectedIndex = 0;
                }
                if (DropDownListPatentB.Items.Count > 1)
                {
                    DropDownListPatentB.SelectedIndex = 1;
                }
                ShowPatentA(DropDownListPatentA.SelectedIndex);
                ShowPatentB(DropDownListPatentB.SelectedIndex);
            }
        }
    }

    protected void ShowPatentA(int i)
    {
        //SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
        //if (search.GetResultByAppNo(DropDownListPatentA.SelectedValue).Count > 0)
        //{
        List<xmlDataInfo> lst = new List<xmlDataInfo>();
        lst =(List<xmlDataInfo>)ViewState["List"];
        xmlDataInfo currentXmlDataInfo = lst[i];
        LiteralTitleA.Text = "<a href='frmPatDetails.aspx?Id=" + currentXmlDataInfo.StrANX + "' target='_blank' >" + currentXmlDataInfo.StrTitle + "</a>";
            
        LiteralApDateA.Text = currentXmlDataInfo.StrApDate;
        LiteralApNoA.Text = currentXmlDataInfo.StrApNo;
        LiteralInventorA.Text = currentXmlDataInfo.StrInventor;
        LiteralApplyA.Text = currentXmlDataInfo.StrApply;
        LiteralCountryCodeA.Text = currentXmlDataInfo.StrCountryCode;
        LiteralPubNoA.Text = currentXmlDataInfo.StrPubNo;
        LiteralPubDateA.Text = currentXmlDataInfo.StrPubDate;
        LiteralAnnNoA.Text = currentXmlDataInfo.StrAnnNo;
        LiteralAnnDateA.Text = currentXmlDataInfo.StrAnnDate;
        LiteralBriefA.Text = currentXmlDataInfo.StrAbstr;
        LiteralAddsA.Text = currentXmlDataInfo.StrShenQingRenDiZhi;
        ImageFtA.ImageUrl = currentXmlDataInfo.StrFtUrl;
        litYSQ_A.Text=currentXmlDataInfo.StrPri;
        if (!gonggao.Visible)
        {
            litFlzt_A.Text = string.Format("<iframe id='irmLagel' src='{0}comm/epo_legal.aspx?pubno={1}' style='z-index:0;' frameborder='0' width='100%' height='300'></iframe>", 
                SearchInterface.XmPatentComm.strUrlDome,currentXmlDataInfo.StrANX);
        }
        else
        {
            litFlzt_A.Text = string.Format("<iframe id='irmLagel' src='/my/frmLawInfo.aspx?Idx={0}' style='z-index:0;' frameborder='0' width='100%' height='300'></iframe>",
                currentXmlDataInfo.StrANX);
        }       
        //LiteralClaimsA.Text = currentXmlDataInfo.StrClaim;
        //}
    }

    protected void ShowPatentB(int i)
    {
        //SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
        //if (search.GetResultByAppNo(DropDownListPatentB.SelectedValue).Count > 0)
        //{
        List<xmlDataInfo> lst = new List<xmlDataInfo>();
        lst = (List<xmlDataInfo>)ViewState["List"];
        xmlDataInfo currentXmlDataInfo = lst[i];
            
        LiteralTitleB.Text = "<a href='frmPatDetails.aspx?Id=" + currentXmlDataInfo.StrANX + "' target='_blank' >" + currentXmlDataInfo.StrTitle + "</a>";
            
        LiteralApDateB.Text = currentXmlDataInfo.StrApDate;
        LiteralApNoB.Text = currentXmlDataInfo.StrApNo;
        LiteralInventorB.Text = currentXmlDataInfo.StrInventor;
        LiteralApplyB.Text = currentXmlDataInfo.StrApply;
        LiteralCountryCodeB.Text = currentXmlDataInfo.StrCountryCode;
        LiteralPubNoB.Text = currentXmlDataInfo.StrPubNo;
        LiteralPubDateB.Text = currentXmlDataInfo.StrPubDate;
        LiteralAnnNoB.Text = currentXmlDataInfo.StrAnnNo;
        LiteralAnnDateB.Text = currentXmlDataInfo.StrAnnDate;
        LiteralBriefB.Text = currentXmlDataInfo.StrAbstr;
        LiteralAddsB.Text = currentXmlDataInfo.StrShenQingRenDiZhi;
        ImageFtB.ImageUrl = currentXmlDataInfo.StrFtUrl;
        litYSQ_B.Text = currentXmlDataInfo.StrPri;
        //LiteralClaimsB.Text = currentXmlDataInfo.StrClaim;
        //}
        if (!gonggao.Visible)
        {
            litFlzt_B.Text = string.Format("<iframe id='irmLagel' src='{0}comm/epo_legal.aspx?pubno={1}' style='z-index:0;' frameborder='0' width='100%' height='300'></iframe>",
                SearchInterface.XmPatentComm.strUrlDome, currentXmlDataInfo.StrANX);
        }
        else
        {
            litFlzt_B.Text = string.Format("<iframe id='irmLagel' src='/my/frmLawInfo.aspx?Idx={0}' style='z-index:0;' frameborder='0' width='100%' height='300'></iframe>",
                currentXmlDataInfo.StrANX);
        }      
        UpdatePanel1.Update();
    }

    protected void DropDownListPatentA_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowPatentA(DropDownListPatentA.SelectedIndex);
        UpdatePanel1.Update();
    }

    protected void DropDownListPatentB_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowPatentB(DropDownListPatentB.SelectedIndex);
        UpdatePanel1.Update();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.open(frmpatDetails.aspx?Id=" + DropDownListPatentA.SelectedValue);
    }
}