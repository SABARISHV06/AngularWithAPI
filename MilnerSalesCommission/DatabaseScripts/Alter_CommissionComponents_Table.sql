IF EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[CommissionComponents]') 
         AND name = 'AccountPeriod')
BEGIN
ALTER TABLE CommissionComponents 
DROP COLUMN AccountPeriod  

ALTER TABLE CommissionComponents 
ADD AccountPeriod INT 
END

IF EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[ReportDetails]') 
         AND name = 'ReportPeriod')
BEGIN
ALTER TABLE ReportDetails 
DROP COLUMN ReportPeriod  

ALTER TABLE ReportDetails 
ADD ReportPeriod INT 
END