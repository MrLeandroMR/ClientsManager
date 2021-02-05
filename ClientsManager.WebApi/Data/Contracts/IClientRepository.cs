using ClientsManager.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebApi.Data.Contracts
{
    public interface IClientRepository
    {
        void AddAsync<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        bool SaveChanges();
        T GetObjectById<T>(int id) where T : class;

        IQueryable<Cliente> GetAllClientes();
        IQueryable<Endereco> GetAllEnderecos();
        Cliente GetClienteByCpf(string cpf);
        Endereco[] GetAllEnderecosByClienteId(int clienteId);
    }
}
