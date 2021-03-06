USE [nadis]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 19.03.2020 22:49:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KIDro] [nchar](7) NULL,
	[IDuser] [int] NOT NULL,
	[UserFullname] [nvarchar](50) NULL,
	[username] [nvarchar](50) NULL,
	[userpassword] [nchar](21) NULL,
	[Role] [nvarchar](50) NULL,
	[isMaster] [bit] NULL,
	[editORG] [bit] NULL,
	[editNADIS] [bit] NULL,
	[editVR] [bit] NULL,
	[editVC] [bit] NULL,
	[editTOOLS] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[nadis_Add_CtVet1a]    Script Date: 19.03.2020 22:49:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[nadis_Add_CtVet1a]
	-- Add the parameters for the stored procedure here
@KIDro nchar(7),
@repMO date,
@KIDdiv nchar(10),
@KIDspc nchar(4),
@KIDdis nchar(4),
@pos_units int,
@positives int,
@dead int,
@end_pos_units int,
@end_pos_animals int,
@culled int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO ctVET1a 
	(KIDro,repMO,KIDdiv,KIDspc,KIDdis,pos_units,positives,dead,end_pos_units,end_pos_animals,culled)
	VALUES
	(@KIDro,@repMO,@KIDdiv,@KIDspc,@KIDdis,@pos_units,@positives,@dead,@end_pos_units,@end_pos_animals,@culled)
