using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Core.Models
{
    public class ClienteEndereco
    {
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
    }
}
