using Vendinha.Data;
using Vendinha.Model;
using Vendinha.Services;

var clienteService = new ClienteService();
var dividaService = new DividaService();

while (true)
{
    Console.WriteLine("=-=-= PROJETO VENDINHA =-=-=");
    Console.WriteLine("1 - Cadastrar Cliente");
    Console.WriteLine("2 - Criar dívida");
    Console.WriteLine("3 - Listar clientes");
    Console.WriteLine("4 - Marcar divida como paga");
    Console.WriteLine("5 - Listar clientes com paginação");
    Console.WriteLine("6 - Atualizar cliente");
    Console.WriteLine("7 - Remover cliente");
    Console.WriteLine("0 - Sair");

    Console.Write("Escolha uma opção: ");
    var opcao = Console.ReadLine();

    if (opcao == "1")
    {
        Console.WriteLine("Nome Completo: ");
        string nome = Console.ReadLine();

        Console.WriteLine("CPF: ");
        string cpf = Console.ReadLine();

        Console.WriteLine("Data de Nascimento: ");
        DateTime data = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Email: ");
        string email = Console.ReadLine();

        var resultado = clienteService.CadastrarCliente(nome, cpf, data, email);
        Console.WriteLine(resultado);
    }

    else if (opcao == "2")
    {
        Console.WriteLine("Digite o Id do cliente: ");
        int clienteId = int.Parse(Console.ReadLine());

        Console.WriteLine("Valor da Divida: ");
        decimal valor = decimal.Parse(Console.ReadLine());

        var resultado = dividaService.CriarDivida(clienteId, valor);
        Console.WriteLine(resultado);
    }

    else if (opcao == "3")
    {
        Console.Write("Buscar por nome/Enter para trazer todos: ");
        string busca = Console.ReadLine();

        var clientes = clienteService.ListarClientesSimples(busca);

        if (clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente encontrado!");
            continue;
        }

        foreach (var c in clientes)
        {
            decimal total = c.Dividas
                .Where(d => d.Status == StatusDivida.Pendente)
                .Sum(d => d.Valor);

            int idade = DateTime.Today.Year - c.DataNascimento.Year;
            if (c.DataNascimento.Date > DateTime.Today.AddYears(-idade))
                idade--;

            Console.WriteLine($"Id: {c.Id}");
            Console.WriteLine($"Nome: {c.NomeCompleto}");
            Console.WriteLine($"Idade: {idade}");
            Console.WriteLine($"Total devido: R$ {total}");

            if (c.Dividas.Count == 0)
            {
                Console.WriteLine("--> Cliente sem dívidas");
            }
            else
            {
                foreach (var d in c.Dividas)
                {
                    Console.WriteLine($"-> ID Dívida: {d.Id} | Valor: R$ {d.Valor} | Status: {d.Status}");
                }
            }

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
        }
    }
    else if (opcao == "4")
    {
        Console.WriteLine("Id da dívida: ");
        int dividaId = int.Parse(Console.ReadLine());

        var resultado = dividaService.MarcarComoPago(dividaId);
        Console.WriteLine(resultado);
    }
    else if (opcao == "5")
    {
        Console.Write("Buscar por nome/Enter para trazer todos: ");
        string busca = Console.ReadLine();

        Console.Write("Digite a página: ");
        int pagina;

        if (!int.TryParse(Console.ReadLine(), out pagina) || pagina <= 0)
        {
            Console.WriteLine("Página inválida!");
            continue;
        }

        var clientes = clienteService.ListarClientesPaginado(busca, pagina);

        if (clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente encontrado.");
            continue;
        }

        foreach (var c in clientes)
        {
            decimal total = c.Dividas
                .Where(d => d.Status == StatusDivida.Pendente)
                .Sum(d => d.Valor);

            int idade = DateTime.Today.Year - c.DataNascimento.Year;
            if (c.DataNascimento.Date > DateTime.Today.AddYears(-idade))
                idade--;

            Console.WriteLine($"Id: {c.Id}");
            Console.WriteLine($"Nome: {c.NomeCompleto}");
            Console.WriteLine($"Idade: {idade}");
            Console.WriteLine($"Total devido: R$ {total}");

            if (c.Dividas.Count == 0)
            {
                Console.WriteLine("--> Cliente sem dívidas");
            }
            else
            {
                foreach (var d in c.Dividas)
                {
                    Console.WriteLine($"   -> Dívida ID: {d.Id} | Valor: R$ {d.Valor} | Status: {d.Status}");
                }
            }

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
        }
    }
    else if (opcao == "6")
    {
        Console.Write("ID do cliente: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Novo nome: ");
        string nome = Console.ReadLine();

        Console.Write("Novo email: ");
        string email = Console.ReadLine();

        var resultado = clienteService.AtualizarCliente(id, nome, email);
        Console.WriteLine(resultado);
    }
    else if (opcao == "7")
    {
        Console.Write("Id do cliente: ");
        int id = int.Parse(Console.ReadLine());

        var resultado = clienteService.RemoverCliente(id);
        Console.WriteLine(resultado);
    }

    else if (opcao == "0")
    {
        Console.WriteLine("Saindo...");
        break;
    }
    else
    {
        Console.WriteLine("Opção inválida!");
    }

}