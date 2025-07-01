IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'SinpeEmpresarialDB')
BEGIN
    CREATE DATABASE SinpeEmpresarialDB;
END
GO

USE SinpeEmpresarialDB;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comercios]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Comercios](
        [IdComercio] [int] IDENTITY(1,1) NOT NULL,
        [Identificacion] [varchar](50) NOT NULL,
        [TipoIdentificacion] [int] NOT NULL,
        [Nombre] [varchar](200) NOT NULL,
        [TipoDeComercio] [int] NOT NULL,
        [Telefono] [varchar](10) NOT NULL,
        [CorreoElectronico] [varchar](100) NOT NULL,
        [Direccion] [varchar](500) NOT NULL,
        [FechaDeRegistro] [datetime] NOT NULL,
        [FechaDeModificacion] [datetime] NULL,
        [Estado] [bit] NOT NULL,
        CONSTRAINT [PK_Comercios] PRIMARY KEY CLUSTERED ([IdComercio] ASC)
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cajas]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Cajas](
        [IdCaja] [int] IDENTITY(1,1) NOT NULL,
        [IdComercio] [int] NOT NULL,
        [Nombre] [varchar](200) NOT NULL,
        [Descripcion] [varchar](500) NOT NULL,
        [TelefonoSINPE] [varchar](10) NOT NULL,
        [FechaDeRegistro] [datetime] NOT NULL,
        [FechaDeModificacion] [datetime] NULL,
        [Estado] [bit] NOT NULL,
        CONSTRAINT [PK_Cajas] PRIMARY KEY CLUSTERED ([IdCaja] ASC),
        CONSTRAINT [FK_Cajas_Comercios] FOREIGN KEY([IdComercio]) REFERENCES [dbo].[Comercios] ([IdComercio])
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sinpes]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Sinpes](
        [IdSinpe] [int] IDENTITY(1,1) NOT NULL,
        [TelefonoOrigen] [varchar](10) NOT NULL,
        [NombreOrigen] [varchar](200) NOT NULL,
        [TelefonoDestino] [varchar](10) NOT NULL,
        [NombreDestino] [varchar](200) NOT NULL,
        [Monto] [decimal](18,2) NOT NULL,
        [Descripcion] [varchar](50) NULL,
        [FechaDeRegistro] [datetime] NOT NULL,
        [Estado] [bit] NOT NULL,
        CONSTRAINT [PK_Sinpes] PRIMARY KEY CLUSTERED ([IdSinpe] ASC)
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BITACORA_EVENTOS]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[BITACORA_EVENTOS](
        [IdEvento] [int] IDENTITY(1,1) NOT NULL,
        [TablaDeEvento] [varchar](100) NOT NULL,
        [TipoDeEvento] [varchar](50) NOT NULL,
        [FechaDeEvento] [datetime] NOT NULL,
        [DescripcionDeEvento] [varchar](500) NOT NULL,
        [StackTrace] [text] NULL,
        [DatosAnteriores] [text] NULL,
        [DatosPosteriores] [text] NULL,
        CONSTRAINT [PK_BITACORA_EVENTOS] PRIMARY KEY CLUSTERED ([IdEvento] ASC)
    );
END
GO

--aca insertamos datos de prueba para Comercio
IF NOT EXISTS (SELECT * FROM Comercios)
BEGIN
    INSERT INTO [dbo].[Comercios] ([Identificacion], [TipoIdentificacion], [Nombre], [TipoDeComercio], [Telefono], [CorreoElectronico], [Direccion], [FechaDeRegistro], [Estado])
    VALUES ('123456789', 1, 'Comercio de Prueba', 1, '88888888', 'comercio@test.com', 'San José, Costa Rica', GETDATE(), 1);
END
GO

--aca insertamos datos de prueba para Caja (y se ocupa para poder probar sinpe)
IF NOT EXISTS (SELECT * FROM Cajas)
BEGIN
    INSERT INTO [dbo].[Cajas] ([IdComercio], [Nombre], [Descripcion], [TelefonoSINPE], [FechaDeRegistro], [Estado])
    VALUES (1, 'Caja Principal', 'Caja principal para recibir pagos SINPE', '87654321', GETDATE(), 1);
    
    INSERT INTO [dbo].[Cajas] ([IdComercio], [Nombre], [Descripcion], [TelefonoSINPE], [FechaDeRegistro], [Estado])
    VALUES (1, 'Caja Inactiva', 'Caja inactiva para pruebas de validación', '87654322', GETDATE(), 0);
END
GO
