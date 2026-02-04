namespace Entities.Models
{
    public class Cliente
    {
        public int clienteId { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }
    }

    public class Tienda
    {
        public int tiendaId { get; set; }
        public string sucursal { get; set; }
        public string direccion { get; set; }
    }

    public class Articulo
    {
        public int articuloId { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string precio { get; set; }
        public string imagenUrl { get; set; }
        public int stock { get; set; }
        public int tiendaId { get; set; }
        public string? sucursal { get; set; }
    }

    public class ClienteArticulo
    {
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }

        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class AddArticuloCarritoDto
    {
        public int clienteId { get; set; }
        public int articuloId { get; set; }
        public int cantidad { get; set; }
    }

    public class CarritoArticuloDto
    {
        public int articuloId { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string precio { get; set; }
        public string imagenUrl { get; set; }
        public int cantidad { get; set; }
    }

    public class PagarCarritoDto
    {
        public int clienteId { get; set; }
    }



}
