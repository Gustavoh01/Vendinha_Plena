# Vendinha Plena
Cenário: Uma vendinha precisa informatizar o controle de contas de seus clientes (dívidas penduradas) para facilitar a
busca e o cadastro desses dados que antes eram feitas por papel. O cliente chega na loja, faz a compra e
pede para o atendente pendurar para que seja acertado no final do mês.
Pensando nisso, é necessário criar um sistema simples de cadastro para que o dono da venda consiga
controlar as dívidas de seus clientes.
***
- Ambiente de desenvolvimento: Visual Studio
- Plataforma: .NET 10.0.201
- Tipo de aplicação: Console
- Linguagem: C#
- Pacotes NuGet:
- Microsoft.EntityFrameworkCore
- Npgsql.EntityFrameworkCore.PostgreSQL
- Banco de Dados:
- PostgreSQL
- DBeaver
***
# Passo a Passo para rodar o Projeto
- Instale o ambiente de desenvolvimento Visual Studio.
- Caso necessario instale o .Net Sdk para compilar e rodar o código.
- Baixe o arquivo zip disponibilizado no reposotório.
- Apos baixar o arquivo, extraia e abra com o Visual Studio.
- De dois click na Solução do projeto.
- Baixe os pacotes mencionados a cima.
- Baixe o gerenciador de banco de dados Dbeaver.
- Dentro do Dbeaver selecione o Banco de dados PostgreSQL.
- Abra a pasta "Script" do projeto e copie o codigo dentro de uma query PostgreSQl do Dbeaver.
- Execute a query do banco PostgreSQL e confira a criação das tabelas.
- Volte ao Visual Studio e dentro da pasta Data/AppDbContext.cs configura a string connection para as configurações da sua maquina.
- Dentro do Vistual Studio rode o código do projeto com o atalho Ctrl + F5 ou na seta verde localizada ao topo.
- Ao abrir a janela do console, teste a aplicação de diferentes formas.
