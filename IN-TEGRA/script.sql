create database IntegraDB;
use IntegraDB;
drop database IntegraDB;


-- TABELA DO cliente (SE EU SOFRER COM ALGO VAI FICAR ANOTADO AQUI):

create table tbCliente(
	IdCli int primary key auto_increment,
	CpfCli decimal(11,0) not null,
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
    CpfFunc decimal(11,0) unique not null,
    TipoFunc varchar(8) not null
);

-- TABELA DO LOGIN (SE EU SOFRER COM ALGO VAI FICAR ANOTADO AQUI):

create table tbLogin(
	IdLogin int primary key auto_increment,
	IdFunc int,
    IdCli int,
    TipoLogin varchar(11),
    DataLogin datetime not null default current_timestamp,
    foreign key (IdFunc) references tbFuncionario(IdFunc),
    foreign key (IdCli) references tbCliente(IdCli)
);


-- trigger de quando fizer um novo registro/login ele registra o login



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
    PrecoProd decimal(10,2) not null,
    QtdProd int not null,
    CategoriaProd varchar(150)  not null
);

create table tbUF (
	IdUF int primary key auto_increment,
    SiglaUF char(2) not null
);

create table tbcidade (
	IdCidade int primary key auto_increment,
    NomeCidade	varchar(60) not null
);
    
create table tbEndereco (
	IdEndereco int primary key auto_increment,
    CepEndereco Char(8) not null,
    NumEndereco int not null,
    LogradouroEndereco varchar(60) not null,
    ComplementoEndereco varchar(100),
    IdCidade int not null,
    IdUF int not null,
    foreign key (IdCidade) references tbCidade(IdCidade),
    foreign key (IdUF) references tbUF(IdUF)
);



-- tabela do itemcarrinho blablabla vc ja sabe

create table tbPedido (
	IdPedido int primary key auto_increment,
	IdCli int not null,
    FretePedido decimal(10,2) not null default 0,
    ValorPedido decimal(10,2) not null,
	DataHoraPedido datetime ,
    ConfirmacaoPedido boolean not null default false,				
	IdEndereco int,
    foreign key (IdCli) references tbCliente(IdCli),
    foreign key (IdEndereco) references tbEndereco(IdEndereco)
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
    idCli int not null,
    ValorPagamento decimal (10,2) not null,
    DataHoraPagamento datetime null default current_timestamp ,
    TipoPagamento varchar(50) null,
    foreign key (IdPedido) references tbPedido(IdPedido),
    foreign key (IdCli) references tbCliente(IdCli)
);


select * from tbcliente;
select * from tbproduto;
select * from tbfuncionario;


SELECT * FROM tbpedido;
insert into tbfuncionario (nomefunc, emailfunc, senhafunc, cpffunc, tipofunc) values ("admin", "admin@admin.com", "123", 12333, "G");

insert into tbProduto(NomeProd, DescProd, ImgProd, PrecoProd) values ("Cadeira de rodas xlr8", "Uma cadeira de rodas baseada na coloração do alien XLR8 do Ben 10", "image/cadeira.png", 1234.99);

insert into tbProduto(NomeProd, DescProd, ImgProd, PrecoProd) values ("Garfo Adaptado", "um garfo adaptado para facilitar a praticidade de pessoas com deficiencias motoras ao se alimentarem! ", "image/garfo.png", 78.99);

insert into tbProduto(NomeProd, DescProd, ImgProd, PrecoProd) values ("Muletas 'The root'", "Par de muletas com tamanho ajustavel! ", "image/garfo.png", 83.99);



