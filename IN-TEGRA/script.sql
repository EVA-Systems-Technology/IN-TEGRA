create database IntegraDB;

use IntegraDB;

-- TABELA DO cliente (SE EU SOFRER COM ALGO VAI FICAR ANOTADO AQUI):

create table tbCliente(
	CpfCli char(11) primary key,
    NomeCli varchar(250) not null,
    EmailCli varchar(256) unique not null,
    TelefoneCli varchar(11),
    SenhaCli varchar(100) not null,
    NascCli date not null
);

-- TABELA DO FUNC (SE EU SOFRER COM ALGO VAI FICAR ANOTADO AQUI):

create table tbFuncionario(
	IdFunc int primary key auto_increment,
    NomeFunc varchar(250) not null,
    EmailFunc varchar(256) unique not null,
    SenhaFunc varchar(100) not null,
    CpfFunc char(11) unique not null
);

-- TABELA DO LOGIN (SE EU SOFRER COM ALGO VAI FICAR ANOTADO AQUI):

create table tbLogin(
	IdLogin int primary key auto_increment,
	IdFunc int,
    CpfCli char(11),
    TipoLogin varchar(11),
    DataLogin datetime not null default current_timestamp,
    foreign key (IdFunc) references tbFuncionario(IdFunc),
    foreign key (CpfCli) references tbCliente(CpfCli)
);

-- TABELA DO PRODUTO (SE EU SOFRER COM ALGO VAI FICAR ANOTADO AQUI):

/* talvez fazer o nome ser unique para nao criar dois produtos iguais?
   ve ai e me fala o que acha lmao :P
*/

/* outra coisa, na teoria a gente nao teria que fazer uma tabela prodimg?
   pq ne, cada produto pode ter mais de uma imagem e pipipopop penes
   mas nao sei to matutando sobre isso, vou fazer e deixar comentado ai tu pensa sobre i guess :3
*/

/*
create table ImgProd(
	IdImg int primary key auto_increment,
    IdProd int not null,
    CaminhoImg varchar(255) not null,
    foreign key (IdProd) references Produto(IdProd)
);

*/

create table tbProduto(
	IdProd int primary key auto_increment,
    NomeProd varchar(150) unique not null,
    DescProd varchar (700) not null,
    ImgProd varchar(700) not null,
    PrecoProd decimal(10,2) not null
);

-- ESQUECI O ESTOQUE ANTES VAI FICAR AQ 

create table tbEstoque(
	IdEstoque int primary key auto_increment,
    IdProd int not null,
    QtdEstoque int not null,
    foreign key (IdProd) references tbProduto(IdProd)
);


-- TABELA DO carrinho (SE EU SOFRER COM ALGO VAI FICAR ANOTADO AQUI):

create table tbCarrinho (
	IdCarrinho int primary key auto_increment,
    CpfCli char(11) not null,
    ValorCarrinho decimal (10,2) default 0,
    foreign key (CpfCli) references tbCliente(CpfCli)
);

-- tabela do itemcarrinho blablabla vc ja sabe

create table tbItemCarrinho (
	IdCarrinho int not null,
    IdProd int not null,
    QtdItemCarrinho int not null check (QtdItemCarrinho > 0),
    primary key (IdCarrinho, IdProd),
    foreign key (IdCarrinho) references tbCarrinho(IdCarrinho),
    foreign key (IdProd) references tbProduto(IdProd)
);

-- tabela do itemcarrinho blablabla vc ja sabe

create table tbPedido (
	IdPedido int primary key auto_increment,
    CpfCli char(11) not null,
    IdCarrinho int not null,
    FretePedido decimal (10,2) not null default 0,
    ValorPedido decimal(10,2) not null,
    ConfirmacaoPedido boolean not null default false,
    foreign key (CpfCli) references tbCliente(CpfCli),
    foreign key (IdCarrinho) references tbCarrinho(IdCarrinho)
);

-- ITEM PEDIDO EH META I GUESS? pelo que eu tinha falado com o enildon lmao

create table tbItemPedido(
	IdPedido int not null,
    IdProd int not null,
    QtdItemPedido int not null check (QtdItemPedido > 0),
    PrecoItemPedido decimal(10,2) not null,
    primary key (IdPedido, IdProd),
    foreign key (IdPedido) references tbPedido(IdPedido),
    foreign key (IdProd) references tbProduto(IdProd)
);

-- pagamento !

create table tbPagamento (
	IdPagamento int primary key auto_increment,
    IdPedido int not null,
    ValorPagamento decimal (10,2) not null,
    DataHoraPagamento datetime null default current_timestamp ,
    TipoPagamento varchar(50) null,
    ConfirmacaoPagamento boolean default false,
    NfPagamento varchar(100) unique not null,
    foreign key (IdPedido) references tbPedido(IdPedido)
);

select * from tbProduto;

insert into tbProduto(NomeProd, DescProd, ImgProd, PrecoProd) values ("Cadeira de rodas xlr8", "Uma cadeira de rodas baseada na coloração do alien XLR8 do Ben 10", "image/cadeira.png", 1234.99);

insert into tbProduto(NomeProd, DescProd, ImgProd, PrecoProd) values ("Garfo Adaptado", "um garfo adaptado para facilitar a praticidade de pessoas com deficiencias motoras ao se alimentarem! ", "image/garfo.png", 78.99);

insert into tbProduto(NomeProd, DescProd, ImgProd, PrecoProd) values ("Muletas 'The root'", "Par de muletas com tamanho ajustavel! ", "image/garfo.png", 83.99)