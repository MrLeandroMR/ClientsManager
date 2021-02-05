using ClientsManager.Core.Models;
using ClientsManager.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientsManager.Core.Services
{
    public class ClienteDataService : IClienteData
    {
        private readonly IHttpData _httpData;

        public ClienteDataService(IHttpData httpData)
        {
            _httpData = httpData;
        }

        public async Task<Cliente[]> GetAllClientes()
        {
            var clientes = await _httpData.GetAsync<Cliente[]>("https://localhost:44340/api/cliente", null, true);

            if (clientes == null || clientes.Length == 0)
                return default;

            return clientes;
        }

        public async Task<Endereco[]> GetAllEnderecosFromCliente(int id)
        {
            var enderecos = await _httpData.GetAsync<Endereco[]>($"https://localhost:44340/api/cliente/{id}/endereco");

            if (enderecos == null || enderecos.Length == 0)
                return default;

            return enderecos;
        }

        public async Task<bool> PostNewClient(Cliente cliente)
        {
            return await _httpData.PostAsJsonAsync<Cliente>("https://localhost:44340/api/cliente", cliente);
        }

        public async Task<bool> PostNewClientAdress(int clientId, Endereco endereco)
        {
            return await _httpData.PostAsJsonAsync<Endereco>($"https://localhost:44340/api/cliente/{clientId}/endereco", endereco);
        }

        public async Task<bool> UpdateCliente(Cliente cliente)
        {
            return await _httpData.PutAsJsonAsync<Cliente>("https://localhost:44340/api/cliente", cliente);
        }
    }
}
