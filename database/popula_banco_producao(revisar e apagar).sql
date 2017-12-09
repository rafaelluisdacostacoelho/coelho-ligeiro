insert into nosempreendedores.status_etapas (nome) 
values 
('Agendada'),
('Aguardando Cliente'),
('Atrasada'),
('Concluida'),
('Em Execução');


insert into nosempreendedores.status_empreendedor (nome)
values
('Habilitado'),
('Desabilitado'),
('Bloqueado');

insert into nosempreendedores.status_usuarios (nome)
values
('Habilitado'),
('Desabilitado');

insert into nosempreendedores.status_contas (nome)
values
('Agendada'),
('Atrasada'),
('Cancelada'),
('Paga');
;

insert into nosempreendedores.status_cobrancas (nome)
values
('Agendada'),
('Atrasada'),
('Cancelada'),
('Recebida');

insert into nosempreendedores.mes (mes)
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

insert into nosempreendedores.empreendedores (nome, nome_fantasia, cnpj, email, status_empreendedor_id, dt_criacao, categorias_id)
values
('Felipe Chagas', 'Studiosites', '26286788000138', 'felipe.chagas@outlook.com', 
(select id from nosempreendedores.status_empreendedor where nome = 'Habilitado'), sysdate(), 
(select id from nosempreendedores.categorias where  nome like '%MEI%'));

insert into nosempreendedores.usuarios (nome, email,  senha, empreendedores_id, dt_criacao, status_usuarios_id)
value 
('Felipe Chagas', 'felipe.chagas@outlook.com', md5('teste'),
(select id from nosempreendedores.empreendedores where cnpj = '26286788000138'), sysdate(),
(select id from nosempreendedores.status_usuarios where nome = 'Habilitado'));

insert into nosempreendedores.clientes (nome, email, responsavel, descricao, empreendedores_id, dt_criacao)
values
('BoulevardHall', 'faleconosco@boulevardhall.com.br', 'Renata','Cerimonial Boulevard Hall', 
(select id from nosempreendedores.empreendedores where cnpj = '26286788000138'), sysdate());

insert into nosempreendedores.contato (contato, clientes_id, empreendedores_id, dt_criacao)
values
('027981171111', (select id from nosempreendedores.clientes where nome like 'Boulevard%'), 
(select id from nosempreendedores.empreendedores where cnpj = '26286788000138'), sysdate());
