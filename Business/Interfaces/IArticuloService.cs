using Entities.Models;

public interface IArticuloService
{
    Task<IEnumerable<Articulo>> GetAllArticulos();
    Task<List<Articulo?>> GetArticuloById(int id);
    Task<GenericResponse> AddArticulo(Articulo articulo);
    Task<GenericResponse> UpdateArticulo(Articulo articulo);
    Task<GenericResponse> DeleteArticulo(int id);
}