END
GO
/****** Object:  StoredProcedure [dbo].[nadis_Add_CtVet1b]    Script Date: 19.03.2020 22:49:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[nadis_Add_CtVet1b]
	-- Add the parameters for the stored procedure here
@KIDro nchar(7),
@repMO date,
@KIDdiv nchar(10),
@KIDspc nchar(4),
@KIDdis nchar(4),
@test nchar(5),
@femage_1 int,
@femage_2 int,
@fage1_pos int,
@fage2_pos int,
@dtObs date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO ctVET1b 
	(KIDro,repMO,KIDdiv,KIDspc,KIDdis,test,femage_1,femage_2,fage1_pos,fage2_pos,dtObs)
	VALUES
	(@KIDro,@repMO,@KIDdiv,@KIDspc,@KIDdis,@test,@femage_1,@femage_2,@fage1_pos,@fage2_pos,@dtObs)
END
GO
/****** Object:  StoredProcedure [dbo].[nadis_CheckRecord_CtVet1a]    Script Date: 19.03.2020 22:49:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[nadis_CheckRecord_CtVet1a]
	-- Add the parameters for the stored procedure here
@repMO date,
@KIDdiv nchar(10),
@KIDspc nchar(4),
@KIDdis nchar(4),
@KIDro nchar(7)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(ID) as kolvo FROM ctVET1a
	WHERE (repMO = @repMO and KIDdiv=@KIDDiv and KIDspc=@KIDspc and KIDdis=@KIDdis and KIDro=@KIDro)
END
GO
/****** Object:  StoredProcedure [dbo].[nadis_CheckRecord_CtVet1b]    Script Date: 19.03.2020 22:49:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[nadis_CheckRecord_CtVet1b]
	-- Add the parameters for the stored procedure here
@repMO date,
@KIDdiv nchar(10),
@KIDspc nchar(4),
@KIDdis nchar(4),
@KIDro nchar(7),
@test nchar(5)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(ID) as kolvo FROM ctVET1b
	WHERE (repMO = @repMO and KIDdiv=@KIDDiv and KIDspc=@KIDspc and KIDdis=@KIDdis and KIDro=@KIDro and test=@test)
END
GO
/****** Object:  StoredProcedure [dbo].[nadis_Delete_CtVet1a]    Script Date: 19.03.2020 22:49:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[nadis_Delete_CtVet1a] 
	-- Add the parameters for the stored procedure here
@Id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM ctVET1a WHERE ID=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[nadis_Delete_CtVet1b]    Script Date: 19.03.2020 22:49:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[nadis_Delete_CtVet1b] 
	-- Add the parameters for the stored procedure here
@Id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM ctVET1b WHERE ID=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[nadis_GetAll_CtVet1a]    Script Date: 19.03.2020 22:49:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[nadis_GetAll_CtVet1a] 
	-- Add the parameters for the stored procedure here
@KIDro nchar(7)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ID,KIDro,repMO,
	KIDdiv,
	[73GEO3].Socunit as KIDdivDisplay,
	KIDspc,
	[d2SPECIES].Species as KIDspcDisplay,
	KIDdis,
	[d2DISEASES].Disease as KIDdisDisplay,
	pos_units,
	positives,
	dead,
	end_pos_units,
	end_pos_animals,
	culled
		
	FROM ctVET1a
	INNER JOIN [1VETUNITS]	ON ctVET1a.KIDro = [1VETUNITS].KID
	INNER JOIN [73GEO3]		ON ctVET1a.KIDdiv = [73GEO3].gAID
	INNER JOIN [d2SPECIES]	ON [d2SPECIES].KID = ctVET1a.KIDspc
	INNER JOIN [d2DISEASES]	ON [d2DISEASES].KID = ctVET1a.KIDdis
 	WHERE KIDro = @KIDro AND (YEAR(dbo.ctVET1a.repMO) = YEAR(GETDATE())) AND (MONTH(dbo.ctVET1a.repMO) IN (MONTH(GETDATE()), MONTH(GETDATE()) - 1))
	--WHERE KIDro = @KIDro AND (YEAR(dbo.ctVET1a.repMO) in (YEAR(dbo.ctVET1a.repMO), YEAR(dbo.ctVET1a.repMO)-1))
		and (pos_units is not null) and (positives is not null) and (dead is not null)and (end_pos_units is not null)
		and (end_pos_animals is not null) and (culled is not null)
	ORDER BY repMO DESC

END
GO
/****** Object:  StoredProcedure [dbo].[nadis_GetAll_CtVet1b]    Script Date: 19.03.2020 22:49:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[nadis_GetAll_CtVet1b] 
	-- Add the parameters for the stored procedure here
@KIDro nchar(7)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ID,KIDro,repMO,dtObs,
	[ctvet1b].KIDdiv as KIDdiv,
	[73GEO3].Socunit as KIDdivDisplay,
	[ctvet1b].KIDspc,
	[d2SPECIES].Species as KIDspcDisplay,
	[ctvet1b].KIDdis,
	[d2DISEASES].Disease as KIDdisDisplay,
	[ctvet1b].dtObs,
	[ctvet1b].test,
	d2TESTS.Testname as testDisplay,
	[ctvet1b].femage_1,
	[ctvet1b].femage_2,
	[ctvet1b].fage1_pos,
	[ctvet1b].fage2_pos
		
	FROM ctVET1b
		INNER JOIN [1VETUNITS]	ON ctVET1b.KIDro = [1VETUNITS].KID
		INNER JOIN [73GEO3]		ON ctVET1b.KIDdiv = [73GEO3].gAID
		INNER JOIN [d2SPECIES]	ON [d2SPECIES].KID = ctVET1b.KIDspc
		INNER JOIN [d2DISEASES]	ON [d2DISEASES].KID = ctVET1b.KIDdis
		INNER JOIN [d2TESTS] ON [d2TESTS].KID = ctVET1b.test
 	WHERE KIDro = @KIDro AND (YEAR(dbo.ctVET1b.repMO) = YEAR(GETDATE())) AND (MONTH(dbo.ctVET1b.repMO) IN (MONTH(GETDATE()), MONTH(GETDATE()) - 1))
	--WHERE KIDro = @KIDro AND (YEAR(dbo.ctVET1b.repMO) in (YEAR(dbo.ctVET1b.repMO), YEAR(dbo.ctVET1b.repMO)-1))
			and (dtObs is not null) and (repMO is not null) 
			and (femage_1 is not null) and (femage_2 is not null) and (fage1_pos is not null) and (fage2_pos is not null)
	ORDER BY repMO DESC

END
GO
/****** Object:  StoredProcedure [dbo].[nadis_GetByID_CtVet1a]    Script Date: 19.03.2020 22:49:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[nadis_GetByID_CtVet1a] 
	-- Add the parameters for the stored procedure here
@Id UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT ID,KIDro,repMO,
	KIDdiv,
	[73GEO3].Socunit as KIDdivDisplay,
	KIDspc,
	[d2SPECIES].Species as KIDspcDisplay,
	KIDdis,
	[d2DISEASES].Disease as KIDdisDisplay,
	pos_units,
	positives,
	dead,
	end_pos_units,
	end_pos_animals,
	culled
		
	FROM ctVET1a
	INNER JOIN [1VETUNITS]	ON ctVET1a.KIDro = [1VETUNITS].KID
	INNER JOIN [73GEO3]		ON ctVET1a.KIDdiv = [73GEO3].gAID
	INNER JOIN [d2SPECIES]	ON [d2SPECIES].KID = ctVET1a.KIDspc
	INNER JOIN [d2DISEASES]	ON [d2DISEASES].KID = ctVET1a.KIDdis
 	WHERE ID=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[nadis_GetByID_CtVet1b]    Script Date: 19.03.2020 22:49:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[nadis_GetByID_CtVet1b] 
	-- Add the parameters for the stored procedure here
@ID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ID,KIDro,repMO,ctVET1b.dtObs,
	KIDdiv,
	[73GEO3].Socunit as KIDdivDisplay,
	KIDspc,
	[d2SPECIES].Species as KIDspcDisplay,
	KIDdis,
	[d2DISEASES].Disease as KIDdisDisplay,
	test,
	[d2TESTS].Testname as testDisplay,
	femage_1,
	femage_2,
	fage1_pos,
	fage2_pos
		
	FROM ctVET1b
		INNER JOIN [1VETUNITS]	ON ctVET1b.KIDro = [1VETUNITS].KID
		INNER JOIN [73GEO3]		ON ctVET1b.KIDdiv = [73GEO3].gAID
		INNER JOIN [d2SPECIES]	ON [d2SPECIES].KID = ctVET1b.KIDspc
		INNER JOIN [d2DISEASES]	ON [d2DISEASES].KID = ctVET1b.KIDdis
		INNER JOIN [d2TESTS]	ON ctVET1b.test = d2TESTS.KID
 	--WHERE KIDro = @KIDro AND (YEAR(dbo.ctVET1a.repMO) = YEAR(GETDATE())) AND (MONTH(dbo.ctVET1a.repMO) IN (MONTH(GETDATE()), MONTH(GETDATE()) - 1))
	WHERE ID = @ID 
END
GO
/****** Object:  StoredProcedure [dbo].[nadis_Update_CtVet1a]    Script Date: 19.03.2020 22:49:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[nadis_Update_CtVet1a] 
	-- Add the parameters for the stored procedure here
@ID UNIQUEIDENTIFIER,
@KIDro nchar(7),
@repMO date,
@KIDdiv nchar(10),
@KIDspc nchar(4),
@KIDdis nchar(4),
@pos_units int,
@positives int,
@dead int,
@end_pos_units int,
@end_pos_animals int,
@culled int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE ctVET1a
	SET 
		KIDro=@KIDro,
		repMO=@repMO,
		KIDdiv=@KIDdiv,
		KIDspc=@KIDspc,
		KIDdis=@KIDdis,
		pos_units=@pos_units,
		positives=@positives,
		dead=@dead,
		end_pos_units=@end_pos_units,
		end_pos_animals=@end_pos_animals,
		culled=@culled
	WHERE ID=@ID
END
GO
/****** Object:  StoredProcedure [dbo].[nadis_Update_CtVet1b]    Script Date: 19.03.2020 22:49:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[nadis_Update_CtVet1b] 
	-- Add the parameters for the stored procedure here
@ID UNIQUEIDENTIFIER,
@KIDro nchar(7),
@repMO date,
@KIDdiv nchar(10),
@KIDspc nchar(4),
@KIDdis nchar(4),
@test nchar(5),
@dtObs date,
@femage_1 int,
@femage_2 int,
@fage1_pos int,
@fage2_pos int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE ctVET1b
	SET 
		KIDro=@KIDro,
		repMO=@repMO,
		KIDdiv=@KIDdiv,
		KIDspc=@KIDspc,
		KIDdis=@KIDdis,
		test = @test,
		dtObs = @dtObs,
		femage_1=@femage_1,
		femage_2=@femage_2,
		fage1_pos = @fage1_pos,
		fage2_pos = @fage2_pos
	WHERE ID=@ID
END
GO
