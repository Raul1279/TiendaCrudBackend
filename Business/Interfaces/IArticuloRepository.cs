using Entities.Models;

namespace Data.Interfaces
{
    public interface IArticuloRepository
    {
        Task<List<Articulo>> GetAllArticulos();
        Task<List<Articulo?>> GetArticuloById(int id);
        Task<GenericResponse> AddArticulo(Articulo articulo);
        Task<GenericResponse> UpdateArticulo(Articulo articulo);
        Task<GenericResponse> DeleteArticulo(int id);
    }
}
