using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLC.BusinessLogicLayer;

/// <summary>
    ///TbCnLegalStatusNew 的摘要说明
    /// </summary>
public class TbCnLegalStatusNew
{
    public TbCnLegalStatusNew()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    //SHENQINGH（申请号）
    private string _SHENQINGH;
    public string SHENQINGH
    {
        get { return _SHENQINGH; }
        set { _SHENQINGH = value; }
    }
    //LegalDate（公告日）
    private string _LegalDate;
    public string LegalDate
    {
        get { return _LegalDate; }
        set { _LegalDate = value; }
    }
    //LegalStatusInfo（法律状态）
    private string _LegalStatusInfo;
    public string LegalStatusInfo
    {
        get { return _LegalStatusInfo; }
        set { _LegalStatusInfo = value; }
    }
    //Detail（状态详情）
    private string _Detail;
    public string Detail
    {
        get { return _Detail; }
        set { _Detail = value; }
    }
}