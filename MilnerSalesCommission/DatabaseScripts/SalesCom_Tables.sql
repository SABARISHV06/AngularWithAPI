USE [SalesComDB]
GO

/****** Object:  Table [dbo].[TenureBonus]    Script Date: 11/14/2016 17:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TenureBonus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TenureBonus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PlanID] [int] NOT NULL,
	[Months] [nvarchar](10) NOT NULL,
	[EntryPointA] [int] NOT NULL,
	[EntryPointB] [int] NOT NULL,
	[Percentage] [int] NOT NULL,
	[Tier] [nvarchar](50)NOT NULL,
) ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[Roles]    Script Date: 11/14/2016 17:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RoleOperations]    Script Date: 11/14/2016 17:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RoleOperations](
	[RoleID] [int] NOT NULL,
	[OperationID] [int] NOT NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Operations]    Script Date: 11/14/2016 17:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Operations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Operations](
	[OperationID] [int] NOT NULL,
	[OperationName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Operations] PRIMARY KEY CLUSTERED 
(
	[OperationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[EmpComponents]    Script Date: 11/14/2016 17:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpComponents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmpComponents](
	[UID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [nvarchar] (255) NOT NULL,
	[AccountName] [nvarchar](255) NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[MiddleName] [nvarchar](255) NULL,
	[RoleID] [int] NOT NULL,
	[DateofHire] [datetime] NULL,
	[DateInPosition] [datetime] NULL,
	[PrimaryBranch] [nvarchar](255) NULL,
	[SecondaryBranch] [nvarchar](255) NULL,
	[PayPlanID] [int] NULL,
	[BPSalary] [bit] NULL,
	[BPDraw] [bit] NULL,
	[MonthAmount] Decimal(20, 2) NULL,
	[TypeofDraw] [int] NULL,
	[DRPercentage] [int] NULL,
	[DDPeriod] [datetime] NULL,
	[DDAmount] Decimal(20, 2) NULL,
	[ReportMgr] [int] NULL,
	[ApproveMgr] [int] NULL,
	[Email] [nvarchar](255) NULL,
	[DrawTerm] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[LastLogin] [datetime] NOT NULL
 CONSTRAINT [PK_EmpComponents] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CPlanComponents]    Script Date: 11/14/2016 17:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CPlanComponents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CPlanComponents](
	[PlanID] [int] IDENTITY(1,1) NOT NULL,
	[PlanName] [nvarchar](255) NOT NULL,
	[BasisType] [int] NOT NULL,
	[BMQuotaBonus] [bit] NOT NULL,
	[SMEligible] [bit] NOT NULL,
	[TenureBonus] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_CPlanComponents] PRIMARY KEY CLUSTERED 
(
	[PlanID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
--/****** Object:  Table [dbo].[CommissionComponents]    Script Date: 11/14/2016 17:10:25 ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommissionComponents]') AND type in (N'U'))
--BEGIN
--CREATE TABLE [dbo].[CommissionComponents](
--	[ID] [int] IDENTITY(1,1) NOT NULL,
--	[SalesPerson] [nvarchar](255) NULL,
--	[DateofSale] [datetime] NULL,
--	[EntryDate] [datetime] NULL,
--	[InvoiceNumber] [nvarchar] (255) NULL,
--	[CustomerNumber] [int] NULL,
--	[CustomerName] [nvarchar](255) NULL,
--	[CommentSold] [nvarchar](255) NULL,
--	[CustomerType] [int] NULL,
--	[SplitSalePerson] [nvarchar](255) NULL,
--	[SplitSalePersonID] [nvarchar](255) NULL,
--	[AmountofSale] Decimal(20, 2) NULL,
--	[CostofGoods] Decimal(20, 2) NULL,
--	[DollarVolume] Decimal(20, 2) NULL,
--	[BaseCommission] Decimal(20, 2) NULL,
--	[LeaseCommission] Decimal(20, 2) NULL,
--	[ServiceCommission] Decimal(20, 2) NULL,
--	[TravelCommission] Decimal(20, 2) NULL,
--	[CashCommission] Decimal(20, 2) NULL,
--	[SpecialCommission] Decimal(20, 2) NULL,
--	[Adjustments] Decimal(20, 2) NULL,
--	[TipLeadName] [nvarchar](255) NULL,
--	[TipLeadAmount] Decimal(20, 2) NULL,
--	[TotalCEarned] Decimal(20, 2) NULL,
--	[TradeIn] Decimal(20, 2) NULL,
--	[Status] [int] NULL,
--	[AccountPeriod] [datetime] NULL,
--	[ProcesByPayroll] [bit] NULL,
--	[BranchID] [int] NULL,
--	[SaleType] [int] NULL,
--	[ProductLine] [int] NULL,
--	[Comments] [xml] NULL,
--	[CreatedOn] [datetime] NULL,
--	[CreatedBy] [int] NULL,
--	[ModifiedOn] [datetime] NULL,
--	[ModifiedBy] [int] NULL,
--	[IsActive] [bit] NOT NULL,
-- CONSTRAINT [PK_CommissionComponents] PRIMARY KEY CLUSTERED 
--(
--	[ID] ASC
--)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
--) ON [PRIMARY]
--END
--GO
/****** Object:  Table [dbo].[AuditLogs]    Script Date: 11/14/2016 17:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuditLogs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AuditLogs](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [smallint] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Action] [nvarchar](1500) NOT NULL,
	[UserID] [int] NULL,
	[ExceptionID] [int] NULL,
	[StackTrace] [nvarchar](max) NOT NULL,
	[ThreadId] [nvarchar](128) NULL,
	[SessionUser] [nvarchar](50) NULL,
	[SessionID] [nvarchar](50) NULL,
 CONSTRAINT [PK_AuditLogs] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[BIMonthlyBonusInfo]    Script Date: 11/14/2016 17:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BIMonthlyBonusInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BIMonthlyBonusInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PlanID] [int] NOT NULL,
	[Months] [nvarchar](10) NOT NULL,
	[EntryPointA] [int] NOT NULL,
	[EntryPointB] [int] NOT NULL,
	[Percentage] [int] NOT NULL,
	[Tier] [nvarchar](50)NOT NULL,
 CONSTRAINT [PK_BIMonthlyBonusInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TGPCustomerInfo]    Script Date: 11/14/2016 17:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TGPCustomerInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TGPCustomerInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PlanID] [int] NOT NULL,
	[SalesType] [int] NOT NULL,
	[Percentage] [int] NOT NULL,
	[CustomerType] [int] NOT NULL,
 CONSTRAINT [PK_TGPCustomerInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  ForeignKey [FK_TGPCustomerInfo_CPlanComponents]    Script Date: 11/14/2016 17:10:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TGPCustomerInfo_CPlanComponents]') AND parent_object_id = OBJECT_ID(N'[dbo].[TGPCustomerInfo]'))
ALTER TABLE [dbo].[TGPCustomerInfo]  WITH CHECK ADD  CONSTRAINT [FK_TGPCustomerInfo_CPlanComponents] FOREIGN KEY([PlanID])
REFERENCES [dbo].[CPlanComponents] ([PlanID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TGPCustomerInfo_CPlanComponents]') AND parent_object_id = OBJECT_ID(N'[dbo].[TGPCustomerInfo]'))
ALTER TABLE [dbo].[TGPCustomerInfo] CHECK CONSTRAINT [FK_TGPCustomerInfo_CPlanComponents]
GO

/****** Object:  Table [dbo].[Branches]    Script Date: 11/14/2016 17:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Branches]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Branches](
	[BranchID] [int] IDENTITY(1,1) NOT NULL,
	[BranchName] [nvarchar](255) NOT NULL,
) ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[UsersLog]    Script Date: 11/14/2016 17:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].UsersLog(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[LoggedIn] [datetime] NOT NULL,
	[LoggedOut] [datetime] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ReportDetails]    Script Date: 11/14/2016 17:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReportDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ReportDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[EmployeeID] [nvarchar] (255) NOT NULL,
	[ReportPeriod] datetime NOT NULL,
	--[DrawDificitPeriod] datetime,
	[TotalCommission] Decimal(20, 2) NULL,
	[DrawPaid] Decimal(20, 2) NULL,
	[DrawRecovered] Decimal(20, 2) NULL,
	[CommissionDue] Decimal(20, 2) NULL,
	[Salary] Decimal(20, 2) NULL,
	[BimonthlyBonus] Decimal(20, 2) NULL,
	[TenureBonus] Decimal(20, 2) NULL,
	[TotalEarnings] Decimal(20, 2) NULL,
	[DrawType] int NULL,
	[ApplicableTill] nvarchar(255)NULL,
	[DrawAmount] Decimal(20,2) NULL,
	[RecoverablePercentage] Decimal(20, 2) NULL,
	[DrawDificit] Decimal(20, 2) NULL,
 CONSTRAINT [PK_ReportDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CommissionComponents]    Script Date: 11/14/2016 17:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommissionComponents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CommissionComponents](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SalesPerson] [nvarchar](255) NULL,
	[DateofSale] [datetime] NULL,
	[EntryDate] [datetime] NULL,
	[InvoiceNumber] [nvarchar] (255) NULL,
	[CustomerNumber] [nvarchar](255) NULL,
	[CustomerName] [nvarchar](255) NULL,
	[CommentSold] [nvarchar](255) NULL,
	[CustomerType] [int] NULL,
	[SplitSalePerson] [nvarchar](255) NULL,
	[SplitSalePersonID] [nvarchar](255) NULL,
	[AmountofSale] Decimal(20, 2) NULL,
	[CostofGoods] Decimal(20, 2) NULL,
	[DollarVolume] Decimal(20, 2) NULL,
	[BaseCommission] Decimal(20, 2) NULL,
	[LeaseCommission] Decimal(20, 2) NULL,
	[ServiceCommission] Decimal(20, 2) NULL,
	[TravelCommission] Decimal(20, 2) NULL,
	[CashCommission] Decimal(20, 2) NULL,
	[SpecialCommission] Decimal(20, 2) NULL,
	[TradeIn] Decimal(20, 2) NULL,
	[MainCommissionID] [int] NULL,
	[TipLeadID] [int] NULL,
	[TipLeadEmpID] [nvarchar] (255) NULL,
	[TipLeadName] [nvarchar](255) NULL,
	[TipLeadAmount] Decimal(20, 2) NULL,
	[PositiveAdjustments] Decimal(20, 2) NULL,
	[NegativeAdjustments] Decimal(20, 2) NULL,
	[CompanyContribution] Decimal(20, 2) NULL,
	[SlipType] [int] NULL,
	[TotalCEarned] Decimal(20, 2) NULL,
	[Status] [int] NULL,
	[AccountPeriod] [datetime] NULL,
	[ProcesByPayroll] [bit] NULL,
	[BranchID] [int] NULL,
	[SaleType] [int] NULL,
	[ProductLine] [int] NULL,
	[Comments] [xml] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsNotified] [bit] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_CommissionComponents] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SaleType]    Script Date: 05/11/2017 17:10:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaleType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SaleType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SalesTypeName] [varchar](50) NOT NULL,
	[IsNewCustomer] [bit] NULL,
	[IsExistingCustomer] [bit] NULL,
	[IsBiMonthlyBonus] [bit] NULL,
	[IsTenureBonus] [bit] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[PayrollConfig]  *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayrollConfig]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayrollConfig](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NOT NULL,
	[Period] [int] NOT NULL,
	[Month] [nvarchar](50) NOT NULL,
	[DateFrom] [datetime] NULL,
	[DateTo] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
) ON [PRIMARY]
END
GO