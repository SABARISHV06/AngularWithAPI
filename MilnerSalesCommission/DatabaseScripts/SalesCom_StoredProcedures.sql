-- Copyright 2016-2017, Milner Technologies, Inc.
--
-- This document contains data and information proprietary to
-- Milner Technologies, Inc.  This data shall not be disclosed,
-- disseminated, reproduced or otherwise used outside of the
-- facilities of Milner Technologies, Inc., without the express
-- written consent of an officer of the corporation.

USE [SalesComDB]
GO


/****** Object:  StoredProcedure [dbo].[SetAuditLogs]    Script Date: 11/12/2016 7:52:10 AM
-- Description:	Insert Audit Logs ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetAuditLogs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SetAuditLogs]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SetAuditLogs]    
(  
 @Type smallint,  
 @DateTime datetime,  
 @Action nvarchar(1500),  
 @UserID int,
 @ExceptionID int,  
 @StackTrace nvarchar(max),  
 @ThreadId nvarchar(128),  
 @SessionUser nvarchar(50),  
 @SessionID nvarchar(50)  
)  
AS    
BEGIN  
  SET NOCOUNT ON;  
  DECLARE @errMsg VARCHAR(255)  
  DECLARE @beginTranCount INT  
  SET @beginTranCount = @@TRANCOUNT     
  BEGIN TRANSACTION  
  INSERT INTO [dbo].[AuditLogs]  
      ([Type]  
      ,[DateTime]  
      ,[Action]  
      ,[UserID]  
      ,[ExceptionID]  
      ,[StackTrace]  
      ,[ThreadId]  
      ,[SessionUser]  
      ,[SessionID])  
   VALUES  
      (@Type,@DateTime,@Action,@UserID,@ExceptionID,  
      @StackTrace,@ThreadId,@SessionUser,@SessionID)    
		IF (@@ERROR <> 0 )
			BEGIN 
				set @errMsg = 'Error: SetAuditLogs -> Unable to insert into AuditLogs table';
				GOTO CLEANUP
			END
		END
		CLEANUP:
		IF @errMsg IS NULL 
			BEGIN
				IF @@TRANCOUNT > @beginTranCount 
					BEGIN   
						COMMIT TRANSACTION
						RETURN 0
					END
			END
		ELSE 
			BEGIN
				IF @@TRANCOUNT > 0 
					ROLLBACK TRANSACTION   
					RAISERROR(@errMsg, 16, 1)
				RETURN -1
			END

GO
/****** Object:  StoredProcedure [dbo].[GetAllRoles]    Script Date: 11/12/2016 7:52:10 AM 
-- Description:	Get All Roles ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllRoles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllRoles]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllRoles]  
AS  
BEGIN
	SET NOCOUNT OFF;
	SELECT * FROM [dbo].[Roles] ORDER BY RoleID ASC   
END
GO

/****** Object:  StoredProcedure [dbo].[GetRoleOperations]    Script Date: 11/12/2016 7:52:10 AM ******/
-- Description:	Get Role Operations by RoleID

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRoleOperations]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRoleOperations]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRoleOperations]  
( 
	@RoleID int 
)
AS  
BEGIN
	SET NOCOUNT OFF;
	SELECT OperationID FROM [dbo].[RoleOperations] WHERE ([RoleID] = @RoleID )     
END
GO

/****** Object:  StoredProcedure [dbo].[GetAllRoleOperations]    Script Date: 11/12/2016 7:52:10 AM ******/
-- Description:	Get All Role Operations

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllRoleOperations]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllRoleOperations]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllRoleOperations]  
AS  
BEGIN
	SET NOCOUNT OFF;
	SELECT * FROM [dbo].[RoleOperations] ORDER BY OperationID ASC   
END
GO
/****** Object:  StoredProcedure [dbo].[GetRoleOperationsByRoleID]    Script Date: 11/12/2016 7:52:10 AM ******/
-- Description:	Get All Role Operations by RoleID

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRoleOperationsByRoleID]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRoleOperationsByRoleID]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRoleOperationsByRoleID]  
( 
	@RoleID int 
)
AS  
BEGIN
	SET NOCOUNT OFF;
	SELECT * FROM [dbo].[RoleOperations] WHERE RoleID=@RoleID ORDER BY OperationID ASC   
END
GO


/****** Object:  StoredProcedure [dbo].[AddPlanComponents]    Script Date: 12/15/2016 14:13:23 ******/
-- Description:	Insert Plan details

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddPlanComponents]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddPlanComponents]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddPlanComponents]  
(
	@PlanName nvarchar(255),
	@BasisType int,
	@BMQuotaBonus bit,
	@SMEligible bit,
	@TenureBonus bit,
	@CreatedOn datetime,
	@CreatedBy int,
	@IsActive bit,
	@TGPCustomerInfo TGPCustomerInfo readonly,
	@Tenure TenureBonus readonly,
	@BIMonthlyBonusInfo BIMonthlyBonusInfo readonly,
	@PlanID int out
)
AS  
BEGIN
			SET NOCOUNT ON;
			DECLARE @errMsg VARCHAR(255)
			DECLARE @beginTranCount INT
			SET @beginTranCount = @@TRANCOUNT			
			BEGIN TRANSACTION
			
	INSERT INTO [dbo].[CPlanComponents]
           (PlanName,
			BasisType,
			BMQuotaBonus,
			SMEligible,
			TenureBonus,
			CreatedOn,
			CreatedBy,
			IsActive)
     VALUES
           (@PlanName,@BasisType,@BMQuotaBonus,@SMEligible,@TenureBonus,@CreatedOn,@CreatedBy,@IsActive)  
     SET @PlanID= @@IDENTITY
     INSERT INTO TGPCustomerInfo(PlanID,SalesType,Percentage,CustomerType) 
            SELECT @PlanID,SalesType,Percentage,CustomerType FROM @TGPCustomerInfo;     
     INSERT INTO TenureBonus(PlanID,Months,EntryPointA,EntryPointB,Percentage,Tier) 
            SELECT @PlanID,Months,EntryPointA,EntryPointB,Percentage,Tier FROM @Tenure;       
     INSERT INTO BIMonthlyBonusInfo(PlanID,Months,EntryPointA,EntryPointB,Percentage,Tier) 
            SELECT @PlanID,Months,EntryPointA,EntryPointB,Percentage,Tier FROM @BIMonthlyBonusInfo;       
           
IF (@@ERROR <> 0 )
    BEGIN 
		set @errMsg = 'Error: AddPlanComponents -> Unable to insert into PlanComponents tables';
		GOTO CLEANUP
	END

CLEANUP:
        IF @errMsg IS NULL 
            BEGIN
                IF @@TRANCOUNT > @beginTranCount 
                    BEGIN   
                        COMMIT TRANSACTION
                        RETURN 0
                    END
            END
        ELSE 
            BEGIN
                IF @@TRANCOUNT > 0 
                    ROLLBACK TRANSACTION   
                RAISERROR(@errMsg, 16, 1)
                RETURN -1
            END
    END

GO
/****** Object:  StoredProcedure [dbo].[SetPlanComponents]    Script Date: 12/15/2016 14:17:51 ******/
-- Description:	Update Plan details

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetPlanComponents]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[SetPlanComponents]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SetPlanComponents]  
(@PlanID int,
	@PlanName nvarchar(255),
	@BasisType int,
	@BMQuotaBonus bit,
	@SMEligible bit,
	@TenureBonus bit,
	@ModifiedOn datetime,
	@ModifiedBy int,
	@TGPCustomerInfo TGPCustomerInfo readonly,
	@Tenure TenureBonus readonly,
	@BIMonthlyBonusInfo BIMonthlyBonusInfo readonly
	)
AS  
BEGIN
			SET NOCOUNT ON;
			DECLARE @errMsg VARCHAR(255)
			DECLARE @beginTranCount INT
			SET @beginTranCount = @@TRANCOUNT			
			BEGIN TRANSACTION
		
	UPDATE  [dbo].[CPlanComponents] SET
           PlanName=@PlanName,
			BasisType=@BasisType,
			BMQuotaBonus=@BMQuotaBonus,
			SMEligible=@SMEligible,
			TenureBonus=@TenureBonus,
			ModifiedOn=@ModifiedOn,
			ModifiedBy=@ModifiedBy
	WHERE PlanID=@PlanID
	
	DELETE FROM TGPCustomerInfo WHERE PlanID=@PlanID		
    DELETE FROM TenureBonus WHERE PlanID=@PlanID
    DELETE FROM BIMonthlyBonusInfo WHERE PlanID=@PlanID 
     
     INSERT INTO TGPCustomerInfo(PlanID,SalesType,Percentage,CustomerType) 
            SELECT @PlanID,SalesType,Percentage,CustomerType FROM @TGPCustomerInfo;     
     INSERT INTO TenureBonus(PlanID,Months,EntryPointA,EntryPointB,Percentage,Tier) 
            SELECT @PlanID,Months,EntryPointA,EntryPointB,Percentage,Tier FROM @Tenure;       
     INSERT INTO BIMonthlyBonusInfo(PlanID,Months,EntryPointA,EntryPointB,Percentage,Tier) 
            SELECT @PlanID,Months,EntryPointA,EntryPointB,Percentage,Tier FROM @BIMonthlyBonusInfo;       
           
IF (@@ERROR <> 0 )
    BEGIN 
		SET @errMsg = 'Error: SetPlanComponents -> Unable to update into PlanComponents tables';
		GOTO CLEANUP
	END

CLEANUP:
        IF @errMsg IS NULL 
            BEGIN
                IF @@TRANCOUNT > @beginTranCount 
                    BEGIN   
                        COMMIT TRANSACTION
                        RETURN 0
                    END
            END
        ELSE 
            BEGIN
                IF @@TRANCOUNT > 0 
                    ROLLBACK TRANSACTION   
                RAISERROR(@errMsg, 16, 1)
                RETURN -1
            END
    END

GO

/****** Object:  StoredProcedure [dbo].[GetPlanComponents]    Script Date: 12/15/2016 15:00:42 ******/
-- Description:	Get All Plan details

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPlanComponents]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPlanComponents]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPlanComponents]  
AS  
BEGIN
	SET NOCOUNT OFF;
	SELECT ISNULL(PlanID,'') PlanID,
		   ISNULL(PlanName,'') PlanName,
           ISNULL(BasisType,'') BasisType,
           ISNULL(BMQuotaBonus,'') BMQuotaBonus,
           ISNULL(SMEligible,'') SMEligible,
           ISNULL(TenureBonus,'') TenureBonus,
           ISNULL(IsActive,'') IsActive
	
	 FROM  [dbo].[CPlanComponents] ORDER BY IsActive DESC,PlanID DESC
END
GO

/****** Object:  StoredProcedure [dbo].[GetPlanComponentsByID]    Script Date: 12/15/2016 15:28:36 ******/
-- Description:	Get Plan details by PlanID

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPlanComponentsByID]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPlanComponentsByID]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPlanComponentsByID]  
(
@PlanID nvarchar(100))
AS  
BEGIN
	SET NOCOUNT OFF;
	SELECT ISNULL(PlanID,'') PlanID,
		   ISNULL(PlanName,'') PlanName,
           ISNULL(BasisType,'') BasisType,
           ISNULL(BMQuotaBonus,'') BMQuotaBonus,
           ISNULL(SMEligible,'') SMEligible,
           ISNULL(TenureBonus,'') TenureBonus,
           ISNULL(IsActive,'') IsActive
		 FROM  [dbo].[CPlanComponents] WHERE PlanID=@PlanID
END
GO
/****** Object:  StoredProcedure [dbo].[DeletePlanComponentsByID]    Script Date: 12/15/2016 17:37:53 ******/
-- Description:	Delete Plan details

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeletePlanComponentsByID]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeletePlanComponentsByID]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeletePlanComponentsByID]  
		(@PlanID int,
		@ModifiedOn datetime,
		@ModifiedBy int,
		@IsActive bit)
AS  
BEGIN
			SET NOCOUNT ON;
			DECLARE @errMsg VARCHAR(255)
			DECLARE @beginTranCount INT
			SET @beginTranCount = @@TRANCOUNT			
			BEGIN TRANSACTION
		
	UPDATE  [dbo].[CPlanComponents] SET
			ModifiedOn=@ModifiedOn,
			ModifiedBy=@ModifiedBy,
            IsActive=@IsActive
	where PlanID=@PlanID
IF (@@ERROR <> 0 )
    BEGIN 
		SET @errMsg = 'Error: DeletePlanComponentsByID -> Unable to delete in CPlanComponents table';
		GOTO CLEANUP
	END

CLEANUP:
        IF @errMsg IS NULL 
            BEGIN
                IF @@TRANCOUNT > @beginTranCount 
                    BEGIN   
                        COMMIT TRANSACTION
                        RETURN 0
                    END
            END
        ELSE 
            BEGIN
                IF @@TRANCOUNT > 0 
                    ROLLBACK TRANSACTION   
                RAISERROR(@errMsg, 16, 1)
                RETURN -1
            END
    END

GO
/****** Object:  StoredProcedure [dbo].[GetTGPCustomerInfo]    Script Date: 12/15/2016 17:39:29 ******/
-- Description:	Get all TGP Customer Info details by PlanID

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTGPCustomerInfo]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTGPCustomerInfo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTGPCustomerInfo]
(@PlanID int) 
AS  
BEGIN
	SET NOCOUNT OFF;
	SELECT ISNULL(TGPCustomerInfo.ID,'') ID,  
      ISNULL(PlanID,'') PlanID,  
      ISNULL(SalesType,'') SalesType,
      ISNULL(SaleType.SalesTypeName,'') SalesTypeName,  
      ISNULL(Percentage,'0.000') Percentage,  
      ISNULL(CustomerType,'') CustomerType  
	FROM  [dbo].[TGPCustomerInfo] 
	INNER JOIN SaleType ON TGPCustomerInfo.SalesType = SaleType.ID
	WHERE PlanID=@PlanID AND SaleType.IsActive = 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetTenureBonus]    Script Date: 12/15/2016 17:40:21 ******/
-- Description:	Get all Tenure Bonus details by PlanID

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTenureBonus]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTenureBonus]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTenureBonus] 
(@PlanID int) 
AS  
BEGIN
	SET NOCOUNT OFF;
	SELECT ISNULL(ID,'')ID,
      ISNULL(PlanID,'') PlanID,
      ISNULL(Months,'') Months,
      ISNULL(EntryPointA,'')EntryPointA,
      ISNULL(EntryPointB,'')EntryPointB,
      ISNULL(Percentage,'0.000')Percentage,
      ISNULL(Tier,'') Tier
	FROM  [dbo].[TenureBonus] WHERE PlanID=@PlanID
END
GO
/****** Object:  StoredProcedure [dbo].[GetBiMonthlyBonusInfo]    Script Date: 12/15/2016 17:41:13 ******/
-- Description:	Get all Bi Monthly Bonus Info details by PlanID

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBiMonthlyBonusInfo]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetBiMonthlyBonusInfo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBiMonthlyBonusInfo] 
(@PlanID int) 
AS  
BEGIN
	SET NOCOUNT OFF;
	SELECT ISNULL(ID,'')ID,
      ISNULL(PlanID,'')PlanID,
      ISNULL(Months,'')Months,
      ISNULL(EntryPointA,'')EntryPointA,
      ISNULL(EntryPointB,'') EntryPointB,
      ISNULL(Percentage,'0.000')Percentage,
      ISNULL(Tier,'')Tier
	FROM  [dbo].[BIMonthlyBonusInfo] WHERE PlanID=@PlanID
END
GO

