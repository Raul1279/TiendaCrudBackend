using Data.Interfaces;
using Entities.Models;

public class TiendaService : ITiendaService
{
    private readonly ITiendaRepository _tiendaRepository;

    public TiendaService(ITiendaRepository repo)
    {
        _tiendaRepository = repo;
    }
    public async Task<IEnumerable<Tienda>> GetAllTiendas()
    {
        try
        {
            return await _tiendaRepository.GetAllTiendas();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener tiendas: {ex.Message}");
        }
    }
    public async Task<List<Tienda?>> GetTiendaById(int id)
    {
        try
        {
            return await _tiendaRepository.GetTiendaById(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener la tienda con ID {id}: {ex.Message}");
        }
    }
    public async Task<GenericResponse> AddTienda(Tienda tienda)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(tienda.sucursal) || string.IsNullOrWhiteSpace(tienda.direccion))
            {
                return new GenericResponse
                {
                    Result = 0,
                    Message = "La sucursal y direccion son obligatorios."
                };
            }

            return await _tiendaRepository.AddTienda(tienda);
        }
        catch (Exception ex)
        {
            return new GenericResponse
            {
                Result = 0,
                Message = $"Error al agregar la tienda: {ex.Message}"
            };
        }
    }
    public async Task<GenericResponse> UpdateTienda(Tienda tienda)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(tienda.sucursal) || string.IsNullOrWhiteSpace(tienda.direccion))
            {
                return new GenericResponse
                {
                    Result = 0,
                    Message = "La sucursal y direccion son obligatorios."
                };
            }

            return await _tiendaRepository.UpdateTienda(tienda);
        }
        catch (Exception ex)
        {
            return new GenericResponse
            {
                Result = 0,
                Message = $"Error al actualizar la tienda: {ex.Message}"
            };
        }
    }
    public async Task<GenericResponse> DeleteTienda(int id)
    {
        try
        {
            return await _tiendaRepository.DeleteTienda(id);
        }
        catch (Exception ex)
        {
            return new GenericResponse
            {
                Result = 0,
                Message = $"Error al eliminar la tienda: {ex.Message}"
            };
        }
    }
}