INSERT INTO tbProduto(NomeProd, DescProd, ImgProd, PrecoProd, QtdProd, CategoriaProd)
VALUES
("Abridor de Potes Elétrico", "Abre potes e tampas de garrafa com um único toque, ideal para quem tem artrite ou força limitada nas mãos.", "image/abridor_potes.png", 189.90, 30, "Acessórios"),
("Tábua de Corte Adaptada", "Tábua com pinos de inox e borda elevada, permite fixar alimentos para cortar usando apenas uma mão.", "image/tabua_adaptada.png", 139.90, 25, "Acessórios"),
("Calçador de Meias", "Dispositivo curvo que ajuda a vestir meias e meias de compressão sem precisar se curvar ou levantar as pernas.", "image/calcador_meias.png", 45.00, 50, "Acessórios"),
("Pinça Pegadora Dobrável 80cm", "Braço extensor leve com gatilho para pegar objetos em locais altos ou no chão, evitando quedas.", "image/pinca_alcancador.png", 65.00, 75, "Acessórios"),
("Campainha sem Fio com Alerta Visual", "Campainha com receptor portátil que pisca luzes de LED e vibra. Ideal para deficiência auditiva.", "image/campainha_luz.png", 175.00, 30, "Audição"),
("Botão Comunicador de Voz Gravável", "Botão de fácil pressão que grava e reproduz uma mensagem de 30 segundos. Para comunicação alternativa (AAC).", "image/botao_comunicador.png", 250.00, 20, "Comunicação"),
("Disco de Transferência Giratório", "Disco de 40cm que gira 360º, facilita a transferência segura de pacientes da cama para a cadeira.", "image/disco_transferencia.png", 210.00, 15, "Mobilidade");

-- trigger de quando fizer login ele registra na tb login

select * from tbcliente;
select * from tbproduto;
select * from tbfuncionario;


SELECT * FROM tbpedido;
insert into tbfuncionario (nomefunc, emailfunc, senhafunc, cpffunc, tipofunc) values ("admin", "admin@admin.com", "123", 12333, "G");

insert into tbProduto(NomeProd, DescProd, ImgProd, PrecoProd) values ("Cadeira de rodas xlr8", "Uma cadeira de rodas baseada na coloração do alien XLR8 do Ben 10", "image/cadeira.png", 1234.99);

insert into tbProduto(NomeProd, DescProd, ImgProd, PrecoProd) values ("Garfo Adaptado", "um garfo adaptado para facilitar a praticidade de pessoas com deficiencias motoras ao se alimentarem! ", "image/garfo.png", 78.99);

insert into tbProduto(NomeProd, DescProd, ImgProd, PrecoProd) values ("Muletas 'The root'", "Par de muletas com tamanho ajustavel! ", "image/garfo.png", 83.99);



INSERT INTO tbProduto(NomeProd, DescProd, ImgProd, PrecoProd, QtdProd, CategoriaProd)
VALUES
("Abridor de Potes Elétrico", "Abre potes e tampas de garrafa com um único toque, ideal para quem tem artrite ou força limitada nas mãos.", "image/abridor_potes.png", 189.90, 30, "Acessórios"),
("Tábua de Corte Adaptada", "Tábua com pinos de inox e borda elevada, permite fixar alimentos para cortar usando apenas uma mão.", "image/tabua_adaptada.png", 139.90, 25, "Acessórios"),
("Calçador de Meias", "Dispositivo curvo que ajuda a vestir meias e meias de compressão sem precisar se curvar ou levantar as pernas.", "image/calcador_meias.png", 45.00, 50, "Acessórios"),
("Pinça Pegadora Dobrável 80cm", "Braço extensor leve com gatilho para pegar objetos em locais altos ou no chão, evitando quedas.", "image/pinca_alcancador.png", 65.00, 75, "Acessórios"),
("Campainha sem Fio com Alerta Visual", "Campainha com receptor portátil que pisca luzes de LED e vibra. Ideal para deficiência auditiva.", "image/campainha_luz.png", 175.00, 30, "Audição"),
("Botão Comunicador de Voz Gravável", "Botão de fácil pressão que grava e reproduz uma mensagem de 30 segundos. Para comunicação alternativa (AAC).", "image/botao_comunicador.png", 250.00, 20, "Comunicação"),
("Disco de Transferência Giratório", "Disco de 40cm que gira 360º, facilita a transferência segura de pacientes da cama para a cadeira.", "image/disco_transferencia.png", 210.00, 15, "Mobilidade");

-- trigger de quando fizer login ele registra na tb login