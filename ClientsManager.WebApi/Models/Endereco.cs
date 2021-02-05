using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebApi.Models
{
    public class Endereco
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Logradouro { get; set; }

        [Required]
        [MaxLength(40)]
        public string Bairro { get; set; }

        [Required]
        [MaxLength(40)]
        public string Cidade { get; set; }

        [Required]
        [MaxLength(40)]
        public string Estado { get; set; }

        public ICollection<ClienteEndereco> ClientesEnderecos { get; set; }
    }
}
