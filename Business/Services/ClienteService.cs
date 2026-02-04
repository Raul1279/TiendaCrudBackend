using Data.Interfaces;
using Entities.Models;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository repo)
    {
        _clienteRepository = repo;
    }

    public async Task<IEnumerable<Cliente>> GetAllClientes()
    {
        try
        {
            return await _clienteRepository.GetAllClientes();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener clientes: {ex.Message}");
        }
    }

    public async Task<IEnumerable<CarritoArticuloDto>> GetArticulosByCliente(int clienteId)
    {
        try
        {
            return await _clienteRepository.GetArticulosByCliente(clienteId);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener clientes: {ex.Message}");
        }
    }

    public async Task<List<Cliente?>> GetClienteById(int id)
    {
        try
        {
            return await _clienteRepository.GetClienteById(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener el cliente con ID {id}: {ex.Message}");
        }
    }

    public async Task<GenericResponse> AddCliente(Cliente cliente)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cliente.nombre) 
                || string.IsNullOrWhiteSpace(cliente.apellidos)
                || string.IsNullOrWhiteSpace(cliente.direccion)
                || string.IsNullOrWhiteSpace(cliente.email)
                || string.IsNullOrWhiteSpace(cliente.passwordHash)
            )
            {
                return new GenericResponse
                {
                    Result = 0,
                    Message = "Todos los campos son requeridos. Por favor verifique su informacion."
                };
            }

            return await _clienteRepository.AddCliente(cliente);
        }
        catch (Exception ex)
        {
            return new GenericResponse
            {
                Result = 0,
                Message = $"Error al agregar el cliente: {ex.Message}"
            };
        }
    }

    public async Task<GenericResponse> UpdateCliente(Cliente cliente)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cliente.nombre)
                            || string.IsNullOrWhiteSpace(cliente.apellidos)
                            || string.IsNullOrWhiteSpace(cliente.direccion)
                            || string.IsNullOrWhiteSpace(cliente.email)
                            || string.IsNullOrWhiteSpace(cliente.passwordHash)
                        )
            {
                return new GenericResponse
                {
                    Result = 0,
                    Message = "Todos los campos son requeridos. Por favor verifique su informacion."
                };
            }

            return await _clienteRepository.UpdateCliente(cliente);
        }
        catch (Exception ex)
        {
            return new GenericResponse
            {
                Result = 0,
                Message = $"Error al actualizar el cliente: {ex.Message}"
            };
        }
    }

    public async Task<GenericResponse> DeleteCliente(int id)
    {
        try
        {
            return await _clienteRepository.DeleteCliente(id);
        }
        catch (Exception ex)
        {
            return new GenericResponse
            {
                Result = 0,
                Message = $"Error al eliminar el cliente: {ex.Message}"
            };
        }
    }

    public async Task<GenericResponse> DeleteArticuloFromCarrito(int clienteId, int articuloId)
    {
        try
        {
            return await _clienteRepository.DeleteArticuloFromCarrito( clienteId, articuloId);
        }
        catch (Exception ex)
        {
            return new GenericResponse
            {
                Result = 0,
                Message = $"Error al eliminar el cliente: {ex.Message}"
            };
        }
    }

    public async Task<GenericResponse> AddArticuloToCarrito(AddArticuloCarritoDto dto)
    {
        try
        {
            return await _clienteRepository.AddArticuloToCarrito(dto);
        }
        catch (Exception ex)
        {
            return new GenericResponse
            {
                Result = 0,
                Message = $"Error al eliminar el cliente: {ex.Message}"
            };
        }
    }
    public async Task<GenericResponse> PagarCarrito(int clienteId)
    {
        return await _clienteRepository.PagarCarrito(clienteId);
    }
}
