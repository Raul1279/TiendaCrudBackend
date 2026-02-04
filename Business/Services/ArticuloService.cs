using Data.Interfaces;
using Entities.Models;

public class ArticuloService : IArticuloService
{
    private readonly IArticuloRepository _articuloRepository;

    public ArticuloService(IArticuloRepository repo)
    {
        _articuloRepository = repo;
    }

    public async Task<IEnumerable<Articulo>> GetAllArticulos()
    {
        try
        {
            return await _articuloRepository.GetAllArticulos();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener articulos: {ex.Message}");
        }
    }

    public async Task<List<Articulo?>> GetArticuloById(int id)
    {
        try
        {
            return await _articuloRepository.GetArticuloById(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener el articulo con ID {id}: {ex.Message}");
        }
    }

    public async Task<GenericResponse> AddArticulo(Articulo articulo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(articulo.codigo)
                || string.IsNullOrWhiteSpace(articulo.descripcion)
                || string.IsNullOrWhiteSpace(articulo.imagenUrl)
            )
            {
                return new GenericResponse
                {
                    Result = 0,
                    Message = "Todos los campos son requeridos. Por favor verifique su informacion."
                };
            }

            return await _articuloRepository.AddArticulo(articulo);
        }
        catch (Exception ex)
        {
            return new GenericResponse
            {
                Result = 0,
                Message = $"Error al agregar el articulo: {ex.Message}"
            };
        }
    }

    public async Task<GenericResponse> UpdateArticulo(Articulo articulo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(articulo.codigo)
                || string.IsNullOrWhiteSpace(articulo.descripcion)
                || string.IsNullOrWhiteSpace(articulo.imagenUrl)
            )
            {
                return new GenericResponse
                {
                    Result = 0,
                    Message = "Todos los campos son requeridos. Por favor verifique su informacion."
                };
            }

            return await _articuloRepository.UpdateArticulo(articulo);
        }
        catch (Exception ex)
        {
            return new GenericResponse
            {
                Result = 0,
                Message = $"Error al actualizar el articulo: {ex.Message}"
            };
        }
    }

    public async Task<GenericResponse> DeleteArticulo(int id)
    {
        try
        {
            return await _articuloRepository.DeleteArticulo(id);
        }
        catch (Exception ex)
        {
            return new GenericResponse
            {
                Result = 0,
                Message = $"Error al eliminar el articulo: {ex.Message}"
            };
        }
    }
}
