-- Copyright 2016-2017, Milner Technologies, Inc.
--
-- This document contains data and information proprietary to
-- Milner Technologies, Inc.  This data shall not be disclosed,
-- disseminated, reproduced or otherwise used outside of the
-- facilities of Milner Technologies, Inc., without the express
-- written consent of an officer of the corporation.

USE [SalesComDB]
GO

IF NOT EXISTS (SELECT * FROM [Roles])
BEGIN

	DECLARE @AdminID int
	DECLARE @SalesManagerID int 
	DECLARE @GeneralManagerID int
	DECLARE @PayRollID int 
	DECLARE @EmployeeID int
	DECLARE @NSE int

	-- Add the Administrator Role and Operations
	INSERT INTO [Roles] (Name) VALUES ('Administrator')
	SELECT @AdminID = [RoleID] FROM [Roles] WHERE ( [Name] = 'Administrator' ) 

	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@AdminID, 1007) -- OP_PAYPLANS
	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@AdminID, 1008) -- OP_OMG_BUTTON

	-- Add the Sales Manager Role and Operations
	INSERT INTO [Roles] (Name) VALUES ('Sales Manager')
	SELECT @SalesManagerID = [RoleID] FROM [Roles] WHERE ( [Name] = 'Sales Manager' )

	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@SalesManagerID, 1003) -- OP_REVIEW

	-- Add the General Manager Role and Operations
	INSERT INTO [Roles] (Name) VALUES ('General Manager')
	SELECT @GeneralManagerID = [RoleID] FROM [Roles] WHERE ( [Name] = 'General Manager' )

	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@GeneralManagerID, 1002) -- OP_REVIEW
	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@GeneralManagerID, 1004) -- OP_APPROVAL
	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@GeneralManagerID, 1003) -- OP_CORRECTION
	
	-- Add the PayRoll Role and Operations
	INSERT INTO [Roles] (Name) VALUES ('PayRoll')
	SELECT @PayRollID = [RoleID] FROM [Roles] WHERE ( [Name] = 'PayRoll' )
	
	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@PayRollID, 1002) -- OP_REVIEW
	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@PayRollID, 1003) -- OP_CORRECTION
	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@PayRollID, 1004) -- OP_APPROVAL
	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@PayRollID, 1005) -- OP_EMPLOYEE_SETUP
	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@PayRollID, 1006) -- OP_EDITS
	
	-- Add the Sales Rep Role and Operations
	INSERT INTO [Roles] (Name) VALUES ('Sales Rep')
	SELECT @EmployeeID = [RoleID] FROM [Roles] WHERE ( [Name] = 'Sales Rep' )

	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@EmployeeID, 1001) -- OP_INPUT
	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@EmployeeID, 1002) -- OP_REVIEW
	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@EmployeeID, 1003) -- OP_CORRECTION
	
	-- Add the Sales Rep Role and Operations
	INSERT INTO [Roles] (Name) VALUES ('Non-Sales Employee')
	SELECT @NSE = [RoleID] FROM [Roles] WHERE ( [Name] = 'Non-Sales Employee' )

	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@NSE, 1001) -- OP_INPUT
	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@NSE, 1002) -- OP_REVIEW
	INSERT INTO [RoleOperations] (RoleID, OperationID) VALUES (@NSE, 1003) -- OP_CORRECTION
END
IF NOT EXISTS (SELECT * FROM [Operations])
BEGIN
--Add Operation details in Operation table
	INSERT INTO [Operations] (OperationID, OperationName, Description) VALUES (1001,'Input', 'Input Operation')
	INSERT INTO [Operations] (OperationID, OperationName, Description) VALUES (1002,'Review', 'Review Operation')
	INSERT INTO [Operations] (OperationID, OperationName, Description) VALUES (1003,'Correction', 'Correction Operation')
	INSERT INTO [Operations] (OperationID, OperationName, Description) VALUES (1004,'Approval', 'Approval Operation')
	INSERT INTO [Operations] (OperationID, OperationName, Description) VALUES (1005,'Employee_Setup', 'Employee_Setup Operation')
	INSERT INTO [Operations] (OperationID, OperationName, Description) VALUES (1006,'Edits', 'Edits Operation')
	INSERT INTO [Operations] (OperationID, OperationName, Description) VALUES (1007,'Payplans', 'Payplans Operation')
	INSERT INTO [Operations] (OperationID, OperationName, Description) VALUES (1008,'OMG_Button', 'OMG_Button Operation')
END