/****** Object:  StoredProcedure [dbo].[AddCommission]    Script Date: 12/15/2016 19:38:58 ******/
-- Description:	Insert Commission and return Message

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddCommission]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddCommission]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddCommission]
@SalespersonName nvarchar(255),
@Dateofsale datetime,
@EntryDate datetime,
@InvoiceNumber nvarchar(255),
@AccountingID INT,
@Customertype int,
@CustomerName nvarchar(255),
@CustomerNumber nvarchar(255),
@Solditem nvarchar(255),
@Splitsalepersonname nvarchar(255),
@SplitsalepersonnameID nvarchar(255),
@Amountofsale decimal(20,3),
@Costofgoods decimal(20,3),
@BranchID int,
@ProductLine int,
@Saletype int,
@Dollarvalue decimal(20,3),
@Base decimal(20,3),
@Lease decimal(20,3),
@Service decimal(20,3),
@Travel decimal(20,3),
@Cash decimal(20,3),
@Special decimal(20,3),
@TradeIn decimal(20,3),
@SlipType int,
@MainCommissionID int,
@TipLeadID int,
@TipLeadEmpID nvarchar(255),
@TipLeadName nvarchar(255),
@TipLeadAmount decimal(20,3),
@PositiveAdjustments decimal(20,3),
@NegativeAdjustments decimal(20,3),
@CompanyContribution decimal(20,3),
@TotalCEarned decimal(20,3),
@Status int,
@IsActive bit,
@AddedOn datetime,
@AddedBy int,
@Tipleadslip TipLeadSlip readonly,
@Result int Output,
@ID int Output
	
AS
BEGIN
			SET NOCOUNT ON;
			DECLARE @errMsg VARCHAR(255)
			DECLARE @beginTranCount INT
			SET @beginTranCount = @@TRANCOUNT			
			BEGIN TRANSACTION
Insert into CommissionComponents(SalesPerson,Dateofsale,EntryDate,InvoiceNumber,AccountPeriod,Customertype,
CustomerName,CustomerNumber,CommentSold,SplitSalePerson,SplitSalePersonID,Amountofsale,Costofgoods,
BranchID,ProductLine,Saletype,DollarVolume,BaseCommission,LeaseCommission,ServiceCommission,TravelCommission,CashCommission,
SpecialCommission,TradeIn,SlipType,MainCommissionID,
TipLeadID,TipLeadEmpID,TipLeadName,TipLeadAmount,PositiveAdjustments,NegativeAdjustments,CompanyContribution,
TotalCEarned,Status,IsActive,CreatedOn,CreatedBy)
VALUES(@SalespersonName,@Dateofsale,@EntryDate,@InvoiceNumber,@AccountingID,@Customertype,
@CustomerName,@CustomerNumber,@Solditem,@Splitsalepersonname,@SplitsalepersonnameID,@Amountofsale,@Costofgoods,
@BranchID,@ProductLine,@Saletype,@Dollarvalue,@Base,@Lease,@Service,@Travel,
@Cash,@Special,@TradeIn,@SlipType,@MainCommissionID,@TipLeadID,@TipLeadEmpID,
@TipLeadName,@TipLeadAmount,@PositiveAdjustments,@NegativeAdjustments,@CompanyContribution,
@TotalCEarned,@Status,@IsActive,@AddedOn,@AddedBy)

SET @MainCommissionID= @@IDENTITY
Insert into CommissionComponents(SalesPerson,Dateofsale,EntryDate,InvoiceNumber,AccountPeriod,Customertype,
CustomerName,CustomerNumber,Amountofsale,
BranchID,ProductLine,Saletype,DollarVolume,SlipType,MainCommissionID,
TipLeadID,TipLeadEmpID,TipLeadName,TipLeadAmount,PositiveAdjustments,NegativeAdjustments,CompanyContribution,
TotalCEarned,Status,IsActive,CreatedOn,CreatedBy)
Select @SalespersonName,@Dateofsale,@EntryDate,@InvoiceNumber,@AccountingID,@Customertype,@CustomerName,
@CustomerNumber,@Amountofsale,@BranchID,@ProductLine,@Saletype,@Dollarvalue,SlipType,@MainCommissionID,
TipLeadID,TipLeadEmpID,TipLeadName,TipLeadAmount,PositiveAdjustments,NegativeAdjustments,CompanyContribution,
TotalCEarned,@Status,@IsActive,@AddedOn,@AddedBy from @Tipleadslip;


			
IF (@@ERROR <> 0 )
    BEGIN 
		SET @errMsg = 'Error: AddCommission -> Unable to insert into CommissionComponents table';
		GOTO CLEANUP
	END
END
CLEANUP:
			IF @errMsg IS NULL 
				BEGIN
					IF @@TRANCOUNT > @beginTranCount 
						BEGIN   
							COMMIT TRANSACTION
							SET @ID = @@IDENTITY
							SET @Result = 0
							RETURN @Result
						END
				END
			ELSE 
				BEGIN
					IF @@TRANCOUNT > 0 
						ROLLBACK TRANSACTION  
						RAISERROR(@errMsg, 16, 1) 
						SET @Result = -1
					RETURN @Result
				END

GO

/****** Object:  StoredProcedure [dbo].[AddEmployee]    Script Date: 12/15/2016 19:39:22 ******/
-- Description:	Insert Employee and return Message

/****** Object:  StoredProcedure [dbo].[AddEmployee]    Script Date: 01/19/2017 17:38:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddEmployee]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddEmployee]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[AddEmployee]
@EmployeeID nvarchar(255),
@FName nvarchar(255),
@LName nvarchar(255),
@AccountName nvarchar (255),
@RoleID int,
@Email nvarchar(255),
@DateofHire datetime,
@DateInPosition datetime,
@PrimaryBranch nvarchar(255),
@SecondaryBranch nvarchar(255),
@ReportMgr nvarchar(255),
@ApproveMgr nvarchar(255),
@PlanID int,
@BPSalary bit,
@BPDraw bit,
@MonthAmount decimal(20,3),
@DrawType int,
@DRPercentage decimal(20,3),
@DDPeriod datetime,
@DrawTerm int,
@DDAmount decimal(20,3),
@IsActive bit,
@AddedOn datetime,
@AddedBy int,
@UID int Output,
@Result int Output
AS
BEGIN
			SET NOCOUNT ON;
			DECLARE @errMsg VARCHAR(255)
			DECLARE @beginTranCount INT
			SET @beginTranCount = @@TRANCOUNT			
			BEGIN TRANSACTION
IF NOT EXISTS (SELECT UID FROM EmpComponents WHERE AccountName=@AccountName or EmployeeID=@EmployeeID)
BEGIN			
INSERT INTO EmpComponents (EmployeeID, FirstName,LastName,AccountName,RoleID,Email,DateofHire,DateInPosition,
 PrimaryBranch,SecondaryBranch,ReportMgr,ApproveMgr,PayPlanID,BPSalary,BPDraw,MonthAmount,TypeofDraw,
 DRPercentage,DrawTerm,DDPeriod,DDAmount,IsActive,CreatedOn,CreatedBy,LastLogin)
 
VALUES (@EmployeeID, @FName,@LName,@AccountName,@RoleID,@Email,@DateofHire,@DateInPosition,@PrimaryBranch,@SecondaryBranch,
@ReportMgr,@ApproveMgr,@PlanID,@BPSalary,@BPDraw,@MonthAmount, @DrawType,@DRPercentage,@DrawTerm,@DDPeriod,
@DDAmount,@IsActive,@AddedOn,@AddedBy,'')
END
ELSE IF EXISTS(SELECT UID FROM EmpComponents WHERE EmployeeID=@EmployeeID)
BEGIN
 COMMIT TRANSACTION	
 SET @Result =3
 RETURN @Result
END
ELSE 
BEGIN
 COMMIT TRANSACTION	
 SET @Result =2
 RETURN @Result
END
IF (@@ERROR <> 0 )
    BEGIN 
SET @errMsg = 'Error: AddEmployee-> Unable to insert into EmpComponents table';
	GOTO CLEANUP
	END
CLEANUP:
IF @errMsg IS NULL 
BEGIN
IF @@TRANCOUNT > @beginTranCount 
BEGIN   
COMMIT TRANSACTION
SET @UID = @@IDENTITY
SET @Result = 0
RETURN @Result
END
END
ELSE 
BEGIN
IF @@TRANCOUNT > 0 
ROLLBACK TRANSACTION   
RAISERROR(@errMsg, 16, 1)
SET @Result = -1
RETURN @Result
END
END

GO



/****** Object:  StoredProcedure [dbo].[DeleteCommissionByID]    Script Date: 12/15/2016 19:39:59 ******/
-- Description:	Delete Commission and return Message

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteCommissionByID]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteCommissionByID]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteCommissionByID]
@ID int,
@ModifiedOn datetime,
@ModifiedBy int,
@Result int Output
AS
BEGIN
			SET NOCOUNT ON;
			DECLARE @errMsg VARCHAR(255)
			DECLARE @beginTranCount INT
			SET @beginTranCount = @@TRANCOUNT			
			BEGIN TRANSACTION
			
UPDATE CommissionComponents
SET
IsActive=0,
ModifiedOn=@ModifiedOn,
ModifiedBy=@ModifiedBy
WHERE (ID=@ID OR MainCommissionID=@ID)

IF (@@ERROR <> 0 )
    BEGIN 
		SET @errMsg = 'Error: DeleteCommissionByID -> Unable to Delete in CommissionComponents table';
		GOTO CLEANUP
	END
END
CLEANUP:
			IF @errMsg IS NULL 
				BEGIN
					IF @@TRANCOUNT > @beginTranCount 
						BEGIN   
							COMMIT TRANSACTION
							SET @Result = 0
							RETURN @Result
						END
				END
			ELSE 
				BEGIN
					IF @@TRANCOUNT > 0 
						ROLLBACK TRANSACTION   
						RAISERROR(@errMsg, 16, 1)
						SET @Result = -1
					RETURN @Result
				END

GO


/****** Object:  StoredProcedure [dbo].[ActiveDeactiveEmployee]    Script Date: 12/20/2016 19:27:16 ******/
-- Description:	Active and Deactive the Employee

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActiveDeactiveEmployee]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[ActiveDeactiveEmployee]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActiveDeactiveEmployee]
@ID int,
@ModifiedOn datetime,
@ModifiedBy int,
@IsActive int,
@Result int Output
AS
BEGIN
			SET NOCOUNT ON;
			DECLARE @errMsg VARCHAR(255)
			DECLARE @beginTranCount INT
			SET @beginTranCount = @@TRANCOUNT			
			BEGIN TRANSACTION
			
UPDATE EmpComponents
SET
IsActive=@IsActive,
ModifiedOn=@ModifiedOn,
ModifiedBy=@ModifiedBy
WHERE UID=@ID

IF (@@ERROR <> 0 )
    BEGIN 
		SET @errMsg = 'Error: ActiveDeactiveEmployee -> Unable to Active/Deactive in EmpComponents table';
		GOTO CLEANUP
	END
END
CLEANUP:
			IF @errMsg IS NULL 
				BEGIN
					IF @@TRANCOUNT > @beginTranCount 
						BEGIN   
							COMMIT TRANSACTION
							SET @Result = 0
							RETURN @Result
						END
				END
			ELSE 
				BEGIN
					IF @@TRANCOUNT > 0 
						ROLLBACK TRANSACTION   
						RAISERROR(@errMsg, 16, 1)
						SET @Result = -1
					RETURN @Result
				END


GO

