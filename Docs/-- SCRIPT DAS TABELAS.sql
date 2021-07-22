-- SCRIPT DAS TABELAS

--criação da tabela de usuários

CREATE TABLE ENDERECO(

ID  uniqueIdentifier NOT NULL PRIMARY KEY,
CEP VARCHAR(8) NOT NULL,
RUA VARCHAR(100) NOT NULL,
BAIRRO VARCHAR(50) NOT NULL,
CIDADE VARCHAR(50) NOT NULL,
ESTADO VARCHAR(50) NOT NULL,
DATA_CADASTRO DATE NOT NULL
);

CREATE TABLE USUARIO(

ID  uniqueIdentifier NOT NULL PRIMARY KEY,
ID_ENDERECO uniqueIdentifier not null,
NOME VARCHAR(50) NOT NULL,
SOBRENOME VARCHAR(50) NOT NULL,
EMAIL VARCHAR(50) NOT NULL,
SENHA VARCHAR(250) NOT NULL,
DATA_CADASTRO DATE NOT NULL,
FOREIGN KEY (ID_ENDERECO) REFERENCES ENDERECO(ID)
);

CREATE TABLE CATEGORIA(
ID  uniqueIdentifier NOT NULL PRIMARY KEY,
NOME VARCHAR(50) NOT NULL,
DESCRICAO VARCHAR(50) NOT NULL,
DATA_CADASTRO DATE NOT NULL
);

CREATE TABLE PRODUTO(
ID  uniqueIdentifier NOT NULL PRIMARY KEY,
ID_CATEGORIA uniqueIdentifier NOT NULL,
ID_USUARIO uniqueIdentifier NOT NULL,
NOME VARCHAR(50) NOT NULL,
DESCRICAO VARCHAR(50) NOT NULL,
PRECO DECIMAL(19,4) NOT NULL,
QUANTIDADE INT NOT NULL,
DATA_CADASTRO DATE NOT NULL,
FOREIGN KEY (ID_CATEGORIA) REFERENCES CATEGORIA(ID)
FOREIGN KEY (ID_USUARIO) REFERENCES USUARIO(ID)
); 