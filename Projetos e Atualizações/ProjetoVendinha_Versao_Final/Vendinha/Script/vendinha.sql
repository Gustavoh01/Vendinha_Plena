create table clientes (
	id SERIAL primary key,
	nome_completo TEXT not null,
	cpf TEXT not null unique,
	data_nascimento DATE not null,
	email TEXT not null
);

create table dividas (
	id SERIAL primary key,
	valor numeric(10, 2) not null,
	status int not null,
	data_criacao TIMESTAMP not null,
	data_pagamento TIMESTAMP,
	cliente_id INT not null,
	constraint fk_cliente
		foreign key (cliente_id)
		references clientes(id)
		on delete cascade
);