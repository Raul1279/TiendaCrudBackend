using Entities.Models;

public interface IClienteService
{
    Task<IEnumerable<Cliente>> GetAllClientes();
    Task<List<Cliente?>> GetClienteById(int id);
    Task<GenericResponse> AddCliente(Cliente cliente);
    Task<GenericResponse> UpdateCliente(Cliente cliente);
    Task<GenericResponse> DeleteCliente(int id);
    Task<GenericResponse> AddArticuloToCarrito(AddArticuloCarritoDto dto);
    Task<IEnumerable<CarritoArticuloDto>> GetArticulosByCliente(int clienteId);
    Task<GenericResponse> DeleteArticuloFromCarrito(int clienteId, int articuloId);
    Task<GenericResponse> PagarCarrito(int clienteId);
}

