using Vendinha.Data;
using Vendinha.Model;
using Vendinha.Services;

Console.WriteLine("Hello, World!");
var clienteService = new ClienteService();
var dividaService = new DividaService();

while (true)
{
    Console.WriteLine("=== SISTEMA ===");
    Console.WriteLine("1 - Cadastrar Cliente");
    Console.WriteLine("2 - Criar dívida");
    Console.WriteLine("3 - Listar clientes");
    Console.WriteLine("0 - Sair");

    Console.Write("Escolha: ");
    var opcao = Console.ReadLine();

    if (opcao == "1")
    {
        Console.WriteLine("Nome: ");
        string nome = Console.ReadLine();

        Console.WriteLine("CPF: ");
        string cpf = Console.ReadLine();

        Console.WriteLine("Data de Nascimento: ");
        DateTime data = DateTime.Parse(Console.ReadLine());

        clienteService.CadastrarCliente(nome, cpf, data);
        Console.WriteLine("Cliente cadastrado com Sucesso!");
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
        var clientes = clienteService.ListarClientes();

        foreach (var cliente in clientes)
        {
            Console.WriteLine($"\nID: {cliente.Id}");
            Console.WriteLine($"Nome: {cliente.NomeCompleto}");
            Console.WriteLine($"CPF: {cliente.CPF}");

            var total = cliente.Dividas.Sum(d => d.Valor);

            Console.WriteLine($"Total devido: {total}");

            foreach (var d in cliente.Dividas)
            {
                Console.WriteLine($" - Dívida ID: {d.Id} | Valor: {d.Valor} | Status: {d.Status}");
            }
        }
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