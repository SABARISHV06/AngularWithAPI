/****** Object:  StoredProcedure [dbo].[SetBranches]    */
-- =====================================================
-- Author: Sabarish
-- Create Date: 04-05-2017
-- Description:	Insert/Update Branches and return Message
-- =====================================================
/****** Object:  StoredProcedure [dbo].[SetBranches]******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetBranches]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SetBranches]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SetBranches
@BranchID int,
@BranchName nvarchar(255),
@BranchCode nvarchar(20),
@IsActive bit,
@AddedOn datetime,
@AddedBy int,
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

IF EXISTS (SELECT * FROM Branches WHERE BranchID = @BranchID)
        BEGIN
            UPDATE Branches SET BranchName = @BranchName, BranchCode = @BranchCode, IsActive = @IsActive,
            ModifiedOn= @ModifiedOn, ModifiedBy = @ModifiedBy
            WHERE BranchID = @BranchID
        END
ELSE
        BEGIN
            INSERT INTO Branches (BranchName, BranchCode, IsActive, CreatedOn, CreatedBy) 
            VALUES (@BranchName, @BranchCode, @IsActive, @AddedOn, @AddedBy )
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
