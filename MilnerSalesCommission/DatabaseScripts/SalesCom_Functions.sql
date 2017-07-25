USE [SalesComDB]
GO

/****** Object:  UserDefinedFunction [dbo].[GetBiMonthlyBonusReportFn]    Script Date: 02/10/2017 19:15:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBiMonthlyBonusReportFn]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetBiMonthlyBonusReportFn]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetBiMonthlyBonusReportFn]
(
	@PlanID  int,
	@Monthsofexp int,
	@UID int,
	@Month int,
	@Year int
)
RETURNS decimal(20,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Bimonthreport decimal(20,2)
	DECLARE @Fromdate datetime,@Todate datetime,@NetDV decimal(20,2),@Percentage int
	SET @Todate=(SELECT dateadd(month,((@Year-1900)*12)+@Month-1,0))
	SET @Fromdate=DATEADD(month,-1,@Todate)
	
IF(@Monthsofexp=1)
	BEGIN
		SELECT @NetDV=ISNULL(SUM(ISNULL(DollarVolume, '0.00')),0.00) FROM CommissionComponents 
		WHERE IsActive=1 AND Status=7 AND CreatedBy=1 AND  DATEADD(month, DATEDIFF(month, 0, AccountPeriod), 0)=DATEADD(month, DATEDIFF(month, 0, @Todate), 0)
	END
ELSE
	BEGIN
			SELECT @NetDV=ISNULL(SUM(ISNULL(DollarVolume, '0.00')),0.00) FROM CommissionComponents 
			WHERE IsActive=1 AND Status=7 AND CreatedBy=@UID AND  DATEADD(month, DATEDIFF(month, 0, AccountPeriod), 0) BETWEEN 
					  DATEADD(month, DATEDIFF(month, 0, @Fromdate), 0) AND
					  DATEADD(month, DATEDIFF(month, 0, @Todate), 0)
	END
IF(@Monthsofexp<=13)
	BEGIN
		SET @Percentage=(SELECT ISNULL(Percentage,0) AS Percentage FROM BIMonthlyBonusInfo WHERE PlanID=@PlanID AND Months='1-12'AND
		((EntryPointA <= @NetDV AND EntryPointB >= @NetDV) OR (EntryPointA <= @NetDV AND EntryPointB=0)))
	END
ELSE IF(@Monthsofexp>13)
	BEGIN
		SET @Percentage=(SELECT ISNULL(Percentage,0) AS Percentage FROM BIMonthlyBonusInfo WHERE PlanID=@PlanID AND Months='13+'AND
		((EntryPointA <= @NetDV AND EntryPointB >= @NetDV) OR (EntryPointA <= @NetDV AND EntryPointB=0)))
	END
SET @Bimonthreport=ISNULL(@NetDV*@Percentage,0)

RETURN @Bimonthreport
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetTenureBonusReportFn]    Script Date: 02/10/2017 19:15:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTenureBonusReportFn]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetTenureBonusReportFn]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetTenureBonusReportFn]
(
	@PlanID  int,
	@Monthsofexp int,
	@UID int,
	@Month int,
	@Year int
)
RETURNS decimal(20,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Tenurereport decimal(20,2)
	DECLARE @Fromdate datetime,@Todate datetime,@NetDV decimal(20,2),@Percentage int
	SET @Todate=(SELECT dateadd(month,((@Year-1900)*12)+@Month-1,0))
	SET @Fromdate=DATEADD(month,-2,@Todate)
	SELECT @NetDV=ISNULL(SUM(ISNULL(DollarVolume, '0.00')),0.00) FROM CommissionComponents 
		WHERE IsActive=1 AND Status=7 AND CreatedBy=@UID AND  DATEADD(month, DATEDIFF(month, 0, AccountPeriod), 0) BETWEEN 
			  DATEADD(month, DATEDIFF(month, 0, @Fromdate), 0) AND
			  DATEADD(month, DATEDIFF(month, 0, @Todate), 0)
IF(@Monthsofexp<13)
	BEGIN
		SET @Percentage=0
	END
ELSE IF(@Monthsofexp>=13 AND @Monthsofexp<=24)
	BEGIN
		SET @Percentage=(
		SELECT ISNULL(Percentage,0) AS Percentage FROM TenureBonus WHERE PlanID=@PlanID AND Months='13-24'AND
		EntryPointA<=@NetDV)
	END
ELSE IF(@Monthsofexp>=25 AND @Monthsofexp<=36)
	BEGIN
		IF EXISTS(SELECT Percentage AS Percentage FROM TenureBonus WHERE PlanID=@PlanID AND Months IN ('25-36') AND
		EntryPointA<=@NetDV)
			BEGIN
				SET @Percentage=(SELECT ISNULL(Percentage,0) AS Percentage FROM TenureBonus WHERE PlanID=@PlanID AND Months IN ('25-36') AND
				EntryPointA<=@NetDV)
			END
		ELSE
			BEGIN
				SET @Percentage=(SELECT ISNULL(Percentage,0) AS Percentage FROM TenureBonus WHERE PlanID=@PlanID AND Months='13-24'AND
				EntryPointA<=@NetDV)
			END
	END
ELSE IF(@Monthsofexp>=37)
	BEGIN
		IF EXISTS(SELECT Percentage AS Percentage FROM TenureBonus WHERE PlanID=@PlanID AND Months='37+'AND
		EntryPointA<=@NetDV)
			BEGIN
				SET @Percentage=(SELECT ISNULL(Percentage,0) AS Percentage FROM TenureBonus WHERE PlanID=@PlanID AND Months='37+'AND
				EntryPointA<=@NetDV)
			END
		ELSE IF EXISTS(select Percentage AS Percentage FROM TenureBonus WHERE PlanID=@PlanID AND Months='25-36'AND
			EntryPointA<=@NetDV)
			BEGIN
				SET @Percentage=(SELECT ISNULL(Percentage,0) AS Percentage FROM TenureBonus WHERE PlanID=@PlanID AND Months='25-36'AND
				EntryPointA<=@NetDV)
			END
		ELSE
			BEGIN
				SET @Percentage=(SELECT ISNULL(Percentage,0) AS Percentage FROM TenureBonus WHERE PlanID=@PlanID AND Months='13-24'AND
				EntryPointA<=@NetDV)
			END
	END
SET @Tenurereport=ISNULL(@NetDV*@Percentage,0)
RETURN @Tenurereport
END

GO


