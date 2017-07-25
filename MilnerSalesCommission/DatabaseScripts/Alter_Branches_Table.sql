-- =====================================================
-- Author: Sabarish
-- Create Date: 04-05-2017
-- Description:	ALTER Branches table to add a new column
-- =====================================================
IF NOT EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Branches]') 
         AND name = 'BranchCode')
BEGIN
ALTER TABLE Branches
ADD BranchCode NVARCHAR(20) NOT NULL DEFAULT '0';
END

IF NOT EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Branches]') 
         AND name = 'IsActive')
BEGIN
ALTER TABLE Branches
ADD IsActive Bit;
END

IF NOT EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Branches]') 
         AND name = 'CreatedOn')
BEGIN
ALTER TABLE Branches
ADD CreatedOn datetime;
END

IF NOT EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Branches]') 
         AND name = 'CreatedBy')
BEGIN
ALTER TABLE Branches
ADD CreatedBy Bit;
END

IF NOT EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Branches]') 
         AND name = 'ModifiedOn ')
BEGIN
ALTER TABLE Branches
ADD ModifiedOn datetime;
END

IF NOT EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[Branches]') 
         AND name = 'ModifiedBy')
BEGIN
ALTER TABLE Branches
ADD ModifiedBy Bit;
END
