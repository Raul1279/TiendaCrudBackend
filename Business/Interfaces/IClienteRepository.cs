using Entities;
using Entities.Models;

public interface IClienteRepository
{
    Task<Cliente?> GetByEmailByCliente(string email);
    Task<List<Cliente>> GetAllClientes();
    Task<List<Cliente?>> GetClienteById(int id);
    Task<GenericResponse> AddCliente(Cliente cliente);
    Task<GenericResponse> UpdateCliente(Cliente cliente);
    Task<GenericResponse> DeleteCliente(int id);
    Task<GenericResponse> AddArticuloToCarrito(AddArticuloCarritoDto dto);
    Task<List<CarritoArticuloDto>> GetArticulosByCliente(int clienteId);
    Task<GenericResponse> DeleteArticuloFromCarrito(int clienteId, int articuloId);
    Task<GenericResponse> PagarCarrito(int clienteId);

}
