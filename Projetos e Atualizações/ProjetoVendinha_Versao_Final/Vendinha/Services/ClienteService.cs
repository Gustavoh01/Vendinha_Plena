using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Vendinha.Data;
using Vendinha.Model;
using Microsoft.EntityFrameworkCore;

namespace Vendinha.Services
{
    public class ClienteService
    {
        public string CadastrarCliente(string nome, string cpf, DateTime dataNascimento, string email)
        {

            
            using var context = new AppDbContext();

            if (context.Clientes.Any(c => c.CPF == cpf))
            {
                return "CPF já cadastrado.";
            }

            var cliente = new Cliente
            {
                
                NomeCompleto = nome,
                CPF = cpf,
                DataNascimento = DateTime.SpecifyKind(dataNascimento.Date, DateTimeKind.Utc),
                Email = email
            };

            context.Clientes.Add(cliente);
            context.SaveChanges();

            Console.WriteLine("Cliente salvo no banco");
            return "Cliente cadastrado com sucesso!";
        }

        public List<Cliente> ListarClientes()
        {
            using var context = new AppDbContext();
            return context.Clientes.Include(c => c.Dividas).ToList();
        }

        public List<Cliente> ListarClientesSimples(string busca)
        {
            using var context = new AppDbContext();

            List<Cliente> clientes;

            if (string.IsNullOrWhiteSpace(busca))

            {
                clientes = context.Clientes
                    .Include(c => c.Dividas)
                    .ToList();
            }
            else
            {
                clientes = context.Clientes
                    .Include(c => c.Dividas)
                    .Where(c => c.NomeCompleto.ToLower().Contains(busca.ToLower()))
                    .ToList();
            }

            clientes = clientes
                .OrderByDescending(c => c.Dividas
                    .Where(d => d.Status == StatusDivida.Pendente)
                    .Sum(d => d.Valor))
                .ToList();

            return clientes;
        }
        public List<Cliente> ListarClientesPaginado(string busca, int numeroPagina)
        {
            using var context = new AppDbContext();

            int itensPorPagina = 10;
            int pular = (numeroPagina - 1) * itensPorPagina;

            var clientes = context.Clientes
                .Include(c => c.Dividas)
                .ToList();

            if (!string.IsNullOrWhiteSpace(busca))
            {
                clientes = clientes
                    .Where(c => c.NomeCompleto != null && c.NomeCompleto.Contains(busca))
                    .ToList();
            }

            clientes = clientes
                .OrderByDescending(c => c.Dividas
                    .Where(d => d.Status == StatusDivida.Pendente)
                    .Sum(d => d.Valor))
                .ToList();

            clientes = clientes
                .Skip(pular)
                .Take(itensPorPagina)
                .ToList();

            return clientes;
        }
        public string AtualizarCliente(int id, string nome, string email)
        {
            using var context = new AppDbContext();

            var cliente = context.Clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
            {
                return "Cliente não encontrado.";
            }

            cliente.NomeCompleto = nome;
            cliente.Email = email;

            context.SaveChanges();

            return "Cliente atualizado com sucesso!";
        }
        public string RemoverCliente(int id)
        {
            using var context = new AppDbContext();

            var cliente = context.Clientes
                .Include(c => c.Dividas)
                .FirstOrDefault(c => c.Id == id);

            if (cliente == null)
            {
                return "Cliente não encontrado.";
            }

            if (cliente.Dividas.Any(d => d.Status == StatusDivida.Pendente))
            {
                return "Cliente possui dívida em aberto e não pode ser removido.";
            }

            context.Clientes.Remove(cliente);
            context.SaveChanges();

            return "Cliente removido com sucesso!";
        }
    }

}
