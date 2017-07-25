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
            VALUES (@SaleTypeName, @IsNewCustomer,@IsExistingCustomer,@IsBiMonthlyBonus,@IsTenureBonus , 1 , @AddedOn, @AddedBy )  
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