/****** Object:  StoredProcedure [dbo].[GetAllActiveCommission]    Script Date: 12/15/2016 19:41:22 
-- Description:	Get all active Commission for dashboard ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllActiveCommission]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllActiveCommission]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllActiveCommission]
AS
BEGIN
SELECT	   ISNULL(ID, '')    ID, 
		   DateofSale,			
           ISNULL(CustomerName, '')     CustomerName, 
           ISNULL(TotalCEarned, '0.00') TotalCEarned, 
           ISNULL(Status, '')    Status           
    FROM  CommissionComponents 
	WHERE IsActive=1 ORDER BY EntryDate DESC
END
--SELECT * FROM CommissionComponents; 
GO

/****** Object:  StoredProcedure [dbo].[GetAllBranch]    Script Date: 12/15/2016 19:41:48
-- Description:	Get All Branches Name and return ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllBranch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllBranch]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllBranch]
AS
BEGIN
SELECT    ISNULL(BranchID, '')    BranchID,     
           ISNULL(BranchName, '')     BranchName,
           ISNULL(BranchCode, '')     BranchCode,
           ISNULL(IsActive, '')     IsActive  
    FROM  Branches ORDER BY IsActive DESC,BranchID desc
END
GO


GO
/****** Object:  StoredProcedure [dbo].[GetAllActiveCommissionbyUID]    Script Date: 12/23/2016 19:16:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllActiveCommissionbyUID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllActiveCommissionbyUID]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAllActiveCommissionbyUID]    
@RoleID INT,    
@UID INT    
AS    
BEGIN    
IF @RoleID = 5 --Sales rep     
BEGIN    
SELECT    ISNULL(ID, '')    ID,     
		   DateofSale,       
           ISNULL(CustomerName, '')     CustomerName,     
           ISNULL(TotalCEarned, '0.00') TotalCEarned,     
           ISNULL(Status, '')    Status,
           ISNULL(ProcesByPayroll,'') ProcesByPayroll,
           ISNULL(SlipType,'') SlipType
    FROM  CommissionComponents     
	WHERE IsActive=1 AND ((CreatedBy=@UID AND SlipType=1)OR(TipLeadID=@UID AND SlipType=2 AND Status<>1)) ORDER BY ID DESC
 
	SELECT cc.PlanID from EmpComponents AS ec
	JOIN CPlanComponents AS cc
	ON ec.PayPlanID = cc.PlanID 
	WHERE UID = @UID AND cc.IsActive=0

	SELECT UID FROM EmpComponents 
	WHERE UID = (SELECT ApproveMgr FROM EmpComponents WHERE UID=@UID)
	AND IsActive = 0    
END    
ELSE IF @RoleID = 4  --Payroll      
BEGIN      
SELECT    ISNULL(ID, '')    ID,       
     DateofSale,         
           ISNULL(EC.FirstName,'') +' '+ ISNULL(EC.LastName,'') CustomerName,       
           ISNULL(TotalCEarned, '0.00') TotalCEarned,       
           ISNULL(Status, '')    Status,
           ISNULL(ProcesByPayroll,'') ProcesByPayroll,
           ISNULL(SlipType,'') SlipType          
    FROM  CommissionComponents AS CC JOIN EmpComponents AS EC ON  CC.CreatedBy = EC.UID
WHERE CC.IsActive=1 AND Status<>1 AND SlipType=1
UNION
SELECT    ISNULL(ID, '')    ID,       
     DateofSale,         
           ISNULL(EC.FirstName,'') +' '+ ISNULL(EC.LastName,'') CustomerName,       
           ISNULL(TotalCEarned, '0.00') TotalCEarned,       
           ISNULL(Status, '')    Status,
           ISNULL(ProcesByPayroll,'') ProcesByPayroll,
           ISNULL(SlipType,'') SlipType          
    FROM  CommissionComponents AS CC JOIN EmpComponents AS EC ON  CC.TipLeadID = EC.UID
WHERE CC.IsActive=1 AND Status<>1 AND SlipType=2  ORDER BY ID DESC      
END      
ELSE IF @RoleID = 2  --Sales Manager      
BEGIN      
SELECT    ISNULL(ID, '')    ID,       
     DateofSale,         
           ISNULL(EC.FirstName,'') +' '+ ISNULL(EC.LastName,'')  CustomerName,       
           ISNULL(TotalCEarned, '0.00') TotalCEarned,       
           ISNULL(Status, '')    Status,
           ISNULL(ProcesByPayroll,'') ProcesByPayroll,
           ISNULL(SlipType,'') SlipType                 
    FROM  CommissionComponents AS CC JOIN EmpComponents AS EC ON  CC.CreatedBy = EC.UID      
 WHERE CC.IsActive=1 AND CC.Status<>1 AND CC.SlipType=1 AND CC.CreatedBy IN(SELECT UID FROM EmpComponents WHERE ReportMgr=@UID) ORDER BY ID DESC      
END      
ELSE IF @RoleID = 3 --General Manager      
BEGIN      
SELECT    ISNULL(ID, '')    ID,       
     DateofSale,         
           ISNULL(EC.FirstName,'') +' '+ ISNULL(EC.LastName,'') CustomerName,       
           ISNULL(TotalCEarned, '0.00') TotalCEarned,       
           ISNULL(Status, '')    Status,
           ISNULL(ProcesByPayroll,'') ProcesByPayroll,
           ISNULL(SlipType,'') SlipType                
    FROM  CommissionComponents AS CC JOIN EmpComponents AS EC ON  CC.CreatedBy = EC.UID       
 WHERE CC.IsActive=1 AND CC.Status<>1 AND (CC.CreatedBy IN(SELECT UID FROM EmpComponents WHERE ApproveMgr=@UID)AND CC.SlipType=1)
 UNION
 SELECT    ISNULL(ID, '')    ID,       
     DateofSale,         
           ISNULL(EC.FirstName,'') +' '+ ISNULL(EC.LastName,'') CustomerName,       
           ISNULL(TotalCEarned, '0.00') TotalCEarned,       
           ISNULL(Status, '')    Status,
           ISNULL(ProcesByPayroll,'') ProcesByPayroll,
           ISNULL(SlipType,'') SlipType                
    FROM  CommissionComponents AS CC JOIN EmpComponents AS EC ON  CC.TipLeadID = EC.UID       
 WHERE CC.IsActive=1 AND CC.Status<>1 AND (CC.TipLeadID IN(SELECT UID FROM EmpComponents WHERE ApproveMgr=@UID)AND CC.SlipType=2) ORDER BY ID DESC      
END  
ELSE IF @RoleID = 6  --Non-Sale Employee      
BEGIN      
SELECT    ISNULL(ID, '')    ID,     
		   DateofSale,       
           ISNULL(CustomerName, '')     CustomerName,     
           ISNULL(TotalCEarned, '0.00') TotalCEarned,     
           ISNULL(Status, '')    Status,
           ISNULL(ProcesByPayroll,'') ProcesByPayroll,
           ISNULL(SlipType,'') SlipType               
    FROM  CommissionComponents     
	WHERE IsActive=1 AND TipLeadID=@UID AND SlipType=2 AND Status<>1 ORDER BY ID DESC
 
	SELECT UID FROM EmpComponents 
	WHERE UID = (SELECT ApproveMgr FROM EmpComponents WHERE UID=@UID)
	AND IsActive = 0 
END       
END  
GO

/****** Object:  StoredProcedure [dbo].[GetCommissionByID]    Script Date: 12/15/2016 19:45:34
-- Description:	Get Commission by ID and return Commission details  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCommissionByID]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCommissionByID]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCommissionByID]
@ID int
AS
BEGIN
SELECT    ISNULL(SalesPerson, '')    SalesPerson,   
     DateofSale,  
     EntryDate,  
		   ISNULL(PC.Month,'') AccountPeriod,
		   ISNULL(PC.ID, '')     AccountPeriodID,
		   ISNULL(CC.ID, '')     ID,	    
           ISNULL(CustomerType, '')     CustomerType,   
           ISNULL(CustomerName, '')     CustomerName,   
           ISNULL(CustomerNumber, '')   CustomerNumber,   
           ISNULL(InvoiceNumber, '')    InvoiceNumber,   
           ISNULL(ProductLine, '')   ProductLine,  
           ISNULL(SaleType, '')    SaleType,  
           ISNULL(AmountofSale, '0.00')     AmountofSale,   
           ISNULL(CostofGoods, '0.00')     CostofGoods,   
           ISNULL(CommentSold, '')   CommentSold,   
           ISNULL(SplitSalePerson, '')    SplitSalePerson,   
           ISNULL(SplitSalePersonID, '')     SplitSalePersonID, 
           ISNULL(TipLeadID,'') TipLeadID,
           ISNULL(TipLeadName, '')   TipLeadName,  
           ISNULL(TipLeadEmpID,'') TipLeadEmpID,
           ISNULL(TipLeadAmount, '0.00')    TipLeadAmount,               
           ISNULL(PositiveAdjustments, '0.00')     PositiveAdjustments, 
           ISNULL(NegativeAdjustments, '0.00')     NegativeAdjustments, 
           ISNULL(CompanyContribution, '0.00')     CompanyContribution, 
           ISNULL(DollarVolume, '0.00')     DollarVolume,   
           ISNULL(BaseCommission, '0.00')   BaseCommission,   
           ISNULL(LeaseCommission, '0.00')    LeaseCommission,   
           ISNULL(ServiceCommission, '0.00')     ServiceCommission,   
           ISNULL(TravelCommission, '0.00')   TravelCommission,  
           ISNULL(CashCommission, '0.00')    CashCommission,  
           ISNULL(SpecialCommission, '0.00')     SpecialCommission,   
           ISNULL(TotalCEarned, '0.00')     TotalCEarned,  
           ISNULL(TradeIn,'0.00') TradeIn,  
           ISNULL(Status,'') Status, 
           ISNULL(cc.ProcesByPayroll,'') ProcesByPayroll, 
           ISNULL(SlipType,'') SlipType, 
           ISNULL(MainCommissionID,'') MainCommissionID, 
           ISNULL(Branch.BranchID,'') BranchID,  
           ISNULL(CC.CreatedBy, '')     CreatedBy, 
           ISNULL(Comments, '')     Comments, 
           --FROM  CommissionComponents WHERE ID= @ID  
           ISNULL(Branch.BranchName, '') BranchName  
    FROM  CommissionComponents as CC  
 LEFT JOIN Branches as Branch ON CC.BranchID = Branch.BranchID  
 LEFT JOIN PayrollConfig as PC ON CC.AccountPeriod = PC.ID
 WHERE (CC.ID= @ID OR CC.MainCommissionID=@ID AND CC.IsActive=1)
END
GO

/****** Object:  StoredProcedure [dbo].[GetEmployeeByID]    Script Date: 12/15/2016 19:45:55 
-- Description:	Get Employee by its ID and return ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEmployeeByID]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetEmployeeByID]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEmployeeByID]
@ID int
AS
BEGIN

SELECT     ISNULL(Emp.UID, '')    UID,
		   ISNULL(Emp.EmployeeID, '')    EmployeeID, 		
           ISNULL(Emp.FirstName, '')     FirstName, 
           ISNULL(Emp.LastName, '')     LastName, 
           ISNULL(Emp.MiddleName, '')   MiddleName, 
           ISNULL(Emp.RoleID, '')    RoleID, 
           ISNULL(Role.Name,'') RoleName,
           ISNULL(Emp.Email, '')   Email,
           ISNULL(Emp.AccountName, '') AccountName,
           Emp.DateofHire,
           Emp.DateInPosition,
           ISNULL(PBID.BranchID, '')    PrimaryBranch,
           ISNULL(PBID.BranchName, '')    PrimaryBranchName,
           ISNULL(EMP.SecondaryBranch, '') SecondaryBranch,  
           ISNULL(SBID.BranchName, '')    SecondaryBranchName,
           ISNULL(Emp.ReportMgr, '')     ReportMgr, 
           ISNULL(E2.FirstName,'') +' '+ ISNULL(E2.LastName,'')ReportMgrName,           
           ISNULL(Emp.ApproveMgr, '')     ApproveMgr,
           ISNULL(E1.FirstName,'') +' '+ ISNULL(E1.LastName,'')ApproveMgrName, 
           ISNULL(Emp.PayPlanID, '')    PayPlanID, 
           ISNULL(CPC.PlanName, '')    PlanName, 
           ISNULL(Emp.BPSalary, '')     BPSalary, 
           ISNULL(Emp.BPDraw, '')   BPDraw,
           ISNULL(Emp.MonthAmount, '0.000')    MonthAmount,             
           ISNULL(Emp.TypeofDraw, '')     TypeofDraw, 
           ISNULL(Emp.DRPercentage, '0.000')     DRPercentage, 
           ISNULL(Emp.DrawTerm, '')   DrawTerm, 
           Emp.DDPeriod,   
           ISNULL(Emp.DDAmount, '0.000') DDAmount  
FROM EmpComponents	AS Emp 
LEFT JOIN Roles AS Role ON EMP.RoleID=Role.RoleID	
LEFT JOIN Branches AS PBID ON EMP.PrimaryBranch=PBID.BranchID
LEFT JOIN Branches AS SBID ON EMP.PrimaryBranch=SBID.BranchID
LEFT JOIN EmpComponents AS E1 ON E1.UID = Emp.ApproveMgr
LEFT JOIN EmpComponents AS E2 ON E2.UID = Emp.ReportMgr
LEFT JOIN CPlanComponents AS CPC ON Emp.PayPlanID = CPC.PlanID
WHERE Emp.UID= @ID
END

GO

/****** Object:  StoredProcedure [dbo].[SetCommission]    Script Date: 02/28/2017 13:30:24 
-- Description:	Update Commission and return Message ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetCommission]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SetCommission]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SetCommission]
@ID int,
@SalespersonName nvarchar(255),
@Dateofsale datetime,
@EntryDate datetime,
@AccountingID INT,
@Customertype int,
@CustomerName nvarchar(255),
@CustomerNumber nvarchar(255),
@InvoiceNumber nvarchar(255),
@BranchID int,
@ProductLine int,
@Saletype int,
@Amountofsale decimal(20,3),
@Costofgoods decimal(20,3),
@Solditem nvarchar(255),
@Splitsalepersonname nvarchar(255),
@SplitsalepersonnameID nvarchar(255),
@SlipType int,
@MainCommissionID int,
@TipLeadID int,
@TipLeadEmpID nvarchar(255),
@TipLeadName nvarchar(255),
@TipLeadAmount decimal(20,3),
@PositiveAdjustments decimal(20,3),
@NegativeAdjustments decimal(20,3),
@CompanyContribution decimal(20,3),
@Dollarvalue decimal(20,3),
@Base decimal(20,3),
@Lease decimal(20,3),
@Service decimal(20,3),
@Travel decimal(20,3),
@Cash decimal(20,3),
@Special decimal(20,3),
@TotalCEarned decimal(20,3),
@TradeIn decimal(20,3),
@Tipleadslip TipLeadSlip readonly,
@Status int,
@IsActive bit,
@ModifiedOn datetime,
@ModifiedBy int,
@Comments XML,
@Result int Output
AS
BEGIN
			SET NOCOUNT ON;
			DECLARE @errMsg VARCHAR(255)
			DECLARE @beginTranCount INT
			SET @beginTranCount = @@TRANCOUNT			
			BEGIN TRANSACTION
UPDATE CommissionComponents
SET
SalesPerson=@SalespersonName,
DateofSale=@Dateofsale,
EntryDate=@EntryDate,
AccountPeriod=@AccountingID,
CustomerType=@Customertype,
CustomerName=@CustomerName,
CustomerNumber=@CustomerNumber,
InvoiceNumber=@InvoiceNumber,
BranchID=@BranchID,
ProductLine=@ProductLine,
SaleType=@Saletype,
AmountofSale=@Amountofsale,
CostofGoods=@Costofgoods,
CommentSold=@Solditem,
SplitSalePerson=@Splitsalepersonname,
SplitSalePersonID=@SplitsalepersonnameID,
SlipType=@SlipType,
MainCommissionID=@MainCommissionID,
TipLeadID=@TipLeadID,
TipLeadEmpID=@TipLeadEmpID,
TipLeadName=@TipLeadName,
TipLeadAmount=@TipLeadAmount,
PositiveAdjustments=@PositiveAdjustments,
NegativeAdjustments=@NegativeAdjustments,
CompanyContribution=@CompanyContribution,
DollarVolume=@Dollarvalue,
BaseCommission=@Base,
LeaseCommission=@Lease,
ServiceCommission=@Service,
TravelCommission=@Travel,
CashCommission=@Cash,
SpecialCommission=@Special,
TotalCEarned=@TotalCEarned,
TradeIn=@TradeIn,
Status=@Status,
IsActive=@IsActive,
Comments=@Comments,
ModifiedOn=@ModifiedOn,
ModifiedBy=@ModifiedBy
WHERE ID=@ID
--Delete existing Tiplead slip
--Update CC set IsActive=0  from CommissionComponents CC inner join @Tipleadslip TS on TS.ID=CC.ID where cc.MainCommissionID=@ID
Update CommissionComponents set IsActive=0 where MainCommissionID=@ID and ID not in (select ID from @Tipleadslip where ID is not null)
--Update existing Tiplead slip
Update CC set CC.SalesPerson=@SalespersonName,CC.Dateofsale=@Dateofsale,CC.EntryDate=@EntryDate,
CC.InvoiceNumber=@InvoiceNumber,CC.AccountPeriod=@AccountingID,CC.Customertype=@Customertype,
CC.CustomerName=@CustomerName,CC.CustomerNumber=@CustomerNumber,CC.Amountofsale=@Amountofsale,
CC.BranchID=@BranchID,CC.ProductLine=@ProductLine,CC.Saletype=@Saletype,CC.DollarVolume=@Dollarvalue,CC.SlipType=TS.SlipType,
CC.TipLeadID=TS.TipLeadID,CC.TipLeadEmpID=TS.TipLeadEmpID,CC.TipLeadName=TS.TipLeadName,
CC.TipLeadAmount=TS.TipLeadAmount,CC.PositiveAdjustments=TS.PositiveAdjustments,CC.NegativeAdjustments=TS.NegativeAdjustments,
CC.CompanyContribution=TS.CompanyContribution,CC.TotalCEarned=TS.TotalCEarned,CC.Status=TS.Status,CC.IsActive=@IsActive,
CC.ModifiedOn=@ModifiedOn,CC.ModifiedBy=@ModifiedBy from CommissionComponents CC Inner join @Tipleadslip TS on TS.ID=CC.ID

--Insert New Tipleadslip
Insert into CommissionComponents(SalesPerson,Dateofsale,EntryDate,InvoiceNumber,AccountPeriod,Customertype,
CustomerName,CustomerNumber,Amountofsale,
BranchID,ProductLine,Saletype,DollarVolume,SlipType,MainCommissionID,
TipLeadID,TipLeadEmpID,TipLeadName,TipLeadAmount,PositiveAdjustments,NegativeAdjustments,CompanyContribution,
TotalCEarned,Status,IsActive,CreatedOn,CreatedBy)
Select @SalespersonName,@Dateofsale,@EntryDate,@InvoiceNumber,@AccountingID,@Customertype,@CustomerName,
@CustomerNumber,@Amountofsale,@BranchID,@ProductLine,@Saletype,@Dollarvalue,SlipType,@ID,
TipLeadID,TipLeadEmpID,TipLeadName,TipLeadAmount,PositiveAdjustments,NegativeAdjustments,CompanyContribution,
TotalCEarned,Tiplead.Status,@IsActive,@ModifiedOn,@ModifiedBy from @Tipleadslip as Tiplead
where NOT EXISTS(Select 1 From CommissionComponents as CC Where CC.ID = Tiplead.ID)


IF (@@ERROR <> 0 )
    BEGIN 
		SET @errMsg = 'Error: SetCommission -> Unable to update into CommissionComponents table';
		GOTO CLEANUP
	END
END
CLEANUP:
			IF @errMsg IS NULL 
				BEGIN
					IF @@TRANCOUNT > @beginTranCount 
						BEGIN   
							COMMIT TRANSACTION
							SET @Result = 0
							RETURN @Result
						END
				END
			ELSE 
				BEGIN
					IF @@TRANCOUNT > 0 
						ROLLBACK TRANSACTION
						RAISERROR(@errMsg, 16, 1)   
						SET @Result = -1
					RETURN @Result
				END
GO	

/****** Object:  StoredProcedure [dbo].[SetTipLeadSlip]    Script Date: 02/28/2017 13:30:24 
-- Description:	Update Commission and return Message ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetTipLeadSlip]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SetTipLeadSlip]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SetTipLeadSlip]
@ID int,
@SalespersonName nvarchar(255),
@Dateofsale datetime,
@EntryDate datetime,
@AccountingID INT,
@Customertype int,
@CustomerName nvarchar(255),
@CustomerNumber nvarchar(255),
@InvoiceNumber nvarchar(255),
@BranchID int,
@ProductLine int,
@Saletype int,
@Amountofsale decimal(20,3),
@Costofgoods decimal(20,3),
@Solditem nvarchar(255),
@Splitsalepersonname nvarchar(255),
@SplitsalepersonnameID nvarchar(255),
@SlipType int,
@MainCommissionID int,
@TipLeadID int,
@TipLeadEmpID nvarchar(255),
@TipLeadName nvarchar(255),
@TipLeadAmount decimal(20,3),
@PositiveAdjustments decimal(20,3),
@NegativeAdjustments decimal(20,3),
@CompanyContribution decimal(20,3),
@Dollarvalue decimal(20,3),
@Base decimal(20,3),
@Lease decimal(20,3),
@Service decimal(20,3),
@Travel decimal(20,3),
@Cash decimal(20,3),
@Special decimal(20,3),
@TotalCEarned decimal(20,3),
@TradeIn decimal(20,3),
@Status int,
@IsActive bit,
@ModifiedOn datetime,
@ModifiedBy int,
@Comments XML,
@Result int Output
AS
BEGIN
			SET NOCOUNT ON;
			DECLARE @errMsg VARCHAR(255)
			DECLARE @beginTranCount INT
			SET @beginTranCount = @@TRANCOUNT			
			BEGIN TRANSACTION
UPDATE CommissionComponents
SET
SalesPerson=@SalespersonName,
DateofSale=@Dateofsale,
EntryDate=@EntryDate,
AccountPeriod=@AccountingID,
CustomerType=@Customertype,
CustomerName=@CustomerName,
CustomerNumber=@CustomerNumber,
InvoiceNumber=@InvoiceNumber,
BranchID=@BranchID,
ProductLine=@ProductLine,
SaleType=@Saletype,
AmountofSale=@Amountofsale,
CostofGoods=@Costofgoods,
CommentSold=@Solditem,
SplitSalePerson=@Splitsalepersonname,
SplitSalePersonID=@SplitsalepersonnameID,
SlipType=@SlipType,
MainCommissionID=@MainCommissionID,
TipLeadID=@TipLeadID,
TipLeadEmpID=@TipLeadEmpID,
TipLeadName=@TipLeadName,
TipLeadAmount=@TipLeadAmount,
PositiveAdjustments=@PositiveAdjustments,
NegativeAdjustments=@NegativeAdjustments,
CompanyContribution=@CompanyContribution,
DollarVolume=@Dollarvalue,
BaseCommission=@Base,
LeaseCommission=@Lease,
ServiceCommission=@Service,
TravelCommission=@Travel,
CashCommission=@Cash,
SpecialCommission=@Special,
TotalCEarned=@TotalCEarned,
TradeIn=@TradeIn,
Status=@Status,
IsActive=@IsActive,
Comments=@Comments,
ModifiedOn=@ModifiedOn,
ModifiedBy=@ModifiedBy
WHERE ID=@ID

Declare @TipAmount decimal(20,3),@PositiveAdj decimal(20,3),@NegativeAdj decimal(20,3),@BaseCommission decimal(20,3),
@LeaseCommission decimal(20,3),@ServiceCommission decimal(20,3),@TravelCommission decimal(20,3),@CashCommission decimal(20,3),
@SpecialCommission decimal(20,3),@TotalCommission decimal(20,3)

select @TipAmount=sum(TipLeadAmount),@PositiveAdj=sum(PositiveAdjustments),@NegativeAdj=sum(NegativeAdjustments) from CommissionComponents where MainCommissionID=@MainCommissionID and IsActive=1
select @BaseCommission=BaseCommission,@LeaseCommission=LeaseCommission,@ServiceCommission=ServiceCommission,
@TravelCommission=TravelCommission,@SpecialCommission=SpecialCommission,@CashCommission=CashCommission from CommissionComponents where ID=@MainCommissionID

set @TotalCommission=@BaseCommission+@LeaseCommission+@ServiceCommission+@TravelCommission+@SpecialCommission+@CashCommission+@NegativeAdj-
(@TipAmount+@PositiveAdj)

update CommissionComponents set TipLeadAmount=@TipAmount,PositiveAdjustments=@PositiveAdj,NegativeAdjustments=@NegativeAdj,
TotalCEarned=@TotalCommission where ID=@MainCommissionID

IF (@@ERROR <> 0 )
    BEGIN 
		SET @errMsg = 'Error: SetTipLeadSlip -> Unable to update into CommissionComponents table';
		GOTO CLEANUP
	END
END
CLEANUP:
			IF @errMsg IS NULL 
				BEGIN
					IF @@TRANCOUNT > @beginTranCount 
						BEGIN   
							COMMIT TRANSACTION
							SET @Result = 0
							RETURN @Result
						END
				END
			ELSE 
				BEGIN
					IF @@TRANCOUNT > 0 
						ROLLBACK TRANSACTION
						RAISERROR(@errMsg, 16, 1)   
						SET @Result = -1
					RETURN @Result
				END
GO	

--/****** Object:  StoredProcedure [dbo].[SetCommission]    Script Date: 12/15/2016 19:46:24 
---- Description:	Update Commission and return Message ******/

