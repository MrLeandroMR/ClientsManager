using ClientsManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientsManager.Core.Services.Contracts
{
    public interface IClienteData
    {
        Task<Cliente[]> GetAllClientes();
        Task<Endereco[]> GetAllEnderecosFromCliente(int id);
        Task<bool> PostNewClient(Cliente cliente);
        Task<bool> PostNewClientAdress(int clientId, Endereco endereco);
        Task<bool> UpdateCliente(Cliente cliente);
    }
}
