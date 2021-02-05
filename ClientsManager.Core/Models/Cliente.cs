using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Core.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string DataNascimento { get; set; }
        public ICollection<ClienteEndereco> ClientesEnderecos { get; set; }
    }
}