--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetCommission]') AND type in (N'P', N'PC'))
--DROP PROCEDURE [dbo].[SetCommission]
--GO
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO

--CREATE PROCEDURE [dbo].[SetCommission]
--@ID int,
--@SalespersonName nvarchar(255),
--@Dateofsale datetime,
--@EntryDate datetime,
--@AccountingDate datetime,
--@Customertype int,
--@CustomerName nvarchar(255),
--@CustomerNumber int,
--@InvoiceNumber nvarchar(255),
--@BranchID int,
--@ProductLine int,
--@Saletype int,
--@Amountofsale decimal(20,3),
--@Costofgoods decimal(20,3),
--@Solditem nvarchar(255),
--@Splitsalepersonname nvarchar(255),
--@SplitsalepersonnameID nvarchar(255),
--@TipleadName nvarchar(255),
--@TipleadAmount decimal(20,3),
--@Adjustment decimal(20,3),
--@Dollarvalue decimal(20,3),
--@Base decimal(20,3),
--@Lease decimal(20,3),
--@Service decimal(20,3),
--@Travel decimal(20,3),
--@Cash decimal(20,3),
--@Special decimal(20,3),
--@TotalCEarned decimal(20,3),
--@TradeIn decimal(20,3),
--@Status int,
--@IsActive bit,
--@ModifiedOn datetime,
--@ModifiedBy bit,
--@Comments XML,
--@Result int Output
--AS
--BEGIN
--			SET NOCOUNT ON;
--			DECLARE @errMsg VARCHAR(255)
--			DECLARE @beginTranCount INT
--			SET @beginTranCount = @@TRANCOUNT			
--			BEGIN TRANSACTION
--UPDATE CommissionComponents
--SET
--SalesPerson=@SalespersonName,
--DateofSale=@Dateofsale,
--EntryDate=@EntryDate,
--AccountPeriod=@AccountingDate,
--CustomerType=@Customertype,
--CustomerName=@CustomerName,
--CustomerNumber=@CustomerNumber,
--InvoiceNumber=@InvoiceNumber,
--BranchID=@BranchID,
--ProductLine=@ProductLine,
--SaleType=@Saletype,
--AmountofSale=@Amountofsale,
--CostofGoods=@Costofgoods,
--CommentSold=@Solditem,
--SplitSalePerson=@Splitsalepersonname,
--SplitSalePersonID=@SplitsalepersonnameID,
--TipLeadName=@TipleadName,
--TipLeadAmount=@TipleadAmount,
--Adjustments=@Adjustment,
--DollarVolume=@Dollarvalue,
--BaseCommission=@Base,
--LeaseCommission=@Lease,
--ServiceCommission=@Service,
--TravelCommission=@Travel,
--CashCommission=@Cash,
--SpecialCommission=@Special,
--TotalCEarned=@TotalCEarned,
--TradeIn=@TradeIn,
--Status=@Status,
--IsActive=@IsActive,
--Comments=@Comments,
--ModifiedOn=@ModifiedOn,
--ModifiedBy=@ModifiedBy
--WHERE ID=@ID
--IF (@@ERROR <> 0 )
--    BEGIN 
--		SET @errMsg = 'Error: SetCommission -> Unable to update into CommissionComponents table';
--		GOTO CLEANUP
--	END
--END
--CLEANUP:
--			IF @errMsg IS NULL 
--				BEGIN
--					IF @@TRANCOUNT > @beginTranCount 
--						BEGIN   
--							COMMIT TRANSACTION
--							SET @Result = 0
--							RETURN @Result
--						END
--				END
--			ELSE 
--				BEGIN
--					IF @@TRANCOUNT > 0 
--						ROLLBACK TRANSACTION
--						RAISERROR(@errMsg, 16, 1)   
--						SET @Result = -1
--					RETURN @Result
--				END
--GO

/****** Object:  StoredProcedure [dbo].[SetEmployee]    Script Date: 12/15/2016 19:47:05 
-- Description:	Update Employee and return Message ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetEmployee]') AND TYPE IN (N'P', N'PC'))

DROP PROCEDURE [dbo].[SetEmployee]
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 Create PROCEDURE [dbo].[SetEmployee]
@UID int,
@EmployeeID nvarchar(255),
@FName nvarchar(255),
@LName nvarchar(255),
--@MName nvarchar(255),
@RoleID nvarchar(25),
@AccountName nvarchar(255),
@Email nvarchar(255),
@DateofHire datetime,
@DateInPosition datetime,
@PrimaryBranch nvarchar(255),
@SecondaryBranch nvarchar(255),
@ReportMgr nvarchar(255),
@ApproveMgr nvarchar(255),
@PlanID int,
@BPSalary bit,
@BPDraw bit,
@MonthAmount Decimal(20,3),
@DrawType int,
@DRPercentage decimal(20,3),
@DDPeriod datetime,
@DrawTerm int,
@DDAmount Decimal(20,3),
@ModifiedOn datetime,
@ModifiedBy bit,
@Result int Output
AS
BEGIN
			SET NOCOUNT ON;
			DECLARE @errMsg VARCHAR(255)
			DECLARE @beginTranCount INT
			SET @beginTranCount = @@TRANCOUNT			
			BEGIN TRANSACTION

UPDATE EmpComponents
SET
EmployeeID=@EmployeeID,
FirstName=@FName,
LastName=@LName,
--MiddleName=@MName,
RoleID=@RoleID,
AccountName=@AccountName,
Email=@Email,
DateofHire=@DateofHire,
DateInPosition=@DateInPosition,
PrimaryBranch=@PrimaryBranch,
SecondaryBranch=@SecondaryBranch,
ReportMgr=@ReportMgr,
ApproveMgr=@ApproveMgr,
PayPlanID=@PlanID,
BPSalary=@BPSalary,
BPDraw=@BPDraw,
MonthAmount=@MonthAmount,
TypeofDraw=@DrawType,
DRPercentage=@DRPercentage,
DrawTerm=@DrawTerm,
DDPeriod=@DDPeriod,
DDAmount=@DDAmount,
ModifiedOn=@ModifiedOn,
ModifiedBy=@ModifiedBy
WHERE UID = @UID
IF (@@ERROR <> 0 )
    BEGIN 
		SET @errMsg = 'Error: SetEmployee -> Unable to update into EmpComponents table';
		GOTO CLEANUP
	END
END
CLEANUP:
			IF @errMsg IS NULL 
				BEGIN
					IF @@TRANCOUNT > @beginTranCount 
						BEGIN   
							COMMIT TRANSACTION
							SET @Result = 0
							RETURN @Result
						END
				END
			ELSE 
				BEGIN
					IF @@TRANCOUNT > 0 
						ROLLBACK TRANSACTION
						RAISERROR(@errMsg, 16, 1)   
						SET @Result = -1
					RETURN @Result
				END


GO

/****** Object:  StoredProcedure [dbo].[GetEmployeeDeactPlan]    Script Date: 12/20/2016 21:49:26
-- Description:	Get List of Employees Linked to deactivate plan method  ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEmployeeDeactPlan]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetEmployeeDeactPlan]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetEmployeeDeactPlan]  
AS  
BEGIN  
SELECT   
ISNULL(EC.UID, '')    UID,  
ISNULL(EC.AccountName, '')   AccountName, 
ISNULL(EC.EmployeeID,'')EmployeeID,  
ISNULL(EC.FirstName,'')FirstName,  
ISNULL(EC.LastName,'')LastName,  
ISNULL(CC.PlanID,'')PlanID,  
ISNULL(CC.PlanName,'')PlanName  
 FROM EmpComponents AS EC INNER JOIN CPlanComponents AS CC  
ON EC.PayPlanID = CC.PlanID  
WHERE CC.IsActive=0 ORDER BY CC.PlanID DESC  
END  
GO

/****** Object:  StoredProcedure [dbo].[GetNamesbyRoleID]    Script Date: 12/22/2016 13:34:38 ******/
-- Description:	Get All Employees Name by its role id and return
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNamesbyRoleID]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetNamesbyRoleID]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetNamesbyRoleID]
@RoleID INT
AS
BEGIN
SELECT	   ISNULL(EmployeeID, '')    EmployeeID, 		
           ISNULL(FirstName, '')     FirstName, 
           ISNULL(LastName, '')     LastName
    FROM  EmpComponents
	WHERE  (IsActive = 0 and RoleID =@RoleID)
END
--SELECT * FROM EmpComponents; 
GO

/****** Object:  StoredProcedure [dbo].[AddUsersLog]    Script Date: 12/23/2016 15:14:41 ******/
-- Description:	Add User Login details
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddUsersLog]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddUsersLog]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUsersLog]  
(

@UID	int,
@LoggedIn	datetime,
@ID int Output
)
AS  
BEGIN
			SET NOCOUNT ON;
			DECLARE @errMsg VARCHAR(255)
			DECLARE @beginTranCount INT
			SET @beginTranCount = @@TRANCOUNT			
			BEGIN TRANSACTION
			
       INSERT INTO [dbo].[UsersLog] (UID, LoggedIn)	
       VALUES (@UID,@LoggedIn)  
           
IF (@@ERROR <> 0 )
    BEGIN 
		SET @errMsg = 'Error: AddUsersLog -> Unable to insert into UsersLog tables';
		GOTO CLEANUP
	END

CLEANUP:
        IF @errMsg IS NULL 
            BEGIN
                IF @@TRANCOUNT > @beginTranCount 
                    BEGIN   
                        COMMIT TRANSACTION
                        SET @ID = @@IDENTITY
                        RETURN 0
                    END
            END
        ELSE 
            BEGIN
                IF @@TRANCOUNT > 0 
                    ROLLBACK TRANSACTION   
                RAISERROR(@errMsg, 16, 1)
                RETURN -1
            END
    END

GO

/****** Object:  StoredProcedure [dbo].[SetUsersLog]    Script Date: 12/23/2016 15:15:41 ******/
-- Description:	Update User Login details
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUsersLog]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[SetUsersLog]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SetUsersLog]  
(
@ID int,
@UID	int,
@LoggedOut	datetime
)
AS  
BEGIN
			SET NOCOUNT ON;
			DECLARE @errMsg VARCHAR(255)
			DECLARE @beginTranCount INT
			SET @beginTranCount = @@TRANCOUNT			
			BEGIN TRANSACTION
			
	UPDATE UsersLog SET LoggedOut=@LoggedOut WHERE ID=@ID
    UPDATE EmpComponents SET LastLogin=@LoggedOut WHERE UID=@UID
           
IF (@@ERROR <> 0 )
    BEGIN 
		SET @errMsg = 'Error: EditUsersLog -> Unable to update into UsersLog tables';
		GOTO CLEANUP
	END

CLEANUP:
        IF @errMsg IS NULL 
            BEGIN
                IF @@TRANCOUNT > @beginTranCount 
                    BEGIN   
                        COMMIT TRANSACTION
                        RETURN 0
                    END
            END
        ELSE 
            BEGIN
                IF @@TRANCOUNT > 0 
                    ROLLBACK TRANSACTION   
                RAISERROR(@errMsg, 16, 1)
                RETURN -1
            END
    END

GO

/****** Object:  StoredProcedure [dbo].[AddFirstEmployee]    Script Date: 12/23/2016 15:17:55 ******/
-- Description:	Add first User Login details and Employee details.
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddFirstEmployee]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddFirstEmployee]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddFirstEmployee]  
(
@AccountName nvarchar(255),
@LoggedIn datetime,
@UID int out,
@Result int out
)
AS  
BEGIN
			SET NOCOUNT ON;
			DECLARE @errMsg VARCHAR(255)
			DECLARE @beginTranCount INT
			SET @beginTranCount = @@TRANCOUNT			
			BEGIN TRANSACTION
--SET @UID=(SELECT UID FROM EmpComponents WHERE AccountName=@AccountName)
			
IF NOT EXISTS (SELECT * FROM EmpComponents)
	BEGIN
		 INSERT INTO [dbo].[EmpComponents] (EmployeeID,AccountName,RoleID,IsActive,LastLogin)	
         VALUES ('',@AccountName,1,1,@LoggedIn)  
         SET @UID=@@IDENTITY
         UPDATE EmpComponents SET CreatedBy=@UID WHERE UID=@UID
	     SET @Result=1 
		 	
	END
