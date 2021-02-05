using ClientsManager.WebApi.Data.Contracts;
using ClientsManager.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClientRepository _repository;

        public ClienteController(IClientRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Cliente[]> ClienteList()
        {
            return _repository.GetAllClientes().ToArray();
        }

        [Route("endereco")]
        [HttpGet]
        public ActionResult<Endereco[]> EnderecoList()
        {
            return _repository.GetAllEnderecos().ToArray();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Cliente> ClienteById(string id)
        {
            int intId;
            if (int.TryParse(id, out intId))
            {
                var cliente = _repository.GetObjectById<Cliente>(intId);

                if (cliente == null)
                    return NotFound("Cliente não foi encontrado");

                return cliente;
            }

            return BadRequest();
        }

        [HttpGet("cpf/{cpf}")]
        public ActionResult<Cliente> ClienteByCpf(string cpf)
        {
            var cliente = _repository.GetClienteByCpf(cpf);

            if (cliente == null)
                return NotFound();

            return cliente;
        }

        [HttpGet("{clienteId}/endereco")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Endereco[]> EnderecosByClienteId(string clienteId)
        {
            int intId;
            if (!int.TryParse(clienteId, out intId))
                return BadRequest("Id não corresponde à um número");

            return _repository.GetAllEnderecosByClienteId(intId);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddCliente(Cliente cliente)
        {
            _repository.AddAsync(cliente);

            if (!_repository.SaveChanges())
                return BadRequest("Cliente não pode ser adicionado");

            return Ok(cliente);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateCliente(Cliente cliente)
        {
            _repository.Update(cliente);
            if (_repository.SaveChanges())
                return Ok(cliente);

            return BadRequest("Cliente não pode ser atualizado");
        }

        [HttpPost("{clienteId}/endereco")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddEndereco(string clienteId, Endereco endereco)
        {
            int intId;
            if (!int.TryParse(clienteId, out intId))
                return BadRequest("Id não corresponde à um número");

                _repository.AddAsync(endereco);
            var isEnderecoSaved = _repository.SaveChanges();

            if (!isEnderecoSaved)
                return BadRequest("Endereço não pode ser salvo.");

            _repository.AddAsync(new ClienteEndereco()
            {
                ClienteId = int.Parse(clienteId),
                Cliente = _repository.GetObjectById<Cliente>(intId),
                EnderecoId = endereco.Id,
                Endereco = _repository.GetObjectById<Endereco>(endereco.Id)
            });

            if (!_repository.SaveChanges())
                return BadRequest("Endereço não pode ser atrelado ao cliente.");

            return Ok(endereco);
        }

        [HttpDelete("endereco")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult RemoveEndereco(Endereco endereco)
        {
            _repository.Remove(endereco);
            if (_repository.SaveChanges())
                return Ok(endereco);

            return BadRequest("Endereço não pode ser removido");
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult RemoveCliente(Cliente cliente)
        {
            _repository.Remove(cliente);
            if (_repository.SaveChanges())
                return Ok(cliente);

            return BadRequest("Cliente não pode ser removido");
        }
    }
}
