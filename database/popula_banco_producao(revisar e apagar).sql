insert into CoelhoLigeiro.status_etapas (nome) 
values 
('Agendada'),
('Aguardando Cliente'),
('Atrasada'),
('Concluida'),
('Em Execução');


insert into CoelhoLigeiro.status_empreendedor (nome)
values
('Habilitado'),
('Desabilitado'),
('Bloqueado');

insert into CoelhoLigeiro.status_usuarios (nome)
values
('Habilitado'),
('Desabilitado');

insert into CoelhoLigeiro.status_contas (nome)
values
('Agendada'),
('Atrasada'),
('Cancelada'),
('Paga');
;

insert into CoelhoLigeiro.status_cobrancas (nome)
values
('Agendada'),
('Atrasada'),
('Cancelada'),
('Recebida');

insert into CoelhoLigeiro.mes (mes)
values
('Janeiro'),
('Fevereiro'),
('Março'),
('Abril'),
('Maio'),
('Junho'),
('Julho'),
('Agosto'),
('Setembro'),
('Outubro'),
('Novembro'),
('Dezembro');

insert into CoelhoLigeiro.empreendedores (nome, nome_fantasia, cnpj, email, status_empreendedor_id, dt_criacao, categorias_id)
values
('Felipe Chagas', 'Studiosites', '26286788000138', 'felipe.chagas@outlook.com', 
(select id from CoelhoLigeiro.status_empreendedor where nome = 'Habilitado'), sysdate(), 
(select id from CoelhoLigeiro.categorias where  nome like '%MEI%'));

insert into CoelhoLigeiro.usuarios (nome, email,  senha, empreendedores_id, dt_criacao, status_usuarios_id)
value 
('Felipe Chagas', 'felipe.chagas@outlook.com', md5('teste'),
(select id from CoelhoLigeiro.empreendedores where cnpj = '26286788000138'), sysdate(),
(select id from CoelhoLigeiro.status_usuarios where nome = 'Habilitado'));

insert into CoelhoLigeiro.clientes (nome, email, responsavel, descricao, empreendedores_id, dt_criacao)
values
('BoulevardHall', 'faleconosco@boulevardhall.com.br', 'Renata','Cerimonial Boulevard Hall', 
(select id from CoelhoLigeiro.empreendedores where cnpj = '26286788000138'), sysdate());

insert into CoelhoLigeiro.contato (contato, clientes_id, empreendedores_id, dt_criacao)
values
('027981171111', (select id from CoelhoLigeiro.clientes where nome like 'Boulevard%'), 
(select id from CoelhoLigeiro.empreendedores where cnpj = '26286788000138'), sysdate());
