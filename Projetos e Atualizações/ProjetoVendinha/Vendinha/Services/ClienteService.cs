using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Vendinha.Data;
using Vendinha.Model;

namespace Vendinha.Services
{
    public class ClienteService
    {
        public void CadastrarCliente(string nome, string cpf, DateTime dataNascimento)
        {
            var cliente = new Cliente
            {
                Id = BancoFake.Clientes.Count + 1,
                NomeCompleto = nome,
                CPF = cpf,
                DataNascimento = dataNascimento
            };

            BancoFake.Clientes.Add(cliente);
        }
        public List<Cliente> ListarClientes()
        {
            return BancoFake.Clientes;
        }

        public Cliente BuscarId(int id)
        {
            return BancoFake.Clientes.FirstOrDefault(c => c.Id == id);
        }
    }
}
