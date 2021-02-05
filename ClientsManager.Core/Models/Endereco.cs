using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Core.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public ICollection<ClienteEndereco> ClientesEnderecos { get; set; }
    }
}