ELSE IF EXISTS (SELECT UID FROM EmpComponents WHERE AccountName=@AccountName and IsActive=1) --@UID<>0
	BEGIN
		SET @UID=(SELECT UID FROM EmpComponents WHERE AccountName=@AccountName and IsActive=1)
		SET @Result=1
		
	END
ELSE 
	BEGIN
	 SET @Result=2 
		
	END
	 
IF (@@ERROR <> 0 )
    BEGIN 
		SET @errMsg = 'Error: AddFirstEmployee -> Unable to update into EmpComponents table';
		GOTO CLEANUP
	END

CLEANUP:
        IF @errMsg IS NULL 
            BEGIN
                IF @@TRANCOUNT > @beginTranCount 
                    BEGIN   
                        COMMIT TRANSACTION
                        RETURN 0
                    END
            END
        ELSE 
            BEGIN
                IF @@TRANCOUNT > 0 
                    ROLLBACK TRANSACTION   
                RAISERROR(@errMsg, 16, 1)
                RETURN -1
            END
    END
GO

--/****** Object:  StoredProcedure [dbo].[GetBiMonthlyReport]    Script Date: 12/27/2016 19:16:17 ******/
--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBiMonthlyPercentage]') AND TYPE IN (N'P', N'PC'))
--DROP PROCEDURE [dbo].[GetBiMonthlyPercentage]
--GO
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE PROCEDURE [dbo].[GetBiMonthlyPercentage] 
--(
--@PlanID int,
--@NetDV decimal,
--@Monthsofexp int,
--@Percentage int out
--) 
--AS  
--BEGIN
--IF(@Monthsofexp<=13)
--BEGIN
--SET @Percentage=(SELECT ISNULL(Percentage,0) AS Percentage FROM BIMonthlyBonusInfo WHERE PlanID=@PlanID AND Months='1-12'AND
--((EntryPointA <= @NetDV AND EntryPointB >= @NetDV) OR (EntryPointA <= @NetDV AND EntryPointB=0)))
--END
--ELSE IF(@Monthsofexp>13)
--BEGIN
--SET @Percentage=(SELECT ISNULL(Percentage,0) AS Percentage FROM BIMonthlyBonusInfo WHERE PlanID=@PlanID AND Months='13+'AND
--((EntryPointA <= @NetDV AND EntryPointB >= @NetDV) OR (EntryPointA <= @NetDV AND EntryPointB=0)))
--END

--END
--GO

--/****** Object:  StoredProcedure [dbo].[GetBiMonthlyReport]    Script Date: 12/27/2016 19:16:17 ******/
--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTenurePercentage]') AND TYPE IN (N'P', N'PC'))
--DROP PROCEDURE [dbo].[GetTenurePercentage]
--GO
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE PROCEDURE [dbo].[GetTenurePercentage] 
--(
--@PlanID int,
--@NetDV decimal,
--@Monthsofexp int,
--@Percentage int out
--) 
--AS  
--BEGIN
--IF(@Monthsofexp<13)
--BEGIN
--SET @Percentage=0
--END
--ELSE IF(@Monthsofexp>=13 AND @Monthsofexp<=24)
--BEGIN
--SET @Percentage=(
--SELECT ISNULL(Percentage,0) AS Percentage FROM TenureBonus WHERE PlanID=@PlanID AND Months='13-24'AND
--(EntryPointA <= @NetDV AND EntryPointB >= @NetDV))
--END
--ELSE IF(@Monthsofexp>=25 AND @Monthsofexp<=36)
--BEGIN
--SET @Percentage=(SELECT ISNULL(Percentage,0) AS Percentage FROM TenureBonus WHERE PlanID=@PlanID AND Months IN ( '13-24','25-36') AND
--(EntryPointA <= @NetDV AND EntryPointB >= @NetDV))
--END
--ELSE IF(@Monthsofexp>=37)
--BEGIN
--SET @Percentage=(SELECT ISNULL(Percentage,0) AS Percentage FROM TenureBonus WHERE PlanID=@PlanID AND
--(EntryPointA <= @NetDV AND EntryPointB >= @NetDV))
--END

--END
--GO
/****** Object:  StoredProcedure [dbo].[GetCommissionReport]    Script Date: 12/27/2016 19:58:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCommissionReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCommissionReport]
GO
  
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCommissionReport] 
(
@UID int,
@PayrollConfigID int) 
AS  
BEGIN

	
	SELECT 
	ID,
	ISNULL(InvoiceNumber, '')    InvoiceNumber, 
	ISNULL(CustomerName, '')     CustomerName,
	ISNULL(CommentSold,'') CommentSold,
	ISNULL(SplitSalePerson, '')    SplitSalePerson,
	ISNULL(TradeIn,'0.00') TradeIn,
	ISNULL(DollarVolume, '0.00')     DollarVolume, 
	ISNULL(BaseCommission, '0.00')   BaseCommission, 
    ISNULL(LeaseCommission, '0.00')    LeaseCommission, 
    ISNULL(ServiceCommission, '0.00')     ServiceCommission, 
    ISNULL(TravelCommission, '0.00')   TravelCommission,
    ISNULL(SpecialCommission, '0.00')     SpecialCommission, 
    ISNULL(CashCommission, '0.00')    CashCommission,
    ISNULL(TipLeadAmount, '0.00')   TipLeadAmount,
    ISNULL(PositiveAdjustments,'0.00') PositiveAdjustments,
    ISNULL(NegativeAdjustments, '0.00')     NegativeAdjustments,
    ISNULL(CompanyContribution,'0.00') CompanyContribution,
    ISNULL(TotalCEarned, '0.00')     TotalCEarned,
    ISNULL(SlipType,'') SlipType,
    ISNULL(ProcesByPayroll,0) ProcesByPayroll
	
 FROM CommissionComponents 
 WHERE IsActive=1 AND Status=7 AND ((CreatedBy=@UID AND SlipType=1)OR (TipLeadID=@UID AND SlipType=2)) 
 AND AccountPeriod=@PayrollConfigID

END

GO
--exec GetCommissionReport 9,3,2017
--/****** Object:  StoredProcedure [dbo].[GetCommissionReport]    Script Date: 12/27/2016 19:58:44 ******/
--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNetDVforReport]') AND type in (N'P', N'PC'))
--DROP PROCEDURE [dbo].[GetNetDVforReport]
--GO

--GO

--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--CREATE PROCEDURE [dbo].[GetNetDVforReport] 
--(
--@UID int,
--@Month int,
--@Year int,
--@BonusType int) 
--AS  
--BEGIN

--DECLARE @Fromdate datetime,@Todate datetime
--SET @Todate=(SELECT dateadd(month,((@Year-1900)*12)+@Month-1,0))
--SET @Fromdate=DATEADD(month,-@BonusType,@Todate)

--SELECT	ISNULL(SUM(DollarVolume), '0.00')     NetDollarVolume
--FROM CommissionComponents 
--WHERE IsActive=1 AND Status=7 AND CreatedBy=@UID AND  DATEADD(month, DATEDIFF(month, 0, AccountPeriod), 0) BETWEEN 
--          DATEADD(month, DATEDIFF(month, 0, @Fromdate), 0) AND
--          DATEADD(month, DATEDIFF(month, 0, @Todate), 0)
--END
--GO

