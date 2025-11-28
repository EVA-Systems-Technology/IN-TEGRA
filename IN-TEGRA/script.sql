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

create table tblogin(
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

    
create table tbEndereco (
	IdEndereco int primary key auto_increment,
    CEP varchar(8) not null,
    Estado varchar(100) not null,
    Cidade varchar(100) not null,
    Bairro varchar(100) not null,
    LogradouroEndereco varchar(100) not null,
	Numero int not null,
    Complemento varchar(100)
);

drop table Tbendereco;
drop table tbLogin;
drop table tbpedido;
drop table tbpagamento;
drop table tbItemPedido;


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
select * from tbcliente;
select * from tbproduto;
select * from tbfuncionario;
select * from tblogin;
SELECT * FROM tbpedido;


insert into tbFuncionario (nomefunc, emailfunc, senhafunc, cpffunc, tipofunc) values ("admin", "admin@admin.com", "123", 12333, "G");




INSERT INTO tbProduto(NomeProd, DescProd, ImgProd, PrecoProd, QtdProd, CategoriaProd)
VALUES
("Abridor de Potes Elétrico", "Abre potes e tampas de garrafa com um único toque, ideal para quem tem artrite ou força limitada nas mãos.", "image/abridor_potes.png", 189.90, 30, "Acessórios"),
("Tábua de Corte Adaptada", "Tábua com pinos de inox e borda elevada, permite fixar alimentos para cortar usando apenas uma mão.", "image/tabua_adaptada.png", 139.90, 25, "Acessórios"),
("Calçador de Meias", "Dispositivo curvo que ajuda a vestir meias e meias de compressão sem precisar se curvar ou levantar as pernas.", "image/calcador_meias.png", 45.00, 50, "Acessórios"),
("Pinça Pegadora Dobrável 80cm", "Braço extensor leve com gatilho para pegar objetos em locais altos ou no chão, evitando quedas.", "image/pinca_alcancador.png", 65.00, 75, "Acessórios"),
("Campainha sem Fio com Alerta Visual", "Campainha com receptor portátil que pisca luzes de LED e vibra. Ideal para deficiência auditiva.", "image/campainha_luz.png", 175.00, 30, "Audição"),
("Botão Comunicador de Voz Gravável", "Botão de fácil pressão que grava e reproduz uma mensagem de 30 segundos. Para comunicação alternativa (AAC).", "image/botao_comunicador.png", 250.00, 20, "Comunicação"),
("Disco de Transferência Giratório", "Disco de 40cm que gira 360º, facilita a transferência segura de pacientes da cama para a cadeira.", "image/disco_transferencia.png", 210.00, 15, "Mobilidade");

INSERT INTO tbProduto(NomeProd, DescProd, ImgProd, PrecoProd, QtdProd, CategoriaProd)
VALUES
('Facilitador de Punho e Polegar', 'Dispositivo auxiliar para facilitar a pegada e escrita.', 'image/facilitador_punho.jpg', 110.00, 50, 'Acessórios'),
('Aranha Mola', 'Acessório de reabilitação para exercícios de coordenação motora.', 'image/aranha_mola.jpg', 44.00, 100, 'Acessórios'),
('Tesoura Mola', 'Tesoura adaptada com mecanismo de mola para facilitar o corte.', 'image/tesoura_mola.jpg', 42.00, 80, 'Acessórios'),
('Caderno de Pauta Ampliada (100 Folhas)', 'Caderno com linhas mais espaçadas, ideal para baixa visão.', 'image/caderno_ampliado.jpg', 40.00, 200, 'Visão'),
('Facilitador Dorsal', 'Auxiliar para posicionamento e conforto dorsal.', 'image/facilitador_dorsal.jpg', 60.00, 75, 'Mobilidade'),
('Prancha de Comunicação Alternativa', 'Prancha com símbolos e imagens para comunicação não-verbal.', 'image/prancha_comunicacao.jpg', 180.00, 40, 'Comunicação'),
('Globo em alto relevo', 'Globo terrestre tátil para pessoas com deficiência visual.', 'image/globo_alto_relevo.jpg', 280.00, 25, 'Visão'),
('Colher Adaptada', 'Colher com cabo e formato ergonômico para facilitar a alimentação.', 'image/colher_adaptada.jpg', 35.00, 150, 'Acessórios'),
('Garfo Adaptado', 'Garfo com cabo e formato ergonômico para facilitar a alimentação.', 'image/garfo_adaptado.jpg', 35.00, 150, 'Acessórios'),
('Mesa Escolar Adaptada', 'Mesa escolar ajustável e adaptada para diferentes necessidades.', 'image/mesa_escolar.jpg', 850.00, 15, 'Mobilidade'),
('Teclado Adaptado em Libras', 'Teclado com marcações em Libras para auxílio na digitação.', 'image/teclado_libras.jpg', 180.00, 30, 'Visão');

