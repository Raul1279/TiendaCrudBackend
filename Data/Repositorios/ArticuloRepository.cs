using Data;
using Data.Interfaces;
using Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

public class ArticuloRepository : IArticuloRepository
{
    private readonly AppDbContext _dbContext;

    public ArticuloRepository(AppDbContext context)
    {
        _dbContext = context;
    }
    public async Task<List<Articulo>> GetAllArticulos()
    {
        try
        {
            return await _dbContext.tblArticulo
                .FromSqlRaw("EXEC spGetAllArticulos")
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting Articulos. Sp: spGetAllArticulos", ex);
        }
    }
    public async Task<List<Articulo?>> GetArticuloById(int id)
    {
        try
        {
            return await _dbContext.tblArticulo
                .FromSqlRaw("EXEC spGetArticuloById @articuloId", new SqlParameter("@articuloId", id))
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting Articulo. Sp: GetArticuloById", ex);
        }
    }
    public async Task<GenericResponse> AddArticulo(Articulo articulo)
    {
        try
        {
            DbConnection connection = _dbContext.Database.GetDbConnection();

            using DbCommand cmd = connection.CreateCommand();

            cmd.CommandText = "spCreateArticulo";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@codigo", articulo.codigo));
            cmd.Parameters.Add(new SqlParameter("@descripcion", articulo.descripcion));
            cmd.Parameters.Add(new SqlParameter("@precio", articulo.precio));
            cmd.Parameters.Add(new SqlParameter("@imagenUrl", articulo.imagenUrl));
            cmd.Parameters.Add(new SqlParameter("@stock", articulo.stock));
            cmd.Parameters.Add(new SqlParameter("@tiendaId", articulo.tiendaId));


            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using DbDataReader reader = await cmd.ExecuteReaderAsync();

            GenericResponse response = new GenericResponse();

            if (await reader.ReadAsync())
            {
                response.Result = reader.GetInt32(0);
                response.Message = reader.GetString(1);
            }

            return response;
        }
        catch (Exception ex)
        {
            return new GenericResponse
            {
                Result = 0,
                Message = ex.Message
            };
        }
    }
    public async Task<GenericResponse> UpdateArticulo(Articulo articulo)
    {
        try
        {
            DbConnection connection = _dbContext.Database.GetDbConnection();

            using DbCommand cmd = connection.CreateCommand();

            cmd.CommandText = "spUpdateArticulo";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@articuloId", articulo.articuloId));
            cmd.Parameters.Add(new SqlParameter("@codigo", articulo.codigo));
            cmd.Parameters.Add(new SqlParameter("@descripcion", articulo.descripcion));
            cmd.Parameters.Add(new SqlParameter("@precio", articulo.precio));
            cmd.Parameters.Add(new SqlParameter("@imagenUrl", articulo.imagenUrl));
            cmd.Parameters.Add(new SqlParameter("@stock", articulo.stock));
            cmd.Parameters.Add(new SqlParameter("@tiendaId", articulo.tiendaId));

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using DbDataReader reader = await cmd.ExecuteReaderAsync();

            GenericResponse response = new GenericResponse();

            if (await reader.ReadAsync())
            {
                response.Result = reader.GetInt32(0);
                response.Message = reader.GetString(1);
            }

            return response;
        }
        catch (Exception ex)
        {
            return new GenericResponse
            {
                Result = 0,
                Message = ex.Message
            };
        }
    }
    public async Task<GenericResponse> DeleteArticulo (int id)
    {
        try
        {
            DbConnection connection = _dbContext.Database.GetDbConnection();

            using DbCommand cmd = connection.CreateCommand();

            cmd.CommandText = "spDeleteArticulo";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@articuloId", id));

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using DbDataReader reader = await cmd.ExecuteReaderAsync();

            GenericResponse response = new GenericResponse();

            if (await reader.ReadAsync())
            {
                response.Result = reader.GetInt32(0);
                response.Message = reader.GetString(1);
            }
            return response;
        }
        catch (Exception ex)
        {
            return new GenericResponse
            {
                Result = 0,
                Message = ex.Message
            };
        }
    }

}
