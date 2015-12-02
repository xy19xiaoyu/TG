using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Util
{
    public class Common
    {
        #region 拆分常量
        public static readonly string nameSpace = "exch:";
        public static readonly string xmlExtension = ".xml";
        public static readonly string xmlAttCountry = "country";//属性国家
        public static readonly string xmlAttDocNum = "doc-number";//属性doc-number
        public static readonly string xmlAttKind = "kind";//属性种类
        public static readonly string splitToken = @"\";
        public static readonly char zeroChar = '0';
        public static readonly string newLine = @"\r\n";
        public static readonly string emptyString = "";

        public static readonly string regSplitPattern = @"<exch:exchange-document country=[\s\S]*?</exch:exchange-document>";
        public static readonly string regFirstLine = @"<exch:exchange-document country=[\s\S]*?>";
        public static readonly string regCountry = @"country=[\w\W]*? ";
        public static readonly string regDocNumber = @"doc-number=[\w\W]*? ";
        public static readonly string regKind = @"kind=[\w\W]*? ";
        public static readonly string regDateOfExchange = @"date-of-last-exchange=[\w\W]*? ";

        public static readonly string ExtractionIndexRecords_BatchNO = "BatchNo";
        //public static readonly string ExtractionIndexRecords_UnpackedXml = "UnpackedXml";
        public static readonly string ExtractionIndexRecords_DocPubID = "DocPubID";
        public static readonly string ExtractionIndexRecords_ExtractionFlag = "ExtractionFlag";
        public static readonly string ExtractionIndexRecords_IndexFlag = "IndexFlag";
        public static readonly string ExtractionIndexRecords_ContinueFlag = "ContinueFlag";

        public static readonly string DocdbSplitRecords_BatchNo = "BatchNo";
        public static readonly string DocdbSplitRecords_UnpackedFile = "UnpackedFile";
        public static readonly string DocdbSplitRecords_SplitFlag = "SplitFlag";

        public static readonly int batchCount = 900;//一次批量导入的数量
        public static readonly string Un_Split_XML_Name = "index.xml";
        public static readonly string xml_namespace = "<exch:exchange-document xmlns:exch=\"http://www.epo.org/exchange\"";
        public static readonly string xml_nodetype = "<exch:exchange-document";
        public static readonly string xml_code = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>";

        public static readonly string xml_exch_exchange_document = "exch:exchange-document";
        public static readonly string Date_Of_Last_Exchange = "date-of-last-exchange";
        public static readonly string Table_Dbo_DocdbExtractionIndexRecords = "dbo.DocdbExtractionIndexRecords";
        public static readonly string regDtdVersion = @"dtd-version=[\w\W]*? ";


        //add by lidonglei
        //public  const XNamespace exch = "http://www.epo.org/exchange";
        
        #endregion  
        
        #region 专利族信息生成 add by lidonglei
        public const string Exchange_document = "exchange-document";
        public const string Priority_claim = "priority-claim";
        public const string Data_format = "data-format";
        public const string Attribute_docdb = "docdb";
        public const string Attribute_docid = "document-id";
        public const string xmlAttDate = "date";
        public const string txtExtension = ".txt";
        public const string Attribute_Epodoc = "epodoc";
        public const string NEW_TXT = "New.txt";
        public const string EXIST_TXT = "Exist.txt";


        //提取索引信息常量
        public static readonly int SegNum_En = 1000000;
        public static readonly int SegNum_Cn = 500000;
        public static readonly String Index_Spliter = @";";
        //DocDB  E:\DOCDB_Service\DOCDB_DATASOURCE_DIR
        public static readonly String DocDB_File_Root = @"\\192.168.70.71\DOCDB_Service\DOCDB_DATASOURCE_DIR\";//docdb文献根路径
        public static readonly String DocDB_DTDPath = String.Empty;// @"\\10.75.8.118\\DOCDB_Service\\DOCDB_DTD\\docdb-entities.dtd";
        public static readonly String DocDB_Index_Root = @"D://DOCDB_Process\\DOCDB_INDEX_DIR\\";//DocDB索引信息根目录
        public static readonly String DocDB_IndexFile_abs = @"Data_Index_abs.txt";
        public static readonly String DocDB_IndexFile_Content = @"Data_Index_Content.txt";
        public static readonly String DocDB_IndexFile_Prio = @"Data_Index_Prio.txt";
        public static readonly String DocDB_IndexFile_abs_cfg_FileName = DocDB_IndexFile_abs.Split('.')[0] + ".cfg.xml"; //摘要信息文件名称
        public static readonly String DocDB_IndexFile_abs_cfg //摘要信息文件说明
                                     = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                                     + "<keys split=\"|\" SerialNoIndex=\"0\" PubNoIndex=\"1\" PubDateIndex=\"\" >"
                                     + "<key name=\"CPIC\" index=\"0\" wordsplit=\"false\" split=\"\"></key>"
                                     + "<key name=\"TI\" index=\"1\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                     + "<key name=\"AB\" index=\"2\" wordsplit=\"false\" split=\"\"></key>"
                                     + "</keys>";
        public static readonly String DocDB_IndexFile_Content_cfg_FileName = DocDB_IndexFile_Content.Split('.')[0] + ".cfg.xml"; //内容信息文件名称
        public static readonly String DocDB_IndexFile_Content_cfg //内容信息文件说明
                                     = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                                     + "<keys split=\"|\" SerialNoIndex=\"0\" PubNoIndex=\"1\" PubDateIndex=\"4\" >"
                                     + "<key name=\"CPIC\" index=\"0\" wordsplit=\"false\" split=\"\"></key>"
                                     + "<key name=\"PN\" index=\"1\" wordsplit=\"false\" split=\"\"></key>"
                                     + "<key name=\"AN\" index=\"2\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                     + "<key name=\"AD\" index=\"3\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                     + "<key name=\"PD\" index=\"4\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                     + "<key name=\"IC\" index=\"5\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                     + "<key name=\"EC\" index=\"6\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                     + "<key name=\"PA\" index=\"7\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                     + "<key name=\"IN\" index=\"8\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                     + "<key name=\"CT\" index=\"9\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                     + "</keys>";
        public static readonly String DocDB_IndexFile_Prio_cfg_FileName = DocDB_IndexFile_Prio.Split('.')[0] + ".cfg.xml"; //优先权信息文件名称
        public static readonly String DocDB_IndexFile_Prio_cfg//优先权信息文件说明
                                     = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                                     + "<keys split=\"|\" SerialNoIndex=\"0\" PubNoIndex=\"1\" PubDateIndex=\"\" >"
                                     + "<key name=\"CPIC\" index=\"0\" wordsplit=\"false\" split=\"\"></key>"
                                     + "<key name=\"PR\" index=\"1\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                     + "</keys>";
        //DWPI
        public static readonly String DWPI_File_Root = @"E:\\DWPI_Service\\DWPI_DATASOURCE_DIR\\";//dwpi文献根路径
        public static readonly String DWPI_DTDPath = @"F://CPRS//DWPI//DTD//WPIMAX.dtd";
        public static readonly String DWPI_INDEX_DIR = @"E://DWPI_Process//DWPI_INDEX_DIR//source//";//DWPI索引信息根目录
        public static readonly String DWPI_TitleAbstractIndex_File = @"TitleAbstractIndex.txt";
        public static readonly String DWPI_OtherBiblographicIndex_File = @"OtherBiblographicIndex.txt";
        public static readonly String DWPI_SerialAccession_File = @"SerialAccession.idx";//流水号入藏号对应二进制文件
        public static readonly String DWPI_TitleAbstractIndex_File_cfg_FileName = DWPI_TitleAbstractIndex_File.Split('.')[0] + ".cfg.xml"; //标题摘要信息文件名称
        public static readonly String DWPI_TitleAbstractIndex_File_cfg //标题摘要信息文件说明
                                    = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                                    + "<keys split=\"|\" SerialNoIndex=\"0\" PubNoIndex=\"1\" PubDateIndex=\"\" >"
                                    + "<key name=\"SN\" index=\"0\" wordsplit=\"false\" split=\"\"></key>"
                                    + "<key name=\"AB\" index=\"1\" wordsplit=\"false\" split=\"\"></key>"
                                    + "<key name=\"TI\" index=\"2\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "</keys>";
        public static readonly String DWPI_OtherBiblographicIndex_File_cfg_FileName = DWPI_OtherBiblographicIndex_File.Split('.')[0] + ".cfg.xml"; //其他著录项目文件名称
        public static readonly String DWPI_OtherBiblographicIndex_File_cfg //其他著录项目文件说明
                                    = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                                    + "<keys split=\"|\" SerialNoIndex=\"0\" PubNoIndex=\"5\" PubDateIndex=\"6\" >"
                                    + "<key name=\"SN\" index=\"0\" wordsplit=\"false\" split=\"\"></key>"
                                    + "<key name=\"AN\" index=\"1\" wordsplit=\"false\" split=\"\"></key>"
                                    + "<key name=\"AP\" index=\"2\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"AD\" index=\"3\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"PA\" index=\"4\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"PN\" index=\"5\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"PD\" index=\"6\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"IC\" index=\"7\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"DC\" index=\"8\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"PR\" index=\"9\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"IN\" index=\"10\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"KW\" index=\"11\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "</keys>";
        //中文
        public static readonly String CN_Abstract_Root = @"\\192.168.70.63\cprs_2010_data\xml\";//存放中文摘要文献的根目录
        public static readonly String CN_Full_Root = @"F://CPRS//CN//full//";//存放中文全文文献的根目录
        public static readonly String CN_Index_Root = @"E:\Cprs2010_Data\CN\Source\";//存放中文索引数据的跟目录
        public static readonly String CN_Serial_ApplyFile_dir = @"F://CPRS//cnserial//apply//";//cnGeneral.idx文件所在的生产目录
        public static readonly String CN_Serial_Name = @"cnGeneral.idx";//cnGeneral.idx文件名称

        public static readonly String CN_ErrorFile_Dir = @"E:\Cprs2010_Data\CN\ErrorFile\";//中文错误文献路径

        public static readonly String CN_AbsMainClaim_File = @"CN_Index_AbsAndMainClaim.txt";//摘要和主权利要求
        public static readonly String CN_INDI_File = @"CN_Index_INDI.txt";//著录项目索引文件
        public static readonly String CN_Translate_File = @"CN_Index_Translate.txt";//翻译数据索引文件
        public static readonly String CN_Claim_File = @"CN_Index_Claims.txt";//权利要求
        public static readonly String CN_Description_File = @"CN_Index_Description.txt";//说明书

        public static readonly String CN_AbsMainClaim_File_cfg_FileName = CN_AbsMainClaim_File.Split('.')[0]+".cfg.xml";//摘要和主权利要求索引文件名称
        public static readonly String CN_AbsMainClaim_File_cfg //摘要和主权利要求索引文件格式说明
                                    = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                                    + "<keys split=\"|\" SerialNoIndex=\"0\" PubNoIndex=\"\" PubDateIndex=\"\" >"
                                    + "<key name=\"SN\" index=\"0\" wordsplit=\"false\" split=\"\"></key>"
                                    + "<key name=\"AN\" index=\"1\" wordsplit=\"false\" split=\"\"></key>"
                                    + "<key name=\"AB\" index=\"2\" wordsplit=\"false\" split=\"\"></key>"
                                    + "<key name=\"CL\" index=\"3\" wordsplit=\"false\" split=\"\"></key>"
                                    + "</keys>";
        public static readonly String CN_INDI_File_cfg_FileName = CN_INDI_File.Split('.')[0] + ".cfg.xml";//著录项目索引文件名称
        public static readonly String CN_INDI_File_cfg //著录项目索引文件格式说明
                                    = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                                    + "<keys split=\"|\" SerialNoIndex=\"0\" PubNoIndex=\"\" PubDateIndex=\"\" >"
                                    + "<key name=\"SN\" index=\"0\" wordsplit=\"false\" split=\"\"></key>"
                                    + "<key name=\"AN\" index=\"1\" wordsplit=\"false\" split=\"\"></key>"
                                    + "<key name=\"PN\" index=\"2\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"GN\" index=\"3\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"IC\" index=\"4\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"PR\" index=\"5\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"IN\" index=\"6\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"PA\" index=\"7\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"AG\" index=\"8\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"TI\" index=\"9\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"AD\" index=\"10\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"PD\" index=\"11\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"GD\" index=\"12\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"CT\" index=\"13\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"CO\" index=\"14\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"DZ\" index=\"15\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "<key name=\"KW\" index=\"16\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "</keys>";
        public static readonly String CN_Claim_File_cfg_FileName = CN_Claim_File.Split('.')[0] + ".cfg.xml";//权利要求索引文件名称
        public static readonly String CN_Claim_File_cfg //权利要求索引文件格式说明
                                    = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                                    + "<keys split=\"|\" SerialNoIndex=\"0\" PubNoIndex=\"\" PubDateIndex=\"\" >"
                                    + "<key name=\"SN\" index=\"0\" wordsplit=\"false\" split=\"\"></key>"
                                    + "<key name=\"AN\" index=\"1\" wordsplit=\"false\" split=\"\"></key>"
                                    + "<key name=\"CM\" index=\"2\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "</keys>";
        public static readonly String CN_Description_File_cfg_FileName = CN_Description_File.Split('.')[0] + ".cfg.xml";//说明书索引文件名称
        public static readonly String CN_Description_File_cfg //说明书索引文件格式说明
                                    = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                                    + "<keys split=\"|\" SerialNoIndex=\"0\" PubNoIndex=\"\" PubDateIndex=\"\" >"
                                    + "<key name=\"SN\" index=\"0\" wordsplit=\"false\" split=\"\"></key>"
                                    + "<key name=\"AN\" index=\"1\" wordsplit=\"false\" split=\"\"></key>"
                                    + "<key name=\"DS\" index=\"2\" wordsplit=\"true\" split=\"" + Index_Spliter + "\"></key>"
                                    + "</keys>";

        #endregion

        #region 更新专利族信息常量
        public static readonly string DocdbFamilyInfo_PubIDs = "PubIDs";
        public static readonly string DocdbFamilyInfo_Table = "dbo.DocdbFamilyInfo";
        public static readonly string DocdbDocInfo_Table = "dbo.DocdbDocInfo";
        public static readonly string DocdbDocInfo_PubID = "PubID";
        public static readonly string DocdbDocInfo_CPIC = "CPIC";
        public static readonly string DocdbFamilyInfo_IndexFlag = "IndexFlag";
        #endregion        

        #region dwpi
        public static readonly string Dwpi_DwpiSplitRecords_BatchNo = "BatchNo";
        public static readonly string Dwpi_DwpiSplitRecords_UnpackedXml = "UnpackedXml";
        public static readonly string Dwpi_UnderLine = "_";
        public static readonly string Dwpi_DwpiFamilyIndexUpdateRecords_BatchNo = "BatchNo";
        public static readonly string Dwpi_DwpiFamilyIndexUpdateRecords_Token = "Token";
        public static readonly string Dwpi_DwpiFamilyIndexUpdateRecords_PatentAccession = "PatentAccession";
        public static readonly string Dwpi_DwpiFamilyIndexUpdateRecords_FamilyFlag = "FamilyFlag";
        public static readonly string Dwpi_DwpiFamilyIndexUpdateRecords_IndexFlag = "IndexFlag";
        public static readonly string Dwpi_DwpiFamilyIndexUpdateRecords_TableName = "DwpiFamilyIndexUpdateRecords";
        public static readonly string Dwpi_DwpiFamilyIndexUpdateRecords_SourcePath = "SourcePath";
        #endregion

    }

    #region 枚举常量
    public enum DWPI_SPLIT_STATUS
    {
        init = 0,
        success = 1,
        fail = 2,
    }
    #endregion
}