/****** Object:  StoredProcedure [dbo].[GetAllEmployeeDetailsbyUID]    Script Date: 01/05/2017 14:45:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllEmployeeDetailsbyRoleID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllEmployeeDetailsbyRoleID]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAllEmployeeDetailsbyRoleID]
@RoleID int
AS
IF @RoleID = 1
BEGIN
SELECT	   ISNULL(Emp.UID, '')    UID,
		   ISNULL(Emp.EmployeeID, '')    EmployeeID,
		   ISNULL(Emp.AccountName, '') AccountName,		
           ISNULL(Emp.FirstName, '')     FirstName,
           ISNULL(Emp.LastName, '')     LastName,
           ISNULL(Role.Name, '')     RoleName,
           ISNULL(Role.RoleID,'') RoleID,
           ISNULL(Emp.ReportMgr, '')     ReportMgrID,
           ISNULL(E2.FirstName,'') +' '+ ISNULL(E2.LastName,'')ReportMgrName,           
           ISNULL(Emp.ApproveMgr, '')     ApproveMgrID,
           ISNULL(E1.FirstName,'') +' '+ ISNULL(E1.LastName,'')ApproveMgrName,
           ISNULL(Emp.IsActive, '')     IsActive
    FROM  EmpComponents AS Emp 
    LEFT JOIN Roles AS Role ON Emp.RoleID=Role.RoleID
    LEFT JOIN EmpComponents AS E1 ON E1.UID = Emp.ApproveMgr
    LEFT JOIN EmpComponents AS E2 ON E2.UID = Emp.ReportMgr
    WHERE Emp.RoleID IN (1,4)
    ORDER BY Emp.IsActive DESC, Emp.UID DESC
END
ELSE IF @RoleID = 4
BEGIN
SELECT	   ISNULL(Emp.UID, '')    UID,
		   ISNULL(Emp.EmployeeID, '')    EmployeeID,
		   ISNULL(Emp.AccountName, '') AccountName,		
           ISNULL(Emp.FirstName, '')     FirstName,
           ISNULL(Emp.LastName, '')     LastName,
           ISNULL(Role.Name, '')     RoleName,
           ISNULL(Role.RoleID,'') RoleID,
           ISNULL(Emp.ReportMgr, '')     ReportMgrID,
           ISNULL(E2.FirstName,'') +' '+ ISNULL(E2.LastName,'')ReportMgrName,           
           ISNULL(Emp.ApproveMgr, '')     ApproveMgrID,
           ISNULL(E1.FirstName,'') +' '+ ISNULL(E1.LastName,'')ApproveMgrName,
           ISNULL(Emp.IsActive, '')     IsActive
    FROM  EmpComponents AS Emp 
    LEFT JOIN Roles AS Role ON Emp.RoleID=Role.RoleID
    LEFT JOIN EmpComponents AS E1 ON E1.UID = Emp.ApproveMgr
    LEFT JOIN EmpComponents AS E2 ON E2.UID = Emp.ReportMgr
    WHERE Emp.RoleID IN (2,3,5,6)
    ORDER BY Emp.IsActive DESC, Emp.UID DESC
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllManager]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllManager]
GO

/****** Object:  StoredProcedure [dbo].[GetAllManager]    Script Date: 01/09/2017 19:34:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllManager]
AS
BEGIN
SELECT	   ISNULL(UID, '')    UID, 		
           ISNULL(FirstName, '')     FirstName,
           ISNULL(LastName, '')     LastName,
           ISNULL(RoleID, '')     RoleID
    FROM  EmpComponents
    WHERE RoleID IN (2,3) AND IsActive=1 --Role ID for SM-2 GM-3
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllEmployeeDetailsbyUID]    Script Date: 01/05/2017 14:45:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommissionCreatedNotification]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CommissionCreatedNotification]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CommissionCreatedNotification]
@UID int,
@Status int,
@Result nvarchar(4000) out 
AS
BEGIN
DECLARE @Email NVARCHAR(4000) 
IF @Status = 2 --Only for Tipleadslip
BEGIN	
	SELECT @Email= COALESCE(@Email + ', ', '') + Email FROM EmpComponents WHERE UID IN (
	(SELECT ApproveMgr FROM EmpComponents WHERE UID=@UID),@UID);
	SET @Result = @Email
END
ELSE IF @Status = 3 --SM-Accepts
BEGIN	
	SELECT @Email = COALESCE(@Email + ', ', '') + Email 
	FROM EmpComponents where UID=(select ApproveMgr from EmpComponents where UID=@UID)
	SET @Result = @Email
END
ELSE IF @Status = 5 --GM-Accepts
BEGIN	
	SELECT @Email = COALESCE(@Email + ', ', '') + Email 
	FROM EmpComponents where RoleID=4
	SET @Result = @Email
END
ELSE IF @Status = 6 --GM-Reject
BEGIN
	SELECT @Email= COALESCE(@Email + ', ', '') + Email FROM EmpComponents WHERE UID IN (
	(SELECT ReportMgr FROM EmpComponents WHERE UID=@UID),@UID);
	SET @Result = @Email
END
ELSE IF @Status = 7 --Payroll Accept and Notify
BEGIN
	SELECT @Email = COALESCE(@Email + ', ', '') + Email 
	FROM EmpComponents where UID=(select ApproveMgr from EmpComponents where UID=@UID)
	SET @Result = @Email
END
ELSE
BEGIN
SET @Result=(select Email from EmpComponents where UID=@UID)
END
END

GO
/****** Object:  StoredProcedure [dbo].[GetDrawDeficit]    Script Date: 12/27/2016 19:58:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDrawDeficit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDrawDeficit]
GO

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetDrawDeficit] 
(
@UID int,
@Month int,
@Year int,
@RunNo int,
@Drawpaid decimal(20,3) out,
@Drawrecoverable decimal(20,3) out,
@Drawdificit decimal(20,3) out,
@Applicableperiod nvarchar(255) out,
@Drawamount decimal(20,3) out,
@Drawtype int out,
@Recoverablepercent decimal(20,3) out) 
AS  
BEGIN
		DECLARE @dp decimal(20,3),@dr decimal(20,3),@dd decimal(20,3), @ddamount decimal(20,3),@drawterm int,@bpdraw bit,
	@ddpercent decimal(20,3),@Totalcommission decimal(20,3),
	@drawperiod datetime,@applicabletill datetime,@selecteddate datetime,@loopperiod datetime;
	
	SELECT @bpdraw=ISNULL(BPDraw,0), @ddamount=ISNULL(DDAmount,0.000),@ddpercent=ISNULL(DRPercentage,0.000)*0.01,@drawperiod=ISNULL(DDPeriod,''),@drawterm=ISNULL(DrawTerm,0),@Drawtype=ISNULL(TypeofDraw,'')
	 ,@applicabletill=DATEADD(MONTH,@drawterm-1,ISNULL(DDPeriod,'')) FROM EmpComponents WHERE UID=@UID
				SET @selecteddate=(SELECT dateadd(month,((@Year-1900)*12)+@Month-1,0))
				SET @drawperiod=(SELECT DATEADD(month,((DATEPART(YYYY, @drawperiod)-1900)*12)+DATEPART(MM, @drawperiod)-1,0))
				SET @applicabletill=(SELECT DATEADD(month,((DATEPART(YYYY, @applicabletill)-1900)*12)+DATEPART(MM, @applicabletill)-1,0))
Declare @payrollconfigID1 int, @Prevcommn decimal(20,3),@exceeds bit=0
IF(@bpdraw=1)--Draw is yes in emp creation
BEGIN		
	SET @Drawdificit=0
	SET @dd=0
	IF(@selecteddate<@drawperiod)--if selected date is before draw period
		BEGIN
		SET @Drawpaid=0
		SET @Drawrecoverable=0
		SET @Drawdificit=0
		END
	ELSE--selected date is after draw period
		BEGIN
		SET @loopperiod=@drawperiod
			WHILE (@loopperiod<=@selecteddate)
				BEGIN
				SET @exceeds=0;
				IF(@loopperiod>@applicabletill)
				BEGIN
				SET @ddamount=0
				END
				
				SET @PrevCommn=0;
				
			SELECT ISNULL(ID,'') as Looprun,
		ISNULL(DateFrom,'')DateFrom,
		ISNULL(DateTo,'')DateTos,ROW_NUMBER()over(order by DateTo)rid into #t FROM PayrollConfig
		WHERE ((Year(@loopperiod) * 100) + (MONTH(@loopperiod))) between year(DateTo) * 100 + month(DateTo) and
                     year(DateTo) * 100 + month(DateTo) ORDER BY DateTo ASC	
         Declare @Totalrun int,@currentrun int=1
         
         DECLARE @Mont int 
         
         select @Mont=MONTH(DateTos) from #t where rid=@currentrun
         
         select @Totalrun=COUNT(*) from #t            

             while (@Totalrun>=@currentrun)
             begin
              select @payrollconfigID1=Looprun from #t where rid=@currentrun
             -- set @currentrun=@currentrun+1
             --end    
						---select runs =3 @prevcom=0
				--while run
				--begin
				--@exceeds
				SELECT @Totalcommission=ISNULL(SUM(ISNULL(TotalCEarned, '0.00')),0.00) FROM CommissionComponents 
					 WHERE IsActive=1 AND Status=7 AND ((CreatedBy=@UID AND SlipType=1)OR(TipLeadID=@UID AND SlipType=2))
									AND AccountPeriod=@payrollconfigID1
				
				IF(@exceeds=1 AND @dd<>0)
				
				Begin
				SET @dp=0;
				SET @dr=@Totalcommission *@ddpercent;
				IF(@dd<@dr)
				BEGIN
				SET @dr=@dd
				SET @dd=((@dd+@dp)-@dr)
				END
				ELSE
				BEGIN	
				SET @dd=@dd - @dr;
				END
				end
				
				ELSE
				BEGIN
				SET @Totalcommission=@Totalcommission+@Prevcommn
				SET @Prevcommn=@Totalcommission
				--if(@exceeds==1 && drawdeficit<>0)
				--{@dp=0;
				--@Dr=@totalcommn/@drawpercent
				--}
				--else if{@exceeds==1 && drawdeficit==0)
				--{}
				--else{
				----@totalcommn=@totalcommn+prevcommn
				--@prevcommn=@totalcommn
				IF(@ddamount=@Totalcommission)
				BEGIN
						SET @dr=0
						SET @dp=@ddamount-@Totalcommission
						SET @dd=(@dd+@dp)-@dr
						IF(@Drawtype=1 AND @loopperiod>@applicabletill)
						BEGIN
						SET @dd=0
						END
				END	
				ELSE IF(@ddamount>@Totalcommission)
				BEGIN
				IF(@Totalrun=@currentrun)
				BEGIN
				--if (finalrun){
						SET @dr=0
						SET @dp=@ddamount-@Totalcommission
						SET @dd=(@dd+@dp)-@dr
				END
				ELSE
				BEGIN
						--}else{
						SET @dr=0
						SET @dp=0
						SET @dd=(@dd+@dp)-@dr
						--}
				END
								
				END	
				ELSE IF(@Totalcommission>@ddamount)
				BEGIN
				SET @exceeds=1
					IF(@Drawtype=2)--Recoverable
					BEGIN
						IF(@loopperiod<=@applicabletill)
						BEGIN
							SET @dp=0
							SET @dr=(@Totalcommission-@ddamount)*@ddpercent
								IF(@dd<@dr)
								BEGIN
									SET @dr=@dd
								END
								SET @dd=((@dd+@dp)-@dr)
						END
						ELSE IF(@dd<>0)--if drawdeficit is not equal to zero even after term ends
						BEGIN
							SET @dp=0
							SET @dr=@Totalcommission*@ddpercent
								IF(@dd<@dr)
								BEGIN
									SET @dr=@dd
								END
								SET @dd=((@dd+@dp)-@dr)
						END
						ELSE
						BEGIN
							SET @dp=0
							SET @dr=0
							SET @dd=0
						END
					END
					ELSE IF(@Drawtype=1)--Guarenteed
					BEGIN
						IF(@loopperiod<=@applicabletill)
						BEGIN
							SET @dp=0
							SET @dr=0
								IF(@dd<@dr)
								BEGIN
									SET @dr=@dd
								END
								SET @dd=((@dd+@dp)-@dr)
						END
						ELSE
						BEGIN
							SET @dp=0
							SET @dr=0
							SET @dd=0
						END
					END
				END
				ELSE IF(@ddamount=0)
				BEGIN
				SET @dp=0
				SET @dr=0
				SET @dd=0
				END
				END
				
				IF @Mont = @Month
				BEGIN
				SET @Totalrun = @RunNo
				END
				
				SET @currentrun=@currentrun+1;
				END --While Begin End
				
				IF (@selecteddate=@loopperiod)
				BEGIN
					SET @Drawpaid=@dp
					SET @Drawrecoverable=@dr
					SET @Drawdificit=@dd
					drop table #t
					BREAK
				END	
				drop table #t
				SET @loopperiod=DATEADD(MONTH,1,ISNULL(@loopperiod,''))--increase month
			END
	END
END
ELSE--Draw is No in emp creation
BEGIN
	SET @Drawpaid=0
	SET @Drawrecoverable=0
	SET @Drawdificit=0
END
IF(@applicabletill>=@selecteddate)
BEGIN
	SET @Applicableperiod=DATENAME(MONTH, @applicabletill)+' '+CONVERT(varchar(10), YEAR(@applicabletill))
	SET @Drawamount=@ddamount
	SET @Drawtype=@Drawtype
	 IF(@Drawtype=1)
	 BEGIN 
		 SET @Recoverablepercent=0.000
	 END
	 ELSE
	 BEGIN
		  SET @Recoverablepercent=@ddpercent
	 END
END
ELSE
BEGIN
SET @Applicableperiod='-'
	SET @Drawamount=0.000
	IF(@Drawdificit<>0 OR @Drawrecoverable<>0)
	BEGIN
	SET @Drawtype=2
	SET @Recoverablepercent=@ddpercent
	END
	ELSE
	BEGIN
	SET @Drawtype=0
	SET @Recoverablepercent=0.000
	END

END
RETURN @Drawpaid
RETURN @Drawrecoverable
RETURN @Drawdificit
RETURN @Applicableperiod
RETURN @Drawamount
RETURN @Drawtype
RETURN @Recoverablepercent
END
GO
/****** Object:  StoredProcedure [dbo].[GetBiMonthlyBonusReport]    Script Date: 12/27/2016 19:16:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBiMonthlyBonusReport]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetBiMonthlyBonusReport]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBiMonthlyBonusReport] 
(
@PlanID int,
@Monthsofexp int,
@UID int,
@Month int,
@Year int,
@PayrollConfigID int,
@Bimonthreport decimal(20,3) out
) 
AS  
BEGIN
DECLARE @Fromdate datetime,@Todate datetime,@NetDV decimal(20,3),@Percentage decimal(20,3)
	--SET @Todate=(SELECT dateadd(month,((@Year-1900)*12)+@Month-1,0))
	SET @Todate=(SELECT DateTo FROM PayrollConfig WHERE ID = @PayrollConfigID)
	--SET @Fromdate=DATEADD(month,-1,@Todate)
	SET @Fromdate=(SELECT TOP(1)DateFrom FROM PayrollConfig
		WHERE ((@Year * 100) + (@Month-1)) between year(DateTo) * 100 + month(DateTo) and
                     year(DateTo) * 100 + month(DateTo) )

if(@Monthsofexp=1)
begin
	SELECT @NetDV=ISNULL(SUM(ISNULL(DollarVolume, '0.00')),0.00) FROM CommissionComponents AS cc
	JOIN SaleType as st on cc.SaleType = st.ID 
	WHERE cc.IsActive=1 AND cc.Status=7 AND st.IsBiMonthlyBonus =1 AND cc.CreatedBy=@UID AND cc.SlipType=1 
	AND cc.CreatedOn BETWEEN DATEADD(MONTH,((@Year-1900)*12)+@Month-1,0) AND @Todate
	--AND  DATEADD(month, DATEDIFF(month, 0, cc.AccountPeriod), 0)=DATEADD(month, DATEDIFF(month, 0, @Todate), 0)
end
else
begin
	SELECT @NetDV=ISNULL(SUM(ISNULL(DollarVolume, '0.00')),0.00) FROM CommissionComponents	AS cc
	JOIN SaleType as st on cc.SaleType = st.ID  
	WHERE cc.IsActive=1 AND Status=7 AND st.IsBiMonthlyBonus =1 AND cc.CreatedBy=@UID AND cc.SlipType=1 
	AND  cc.CreatedOn BETWEEN 
			  @Fromdate AND
			  @Todate
end

IF(@Monthsofexp<=13)
BEGIN
	SET @Percentage=(SELECT ISNULL(Percentage,0.000) AS Percentage FROM BIMonthlyBonusInfo WHERE PlanID=@PlanID AND Months='1-12'AND
	((EntryPointA <= @NetDV AND EntryPointB >= @NetDV) OR (EntryPointA <= @NetDV AND EntryPointB=0)))
END
ELSE IF(@Monthsofexp>13)
BEGIN
	SET @Percentage=(SELECT ISNULL(Percentage,0.000) AS Percentage FROM BIMonthlyBonusInfo WHERE PlanID=@PlanID AND Months='13+'AND
	((EntryPointA <= @NetDV AND EntryPointB >= @NetDV) OR (EntryPointA <= @NetDV AND EntryPointB=0)))
END
	set @Bimonthreport=ISNULL(@NetDV*@Percentage*0.01,0)
	RETURN @Bimonthreport
	
END
GO
/****** Object:  StoredProcedure [dbo].[GetTenureBonusReport]    Script Date: 12/27/2016 19:16:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTenureBonusReport]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetTenureBonusReport]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTenureBonusReport] (
@PlanID int,
@Monthsofexp int,
@UID int,
@Month int,
@Year int,
@PayrollConfigID int,
@Tenurereport decimal(20,3)out
)
AS
BEGIN
	IF(@Monthsofexp<15)
	BEGIN
		SET @Tenurereport=0
	END
	ELSE IF(@Monthsofexp>=15)
	BEGIN
		DECLARE @Fromdate datetime,@Todate datetime,@NetDV decimal(20,3),@Percentage decimal(20,3)
			--SET @Todate=(SELECT dateadd(month,((@Year-1900)*12)+@Month-1,0))
			
			SET @Todate=(SELECT DateTo FROM PayrollConfig WHERE ID = @PayrollConfigID)
			
			SET @Fromdate=DATEADD(month,-2,@Todate)
			
			SET @Fromdate=(SELECT TOP(1)DateFrom FROM PayrollConfig
			WHERE YEAR(DateTo) = YEAR(@Fromdate) AND month(DateTo) = month(@Fromdate))
			
			--WHERE ((2017 * 100) + (2-2)) between year(DateTo) * 100 + month(DateTo) and
			--year(DateTo) * 100 + month(DateTo) )
			
			SELECT @NetDV=ISNULL(SUM(ISNULL(DollarVolume, '0.00')),0.00) FROM CommissionComponents AS cc
			JOIN SaleType as st on cc.SaleType = st.ID  			
			WHERE cc.IsActive=1 AND cc.Status=7 AND st.IsTenureBonus =1 AND cc.CreatedBy=@UID AND cc.SlipType=1
			AND cc.CreatedOn BETWEEN @Fromdate AND @Todate
				 --AND  DATEADD(month, DATEDIFF(month, 0, cc.AccountPeriod), 0) BETWEEN DATEADD(month, DATEDIFF(month, 0, @Fromdate), 0) AND DATEADD(month, DATEDIFF(month, 0, @Todate), 0)
		IF(@Monthsofexp>=15 AND @Monthsofexp<=26)--Months 13-24
		BEGIN
		SET @Percentage=(SELECT ISNULL(MAX(Percentage),0.000) FROM TenureBonus 
			WHERE PlanID=@PlanID AND Months='13-24' AND EntryPointA=( SELECT ISNULL(MAX(EntryPointA),0) AS Percentage FROM TenureBonus 
			WHERE PlanID=@PlanID AND Months='13-24' AND	EntryPointA<=@NetDV))
		END
		ELSE IF(@Monthsofexp>=27 AND @Monthsofexp<=38)--Months 25-36
		BEGIN
		SET @Percentage=(SELECT ISNULL(MAX(Percentage),0.000) FROM TenureBonus 
			WHERE PlanID=@PlanID AND Months IN('13-24','25-36') AND EntryPointA=( SELECT ISNULL(MAX(EntryPointA),0) AS Percentage FROM TenureBonus 
			WHERE PlanID=@PlanID AND Months IN('13-24','25-36') AND	EntryPointA<=@NetDV))
		END
		ELSE IF(@Monthsofexp>=39)--Months 37+
		BEGIN
		SET @Percentage=(SELECT ISNULL(MAX(Percentage),0.000) FROM TenureBonus 
			WHERE PlanID=@PlanID AND EntryPointA=( SELECT ISNULL(MAX(EntryPointA),0) AS Percentage FROM TenureBonus 
			WHERE PlanID=@PlanID AND EntryPointA<=@NetDV))
		END
	SET @Tenurereport=ISNULL(@NetDV*@Percentage*0.01,0)
	RETURN @Tenurereport
	END

END
GO
/****** Object:  StoredProcedure [dbo].[]    Script Date: 12/27/2016 19:16:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCommissionslipreport]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCommissionslipreport]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE GetCommissionslipreport
(
@UID int,
@Month int,
@PayrollConfigID int,
@Year int,
@PlanID int,
@Monthsofexp int,
@IsFinalRun bit,
@RunNo int,

@Totalcommission decimal(20,3) out,
@Commissiondue decimal(20,3) out,
@Drawpaid decimal(20,3) out,
@Drawrecoverable decimal(20,3) out,
@Drawdificit decimal(20,3) out,
@Applicableperiod nvarchar(255) out,
@Drawamount decimal(20,3) out,
@Drawtype int out,
@Recoverablepercent decimal(20,3) out,
@Bimonthreport decimal(20,3) out,
@Tenurereport decimal(20,3) out,
@TotalEarning decimal(20,3) out,
@Salary decimal(20,3) out,
@EmployeeID nvarchar(255) out
)AS
BEGIN
DECLARE @IsProcessed bit--Need to change this
SET @IsProcessed=(SELECT TOP 1 (ISNULL(ProcesByPayroll,0))  FROM CommissionComponents 
					 WHERE IsActive=1 AND Status=7 AND CreatedBy=@UID 
					 AND AccountPeriod=@PayrollConfigID)

IF(@IsProcessed=1 OR EXISTS(SELECT * FROM ReportDetails WHERE UID=@UID 
AND ReportPeriod=@PayrollConfigID))
BEGIN
	SELECT @Totalcommission=ISNULL(TotalCommission,0.00),@Commissiondue=ISNULL(CommissionDue,0.00),
	@Drawpaid=ISNULL(DrawPaid,0.00),@Drawrecoverable=ISNULL(DrawRecovered,0.00),@Drawdificit=ISNULL(DrawDificit,0.00),
	@Bimonthreport=ISNULL(BimonthlyBonus,0.00),@Tenurereport=ISNULL(TenureBonus,0.00),@TotalEarning=ISNULL(TotalEarnings,0.00),
	@Salary=ISNULL(Salary,0.00),@EmployeeID=ISNULL(EmployeeID,''),@Applicableperiod=ISNULL(ApplicableTill,''),@Drawamount=ISNULL(DrawAmount,0.00),
	@Drawtype=ISNULL(DrawType,0),@Recoverablepercent=ISNULL(RecoverablePercentage,0.000) FROM ReportDetails 
	WHERE UID=@UID AND ReportPeriod=@PayrollConfigID

END	
ELSE
	IF @IsFinalRun = 0 --If it is not a Final Run
	BEGIN
		SELECT @Totalcommission=ISNULL(SUM(ISNULL(TotalCEarned, 0.00)),0.00) FROM CommissionComponents 
		WHERE IsActive=1 AND Status=7 AND ((CreatedBy=@UID AND SlipType=1)OR(TipLeadID=@UID AND SlipType=2))
		AND AccountPeriod=@PayrollConfigID
		
		--If Run is not Final then the value is set to zero.
		-----------------------------------------------------
		
		EXEC GetDrawDeficit @UID,@Month,@Year,@RunNo ,@Drawpaid OUT,@Drawrecoverable OUT,@Drawdificit OUT,@Applicableperiod OUT,@Drawamount OUT,@Drawtype OUT,@Recoverablepercent OUT
		
		SET @Drawpaid = 0
		
		SET @Bimonthreport =0
		
		SET @Tenurereport = 0
		
		SET @Salary = 0
		------------------------------------------------------
		SELECT @EmployeeID=ISNULL(EmployeeID,'')  FROM EmpComponents WHERE UID=@UID
		
		SET @Commissiondue=@Totalcommission-@Drawrecoverable
		
		SET @TotalEarning= @Commissiondue+@Drawpaid+@Salary+@Bimonthreport+@Tenurereport
		
	END
	
	ELSE IF @IsFinalRun = 1 --Check it is Final Run
	
	BEGIN								
		SELECT @Totalcommission=ISNULL(SUM(ISNULL(TotalCEarned, 0.00)),0.00) FROM CommissionComponents 
		WHERE IsActive=1 AND Status=7 AND ((CreatedBy=@UID AND SlipType=1)OR(TipLeadID=@UID AND SlipType=2))
		AND AccountPeriod=@PayrollConfigID
	
	SELECT @Salary=ISNULL(MonthAmount,0.00),@EmployeeID=ISNULL(EmployeeID,'')  FROM EmpComponents WHERE UID=@UID
		
	EXEC GetDrawDeficit @UID,@Month,@Year,@RunNo ,@Drawpaid OUT,@Drawrecoverable OUT,@Drawdificit OUT,@Applicableperiod OUT,@Drawamount OUT,@Drawtype OUT,@Recoverablepercent OUT
	SET @Commissiondue=@Totalcommission-@Drawrecoverable


		IF(@Month=1 OR @Month=3 OR @Month=5 OR @Month=7 OR @Month=9 OR @Month=11)
		BEGIN
		SET @Bimonthreport=0
		END                   
		ELSE
		BEGIN
		EXEC GetBiMonthlyBonusReport @PlanID,@Monthsofexp,@UID,@Month,@Year,@PayrollConfigID,@Bimonthreport OUT
		END

		IF(@Month=2 OR @Month=5 OR @Month=8 OR @Month=11)
		BEGIN
		EXEC GetTenureBonusReport @PlanID,@Monthsofexp,@UID,@Month,@Year,@PayrollConfigID,@Tenurereport OUT
		END
		ELSE
		BEGIN
		SET @Tenurereport=0
		END

		SET @TotalEarning= @Commissiondue+@Drawpaid+@Salary+@Bimonthreport+@Tenurereport
	END
	RETURN @Totalcommission
	RETURN @Commissiondue
	RETURN @Drawpaid
	RETURN @Drawrecoverable 
	RETURN @Drawdificit 
	RETURN @Bimonthreport 
	RETURN @Tenurereport 
	RETURN @TotalEarning 
	RETURN @Salary 
	RETURN @EmployeeID
	RETURN @Applicableperiod 
	RETURN @Drawamount 
	RETURN @Drawtype 
	RETURN @Recoverablepercent
END
GO
/****** Object:  StoredProcedure [dbo].[GetUID4PaylocityReport]    Script Date: 02/09/2017 15:23:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUID4PaylocityReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUID4PaylocityReport]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUID4PaylocityReport]
(
@SelectedDate Datetime,
@PayrollConfigID INT,
@BranchID INT
)
AS
BEGIN
SELECT DISTINCT UID, CASE    
WHEN  DATEPART(dd,DateInPosition) > 5 THEN DATEDIFF(MONTH,DateInPosition,@SelectedDate)
ELSE DATEDIFF(MONTH,DateInPosition,@SelectedDate)+1
END AS MonthsinExp,
ISNULL(emp.PayPlanID,'')PayPlanID,
ISNULL(emp.PrimaryBranch,'') BranchID
  
FROM EmpComponents AS emp LEFT JOIN CommissionComponents AS cc
ON ((emp.UID = cc.CreatedBy AND cc.SlipType=1) OR (emp.UID=cc.TipLeadID AND cc.SlipType=2))
WHERE emp.IsActive = 1  AND cc.IsActive = 1 AND cc.BranchID=@BranchID
AND cc.Status = 7 
AND cc.AccountPeriod = @PayrollConfigID
UNION
SELECT DISTINCT UID, CASE    
WHEN  DATEPART(dd,DateInPosition) > 5 THEN DATEDIFF(MONTH,DateInPosition,@SelectedDate)
ELSE DATEDIFF(MONTH,DateInPosition,@SelectedDate)+1
END AS MonthsinExp,
ISNULL(emp.PayPlanID,'')PayPlanID,
ISNULL(emp.PrimaryBranch,'') BranchID 
FROM EmpComponents AS emp
Where Emp.PrimaryBranch=@BranchID AND emp.RoleID=5 
AND emp.IsActive = 1
AND DATEADD(month, DATEDIFF(month, 0, DateInPosition), 0) <= DATEADD(month, DATEDIFF(month, 0, @SelectedDate), 0)  
END
GO
--SELECT DISTINCT UID, CASE    
--WHEN  DATEPART(dd,DateInPosition) > 5 THEN DATEDIFF(MONTH,DateInPosition,@SelectedDate)
--ELSE DATEDIFF(MONTH,DateInPosition,@SelectedDate)+1
--END AS MonthsinExp,
--ISNULL(emp.PayPlanID,'')PayPlanID,
--ISNULL(emp.PrimaryBranch,'') BranchID  
--FROM EmpComponents AS emp LEFT JOIN CommissionComponents AS cc
--ON emp.UID = cc.CreatedBy
--WHERE cc.BranchID = @BranchID 
--AND emp.IsActive = 1  AND cc.IsActive = 1 
--AND cc.Status = 7 
--AND DATEADD(month, DATEDIFF(month, 0,cc.AccountPeriod), 0) = DATEADD(month, DATEDIFF(month, 0,@SelectedDate), 0)
--AND DATEADD(month, DATEDIFF(month, 0, DateInPosition), 0) <= DATEADD(month, DATEDIFF(month, 0, @SelectedDate), 0) 
--UNION
--SELECT DISTINCT UID, CASE    
--WHEN  DATEPART(dd,DateInPosition) > 5 THEN DATEDIFF(MONTH,DateInPosition,@SelectedDate)
--ELSE DATEDIFF(MONTH,DateInPosition,@SelectedDate)+1
--END AS MonthsinExp,
--ISNULL(emp.PayPlanID,'')PayPlanID,
--ISNULL(emp.PrimaryBranch,'') BranchID  
--FROM EmpComponents AS emp LEFT JOIN CommissionComponents AS cc
--ON emp.UID = cc.TipLeadID
--WHERE cc.BranchID = @BranchID 
--AND emp.IsActive = 1  AND cc.IsActive = 1 
--AND cc.Status = 7 
--AND DATEADD(month, DATEDIFF(month, 0,cc.AccountPeriod), 0) = DATEADD(month, DATEDIFF(month, 0,@SelectedDate), 0)
--AND DATEADD(month, DATEDIFF(month, 0, DateInPosition), 0) <= DATEADD(month, DATEDIFF(month, 0, @SelectedDate), 0) 
--END
--GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBranchTotalCommission]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetBranchTotalCommission]
GO
/****** Object:  StoredProcedure [dbo].[GetBranchTotalCommission]    Script Date: 02/14/2017 19:47:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetBranchTotalCommission]
(
@UID int,
@PayrollConfigID int,
@BranchID int,
@Totalcommission decimal(20,3) out
)
AS
BEGIN

	SELECT @Totalcommission=ISNULL(SUM(ISNULL(TotalCEarned, 0.00)),0.00) FROM CommissionComponents 
						 WHERE IsActive=1 AND Status=7 AND BranchID=@BranchID AND ((CreatedBy=@UID AND SlipType=1)OR(TipLeadID=@UID AND SlipType=2))
						  AND AccountPeriod=@PayrollConfigID
	RETURN @Totalcommission
END
GO

/****** Object:  StoredProcedure [dbo].[GetReportSalesReplist]    Script Date: 02/10/2017 13:31:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetReportSalesReplist]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetReportSalesReplist]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetReportSalesReplist]
(@UID int,
@RoleID int
)AS
BEGIN
IF(@RoleID=3)--GM
BEGIN
SELECT	ISNULL(Emp.UID, '')    UID,
		   ISNULL(Emp.EmployeeID, '')    EmployeeID,
		   ISNULL(Emp.AccountName, '') AccountName,		
           ISNULL(Emp.FirstName, '')     FirstName,
           ISNULL(Emp.LastName, '')     LastName,
           ISNULL(Emp.RoleID,'') RoleID
           FROM EmpComponents as Emp WHERE Emp.ApproveMgr=@UID AND (Emp.RoleID=5 OR Emp.RoleID=6) AND Emp.IsActive=1
END
ELSE IF(@RoleID=2)
BEGIN
SELECT	ISNULL(Emp.UID, '')    UID,
		   ISNULL(Emp.EmployeeID, '')    EmployeeID,
		   ISNULL(Emp.AccountName, '') AccountName,		
           ISNULL(Emp.FirstName, '')     FirstName,
           ISNULL(Emp.LastName, '')     LastName,
           ISNULL(Emp.RoleID,'') RoleID
           FROM EmpComponents as Emp  WHERE Emp.ReportMgr=@UID AND Emp.RoleID=5 AND Emp.IsActive=1
END
ELSE IF(@RoleID=4)
BEGIN
SELECT	ISNULL(Emp.UID, '')    UID,
		   ISNULL(Emp.EmployeeID, '')    EmployeeID,
		   ISNULL(Emp.AccountName, '') AccountName,		
           ISNULL(Emp.FirstName, '')     FirstName,
           ISNULL(Emp.LastName, '')     LastName,
           ISNULL(Emp.RoleID,'') RoleID           
           FROM EmpComponents as Emp  WHERE (Emp.RoleID=5 OR Emp.RoleID=6) AND Emp.IsActive=1
END
END
GO

/****** Object:  StoredProcedure [dbo].[GetIncentiveTripReport]    Script Date: 02/10/2017 13:31:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetIncentiveTripReport]') AND TYPE IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetIncentiveTripReport]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetIncentiveTripReport]
(@UID int,
@RoleID int,
@StartYear int
)AS
BEGIN
DECLARE @Fromdate datetime,@Todate datetime
		SET @Fromdate=(SELECT MIN(DateFrom) from PayrollConfig where YEAR(DateTo) = @StartYear and MONTH(DateTo) = 9)
		SET @Todate=(SELECT MIN(DateFrom) from PayrollConfig where YEAR(DateTo) = @StartYear + 1 and MONTH(DateTo) = 9)
		
		IF @Fromdate IS NULL
		BEGIN
		SET @Fromdate=(SELECT dateadd(month,((@StartYear-1900)*12)+8,0))
		END
		
		IF @Todate IS NULL
		BEGIN
		SET @Todate=(SELECT dateadd(month,(((@StartYear+1)-1900)*12)+8,0))
		END
		
IF(@RoleID=3)--GM
BEGIN
select SalesPerson AS Name,ISNULL(SUM(ISNULL(DollarVolume, '0.00')),0.00) AS DollarVolume from CommissionComponents
WHERE CreatedBy IN (SELECT UID FROM EmpComponents WHERE ApproveMgr=@UID AND RoleID=5 AND IsActive=1)
AND IsActive=1 AND SlipType=1 AND Status=7 AND  DATEADD(month, DATEDIFF(month, 0, CreatedOn), 0) BETWEEN 
				  DATEADD(month, DATEDIFF(month, 0, @Fromdate), 0) AND
				  DATEADD(month, DATEDIFF(month, 0, @Todate), 0) GROUP BY CreatedBy,SalesPerson
END
ELSE IF(@RoleID=2)--SM
BEGIN
select SalesPerson AS Name,ISNULL(SUM(ISNULL(DollarVolume, '0.00')),0.00) AS DollarVolume from CommissionComponents
WHERE CreatedBy IN (SELECT UID FROM EmpComponents WHERE ReportMgr=@UID AND RoleID=5 AND IsActive=1)
AND IsActive=1 AND SlipType=1 AND Status=7 AND  DATEADD(month, DATEDIFF(month, 0, CreatedOn), 0) BETWEEN 
				  DATEADD(month, DATEDIFF(month, 0, @Fromdate), 0) AND
				  DATEADD(month, DATEDIFF(month, 0, @Todate), 0) group by CreatedBy,SalesPerson
END
END
GO

/****** Object:  StoredProcedure [dbo].[GetCountforPaylocity]    Script Date: 02/14/2017 19:47:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCountforPaylocity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCountforPaylocity]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCountforPaylocity]
(
@PayrollConfigID int, 
@Acceptedcount int out,
@Pendingcount int out
)
AS
BEGIN

	SELECT @Acceptedcount=COUNT(*) FROM CommissionComponents WHERE Status=7 
	AND AccountPeriod =@PayrollConfigID AND IsActive=1
									
SELECT @Pendingcount=COUNT(*) FROM CommissionComponents WHERE Status<>7 AND Status<>1 
AND AccountPeriod =@PayrollConfigID AND IsActive=1

RETURN @Acceptedcount
RETURN @Pendingcount
END
GO

/****** Object:  StoredProcedure [dbo].[GeneralLedgerReport]    Script Date: 02/14/2017 19:47:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GeneralLedgerReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GeneralLedgerReport]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GeneralLedgerReport]
(
@PayrollConfigID INT
)
AS
BEGIN
SELECT B.BranchName,ISNULL(SUM(C.TotalCEarned),0) AS TotalCommissionPaid FROM Branches AS B
full outer JOIN (SELECT * FROM CommissionComponents AS C WHERE C.Status = 7  AND c.AccountPeriod = @PayrollConfigID) AS C
ON B.BranchID = C.BranchID where B.IsActive =1  Group by BranchName 
END
GO

/****** Object:  StoredProcedure [dbo].[SaveReportDetails] ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaveReportDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SaveReportDetails]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveReportDetails]
(
@UID int,
@Month int,
@Year int,
@PlanID int,
@Monthsofexp int,
@PayrollConfigID int,
@BranchID int,
@IsFinalRun bit,
@RunNo int,
@RETURN_VALUE int Output
)
AS  
BEGIN
		SET NOCOUNT ON;
		DECLARE @errMsg VARCHAR(255)
		DECLARE @beginTranCount INT
		SET @beginTranCount = @@TRANCOUNT			
		BEGIN TRANSACTION
		
DECLARE @selecteddate datetime, @Totalcommission decimal(20,3),@Commissiondue decimal(20,3),@Drawpaid decimal(20,3),@Drawrecoverable decimal(20,3),
@Drawdificit decimal(20,3),@Applicableperiod nvarchar(255),@Drawamount decimal(20,3),@Drawtype int,
@Recoverablepercent decimal(20,3),@Bimonthreport decimal(20,3),@Tenurereport decimal(20,3),@TotalEarning decimal(20,3),
@Salary decimal(20,3),@EmployeeID nvarchar(255),@IsProcessed bit
			
SET @IsProcessed=ISNULL((SELECT TOP 1 (ISNULL(ProcesByPayroll,0))  FROM CommissionComponents 
					 WHERE IsActive=1 AND Status=7 AND BranchID=@BranchID AND ((CreatedBy=@UID AND SlipType=1)OR(TipLeadID=@UID AND SlipType=2)) 
					  AND AccountPeriod = @PayrollConfigID),0)
--SET @selecteddate=(SELECT dateadd(month,((@Year-1900)*12)+@Month-1,0))

IF(@IsProcessed<>1)
BEGIN
EXEC GetCommissionslipreport @UID,@Month,@PayrollConfigID,@Year,@PlanID,@Monthsofexp,@IsFinalRun,@RunNo,@Totalcommission OUT,@Commissiondue OUT,@Drawpaid OUT,@Drawrecoverable OUT,
@Drawdificit OUT,@Applicableperiod OUT,@Drawamount OUT,@Drawtype OUT,
@Recoverablepercent OUT,@Bimonthreport OUT,@Tenurereport OUT,@TotalEarning OUT,
@Salary OUT,@EmployeeID OUT

	IF not exists (SELECT *  FROM ReportDetails WHERE UID=@UID AND ReportPeriod=@PayrollConfigID)
	BEGIN
		INSERT INTO ReportDetails (UID,EmployeeID,ReportPeriod,TotalCommission,DrawPaid,DrawRecovered,CommissionDue,Salary,BimonthlyBonus,TenureBonus,TotalEarnings,
		DrawType,ApplicableTill,DrawAmount,RecoverablePercentage,DrawDificit) values(@UID,@EmployeeID,@PayrollConfigID,@Totalcommission,@Drawpaid,
		@Drawrecoverable,@Commissiondue,@Salary,@Bimonthreport,@Tenurereport,@TotalEarning,@Drawtype,@Applicableperiod,@Drawamount,@Recoverablepercent,@Drawdificit)
	END
	ELSE
	BEGIN
		UPDATE ReportDetails SET TotalCommission=@Totalcommission,DrawPaid=@Drawpaid,DrawRecovered=@Drawrecoverable,
		CommissionDue=@Commissiondue,Salary=@Salary,BimonthlyBonus=@Bimonthreport,TenureBonus=@Tenurereport,TotalEarnings=@TotalEarning,
		DrawType=@Drawtype,ApplicableTill=@Applicableperiod,DrawAmount=@Drawamount,RecoverablePercentage=@Recoverablepercent,DrawDificit=@Drawdificit
		WHERE UID=@UID AND ReportPeriod=@PayrollConfigID
	END

UPDATE CommissionComponents SET ProcesByPayroll=1 WHERE Status=7 AND BranchID=@BranchID AND((CreatedBy=@UID AND SlipType=1)OR(TipLeadID=@UID AND SlipType=2)) 
 AND AccountPeriod=@PayrollConfigID AND IsActive=1
 
 /*update ProcessBypayroll8 for payroll config*/
  UPDATE PayrollConfig
    SET
  ProcessByPayroll = 
  (
            CASE
                WHEN
                    ID=@PayrollConfigID and IsActive =1
                THEN
                    '1'
                    
            END
        )
