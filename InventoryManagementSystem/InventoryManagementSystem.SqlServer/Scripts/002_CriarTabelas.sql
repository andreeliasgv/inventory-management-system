--- 002_CriarTabelas.sql

USE InventoryManagementSystem;

GO

--- Tabela Produtos
CREATE TABLE dbo.Produtos (
	Id INT IDENTITY(1,1) NOT NULL,
	Codigo NVARCHAR(50) NOT NULL,
	Nome NVARCHAR(150) NOT NULL,
	Quantidade DECIMAL(18,3) NOT NULL,
	EstoqueMinimo DECIMAL (18,3) NOT NULL,
	Preco DECIMAL(18,2) NOT NULL CONSTRAINT DF_Produtos_Preco DEFAULT 0.00,
	DataCadastro DATETIME2(3) NOT NULL CONSTRAINT DF_Produtos_DataCadastro DEFAULT SYSDATETIME(),
	Status BIT NOT NULL CONSTRAINT DF_Produtos_Status DEFAULT 1, 

--- Regras
	CONSTRAINT PK_Produtos PRIMARY KEY (Id),
	CONSTRAINT UQ_Produtos_Codigo UNIQUE (Codigo),
	CONSTRAINT CK_Produtos_Quantidade CHECK (Quantidade >= 0),
	CONSTRAINT CK_Produtos_EstoqueMinimo CHECK (EstoqueMinimo >= 0),
	CONSTRAINT CK_Produtos_Preco CHECK (Preco >= 0)
);
	
GO

--- Tabela MovimentacoesEstoque
CREATE TABLE dbo.MovimentacoesEstoque (
	Id BIGINT IDENTITY(1,1) NOT NULL,
	IdProduto INT NOT NULL,
	Tipo CHAR(1) NOT NULL,
	Quantidade DECIMAL(18,3) NOT NULL,
	QuantidadeAnterior DECIMAL(18,3) NOT NULL,
	QuantidadePosterior DECIMAL(18,3) NOT NULL,
	DataHora DATETIME2(3) NOT NULL,
	Observacao NVARCHAR(300) NULL,

	CONSTRAINT PK_MovimentacoesEstoque PRIMARY KEY (Id),
	CONSTRAINT FK_MovimentacoesEstoque_Produtos FOREIGN KEY (IdProduto) REFERENCES dbo.Produtos(Id),
	CONSTRAINT CK_MovimentacoesEstoque_Tipo CHECK (Tipo IN ('E', 'S')),
	CONSTRAINT CK_MovimentacoesEstoque_Quantidade CHECK (Quantidade > 0),
	CONSTRAINT CK_MovimentacoesEstoque_Saldos CHECK (QuantidadeAnterior >= 0 AND QuantidadePosterior >= 0)
);

GO

CREATE INDEX IX_MovimentacoesEstoque_IdProduto_DataHora
ON dbo.MovimentacoesEstoque (IdProduto, DataHora, Id);

GO

CREATE INDEX IX_MovimentacoesEstoque_DataHora
ON dbo.MovimentacoesEstoque (DataHora, Id);

GO
