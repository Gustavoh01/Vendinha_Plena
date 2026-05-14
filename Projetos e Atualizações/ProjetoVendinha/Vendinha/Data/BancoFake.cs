using System;
using System.Collections.Generic;
using System.Text;
using Vendinha.Model;

namespace Vendinha.Data
{
    public static class BancoFake
    {
        public static List<Cliente> Clientes = new();
        public static List<Divida> Dividas = new();
    }
}