END
			
		IF (@@ERROR <> 0 )
			BEGIN 
				set @errMsg = 'Error: SaveReportDetails -> Unable to insert into ReportDetails table';
				GOTO CLEANUP
			END
		CLEANUP:
		IF @errMsg IS NULL 
			BEGIN
				IF @@TRANCOUNT > @beginTranCount 
					BEGIN   
						COMMIT TRANSACTION
						SET @RETURN_VALUE= 0
					END
			END
		ELSE 
			BEGIN
				IF @@TRANCOUNT > 0 
					ROLLBACK TRANSACTION   
					RAISERROR(@errMsg, 16, 1)
				SET @RETURN_VALUE= -1
			END
END
GO

/****** Object:  StoredProcedure [dbo].[GetDeactiveGMandPlanID]******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDeactiveGMandPlanID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDeactiveGMandPlanID]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDeactiveGMandPlanID]    
(
@RoleID INT,    
@UID INT,
@DeactivatedPlanID INT OUT,
@DeactivatedGM INT OUT 
)
AS
BEGIN 
IF @RoleID = 5 --Sales rep     
BEGIN   
 SET @DeactivatedPlanID=ISNULL((SELECT cc.PlanID from EmpComponents AS ec
	JOIN CPlanComponents AS cc
	ON ec.PayPlanID = cc.PlanID 
	WHERE UID = @UID AND cc.IsActive=0),0)

 SET @DeactivatedGM=ISNULL((SELECT UID FROM EmpComponents 
	WHERE UID = (SELECT ApproveMgr FROM EmpComponents WHERE UID=@UID)
	AND IsActive = 0),0)  
END    
ELSE IF @RoleID = 6  --Non-Sale Employee      
BEGIN 
  SET @DeactivatedPlanID=0  
  SET @DeactivatedGM=ISNULL((SELECT UID FROM EmpComponents 
	WHERE UID = (SELECT ApproveMgr FROM EmpComponents WHERE UID=@UID)
	AND IsActive = 0),0) 
END  
	RETURN @DeactivatedPlanID
	RETURN @DeactivatedGM 
END
GO 
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetBranches]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SetBranches]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SetBranches
@BranchID int = 0,
@BranchName nvarchar(255),
@BranchCode nvarchar(20),
@IsActive bit,
@AddedOn datetime = null,  
@AddedBy int = null,  
@ModifiedOn datetime = null,  
@ModifiedBy bit = null,  
@Result int Output
AS
BEGIN
			SET NOCOUNT ON;
			DECLARE @errMsg VARCHAR(255)
			DECLARE @beginTranCount INT
			SET @beginTranCount = @@TRANCOUNT			
			BEGIN TRANSACTION

IF EXISTS (SELECT * FROM Branches WHERE BranchID = @BranchID)
        BEGIN
            UPDATE Branches SET BranchName = @BranchName, BranchCode = @BranchCode, IsActive = @IsActive,
            ModifiedOn= @ModifiedOn, ModifiedBy = @ModifiedBy
            WHERE BranchID = @BranchID
        END
ELSE
        BEGIN
            INSERT INTO Branches (BranchName, BranchCode, IsActive, CreatedOn, CreatedBy) 
            VALUES (@BranchName, @BranchCode, @IsActive , @AddedOn, @AddedBy )
        END
IF (@@ERROR <> 0 )
    BEGIN 
		set @errMsg = 'Error: Unable to insert/update into [Branches] table';
		GOTO cleanup
	END
END
cleanup:
			IF @errMsg IS NULL 
				BEGIN
					IF @@TRANCOUNT > @beginTranCount 
						BEGIN   
							COMMIT TRANSACTION
							SET @Result = 0
							RETURN @Result
						END
				END
			ELSE 
				BEGIN
					IF @@TRANCOUNT > 0 
						ROLLBACK TRANSACTION   
						SET @Result = -1
					RETURN @Result
				END
GO
/****** Object:  StoredProcedure [dbo].[GetAllSaleType]   
-- Description:	Get All SaleType and return ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllSaleType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllSaleType]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllSaleType]
AS
BEGIN
SELECT    ISNULL(ID, '')    ID,     
           ISNULL(SalesTypeName, '')     SalesTypeName,
           ISNULL(IsNewCustomer, '')     IsNewCustomer,
           ISNULL(IsExistingCustomer, '')     IsExistingCustomer,
           ISNULL(IsBiMonthlyBonus, '')     IsBiMonthlyBonus,
           ISNULL(IsTenureBonus, '')     IsTenureBonus,
           ISNULL(IsActive, '')     IsActive  
    FROM  SaleType ORDER BY IsActive DESC,ID desc
END
GO
/****** Object:  StoredProcedure [dbo].[SetSaleType]   
-- Description:	Add/Update SaleType and return ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetSaleType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SetSaleType]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SetSaleType  
@ID int = 0,  
@SaleTypeName nvarchar(255),  
@IsNewCustomer bit,
@IsExistingCustomer bit,
@IsBiMonthlyBonus bit,
@IsTenureBonus bit,  
@IsActive bit,  
@AddedOn datetime = null,    
@AddedBy int = null,    
@ModifiedOn datetime = null,    
@ModifiedBy bit = null,    
@Result int Output  
AS  
BEGIN  
   SET NOCOUNT ON;  
   DECLARE @errMsg VARCHAR(255)  
   DECLARE @beginTranCount INT  
   SET @beginTranCount = @@TRANCOUNT     
   BEGIN TRANSACTION  
  
IF EXISTS (SELECT * FROM SaleType WHERE ID = @ID)  
        BEGIN  
            UPDATE SaleType SET SalesTypeName = @SaleTypeName, IsNewCustomer = @IsNewCustomer, IsExistingCustomer = @IsExistingCustomer, IsBiMonthlyBonus = @IsBiMonthlyBonus, IsTenureBonus= @IsTenureBonus, IsActive = @IsActive,  
            ModifiedOn= @ModifiedOn, ModifiedBy = @ModifiedBy  
            WHERE ID = @ID  
        END  
ELSE  
        BEGIN  
            INSERT INTO SaleType (SalesTypeName, IsNewCustomer, IsExistingCustomer, IsBiMonthlyBonus, IsTenureBonus,  IsActive, CreatedOn, CreatedBy)   
            VALUES (@SaleTypeName, @IsNewCustomer,@IsExistingCustomer,@IsBiMonthlyBonus,@IsTenureBonus , @IsActive , @AddedOn, @AddedBy )  
        END  
IF (@@ERROR <> 0 )  
    BEGIN   
  set @errMsg = 'Error: Unable to insert/update into [SaleType] table';  
  GOTO cleanup  
 END  
END  
cleanup:  
   IF @errMsg IS NULL   
    BEGIN  
     IF @@TRANCOUNT > @beginTranCount   
      BEGIN     
       COMMIT TRANSACTION  
       SET @Result = 0  
       RETURN @Result  
      END  
    END  
   ELSE   
    BEGIN  
     IF @@TRANCOUNT > 0   
      ROLLBACK TRANSACTION     
      SET @Result = -1  
     RETURN @Result  
    END  
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckDeactivateBranch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CheckDeactivateBranch]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckDeactivateBranch]    
(
@BranchID INT
)
AS
BEGIN 

SELECT * FROM EmpComponents as E inner join  CommissionComponents as C 
on E.PrimaryBranch = @BranchID  or  C.BranchID=@BranchID where E.IsActive = 1 or C.IsActive = 1 and C.ProcesByPayroll IS NULL 
 
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckDeactivateSaleType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CheckDeactivateSaleType]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[CheckDeactivateSaleType]    
(
@SaleType INT
)
AS
BEGIN 
	SELECT * FROM CommissionComponents WHERE SaleType = @SaleType AND  ProcesByPayroll IS NULL AND IsActive = 1; 
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllPayrollConfigDetailsByYear]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllPayrollConfigDetailsByYear]   
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetAllPayrollConfigDetailsByYear]   
(
@Year INT
) 
AS
BEGIN
SELECT	   ISNULL(pr.ID, '')    ID,
		   ISNULL(pr.Year, '')    Year,
		   ISNULL(pr.Period, '') Period,		
           ISNULL(pr.Month, '')     Month,
           ISNULL(DateTo,'')DateTo,
           ISNULL(DateFrom,'') DateFrom,
           ISNULL(pr.CreatedBy,'') CreatedBy,
           ISNULL(emp.FirstName+ ' ' + emp.LastName,'') CreatedByName,
           ISNULL(pr.CreatedOn,'')CreatedOn,
           ISNULL(pr.ModifiedBy,'') ModifiedBy,
           ISNULL(emp1.FirstName +' ' + emp1.LastName,'') ModifiedByName,
           ISNULL(pr.ModifiedOn,'')ModifiedOn,
           ISNULL(pr.IsActive, '')     IsActive,
           ISNULL(pr.ProcesByPayroll, '') ProcessByPayroll
    FROM  PayrollConfig AS pr 
    LEFT JOIN EmpComponents AS emp ON pr.CreatedBy=emp.UID
    LEFT JOIN EmpComponents AS emp1 ON pr.ModifiedBy=emp1.UID
    WHERE pr.Year = @Year and pr.IsActive = 1
    ORDER BY pr.Period asc
    
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllPayrollConfigDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllPayrollConfigDetails]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllPayrollConfigDetails]    
AS
BEGIN
  SELECT 
  ISNULL(YEAR,'')YEAR,
  ISNULL(A.CreatedBy,'')CreatedBy,
  ISNULL(A.ModifiedBy,'')ModifiedBy,
  ISNULL(A.CreatedOn,'')CreatedOn,
  ISNULL(emp.FirstName+ ' ' + emp.LastName,'') CreatedByName,
  ISNULL(emp1.FirstName +' ' + emp1.LastName,'') ModifiedByName
  FROM(
  SELECT *,ROW_NUMBER()OVER(PARTITION BY YEAR,CREATEDBY ORDER BY YEAR,CREATEDBY)RID FROM PayrollConfig
  )A 
  LEFT JOIN EmpComponents AS emp ON A.CreatedBy=emp.UID
  LEFT JOIN EmpComponents AS emp1 ON A.ModifiedBy=emp1.UID  WHERE RID=1
  ORDER BY YEAR DESC
END
GO
/****** Object:  StoredProcedure [dbo].[SetPayroll]   
-- Description:	Update Payroll and return ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetPayroll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SetPayroll]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SetPayroll  
@ID int = 0,  
@Year INT,  
@Period INT,
@Month VARCHAR(50),
@DateFrom DATETIME,
@DateTo DATETIME,  
@IsActive bit,     
@ModifiedOn datetime = null,    
@ModifiedBy INT= NULL,    
@Result int Output  
AS  
BEGIN  
   SET NOCOUNT ON;  
   DECLARE @errMsg VARCHAR(255)  
   DECLARE @beginTranCount INT  
   SET @beginTranCount = @@TRANCOUNT     
   BEGIN TRANSACTION   
       IF EXISTS (SELECT * FROM PayrollConfig WHERE ID = @ID )
       BEGIN  
            UPDATE PayrollConfig SET Year = @Year, Period = @Period, Month = @Month, DateFrom = @DateFrom, DateTo= @DateTo, IsActive = @IsActive,  
            ModifiedOn= @ModifiedOn, ModifiedBy = @ModifiedBy  
            WHERE ID = @ID  
        END 
		ELSE
        BEGIN
            INSERT INTO PayrollConfig ( Year,Period,Month,DateFrom,DateTo,IsActive,CreatedBy,CreatedOn)
			VALUES (@Year,@Period,@Month,@DateFrom,@DateTo,@IsActive,@ModifiedBy,@ModifiedOn) 
        END
         
END
IF (@@ERROR <> 0 )  
    BEGIN   
  set @errMsg = 'Error: Unable to update into [Payrollconfig] table';  
  GOTO cleanup  
 END   
cleanup:  
   IF @errMsg IS NULL   
    BEGIN  
     IF @@TRANCOUNT > @beginTranCount   
      BEGIN     
       COMMIT TRANSACTION  
       SET @Result = 0  
       RETURN @Result  
      END  
    END  
   ELSE   
    BEGIN  
     IF @@TRANCOUNT > 0   
      ROLLBACK TRANSACTION     
      SET @Result = -1  
     RETURN @Result  
    END  
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddPayroll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddPayroll]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE AddPayroll  
@ID int = 0,  
@Year INT,  
@Period INT,
@Month VARCHAR(50),
@DateFrom DATETIME,
@DateTo DATETIME,  
@IsActive bit,     
@AddedOn datetime = null,    
@AddedBy INT = null,    
@Result int Output  
AS  
BEGIN  
   SET NOCOUNT ON;  
   DECLARE @errMsg VARCHAR(255)  
   DECLARE @beginTranCount INT  
   SET @beginTranCount = @@TRANCOUNT     
   BEGIN TRANSACTION   
        BEGIN  
			INSERT INTO PayrollConfig ( Year,Period,Month,DateFrom,DateTo,IsActive,CreatedBy,CreatedOn)
			VALUES (@Year,@Period,@Month,@DateFrom,@DateTo,@IsActive,@AddedBy,@AddedOn) 
        END  
END
IF (@@ERROR <> 0 )  
    BEGIN   
  set @errMsg = 'Error: Unable to Insert into [Payrollconfig] table';  
  GOTO cleanup  
 END   
cleanup:  
   IF @errMsg IS NULL   
    BEGIN  
     IF @@TRANCOUNT > @beginTranCount   
      BEGIN     
       COMMIT TRANSACTION  
       SET @Result = 0  
       RETURN @Result  
      END  
    END  
   ELSE   
    BEGIN  
     IF @@TRANCOUNT > 0   
      ROLLBACK TRANSACTION     
      SET @Result = -1  
     RETURN @Result  
    END  
GO
/****** Object:  StoredProcedure [dbo].[GetAllSaleType]   
-- Description:	Get All SaleType and return ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAccountingMonth]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAccountingMonth]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAccountingMonth]
@CurrentDate Date
AS
BEGIN

SELECT ID,ISNULL(MONTH,'')MONTH FROM PayrollConfig 
WHERE CONVERT(date, @CurrentDate) between DateFrom and DateTo

END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPayrollConfig4Run]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPayrollConfig4Run]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPayrollConfig4Run]
@Year INT,
@Month INT
AS
BEGIN

SELECT ISNULL(ID,'')ID,
		ISNULL(DateFrom,'')DateFrom,
		ISNULL(DateTo,'')DateTo FROM PayrollConfig
		WHERE ((@Year * 100) + (@Month)) between year(DateTo) * 100 + month(DateTo) and
                     year(DateTo) * 100 + month(DateTo) ORDER BY DateTo ASC
END
GO
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPayrollConfig4TotEarning]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPayrollConfig4TotEarning]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPayrollConfig4TotEarning]
@Year INT,
@Month INT
AS
BEGIN

SELECT DateTo FROM(
SELECT MAX(DateTo) AS DateTo  from PayrollConfig 
WHERE year(DateTo) * 100 + month(DateTo) >= ((@Year * 100) + (@Month)) and
                     year(DateTo) * 100 + month(DateTo)  <= (((@Year + 1) * 100) + (@Month))
                     GROUP BY MONTH(DateTo))A
                     ORDER BY A.DateTo ASC
                     
END
GO