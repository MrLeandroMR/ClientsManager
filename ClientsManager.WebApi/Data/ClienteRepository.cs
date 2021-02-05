using ClientsManager.WebApi.Data.Contracts;
using ClientsManager.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebApi.Data
{
    public class ClienteRepository : IClientRepository
    {
        private readonly DataContext _context;

        public ClienteRepository(DataContext context)
        {
            _context = context;
        }

        public async void AddAsync<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public T GetObjectById<T>(int id) where T : class
        {
            return _context.Find<T>(id);
        }

        public IQueryable<Cliente> GetAllClientes()
        {
            IQueryable<Cliente> clientes = _context.Clientes;
            clientes = clientes.AsNoTracking().OrderBy(c => c.Id);

            return clientes;
        }

        public IQueryable<Endereco> GetAllEnderecos()
        {
            IQueryable<Endereco> enderecos = _context.Enderecos;
            enderecos = enderecos.AsNoTracking().OrderBy(e => e.Id);

            return enderecos;
        }

        public Cliente GetClienteByCpf(string cpf)
        {
            IQueryable<Cliente> clientes = _context.Clientes;
            var cliente = clientes.AsNoTracking().Where(c => c.CPF == cpf).FirstOrDefault();
            return cliente;
        }

        public Endereco[] GetAllEnderecosByClienteId(int clienteId)
        {
            IQueryable<ClienteEndereco> clientesEnderecos = _context.ClientesEnderecos;

            if (clientesEnderecos == null)
                return default;

            List<Endereco> enderecos = new List<Endereco>();

            foreach (ClienteEndereco clienteEndereco in clientesEnderecos)
            {
                if (clienteEndereco.ClienteId == clienteId)
                {
                    var currentEndereco = GetObjectById<Endereco>(clienteEndereco.EnderecoId);
                    enderecos.Add(currentEndereco);
                }
            }

            return enderecos.ToArray();
        }
    }
}
