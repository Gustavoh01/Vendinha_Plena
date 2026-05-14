using System;
using System.Collections.Generic;
using System.Text;

namespace Vendinha.Model
{
    public class Divida
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public StatusDivida Status {  get; set; }  
        public DateTime  DataCriacao { get; set; }
        public DateTime DataPagamento { get; set; }

        public int ClienteId { get; set; }
    }
}
