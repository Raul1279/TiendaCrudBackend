using Data;
using Data.Interfaces;
using Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

public class TiendaRepository : ITiendaRepository
{
    private readonly AppDbContext _dbContext;

    public TiendaRepository(AppDbContext context)
    {
        _dbContext = context;
    }
    public async Task<List<Tienda>> GetAllTiendas()
    {
        try
        {
            return await _dbContext.tblTienda
                .FromSqlRaw("EXEC spGetAllTiendas")
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting Tiendas. Sp: spGetAllTiendas", ex);
        }
    }
    public async Task<List<Tienda?>> GetTiendaById(int id)
    {
        try
        {
            return await _dbContext.tblTienda
                .FromSqlRaw("EXEC spGetTiendaById @tiendaId",new SqlParameter("@tiendaId", id))
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting Tienda. Sp: GetTiendaById", ex);
        }
    }
    public async Task<GenericResponse> AddTienda(Tienda tienda)
    {
        try
        {
            DbConnection connection = _dbContext.Database.GetDbConnection();

            using DbCommand cmd = connection.CreateCommand();

            cmd.CommandText = "spCreateTienda";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@sucursal", tienda.sucursal));
            cmd.Parameters.Add(new SqlParameter("@direccion", tienda.direccion));

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
    public async Task<GenericResponse> UpdateTienda(Tienda tienda)
    {
        try
        {
            DbConnection connection = _dbContext.Database.GetDbConnection();

            using DbCommand cmd = connection.CreateCommand();

            cmd.CommandText = "spUpdateTienda";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@tiendaId", tienda.tiendaId));
            cmd.Parameters.Add(new SqlParameter("@sucursal", tienda.sucursal));
            cmd.Parameters.Add(new SqlParameter("@direccion", tienda.direccion));

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
    public async Task<GenericResponse> DeleteTienda(int id)
    {
        try
        {
            DbConnection connection = _dbContext.Database.GetDbConnection();

            using DbCommand cmd = connection.CreateCommand();

            cmd.CommandText = "spDeleteTienda";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@tiendaId", id));

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
