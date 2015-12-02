USE [PatentQuery_TG]
GO
/****** Object:  Table [dbo].[C_W_SECARCH]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[C_W_SECARCH](
	[W_ID] [int] IDENTITY(1,1) NOT NULL,
	[C_ID] [int] NULL,
	[S_NAME] [varchar](200) NULL,
	[PATTERN] [varchar](1000) NULL,
	[CHANGEDATE] [datetime] NULL,
	[CURRENTNUM] [int] NULL,
	[CHANGENUM] [int] NULL,
	[TYPE] [int] NULL,
	[SEARCHFILE] [varbinary](max) NULL,
	[COMPAREFILE] [varbinary](max) NULL,
	[NID] [varchar](36) NULL,
 CONSTRAINT [PK_C_W_SECARCH] PRIMARY KEY CLUSTERED 
(
	[W_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[C_W_SEARCHLIS]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[C_W_SEARCHLIS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[W_ID] [int] NULL,
	[C_ID] [int] NULL,
	[S_NAME] [varchar](200) NULL,
	[CHANGEDATE] [datetime] NULL,
	[CURRENTNUM] [int] NULL,
	[CHANGENUM] [int] NULL,
	[SEARCHFILE] [varbinary](max) NULL,
	[COMPAREFILE] [varbinary](max) NULL,
	[type] [int] NULL,
	[HisOrder] [int] NULL,
 CONSTRAINT [PK_C_W_SEARCHLIS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[C_EARLY_WARNING]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[C_EARLY_WARNING](
	[C_ID] [int] IDENTITY(1,1) NOT NULL,
	[USER_ID] [int] NULL,
	[ALIAS] [nvarchar](200) NULL,
	[PERIOD] [int] NULL,
	[C_DATE] [datetime] NULL,
	[C_TYPE] [int] NULL,
	[dbsource] [nchar](10) NULL,
	[BEIZHU] [varchar](1000) NULL,
	[Status] [int] NULL,
	[ISUPDATE] [int] NULL,
 CONSTRAINT [PK_C_EARLY_WARNING] PRIMARY KEY CLUSTERED 
(
	[C_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BaseFunction]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseFunction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FunctionCode] [nchar](10) NULL,
	[FunctionName] [nvarchar](50) NULL,
	[FunctionDes] [nvarchar](550) NULL,
	[EachMoney] [float] NULL,
	[FunctionInterface] [nvarchar](500) NULL,
	[InterfaceDM] [nvarchar](50) NULL,
 CONSTRAINT [PK_BaseFunction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicantYJ]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ApplicantYJ](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[appl] [varchar](200) NULL,
 CONSTRAINT [PK_ApplicantYJ] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Applicant]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Applicant](
	[pubno] [char](14) NULL,
	[pubtype] [char](1) NULL,
	[pubvol] [char](8) NULL,
	[filingno] [char](12) NULL,
	[appl] [char](200) NULL,
	[mark] [char](10) NULL,
	[pubmk] [bit] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ztTree]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ztTree](
	[NodeId] [int] IDENTITY(1,1) NOT NULL,
	[NodeName] [nvarchar](40) NULL,
	[PNodeid] [int] NULL,
	[IsParent] [tinyint] NULL,
	[thId] [int] NULL,
	[type] [nvarchar](10) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[isdel] [bit] NULL,
	[des] [nvarchar](1000) NULL,
	[live] [tinyint] NULL,
	[Nid] [char](36) NULL,
	[PNid] [char](36) NULL,
	[zid] [varchar](36) NULL,
 CONSTRAINT [PK__ztTree__6BAE2263457F2FDE] PRIMARY KEY CLUSTERED 
(
	[NodeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ztsp]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ztsp](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[NodeId] [int] NULL,
	[NodeName] [varchar](50) NULL,
	[SPNum] [int] NULL,
	[sp] [varchar](1200) NOT NULL,
	[Hit] [int] NULL,
	[UpdateSum] [int] NULL,
	[isAutoUpdate] [bit] NULL,
	[Type] [varchar](10) NULL,
	[isUsed] [bit] NULL,
	[UpdateDate] [datetime] NULL,
	[CreateUserId] [int] NULL,
	[CreateDate] [datetime] NULL,
	[isdel] [tinyint] NULL,
	[ztid] [int] NULL,
	[st] [varchar](20) NULL,
	[Nid] [char](36) NULL,
	[zid] [varchar](36) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ZtDbList]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ZtDbList](
	[DbID] [int] IDENTITY(1,1) NOT NULL,
	[ztDbName] [nvarchar](20) NULL,
	[dbType] [int] NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[DbDes] [varchar](500) NULL,
	[IsDel] [bit] NULL,
	[ztHeardImg] [varchar](45) NULL,
	[zid] [varchar](36) NULL,
 CONSTRAINT [PK_ZtDbList] PRIMARY KEY CLUSTERED 
(
	[DbID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0专题数据库；1企业在线数据库；2竟争对手' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ZtDbList', @level2type=N'COLUMN',@level2name=N'dbType'
GO
/****** Object:  Table [dbo].[ztdb]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ztdb](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[NodeId] [int] NULL,
	[Pid] [int] NULL,
	[Iscore] [tinyint] NULL,
	[isUpdate] [bit] NULL,
	[Form] [varchar](20) NULL,
	[type] [varchar](10) NULL,
	[CreateUserId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[isdel] [bit] NULL,
	[nid] [char](36) NULL,
 CONSTRAINT [PK__ztdb__3213E83F6E8B6712] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ZPT_SJWH_DLJGPZB1]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZPT_SJWH_DLJGPZB1](
	[DAILIJGID] [nvarchar](max) NULL,
	[DAILIJGDM] [nvarchar](max) NULL,
	[DAILIJGDZ] [nvarchar](max) NULL,
	[DAILIJGYB] [nvarchar](max) NULL,
	[DAILIJGLXDH] [nvarchar](max) NULL,
	[DAILIJGYHZH] [nvarchar](max) NULL,
	[DAILIJGMC] [nvarchar](max) NULL,
	[DAILIJGFZRXM] [nvarchar](max) NULL,
	[DAILIJGBZH] [nvarchar](max) NULL,
	[DAILIJGBZS] [nvarchar](max) NULL,
	[SHANGJIZGDW] [nvarchar](max) NULL,
	[PIBIANDW] [nvarchar](max) NULL,
	[GONGSDJBJ] [nvarchar](max) NULL,
	[ZHUANLIDJRQ] [nvarchar](255) NULL,
	[DENGJIRQ] [nvarchar](255) NULL,
	[JINGJIXZ] [nvarchar](max) NULL,
	[DAILIJGZT] [nvarchar](max) NULL,
	[SUOZAIDQ] [nvarchar](max) NULL,
	[JINGYINGFW] [nvarchar](max) NULL,
	[SHEWAIQBJ] [nvarchar](max) NULL,
	[DAILIJGJC] [nvarchar](max) NULL,
	[KEZIQXJBJ] [nvarchar](max) NULL,
	[BIANGENGBJ] [nvarchar](max) NULL,
	[REGNAME] [nvarchar](max) NULL,
	[REGTIME] [nvarchar](max) NULL,
	[MODNAME] [nvarchar](max) NULL,
	[MODTIME] [nvarchar](max) NULL,
	[YOUXIAOBJ] [nvarchar](max) NULL,
	[NIANJIANZT] [nvarchar](max) NULL,
	[DAILIJGYDDH] [nvarchar](max) NULL,
	[CHENGJIECXGDQ] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ZPT_SJWH_DLJGPZB]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZPT_SJWH_DLJGPZB](
	[DAILIJGID] [nvarchar](max) NULL,
	[DAILIJGDM] [nvarchar](max) NULL,
	[DAILIJGDZ] [nvarchar](max) NULL,
	[DAILIJGYB] [nvarchar](max) NULL,
	[DAILIJGLXDH] [nvarchar](max) NULL,
	[DAILIJGYHZH] [nvarchar](max) NULL,
	[DAILIJGMC] [nvarchar](max) NULL,
	[DAILIJGFZRXM] [nvarchar](max) NULL,
	[DAILIJGBZH] [nvarchar](max) NULL,
	[DAILIJGBZS] [nvarchar](max) NULL,
	[SHANGJIZGDW] [nvarchar](max) NULL,
	[PIBIANDW] [nvarchar](max) NULL,
	[GONGSDJBJ] [nvarchar](max) NULL,
	[ZHUANLIDJRQ] [nvarchar](255) NULL,
	[DENGJIRQ] [nvarchar](255) NULL,
	[JINGJIXZ] [nvarchar](max) NULL,
	[DAILIJGZT] [nvarchar](max) NULL,
	[SUOZAIDQ] [nvarchar](max) NULL,
	[JINGYINGFW] [nvarchar](max) NULL,
	[SHEWAIQBJ] [nvarchar](max) NULL,
	[DAILIJGJC] [nvarchar](max) NULL,
	[KEZIQXJBJ] [nvarchar](max) NULL,
	[BIANGENGBJ] [nvarchar](max) NULL,
	[REGNAME] [nvarchar](max) NULL,
	[REGTIME] [nvarchar](max) NULL,
	[MODNAME] [nvarchar](max) NULL,
	[MODTIME] [nvarchar](max) NULL,
	[YOUXIAOBJ] [nvarchar](max) NULL,
	[NIANJIANZT] [nvarchar](max) NULL,
	[DAILIJGYDDH] [nvarchar](max) NULL,
	[CHENGJIECXGDQ] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRecharge]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRecharge](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_LogName] [nvarchar](30) NULL,
	[FUNCTIONNAME] [nvarchar](50) NULL,
	[RULEDES] [nvarchar](100) NULL,
	[Money] [float] NULL,
	[trade_no] [nvarchar](100) NULL,
	[out_trade_no] [nvarchar](100) NULL,
	[buyer_email] [nvarchar](50) NULL,
	[trade_status] [nvarchar](50) NULL,
	[ReTime] [datetime] NULL,
 CONSTRAINT [PK_UserRecharge] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsageHis]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsageHis](
	[ID] [nchar](10) NULL,
	[User_LogName] [nvarchar](30) NULL,
	[RuleCode] [nchar](10) NULL,
	[UseTime] [datetime] NULL,
	[MONEY] [int] NULL,
	[PAGEPATH] [nvarchar](1000) NULL,
	[FILENUM] [int] NULL,
	[FunctionCode] [nchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TLC_Patterns]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TLC_Patterns](
	[PatternId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Source] [tinyint] NOT NULL,
	[Types] [tinyint] NOT NULL,
	[Number] [nvarchar](255) NULL,
	[Expression] [nvarchar](4000) NULL,
	[Hits] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_TLC_Patterns] PRIMARY KEY CLUSTERED 
(
	[PatternId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TLC_Logs]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TLC_Logs](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Types] [tinyint] NOT NULL,
	[LogDate] [datetime] NULL,
	[Note] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TLC_Corps]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TLC_Corps](
	[CorpId] [int] IDENTITY(1,1) NOT NULL,
	[Admin] [nvarchar](255) NULL,
	[Title] [nvarchar](100) NULL,
	[Note] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Corps] PRIMARY KEY CLUSTERED 
(
	[CorpId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TLC_Collects]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TLC_Collects](
	[CollectId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[AlbumId] [int] NOT NULL,
	[Types] [tinyint] NOT NULL,
	[Pid] [int] NULL,
	[AppNo] [varchar](25) NULL,
	[Title] [nvarchar](100) NULL,
	[Number] [nvarchar](100) NULL,
	[LawState] [tinyint] NOT NULL,
	[CollectDate] [datetime] NULL,
	[Note] [nvarchar](4000) NULL,
	[NoteDate] [datetime] NULL,
	[Type] [char](2) NULL,
	[isdel] [bit] NULL,
 CONSTRAINT [PK_TLC_Collects] PRIMARY KEY CLUSTERED 
(
	[CollectId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TLC_Authoritys]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TLC_Authoritys](
	[AuthorityId] [int] IDENTITY(1,1) NOT NULL,
	[Types] [tinyint] NOT NULL,
	[Url] [nvarchar](255) NULL,
	[Title] [nvarchar](100) NULL,
	[Note] [nvarchar](4000) NULL,
 CONSTRAINT [PK_TLC_Authoritys] PRIMARY KEY CLUSTERED 
(
	[AuthorityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TLC_Albums]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TLC_Albums](
	[AlbumId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Note] [nvarchar](255) NULL,
	[Collects] [int] NOT NULL,
	[Orders] [int] NOT NULL,
	[live] [tinyint] NULL,
	[isdel] [bit] NULL,
	[IsParent] [tinyint] NULL,
 CONSTRAINT [PK_Albums] PRIMARY KEY CLUSTERED 
(
	[AlbumId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TbZhuZhiJGDMZ]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbZhuZhiJGDMZ](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[Path] [varchar](500) NULL,
 CONSTRAINT [PK_TbZhuZhiJGDMZ] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TbUserRole]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TbUserRole](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_logname] [nvarchar](30) NULL,
	[RoleCode] [nvarchar](4) NULL,
 CONSTRAINT [PK_TbUserRole] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TbUser]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbUser](
	[ID] [int] IDENTITY(1000001,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[UserPWD] [varchar](50) NULL,
	[RealName] [varchar](100) NULL,
	[DepartMentID] [int] NULL,
	[YongHuLeiXing] [char](10) NULL,
	[LianXiDianHua] [varchar](50) NULL,
	[ShouJi] [varchar](50) NULL,
	[TongXinDiZhi] [varchar](250) NULL,
	[EMail] [varchar](100) NULL,
	[QiYeID] [int] NULL,
	[SHFlag] [int] NULL,
	[En_Entrances] [varchar](80) NULL,
	[Cn_Entrances] [varchar](80) NULL,
	[QiYeMingCheng] [varchar](400) NULL,
 CONSTRAINT [PK_TbUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TbStatConfig]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TbStatConfig](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[PageFileName] [nvarchar](255) NULL,
	[ModuleName] [nvarchar](50) NULL,
	[IsStata] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TbShiJie]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbShiJie](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DaiMa] [nvarchar](2) NULL,
	[MingCheng] [varchar](20) NULL,
 CONSTRAINT [PK_TbShiJie] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TbSEntrance_Config]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbSEntrance_Config](
	[ID] [int] NOT NULL,
	[Entrance] [varchar](3) NULL,
	[EntranceDes] [nvarchar](12) NULL,
	[TitleTips] [varchar](200) NULL,
	[DbType] [int] NULL,
 CONSTRAINT [PK_TbSEntrance_Config] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:CN, 1:EN' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TbSEntrance_Config', @level2type=N'COLUMN',@level2name=N'DbType'
GO
/****** Object:  Table [dbo].[TbSendMailLog]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbSendMailLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ShouJianRen] [varchar](200) NULL,
	[YouJianMingCheng] [varchar](200) NULL,
	[ZhuanLiQuYu] [varchar](50) NULL,
	[FaSongShiJian] [datetime] NULL,
	[FaSongZhuangTai] [varchar](10) NULL,
 CONSTRAINT [PK_TbSendMailLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TbSearchNo]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TbSearchNo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[SearchNo] [int] NULL,
 CONSTRAINT [PK_TbSearchNo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TbRoleRight]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TbRoleRight](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleCode] [nvarchar](4) NULL,
	[RightCode] [nvarchar](4) NULL,
 CONSTRAINT [PK_TbRoleRight] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TbRole]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbRole](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NULL,
	[DepartMentID] [int] NULL,
 CONSTRAINT [PK_TbRole] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TbRight]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbRight](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [varchar](500) NULL,
	[PageUrl] [varchar](500) NULL,
	[PageDes] [varchar](50) NULL,
	[Nodelevel] [int] NULL,
	[XianShiShunXu] [int] NULL,
	[XianShiFlag] [int] NULL,
 CONSTRAINT [PK_TbRight] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TbOpinion]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbOpinion](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NULL,
	[UName] [varchar](20) NULL,
	[Title] [varchar](200) NULL,
	[LYTxt] [varchar](1000) NULL,
	[TJDate] [datetime] NULL,
 CONSTRAINT [PK_TbOpinion] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUserInterestSF]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUserInterestSF](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NOT NULL,
	[sfid] [int] NOT NULL,
	[addData] [date] NOT NULL,
	[interestDescription] [varchar](500) NULL,
 CONSTRAINT [PK_tblUserInterestSF] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUserInterestChange]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUserInterestChange](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[batchNo] [int] NOT NULL,
	[userId] [int] NOT NULL,
	[emailTitle] [varchar](500) NULL,
	[emailContent] [text] NULL,
	[ifEmailSended] [tinyint] NULL,
	[emailSendStatus] [varchar](500) NULL,
	[smsTitle] [varchar](100) NULL,
	[smsContent] [text] NULL,
	[ifSmsSended] [varchar](50) NULL,
	[smsSendStatus] [varchar](100) NULL,
 CONSTRAINT [PK_tblUserInterestChange_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1表示邮件发送成功，0发送邮件是否失败' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblUserInterestChange', @level2type=N'COLUMN',@level2name=N'ifEmailSended'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮件发送状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblUserInterestChange', @level2type=N'COLUMN',@level2name=N'emailSendStatus'
GO
/****** Object:  Table [dbo].[tblUserInfoExtension]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUserInfoExtension](
	[userid] [int] NOT NULL,
	[emailTemplate] [varchar](100) NULL,
	[smsTemplate] [varchar](100) NULL,
	[userInterestArea] [varchar](100) NULL,
	[ifReceiveEmail] [smallint] NULL,
	[ifReceiveSms] [smallint] NULL,
 CONSTRAINT [PK_tblUserPhoneAndEmail] PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSFUpdateHistory]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSFUpdateHistory](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[sfid] [int] NOT NULL,
	[date] [datetime] NOT NULL,
	[resultNum] [int] NOT NULL,
	[resultFile] [varchar](200) NOT NULL,
 CONSTRAINT [PK_tblSFUpdateHistory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSFResultCompare]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSFResultCompare](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sfid] [int] NOT NULL,
	[firstDate] [datetime] NOT NULL,
	[secondDate] [datetime] NOT NULL,
	[firstTimeResultNum] [bigint] NOT NULL,
	[secondTimeResultNum] [bigint] NOT NULL,
	[totalNum] [bigint] NOT NULL,
	[addNum] [bigint] NULL,
	[deleteNum] [bigint] NULL,
	[updateNum] [bigint] NULL,
	[totalResult] [varchar](max) NULL,
	[addResult] [varchar](max) NULL,
	[deleteResult] [varchar](max) NULL,
	[updateResult] [varchar](max) NULL,
 CONSTRAINT [PK_tblSFResultCompare] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSFPool]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSFPool](
	[sfid] [int] IDENTITY(1,1) NOT NULL,
	[dataSource] [varchar](20) NOT NULL,
	[searchFormula] [varchar](200) NOT NULL,
	[interestUserNum] [int] NOT NULL,
	[addData] [datetime] NOT NULL,
 CONSTRAINT [PK_tblSFPool] PRIMARY KEY CLUSTERED 
(
	[sfid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TbLog]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbLog](
	[ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[LanMu] [varchar](200) NULL,
	[ShiJian] [datetime] NULL,
	[IP] [varchar](100) NULL,
	[DiQu] [varchar](50) NULL,
	[URL] [varchar](800) NULL,
	[ShengFen] [nvarchar](255) NULL,
	[UserName] [varchar](100) NULL,
	[YongHuLeiXing] [char](10) NULL,
 CONSTRAINT [PK_TbLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TbLegalUrl_Cfg]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbLegalUrl_Cfg](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CO] [varchar](2) NULL,
	[Des] [varchar](50) NULL,
	[LegUrl] [varchar](100) NULL,
 CONSTRAINT [PK_TbLegalUrl_Cfg] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TbIP]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbIP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IP] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[flag] [int] NULL,
 CONSTRAINT [PK_TbIP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0允许 1 拒绝' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TbIP', @level2type=N'COLUMN',@level2name=N'flag'
GO
/****** Object:  Table [dbo].[TbHelpFiles]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TbHelpFiles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[helpTitle] [varchar](200) NULL,
	[fileName] [varchar](100) NULL,
	[UploadDate] [datetime] NULL,
 CONSTRAINT [PK_TbHelpFiles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbEmailActive]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbEmailActive](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_logname] [nvarchar](20) NULL,
	[lscode] [nvarchar](50) NULL,
	[lsEmail] [nvarchar](50) NULL,
	[updatetime] [datetime] NULL,
 CONSTRAINT [PK_tbEmailActive] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_UsetPatentInfo]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tb_UsetPatentInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UId] [int] NULL,
	[ApNo] [varchar](12) NULL,
	[Info] [varchar](150) NULL,
 CONSTRAINT [PK_Tb_UsetPatentInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_SameWord1]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_SameWord1](
	[ID] [int] NOT NULL,
	[Word_CH] [nvarchar](200) NULL,
	[Word_Same] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_SameWord]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_SameWord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Word_CH] [nvarchar](200) NULL,
	[Word_Same] [nvarchar](200) NULL,
 CONSTRAINT [PK_Tb_SameWord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TabMenus]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TabMenus](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](10) NULL,
	[DefaultUrl] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sysTree]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sysTree](
	[NodeId] [int] IDENTITY(1,1) NOT NULL,
	[NodeName] [nvarchar](450) NULL,
	[PNodeid] [int] NULL,
	[IsParent] [bit] NULL,
	[type] [nvarchar](10) NULL,
	[isdel] [bit] NULL,
	[des] [varchar](400) NULL,
	[live] [tinyint] NULL,
	[NodeName1] [nvarchar](800) NULL,
 CONSTRAINT [PK__sysTree__6BAE226352A420D2] PRIMARY KEY CLUSTERED 
(
	[NodeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SubMenus]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SubMenus](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](10) NULL,
	[Url] [nvarchar](255) NULL,
	[MainMenuId] [int] NULL,
	[PageFileName] [varchar](255) NULL,
	[IsMenu] [tinyint] NULL,
	[Sort] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoleRight]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleRight](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NULL,
	[RightID] [int] NULL,
 CONSTRAINT [PK_RoleRight] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecordDownload]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecordDownload](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pid] [int] NULL,
	[type] [varchar](3) NULL,
	[ipc1] [char](1) NULL,
	[ipc3] [char](3) NULL,
	[ipc4] [char](5) NULL,
	[ipc7] [char](7) NULL,
	[ipc] [varchar](15) NULL,
	[dtime] [datetime] NULL,
	[UserName] [varchar](50) NULL,
 CONSTRAINT [PK__RecordDo__3213E83F021E29CA] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RechargeTmp]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RechargeTmp](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Trade_no] [nvarchar](50) NULL,
	[USER_LOGNAME] [nvarchar](30) NULL,
	[FUNCTIONCODE] [char](10) NULL,
	[RULECODE] [nchar](10) NULL,
	[RULETYPE] [int] NULL,
	[CUSTOMTIME] [datetime] NULL,
	[STATUS] [int] NULL,
	[CAR] [int] NULL,
	[Money] [float] NULL,
	[FUNCTIONNAME] [nvarchar](50) NULL,
 CONSTRAINT [PK_RechargeTmp] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuestionInfo]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TITLE] [nvarchar](200) NULL,
	[QuestionContent] [nvarchar](1000) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [int] NULL,
	[AnserContent] [nvarchar](1000) NULL,
	[AnserUser] [nvarchar](20) NULL,
	[AnserDate] [datetime] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_QuestionInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[provincial]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[provincial](
	[provincialID] [int] NOT NULL,
	[provincialName] [varchar](50) NULL,
	[PY] [varchar](100) NULL,
	[DaiMa] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[provincialID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProUser]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProUser](
	[ID] [int] NOT NULL,
	[ProID] [int] NULL,
	[UserID] [int] NULL,
	[FuZeRenFlag] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[online]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[online](
	[id] [int] NOT NULL,
	[nc] [nvarchar](20) NULL,
	[frompage] [nvarchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NEWSINFO]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NEWSINFO](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TITLE] [nvarchar](200) NULL,
	[SUMMARY] [nvarchar](100) NULL,
	[NEWS_CONTENT] [text] NULL,
	[CREATEDATE] [datetime] NULL,
	[CREATEUSER] [nvarchar](20) NULL,
 CONSTRAINT [PK_NEWSINFO] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MEDICATION_CLASS]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MEDICATION_CLASS](
	[ID] [int] NOT NULL,
	[PID] [int] NULL,
	[name] [nvarchar](50) NULL,
	[IPC] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MainMenus]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MainMenus](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](10) NULL,
	[TabMenuId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogStat]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogStat](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[LanMu] [nvarchar](20) NULL,
	[DiQu] [nvarchar](20) NULL,
	[RiQi] [datetime] NULL,
	[xiaoshi] [tinyint] NULL,
	[FangWenLiang] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginInfo]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoginInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IP] [varchar](50) NULL,
	[UserName] [varchar](50) NULL,
	[UserPWD] [varchar](50) NULL,
	[LoginDate] [datetime] NULL,
 CONSTRAINT [PK_LoginInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IPTABLE]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IPTABLE](
	[ID] [int] NOT NULL,
	[StartIPNum] [money] NOT NULL,
	[EndIPNum] [money] NOT NULL,
	[Country] [nvarchar](255) NULL,
	[Local] [nvarchar](255) NULL,
	[ShengFen] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ipShengFen]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ipShengFen](
	[Country] [nvarchar](255) NULL,
	[城市] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ipcTreeTable]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ipcTreeTable](
	[NodeId] [int] IDENTITY(1,1) NOT NULL,
	[NodeName] [varchar](20) NULL,
	[des] [nvarchar](450) NULL,
	[Ipcs] [varchar](400) NULL,
	[PNodeid] [int] NULL,
	[IsParent] [bit] NULL,
	[type] [nvarchar](10) NULL,
	[isdel] [bit] NULL,
	[live] [tinyint] NULL,
	[live1] [varchar](2) NULL,
	[live2] [varchar](4) NULL,
	[live3] [varchar](4) NULL,
	[live4] [varchar](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[NodeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ipctree]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ipctree](
	[id] [int] NOT NULL,
	[name] [varchar](900) NULL,
	[fatherid] [int] NULL,
	[IPC] [nvarchar](20) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IPCFI]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IPCFI](
	[ID] [int] NOT NULL,
	[FI] [nvarchar](50) NULL,
	[TREE] [nchar](10) NULL,
	[EDES] [text] NULL,
	[JDES] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ipc_uc]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ipc_uc](
	[id] [int] NOT NULL,
	[ipc] [varchar](50) NULL,
	[uc] [varchar](50) NULL,
	[ipcs] [varchar](5000) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IPC_FT]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IPC_FT](
	[id] [int] NOT NULL,
	[IPC] [nvarchar](50) NULL,
	[FT] [nvarchar](50) NULL,
	[FTEDESC] [text] NULL,
	[FTJDESC] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ipc_ecla]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ipc_ecla](
	[ecla] [varchar](50) NULL,
	[ipc] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ip_Log]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ip_Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[getDate] [datetime] NULL,
	[IpInfo] [varchar](100) NULL,
	[GetUrl] [varchar](200) NULL,
 CONSTRAINT [PK_Ip_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InventorYJ]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InventorYJ](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[inventor] [varchar](200) NULL,
 CONSTRAINT [PK_InventorYJ] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Inventor]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Inventor](
	[pubno] [char](14) NULL,
	[pubtype] [char](1) NULL,
	[pubvol] [char](8) NULL,
	[filingno] [char](12) NULL,
	[inventor] [char](200) NULL,
	[mark] [char](10) NULL,
	[pubmk] [bit] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GK_JQHPZB]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GK_JQHPZB](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[GONGKAIGGR] [varchar](16) NULL,
	[JUANQIH] [varchar](16) NULL,
 CONSTRAINT [PK_GK_JQHPZB] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FunctionRules]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FunctionRules](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RuleCode] [nchar](10) NULL,
	[FunctionCode] [nchar](10) NULL,
	[FunctionType] [nchar](10) NULL,
	[Money] [float] NULL,
	[LimitTimes] [int] NULL,
 CONSTRAINT [PK_FunctionRules] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FIELDC]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FIELDC](
	[ID] [int] NOT NULL,
	[FIELDC] [varchar](5) NULL,
	[DESCRIBE] [nvarchar](200) NULL,
	[PFIELDC] [varchar](5) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ddlitem]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ddlitem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[itemname] [varchar](20) NULL,
	[storid] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomFunction]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomFunction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[User_LogName] [nvarchar](30) NULL,
	[CustomTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[CustomType] [nchar](10) NULL,
	[RuleCode] [nchar](10) NULL,
	[UseTimes] [int] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_CustomProJect] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CSIndex_Value]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CSIndex_Value](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[itemid] [int] NULL,
	[valuename] [nvarchar](50) NULL,
	[createuserid] [int] NULL,
	[createtime] [datetime] NULL,
	[isdel] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CSIndex_Result]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CSIndex_Result](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[zid] [int] NULL,
	[type] [char](2) NULL,
	[pid] [int] NULL,
	[itemid] [int] NULL,
	[valueid] [int] NULL,
	[createuserid] [int] NULL,
	[createtime] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CSIndex_Item]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CSIndex_Item](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[itemname] [nvarchar](50) NULL,
	[createuserid] [int] NULL,
	[createtime] [datetime] NULL,
	[isdel] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreditsConfig]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditsConfig](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CREDID] [nchar](10) NULL,
	[CREDNAME] [nvarchar](50) NULL,
	[MONEY] [float] NULL,
 CONSTRAINT [PK_CreditsConfig] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CredHis]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CredHis](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_logname] [nvarchar](30) NULL,
	[updatetime] [datetime] NULL,
	[CredID] [nchar](10) NULL,
 CONSTRAINT [PK_CredHis] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CountryConfig]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CountryConfig](
	[id] [float] NOT NULL,
	[DaiMa] [nvarchar](255) NULL,
	[MingCheng] [nvarchar](100) NULL,
	[leixing] [float] NULL,
 CONSTRAINT [PK_CountryConfig] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[city]    Script Date: 12/02/2015 16:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[city](
	[cityID] [int] NOT NULL,
	[cityName] [varchar](50) NOT NULL,
	[provincialID] [int] NOT NULL,
	[PY] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[cityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_C_EARLY_WARNING_isupdate]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[C_EARLY_WARNING] ADD  CONSTRAINT [DF_C_EARLY_WARNING_isupdate]  DEFAULT ((0)) FOR [ISUPDATE]
GO
/****** Object:  Default [DF_C_W_SEARCHLIS_HisOrder]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[C_W_SEARCHLIS] ADD  CONSTRAINT [DF_C_W_SEARCHLIS_HisOrder]  DEFAULT ((0)) FOR [HisOrder]
GO
/****** Object:  Default [DF_C_W_SECARCH_CHANGEDATE]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[C_W_SECARCH] ADD  CONSTRAINT [DF_C_W_SECARCH_CHANGEDATE]  DEFAULT (getdate()) FOR [CHANGEDATE]
GO
/****** Object:  Default [DF_C_W_SECARCH_CURRENTNUM]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[C_W_SECARCH] ADD  CONSTRAINT [DF_C_W_SECARCH_CURRENTNUM]  DEFAULT ((0)) FOR [CURRENTNUM]
GO
/****** Object:  Default [DF_C_W_SECARCH_CHANGENUM]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[C_W_SECARCH] ADD  CONSTRAINT [DF_C_W_SECARCH_CHANGENUM]  DEFAULT ((0)) FOR [CHANGENUM]
GO
/****** Object:  Default [DF__CSIndex_I__creat__22401542]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[CSIndex_Item] ADD  DEFAULT (getdate()) FOR [createtime]
GO
/****** Object:  Default [DF__CSIndex_I__isdel__2334397B]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[CSIndex_Item] ADD  DEFAULT ((0)) FOR [isdel]
GO
/****** Object:  Default [DF__CSIndex_R__creat__214BF109]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[CSIndex_Result] ADD  DEFAULT (getdate()) FOR [createtime]
GO
/****** Object:  Default [DF__CSIndex_V__creat__1F63A897]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[CSIndex_Value] ADD  DEFAULT (getdate()) FOR [createtime]
GO
/****** Object:  Default [DF__CSIndex_V__isdel__2057CCD0]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[CSIndex_Value] ADD  DEFAULT ((0)) FOR [isdel]
GO
/****** Object:  Default [DF_CustomFunction_UseTimes]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[CustomFunction] ADD  CONSTRAINT [DF_CustomFunction_UseTimes]  DEFAULT ((0)) FOR [UseTimes]
GO
/****** Object:  Default [DF_CustomFunction_status]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[CustomFunction] ADD  CONSTRAINT [DF_CustomFunction_status]  DEFAULT ((0)) FOR [status]
GO
/****** Object:  Default [DF_LoginInfo_LoginDate]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[LoginInfo] ADD  CONSTRAINT [DF_LoginInfo_LoginDate]  DEFAULT (getdate()) FOR [LoginDate]
GO
/****** Object:  Default [DF_RechargeTmp_CAR]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[RechargeTmp] ADD  CONSTRAINT [DF_RechargeTmp_CAR]  DEFAULT ((0)) FOR [CAR]
GO
/****** Object:  Default [DF__RecordDow__dtime__0406723C]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[RecordDownload] ADD  CONSTRAINT [DF__RecordDow__dtime__0406723C]  DEFAULT (getdate()) FOR [dtime]
GO
/****** Object:  Default [DF_sysTree_isdel]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[sysTree] ADD  CONSTRAINT [DF_sysTree_isdel]  DEFAULT ((0)) FOR [isdel]
GO
/****** Object:  Default [DF_sysTree_live]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[sysTree] ADD  CONSTRAINT [DF_sysTree_live]  DEFAULT ((0)) FOR [live]
GO
/****** Object:  Default [DF_tbEmailActive_updatetime]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[tbEmailActive] ADD  CONSTRAINT [DF_tbEmailActive_updatetime]  DEFAULT (getdate()) FOR [updatetime]
GO
/****** Object:  Default [DF_TbHelpFiles_UploadDate]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbHelpFiles] ADD  CONSTRAINT [DF_TbHelpFiles_UploadDate]  DEFAULT (getdate()) FOR [UploadDate]
GO
/****** Object:  Default [DF_TbIP_CreateDate]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbIP] ADD  CONSTRAINT [DF_TbIP_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_TbIP_flag]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbIP] ADD  CONSTRAINT [DF_TbIP_flag]  DEFAULT ((0)) FOR [flag]
GO
/****** Object:  Default [DF_TbLog_ShiJian]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbLog] ADD  CONSTRAINT [DF_TbLog_ShiJian]  DEFAULT (getdate()) FOR [ShiJian]
GO
/****** Object:  Default [DF_tblSFPool_addData]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[tblSFPool] ADD  CONSTRAINT [DF_tblSFPool_addData]  DEFAULT (((2011)-(8))-(8)) FOR [addData]
GO
/****** Object:  Default [DF_tblSFResultCompare_firstDate]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[tblSFResultCompare] ADD  CONSTRAINT [DF_tblSFResultCompare_firstDate]  DEFAULT (((2011)-(8))-(12)) FOR [firstDate]
GO
/****** Object:  Default [DF_tblSFResultCompare_secondDate]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[tblSFResultCompare] ADD  CONSTRAINT [DF_tblSFResultCompare_secondDate]  DEFAULT (((2011)-(8))-(12)) FOR [secondDate]
GO
/****** Object:  Default [DF_TbOpinion_TJDate]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbOpinion] ADD  CONSTRAINT [DF_TbOpinion_TJDate]  DEFAULT (getdate()) FOR [TJDate]
GO
/****** Object:  Default [DF_TbRight_PageName]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbRight] ADD  CONSTRAINT [DF_TbRight_PageName]  DEFAULT ('') FOR [PageName]
GO
/****** Object:  Default [DF_TbRight_PageUrl]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbRight] ADD  CONSTRAINT [DF_TbRight_PageUrl]  DEFAULT ('') FOR [PageUrl]
GO
/****** Object:  Default [DF_TbRight_PageDes]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbRight] ADD  CONSTRAINT [DF_TbRight_PageDes]  DEFAULT ('') FOR [PageDes]
GO
/****** Object:  Default [DF_TbRight_Nodelevel]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbRight] ADD  CONSTRAINT [DF_TbRight_Nodelevel]  DEFAULT ((0)) FOR [Nodelevel]
GO
/****** Object:  Default [DF_TbRight_XianShiShunXu]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbRight] ADD  CONSTRAINT [DF_TbRight_XianShiShunXu]  DEFAULT ((0)) FOR [XianShiShunXu]
GO
/****** Object:  Default [DF_TbRight_XianShiFlag]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbRight] ADD  CONSTRAINT [DF_TbRight_XianShiFlag]  DEFAULT ((0)) FOR [XianShiFlag]
GO
/****** Object:  Default [DF_TbSearchNo_UserID]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbSearchNo] ADD  CONSTRAINT [DF_TbSearchNo_UserID]  DEFAULT ((0)) FOR [UserID]
GO
/****** Object:  Default [DF_TbSearchNo_SearchNo]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbSearchNo] ADD  CONSTRAINT [DF_TbSearchNo_SearchNo]  DEFAULT ((0)) FOR [SearchNo]
GO
/****** Object:  Default [DF_TbUser_DepartMentID]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbUser] ADD  CONSTRAINT [DF_TbUser_DepartMentID]  DEFAULT ((0)) FOR [DepartMentID]
GO
/****** Object:  Default [DF_TbUser_YongHuLeiXing]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbUser] ADD  CONSTRAINT [DF_TbUser_YongHuLeiXing]  DEFAULT ('') FOR [YongHuLeiXing]
GO
/****** Object:  Default [DF_TbUser_SHFlag]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TbUser] ADD  CONSTRAINT [DF_TbUser_SHFlag]  DEFAULT ((1)) FOR [SHFlag]
GO
/****** Object:  Default [DF__NQuestion__UserI__5BA37B27]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Albums] ADD  CONSTRAINT [DF__NQuestion__UserI__5BA37B27]  DEFAULT ((0)) FOR [UserId]
GO
/****** Object:  Default [DF__NQuestion__Paren__5C979F60]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Albums] ADD  CONSTRAINT [DF__NQuestion__Paren__5C979F60]  DEFAULT ((0)) FOR [ParentId]
GO
/****** Object:  Default [DF__NQuestion__Colle__5D8BC399]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Albums] ADD  CONSTRAINT [DF__NQuestion__Colle__5D8BC399]  DEFAULT ((0)) FOR [Collects]
GO
/****** Object:  Default [DF__NQuestion__Order__5E7FE7D2]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Albums] ADD  CONSTRAINT [DF__NQuestion__Order__5E7FE7D2]  DEFAULT ((0)) FOR [Orders]
GO
/****** Object:  Default [DF_TLC_Albums_isdel]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Albums] ADD  CONSTRAINT [DF_TLC_Albums_isdel]  DEFAULT ((0)) FOR [isdel]
GO
/****** Object:  Default [DF_TLC_Authoritys_Types]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Authoritys] ADD  CONSTRAINT [DF_TLC_Authoritys_Types]  DEFAULT ((0)) FOR [Types]
GO
/****** Object:  Default [DF_TLC_Collects_UserId]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Collects] ADD  CONSTRAINT [DF_TLC_Collects_UserId]  DEFAULT ((0)) FOR [UserId]
GO
/****** Object:  Default [DF_TLC_Collects_AlbumId]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Collects] ADD  CONSTRAINT [DF_TLC_Collects_AlbumId]  DEFAULT ((0)) FOR [AlbumId]
GO
/****** Object:  Default [DF_TLC_Collects_Types]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Collects] ADD  CONSTRAINT [DF_TLC_Collects_Types]  DEFAULT ((0)) FOR [Types]
GO
/****** Object:  Default [DF_TLC_Collects_State]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Collects] ADD  CONSTRAINT [DF_TLC_Collects_State]  DEFAULT ((0)) FOR [LawState]
GO
/****** Object:  Default [DF_TLC_Collects_isdel]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Collects] ADD  CONSTRAINT [DF_TLC_Collects_isdel]  DEFAULT ((0)) FOR [isdel]
GO
/****** Object:  Default [DF__TLC_Logs__UserId__43A1090D]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Logs] ADD  DEFAULT ((0)) FOR [UserId]
GO
/****** Object:  Default [DF__TLC_Logs__Types__44952D46]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Logs] ADD  DEFAULT ((0)) FOR [Types]
GO
/****** Object:  Default [DF_TLC_Patterns_UserId]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Patterns] ADD  CONSTRAINT [DF_TLC_Patterns_UserId]  DEFAULT ((0)) FOR [UserId]
GO
/****** Object:  Default [DF_TLC_Patterns_Source]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Patterns] ADD  CONSTRAINT [DF_TLC_Patterns_Source]  DEFAULT ((0)) FOR [Source]
GO
/****** Object:  Default [DF_TLC_Patterns_Types]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Patterns] ADD  CONSTRAINT [DF_TLC_Patterns_Types]  DEFAULT ((0)) FOR [Types]
GO
/****** Object:  Default [DF_TLC_Patterns_Hits]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[TLC_Patterns] ADD  CONSTRAINT [DF_TLC_Patterns_Hits]  DEFAULT ((0)) FOR [Hits]
GO
/****** Object:  Default [DF_ztdb_Iscore]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[ztdb] ADD  CONSTRAINT [DF_ztdb_Iscore]  DEFAULT ((0)) FOR [Iscore]
GO
/****** Object:  Default [DF_ztdb_isUpdate]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[ztdb] ADD  CONSTRAINT [DF_ztdb_isUpdate]  DEFAULT ((0)) FOR [isUpdate]
GO
/****** Object:  Default [DF_ZtDbList_CreateTime]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[ZtDbList] ADD  CONSTRAINT [DF_ZtDbList_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_ZtDbList_IsDel]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[ZtDbList] ADD  CONSTRAINT [DF_ZtDbList_IsDel]  DEFAULT ((0)) FOR [IsDel]
GO
/****** Object:  Default [DF__ztsp__isAutoUpda__4959E263]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[ztsp] ADD  DEFAULT ((0)) FOR [isAutoUpdate]
GO
/****** Object:  Default [DF__ztsp__isUsed__4A4E069C]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[ztsp] ADD  DEFAULT ((0)) FOR [isUsed]
GO
/****** Object:  Default [DF__ztsp__UpdateDate__4B422AD5]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[ztsp] ADD  DEFAULT (getdate()) FOR [UpdateDate]
GO
/****** Object:  Default [DF__ztsp__isdel__4C364F0E]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[ztsp] ADD  DEFAULT ((0)) FOR [isdel]
GO
/****** Object:  Default [DF_ztTree_CreateTime]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[ztTree] ADD  CONSTRAINT [DF_ztTree_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_ztTree_isdel]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[ztTree] ADD  CONSTRAINT [DF_ztTree_isdel]  DEFAULT ((0)) FOR [isdel]
GO
/****** Object:  Default [DF_ztTree_live]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[ztTree] ADD  CONSTRAINT [DF_ztTree_live]  DEFAULT ((0)) FOR [live]
GO
/****** Object:  Default [DF_ztTree_PNid]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[ztTree] ADD  CONSTRAINT [DF_ztTree_PNid]  DEFAULT ('') FOR [PNid]
GO
/****** Object:  ForeignKey [FK_pro_city_provincialID]    Script Date: 12/02/2015 16:05:05 ******/
ALTER TABLE [dbo].[city]  WITH CHECK ADD  CONSTRAINT [FK_pro_city_provincialID] FOREIGN KEY([provincialID])
REFERENCES [dbo].[provincial] ([provincialID])
GO
ALTER TABLE [dbo].[city] CHECK CONSTRAINT [FK_pro_city_provincialID]
GO
