using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Vendinha.Data;
using Vendinha.Model;

namespace Vendinha.Services
{
    public class DividaService
    {
        public string CriarDivida(int clienteId, Decimal valor)
        {
            var cliente = BancoFake.Clientes.FirstOrDefault(c => c.Id == clienteId);

            if (cliente == null)
            {
                return "Cliente não foi encontrado.";
            }

            var possuiAberto = cliente.Dividas.Any(d => d.Status == StatusDivida.pendente);

            if (possuiAberto)
            {
                return "O cliente tem contas pendentes.";
            }

            var divida = new Divida
            {
                Id = BancoFake.Dividas.Count + 1,
                Valor = valor,
                Status = StatusDivida.pendente,
                DataCriacao = DateTime.Now,
                ClienteId = clienteId
            };

            cliente.Dividas.Add(divida);
            BancoFake.Dividas.Add(divida);

            return "Dívida criada com sucesso!";
        }
    }
}
