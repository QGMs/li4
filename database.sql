USE [CFDB]

CREATE TABLE [dbo].[Utilizador](
  [id] INT IDENTITY(1,1) NOT NULL,
  [nome] VARCHAR(45) NULL,
  [email] VARCHAR(45) NULL,
  [contacto] VARCHAR(45) NULL,
  [password] VARCHAR(45) NULL,
  [nascimento] DATE NULL,
  [morada] VARCHAR(45) NULL,
  PRIMARY KEY ([id]));



CREATE TABLE [dbo].[Stock] (
  [id] INT IDENTITY(1,1) NOT NULL,
  PRIMARY KEY ([id]));



CREATE TABLE [dbo].[LinhaMontagem] (
  [idLinha] INT IDENTITY(1,1) NOT NULL,
  [idStock] INT NOT NULL,
  PRIMARY KEY ([idLinha]),
    FOREIGN KEY ([idStock])
    REFERENCES [dbo].[Stock] ([id]));



CREATE TABLE [dbo].[Funcionario] (
  [id] INT NOT NULL,
  [NIF] INT NULL,
  [idLinha] INT NULL,
  PRIMARY KEY ([id]),

    FOREIGN KEY ([id])
    REFERENCES [dbo].[Utilizador] ([id]),

    FOREIGN KEY ([idLinha])
    REFERENCES [dbo].[LinhaMontagem] ([idLinha]));



CREATE TABLE [dbo].[Admin] (
  [id] INT NOT NULL,
  [NIF] INT NULL,
  PRIMARY KEY ([id]),
  FOREIGN KEY ([id])
    REFERENCES [dbo].[Utilizador] ([id]));



CREATE TABLE [dbo].[Componente] (
  [id] INT IDENTITY(1,1) NOT NULL,
  [designacao] VARCHAR(45) NULL,
  [tipo] VARCHAR(45) NULL,
  [tempoMontagem] INT NULL,
  [precoVenda] FLOAT NULL,
  PRIMARY KEY ([id]));



CREATE TABLE [dbo].[Fornecedor] (
  [id] VARCHAR(45) NOT NULL,
  [contacto] VARCHAR(45) NULL,
  [email] VARCHAR(45) NULL,
  [morada] VARCHAR(45) NULL,
  PRIMARY KEY ([id]));



CREATE TABLE [dbo].[ComponenteFornecedor] (
  [precoCompra] FLOAT NOT NULL,
  [idComponente] INT NOT NULL,
  [idFornecedor] VARCHAR(45) NOT NULL,
  PRIMARY KEY ([idComponente],[idFornecedor]),
    FOREIGN KEY ([idComponente])
    REFERENCES [dbo].[Componente] ([id]),
    FOREIGN KEY ([idFornecedor])
    REFERENCES [dbo].[Fornecedor] ([id]));



CREATE TABLE [dbo].[Produto] (
  [id] INT IDENTITY(1,1) NOT NULL,
  [precoVenda] FLOAT NULL,
  [precoFabrico] FLOAT NULL,
  [nome] VARCHAR(45) NULL,
  PRIMARY KEY ([id]));



CREATE TABLE [dbo].[ComponenteProduto] (
  [idProduto] INT NOT NULL,
  [idComponente] INT NOT NULL,
  [quantidade] INT NOT NULL,
  PRIMARY KEY ([idProduto], [idComponente]),

    FOREIGN KEY ([idProduto])
    REFERENCES [dbo].[Produto] ([id]),

    FOREIGN KEY ([idComponente])
    REFERENCES [dbo].[Componente] ([id]));

CREATE TABLE [dbo].[Encomenda] (
	[id] INT IDENTITY(1,1) NOT NULL,
  [estado] TINYINT NOT NULL,
  [idUtilizador] INT NOT NULL,
  [idLinhaMontagem] INT NULL, 
  PRIMARY KEY ([id]),
  
  FOREIGN KEY ([idUtilizador])
  REFERENCES [dbo].[Utilizador] ([id]),
  
  FOREIGN KEY ([idLinhaMontagem])
  REFERENCES [dbo].[LinhaMontagem] ([idLinha])
);


CREATE TABLE [dbo].[ProdutoEncomenda] (
  [idEncomenda] INT NOT NULL,
  [idProduto] INT NOT NULL,
  [quantidade] INT NOT NULL,
  PRIMARY KEY ([idEncomenda],[idProduto]),
  	FOREIGN KEY ([idEncomenda])
  	REFERENCES dbo.Encomenda ([id]),

    FOREIGN KEY ([idProduto])
    REFERENCES [dbo].[Produto] ([id])
);




CREATE TABLE [dbo].[LinhaAdmin] (
  [idAdmin] INT NOT NULL,
  [idLinha] INT NOT NULL,
  PRIMARY KEY ([idAdmin], [idLinha]),

    FOREIGN KEY ([idAdmin])
    REFERENCES [dbo].[Admin] ([id]),

    FOREIGN KEY ([idLinha])
    REFERENCES [dbo].[LinhaMontagem] ([idLinha])
);


CREATE TABLE [dbo].[StockComponente] (
  [idStock] INT NOT NULL,
  [idComponente] INT NOT NULL,
  [idFornecedor] VARCHAR(45) NOT NULL,
  [quantidade] INT NOT NULL,
  PRIMARY KEY ([idStock], [idComponente],[idFornecedor]),
    FOREIGN KEY ([idStock])
    REFERENCES [dbo].[Stock] ([id]),

    FOREIGN KEY ([idComponente] , [idFornecedor])
    REFERENCES [dbo].[ComponenteFornecedor] ([idComponente],[idFornecedor])
);
