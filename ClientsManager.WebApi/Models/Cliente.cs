using ClientsManager.WebApi.Models.ModelValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebApi.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nome { get; set; }

        [Required]
        [CpfValidation]
        public string CPF { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        public string DataNascimento { get; set; }

        //public Endereco Endereco { get; set; }

        public ICollection<ClienteEndereco> ClientesEnderecos { get; set; }
    }
}
