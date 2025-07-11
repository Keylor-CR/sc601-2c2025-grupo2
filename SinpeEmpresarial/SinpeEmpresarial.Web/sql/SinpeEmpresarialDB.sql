DROP DATABASE IF EXISTS SinpeEmpresarialDB;

CREATE DATABASE SinpeEmpresarialDB;
GO

USE SinpeEmpresarialDB;
GO

CREATE TABLE COMERCIOS (
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

CREATE TABLE CAJAS (
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

CREATE TABLE SINPES (
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

CREATE TABLE CONFIGURACIONES_COMERCIOS (
    IdConfiguracion INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    IdComercio INT NOT NULL,
    TipoConfiguracion INT NOT NULL,
    Comision INT NOT NULL,
    FechaDeRegistro DATETIME NOT NULL,
    FechaDeModificacion DATETIME NULL,
    Estado BIT NOT NULL,
	CONSTRAINT FK_Configuraciones_Comercio_Comercio FOREIGN KEY (IdComercio) REFERENCES COMERCIOS(IdComercio)
);


INSERT INTO COMERCIOS (Identificacion, TipoIdentificacion, Nombre, TipoDeComercio, Telefono, CorreoElectronico, Direccion, FechaDeRegistro, Estado)
VALUES 
('1-1234-3432', 1, 'Soda La Amistad', 1, '2222-1234', 'soda.amistad@email.com', 'Heredia', GETDATE(), 1),
('3-333-555555', 2, 'Soda Mary', 2, '2288-7766', 'admin@sodamary.cr', 'Heredi', GETDATE(), 1),
('1-1111-2222', 1, 'Ferreteria Brenes', 3, '2299-8899', 'ventas@ferrebrennes.com', 'Heredia', GETDATE(), 1);


INSERT INTO CAJAS (IdComercio, Nombre, Descripcion, TelefonoSINPE, FechaDeRegistro, Estado)
VALUES 
(1, 'Caja Principal', 'Caja central para atención general', '88881234', GETDATE(), 1),
(2, 'Caja 1', 'Caja secundaria en Soda Mary', '88884567', GETDATE(), 1),
(3, 'Caja de herramientas', 'Caja exclusiva para ventas de ferretería', '88889876', GETDATE(), 1);

-- Sinpe to Caja 1 (TelefonoDestinatario = '88881234')
INSERT INTO SINPES (TelefonoOrigen, NombreOrigen, TelefonoDestino, NombreDestino, Monto, FechaDeRegistro, Descripcion, Estado)
VALUES 
('70001111', 'Cliente A', '88881234', 'Caja Principal', 1500.00, GETDATE(), 'Pago desayuno', 1),
('70002222', 'Cliente B', '88881234', 'Caja Principal', 2750.00, GETDATE(), 'Pago almuerzo', 1),
('70003333', 'Cliente C', '88881234', 'Caja Principal', 3990.00, GETDATE(), 'Pago cena', 1);

-- Sinpe to Caja 2 (TelefonoDestinatario = '88884567')
INSERT INTO SINPES (TelefonoOrigen, NombreOrigen, TelefonoDestino, NombreDestino, Monto, FechaDeRegistro, Descripcion, Estado)
VALUES 
('70004444', 'Cliente D', '88884567', 'Caja 1', 1850.00, GETDATE(), 'Refresco', 1),
('70005555', 'Cliente E', '88884567', 'Caja 1', 3050.00, GETDATE(), 'Combo especial', 1),
('70006666', 'Cliente F', '88884567', 'Caja 1', 2200.00, GETDATE(), 'Postre', 1);

-- Sinpe to Caja 3 (TelefonoDestinatario = '88889876')
INSERT INTO SINPES (TelefonoOrigen, NombreOrigen, TelefonoDestino, NombreDestino, Monto, FechaDeRegistro, Descripcion, Estado)
VALUES 
('70007777', 'Cliente G', '88889876', 'Caja de herramientas', 10400.00, GETDATE(), 'Compra taladro', 1),
('70008888', 'Cliente H', '88889876', 'Caja de herramientas', 8450.00, GETDATE(), 'Compra tornillos', 1),
('70009999', 'Cliente I', '88889876', 'Caja de herramientas', 13200.00, GETDATE(), 'Compra combo herramientas', 1);