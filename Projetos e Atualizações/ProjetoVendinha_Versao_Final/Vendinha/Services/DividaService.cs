using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Vendinha.Data;
using Vendinha.Model;
using Microsoft.EntityFrameworkCore;

namespace Vendinha.Services
{
    public class DividaService
    {
        public string CriarDivida(int clienteId, Decimal valor)
        {
            using var context = new AppDbContext();

            var cliente = context.Clientes.Include(c => c.Dividas).FirstOrDefault(c => c.Id == clienteId);

            if (cliente == null)
            {
                return "Cliente não foi encontrado.";
            }

            if (valor <= 0)
            {
                return "O valor da dívida deve ser maior que zero.";
            }

            var possuiAberto = cliente.Dividas.Any(d => d.Status == StatusDivida.Pendente);

            if (possuiAberto)
            {
                return "O cliente tem contas pendentes.";
            }

            var divida = new Divida
            {
                
                Valor = valor,
                Status = StatusDivida.Pendente,
                DataCriacao = DateTime.UtcNow,
                ClienteId = clienteId
            };

            context.Dividas.Add(divida);
            context.SaveChanges();

            return "Dívida criada com sucesso!";
        }

        public string MarcarComoPago(int dividaId)
        {
            using var context = new AppDbContext();

            var divida = context.Dividas.FirstOrDefault(d => d.Id == dividaId);
            if(divida == null)
            {
                return "Divida não encontrada";
            }

            if (divida.Status == StatusDivida.Pago)
            {
                return "A divida já foi paga";
            }

            divida.Status = StatusDivida.Pago;
            divida.DataPagamento = DateTime.UtcNow;
            context.SaveChanges();
            return "Divida marcada como Paga";
        }
    }
}
