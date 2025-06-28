DROP DATABASE IF EXISTS SinpeEmpresarialDB;

CREATE DATABASE SinpeEmpresarialDB;
GO

USE SinpeEmpresarialDB;
GO

CREATE TABLE Comercio (
    IdComercio INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Identificacion VARCHAR(30) NOT NULL,
    TipoIdentificacion INT NOT NULL,
    Nombre VARCHAR(200) NOT NULL,
    TipoDeComercio INT NOT NULL,
    Telefono VARCHAR(20) NOT NULL,
    CorreoElectronico VARCHAR(200) NOT NULL,
    Direccion VARCHAR(500) NOT NULL,
    FechaDeRegistro DATETIME NOT NULL,
    FechaDeModificacion DATETIME NULL,
    Estado BIT NOT NULL
);

CREATE TABLE Caja (
    IdCaja INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    IdComercio INT NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(150) NOT NULL,
    TelefonoSINPE VARCHAR(10) NOT NULL,
    FechaDeRegistro DATETIME NOT NULL,
    FechaDeModificacion DATETIME NULL,
    Estado BIT NOT NULL,
    CONSTRAINT FK_Caja_Comercio FOREIGN KEY (IdComercio) REFERENCES Comercio(IdComercio)
);

CREATE TABLE Sinpe (
    IdSinpe INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    TelefonoOrigen VARCHAR(10) NOT NULL,
    NombreOrigen VARCHAR(200) NOT NULL,
    TelefonoDestinatario VARCHAR(10) NOT NULL,
    NombreDestinatario VARCHAR(200) NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    FechaDeRegistro DATETIME NOT NULL,
    Descripcion VARCHAR(50) NULL,
    Estado BIT NOT NULL
);

CREATE TABLE Bitacora (
    IdEvento INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    TablaDeEvento VARCHAR(20) NOT NULL,
    TipoDeEvento VARCHAR(20) NOT NULL,
    FechaDeEvento DATETIME NOT NULL,
    DescripcionDeEvento VARCHAR(MAX) NOT NULL,
    StackTrace VARCHAR(MAX) NOT NULL,
    DatosAnteriores VARCHAR(MAX) NULL,
    DatosPosteriores VARCHAR(MAX) NULL
);
