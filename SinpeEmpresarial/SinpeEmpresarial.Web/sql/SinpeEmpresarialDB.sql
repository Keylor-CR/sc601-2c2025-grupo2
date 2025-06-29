DROP DATABASE IF EXISTS SinpeEmpresarialDB;

CREATE DATABASE SinpeEmpresarialDB;
GO

USE SinpeEmpresarialDB;
GO

CREATE TABLE Comercios (
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

CREATE TABLE Cajas (
    IdCaja INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    IdComercio INT NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(150) NOT NULL,
    TelefonoSINPE VARCHAR(10) NOT NULL,
    FechaDeRegistro DATETIME NOT NULL,
    FechaDeModificacion DATETIME NULL,
    Estado BIT NOT NULL,
    CONSTRAINT FK_Caja_Comercio FOREIGN KEY (IdComercio) REFERENCES Comercios(IdComercio)
);

CREATE TABLE Sinpes (
    IdSinpe INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    TelefonoOrigen VARCHAR(10) NOT NULL,
    NombreOrigen VARCHAR(200) NOT NULL,
    TelefonoDestino VARCHAR(10) NOT NULL,
    NombreDestino VARCHAR(200) NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    FechaDeRegistro DATETIME NOT NULL,
    Descripcion VARCHAR(50) NULL,
    Estado BIT NOT NULL
);

CREATE TABLE BITACORA_EVENTOS (
    IdEvento INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    TablaDeEvento VARCHAR(20) NOT NULL,
    TipoDeEvento VARCHAR(20) NOT NULL,
    FechaDeEvento DATETIME NOT NULL,
    DescripcionDeEvento VARCHAR(MAX) NOT NULL,
    StackTrace VARCHAR(MAX) NOT NULL,
    DatosAnteriores VARCHAR(MAX) NULL,
    DatosPosteriores VARCHAR(MAX) NULL
);

INSERT INTO Comercios (Identificacion, TipoIdentificacion, Nombre, TipoDeComercio, Telefono, CorreoElectronico, Direccion, FechaDeRegistro, Estado)
VALUES 
('1-1234-3432', 1, 'Soda La Amistad', 1, '2222-1234', 'soda.amistad@email.com', 'Heredia', GETDATE(), 1),
('3-333-555555', 2, 'Soda Mary', 2, '2288-7766', 'admin@sodamary.cr', 'Heredi', GETDATE(), 1),
('1-1111-2222', 1, 'Ferreteria Brenes', 3, '2299-8899', 'ventas@ferrebrennes.com', 'Heredia', GETDATE(), 1);

select * from Comercios