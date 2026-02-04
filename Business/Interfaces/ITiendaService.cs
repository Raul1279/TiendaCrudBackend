using Entities.Models;

public interface ITiendaService
{
    Task<IEnumerable<Tienda>> GetAllTiendas();
    Task<List<Tienda?>> GetTiendaById(int id);
    Task<GenericResponse> AddTienda(Tienda tienda);
    Task<GenericResponse> UpdateTienda(Tienda tienda);
    Task<GenericResponse> DeleteTienda(int id);
}
