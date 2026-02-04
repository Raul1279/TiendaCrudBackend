using Entities.Models;

namespace Data.Interfaces
{
    public interface ITiendaRepository
    {
        Task<List<Tienda>> GetAllTiendas();
        Task<List<Tienda?>> GetTiendaById(int id);
        Task<GenericResponse> AddTienda(Tienda tienda);
        Task<GenericResponse> UpdateTienda(Tienda tienda);
        Task<GenericResponse> DeleteTienda(int id);
    }
}
