IF NOT EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'[dbo].[PayrollConfig]') 
         AND name = 'ProcessByPayroll')
Begin
ALTER TABLE PayrollConfig
ADD ProcessByPayroll int;
End