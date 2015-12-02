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

public partial class My_PatentSectionShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")
            {
                String strId = Request.QueryString["Id"].Trim();
                String type = Request.QueryString["type"];
                if (type == null || type.Equals("")) {
                    type = "CN";
                }
                else {
                    type = type.Trim();
                }
                String select = Request.QueryString["select"];
                if (select == null || select.Equals("")) {
                    select = "0";
                }
                else {
                    select = select.Trim();
                }
                String patType = Request.QueryString["patType"];
                if (patType == null || patType.Equals(""))
                {
                    patType = "0";
                }
                else
                {
                    patType = patType.Trim();
                }

                SearchInterface.ClsSearch search = new SearchInterface.ClsSearch();
                xmlDataInfo currentXmlDataInfo = new xmlDataInfo();
                if (type == "CN") {
                    //if (System.Text.RegularExpressions.Regex.IsMatch(currentXmlDataInfo.StrApNo, @"(^.{2}3.{5}.*$)|(^.{2}3.{5}\..$)|(^.{4}3.{7}.*$)|(^.{4}3.{7}\..$)"))
                    if (patType == "1")
                    {
                        DropDownListPatentPart.Items.Add(new ListItem("著录项目", "divMain"));
                        DropDownListPatentPart.Items.Add(new ListItem("摘要信息", "divAbs"));
                        DropDownListPatentPart.Items.Add(new ListItem("摘要附图", "divBrief"));
                        DropDownListPatentPart.Items.Add(new ListItem("法律状态", "divLegal"));
                    }
                    else
                    {
                        DropDownListPatentPart.Items.Add(new ListItem("著录项目", "divMain"));
                        DropDownListPatentPart.Items.Add(new ListItem("权利要求", "divClaim"));
                        DropDownListPatentPart.Items.Add(new ListItem("说明书", "divDes"));
                        DropDownListPatentPart.Items.Add(new ListItem("摘要信息", "divAbs"));
                        DropDownListPatentPart.Items.Add(new ListItem("摘要附图", "divBrief"));
                        DropDownListPatentPart.Items.Add(new ListItem("法律状态", "divLegal"));
                        DropDownListPatentPart.Items.Add(new ListItem("引文信息", "divQuote"));
                    }

                    currentXmlDataInfo = search.GetCnxmlDataInfo(strId);
                    gonggao.Visible = true;
                } else {
                    DropDownListPatentPart.Items.Add(new ListItem("著录项目", "divMain"));
                    DropDownListPatentPart.Items.Add(new ListItem("摘要信息", "divAbs"));
                    DropDownListPatentPart.Items.Add(new ListItem("摘要附图", "divBrief"));
                    DropDownListPatentPart.Items.Add(new ListItem("法律状态", "divLegal"));
                    DropDownListPatentPart.Items.Add(new ListItem("引文信息", "divQuote"));

                    currentXmlDataInfo = search.GetEnxmlDataInfo(strId);
                    gonggao.Visible = false;
                }

                //if (int.Parse(select) > DropDownListPatentPart.Items.Count)
                //{
                //    select = DropDownListPatentPart.Items.Count.ToString();
                //}
                //DropDownListPatentPart.SelectedIndex = int.Parse (select);
                if (select == "0") {
                    DropDownListPatentPart.SelectedIndex = 0;
                }
                else if (type == "CN" && patType == "1") {
                    DropDownListPatentPart.SelectedIndex = 1;
                }
                else if (type == "EN") {
                    DropDownListPatentPart.SelectedIndex = 1;
                }
                else {
                    DropDownListPatentPart.SelectedIndex = int.Parse(select);
                }

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
                LiteralAddsA.Text = currentXmlDataInfo.StrShenQingRenDiZhi;
                litYSQ_A.Text = currentXmlDataInfo.StrPri;

                LiteralAbs.Text = currentXmlDataInfo.StrAbstr;
                //LiteralBrief.ImageUrl = currentXmlDataInfo.StrFtUrl;
                LiteralImageFt.Text = string.Format("<img id='ImageFt' src='../Images/loding_imgFt.gif' onload=\"resizeFt(this,'{0}')\" alt='摘要附图'/>", currentXmlDataInfo.StrFtUrl);

                if (!gonggao.Visible) {
                    litFlzt_A.Text = string.Format("<iframe id='irmLagel' src='{0}comm/epo_legal.aspx?pubno={1}' style='z-index:0;' frameborder='0' width='100%' height='300'></iframe>",
                        SearchInterface.XmPatentComm.strUrlDome, currentXmlDataInfo.StrANX);

                    string strLiteralQuote_1 = search.GetEnCitedWithSrepPhase(Request.QueryString["Id"].Trim(), "APP", "is").Replace(";", "<br />");
                    string strLiteralQuote_2 = search.GetEnCitedWithSrepPhase(Request.QueryString["Id"].Trim (), "APP", "not").Replace(";", "<br />");
                    LiteralQuote.Text = "<p><strong>申请人引用:</strong></p>" + strLiteralQuote_1 + "<hr align=\"center\" width=\"100%\" size=\"1\" style=\"margin: 10px 0;\" /><p><strong>非申请人引用:</strong></p>" + strLiteralQuote_2;
                } else {
                    litFlzt_A.Text = string.Format("<iframe id='irmLagel' src='/my/frmLawInfo.aspx?Idx={0}' style='z-index:0;' frameborder='0' width='100%' height='300'></iframe>",
                        currentXmlDataInfo.StrANX);

                    if (LiteralQuote.Text == "Loading......")
                    {
                        if (LiteralAnnNoA.Text != null && LiteralAnnNoA.Text != "")
                        {
                            string yzInf = search.getYZInf(LiteralAnnNoA.Text);
                            if (!yzInf.Equals(""))
                            {
                                if (yzInf.IndexOf("@@@") > 0)
                                {
                                    LiteralQuote.Text = yzInf.Replace("@@@", "<br />");
                                }
                                else
                                {
                                    LiteralQuote.Text = yzInf;
                                }
                            }
                            else
                            {
                                LiteralQuote.Text = "暂无数据";
                            }
                        }
                        else
                        {
                            LiteralQuote.Text = "暂无数据";
                        }
                    }
                }
            }
        }
    }

    //protected void DropDownListPatentPart_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int i = DropDownListPatentPart.SelectedIndex;

    //    UpdatePanel1.Update();
    //}

    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    Response.Write("<script>window.open(frmpatDetails.aspx?Id=" + DropDownListPatentA.SelectedValue);
    //}
}