-- Copyright 2016-2017, Milner Technologies, Inc.
--
-- This document contains data and information proprietary to
-- Milner Technologies, Inc.  This data shall not be disclosed,
-- disseminated, reproduced or otherwise used outside of the
-- facilities of Milner Technologies, Inc., without the express
-- written consent of an officer of the corporation.

USE [SalesComDB]
GO

/****** Object:  UserDefinedTableType [dbo].[TGPCustomerInfo]    Script Date: 12/20/2016 20:05:38 ******/
CREATE TYPE [dbo].[TGPCustomerInfo] AS TABLE(
	[ID] [int] NULL,
	[PlanID] [int] NOT NULL,
	[SalesType] [int] NOT NULL,
	[Percentage] [decimal](20,3) NOT NULL,
	[CustomerType] [int] NOT NULL
)
GO

/****** Object:  UserDefinedTableType [dbo].[TenureBonus]    Script Date: 12/20/2016 20:05:33 ******/
CREATE TYPE [dbo].[TenureBonus] AS TABLE(
	[ID] [int] NULL,
	[PlanID] [int] NOT NULL,
	[Months] [nvarchar](10) NOT NULL,
	[EntryPointA] [int] NOT NULL,
	[EntryPointB] [int] NOT NULL,
	[Percentage] [decimal](20,3) NOT NULL,
	[Tier] [nvarchar](50) NOT NULL
)
GO

/****** Object:  UserDefinedTableType [dbo].[BIMonthlyBonusInfo]    Script Date: 12/20/2016 19:25:42 ******/
CREATE TYPE [dbo].[BIMonthlyBonusInfo] AS TABLE(
	[ID] [int] NULL,
	[PlanID] [int] NOT NULL,
	[Months] [nvarchar](10) NOT NULL,
	[EntryPointA] [int] NOT NULL,
	[EntryPointB] [int] NOT NULL,
	[Percentage] [decimal](20,3) NOT NULL,
	[Tier] [nvarchar](50) NOT NULL
)
GO

/****** Object:  UserDefinedTableType [dbo].[TipLeadSlip]    Script Date: 24/02/2017 19:25:42 ******/
CREATE TYPE [dbo].[TipLeadSlip] AS TABLE(
[ID] [int] NULL,
[MainCommissionID] [int] NULL,
	[TipLeadID] [int] NULL,
	[TipLeadEmpID] [nvarchar] (50) NULL,
	[TipLeadName] [nvarchar](50) NULL,
	[TipLeadAmount] Decimal(20, 3) NULL,
	[PositiveAdjustments] Decimal(20, 3) NULL,
	[NegativeAdjustments] Decimal(20, 3) NULL,
	[CompanyContribution] Decimal(20, 3) NULL,
	[SlipType] [int] NULL,
	[TotalCEarned] Decimal(20, 3) NULL,
	[Status] [int] NULL
)
GO
