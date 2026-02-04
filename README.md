-----------------------DATABASE SCRIPTS-------------------------

***SCRIPT TO CREATE TABLES IN DATABASE******

CREATE TABLE tblCliente (
    clienteId INT IDENTITY PRIMARY KEY,
    nombre VARCHAR(100),
    apellidos VARCHAR(100),
    direccion VARCHAR(500),
    email VARCHAR(100),
    isActive BIT,
    isDeleted BIT,
    passwordHash VARCHAR(200)
);

CREATE TABLE tblTienda (
    tiendaId INT IDENTITY PRIMARY KEY,
    sucursal VARCHAR(100),
    direccion VARCHAR(500),
    isActive BIT,
    isDeleted BIT
);

CREATE TABLE tblArticulo (
    articuloId INT IDENTITY PRIMARY KEY,
    tiendaId INT,
    codigo VARCHAR(50),
    descripcion VARCHAR(500),
    precio VARCHAR(50),
    imagenUrl VARCHAR(MAX),
    stock INT,
    isActive BIT,
    isDeleted BIT,
    FOREIGN KEY (tiendaId) REFERENCES tbltienda(tiendaId),
);

CREATE TABLE tblClienteArticulo (
    clienteArticuloId INT IDENTITY PRIMARY KEY,
    clienteId INT,
    articuloId INT,
    cantidad INT,
    fecha DATETIME,
    isActive BIT,
    isDeleted BIT,
    FOREIGN KEY (clienteId) REFERENCES tblCliente(clienteId),
    FOREIGN KEY (articuloId) REFERENCES tblArticulo(articuloId)
);

CREATE TABLE tblArticuloTienda (
    articuloTiendaId INT IDENTITY PRIMARY KEY,
    articuloId INT,
    tiendaId INT,
    isActive BIT,
    isDeleted BIT,
    fecha DATETIME,
    FOREIGN KEY (articuloId) REFERENCES tblArticulo(articuloId),
    FOREIGN KEY (tiendaId) REFERENCES tblTienda(tiendaId)
);


*****STORED PROCEDURES ARE ON THE ATTACHED DOCUMENTS**********


STORED PROCEDDURES.winrar
