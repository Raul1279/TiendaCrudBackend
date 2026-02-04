using Data;
using Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _dbContext;

    public ClienteRepository(AppDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Cliente?> GetByEmailByCliente(string email)
    {
        return await _dbContext.tblCliente
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.email == email);
    }
    public async Task<List<Cliente>> GetAllClientes()
    {
        try
        {
            return await _dbContext.tblCliente
                .FromSqlRaw("EXEC spGetAllClientes")
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting Clientes. Sp: spGetAllClientes", ex);
        }
    }
    public async Task<List<CarritoArticuloDto>> GetArticulosByCliente(int clienteId)
    {
        List<CarritoArticuloDto> result = new List<CarritoArticuloDto>();

        DbConnection connection = _dbContext.Database.GetDbConnection();

        using DbCommand command = connection.CreateCommand();
        command.CommandText = "spGetArticulosByCliente";
        command.CommandType = CommandType.StoredProcedure;

        DbParameter param = command.CreateParameter();
        param.ParameterName = "@clienteId";
        param.Value = clienteId;
        command.Parameters.Add(param);

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();

        using DbDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(new CarritoArticuloDto
            {
                articuloId = reader.GetInt32(0),
                codigo = reader.GetString(1),
                descripcion = reader.GetString(2),
                precio = reader.GetString(3),
                imagenUrl = reader.GetString(4),
                cantidad = reader.GetInt32(5)
            });
        }
        return result;
    }
    public async Task<List<Cliente?>> GetClienteById(int id)
    {
        try
        {
            return await _dbContext.tblCliente
                .FromSqlRaw("EXEC spGetClienteById @clienteId", new SqlParameter("@clienteId", id))
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting Cliente. Sp: GetClienteById", ex);
        }
    }
    public async Task<GenericResponse> AddCliente(Cliente cliente)
    {
        try
        {
            DbConnection connection = _dbContext.Database.GetDbConnection();

            using DbCommand cmd = connection.CreateCommand();

            cmd.CommandText = "spCreateCliente";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@nombre", cliente.nombre));
            cmd.Parameters.Add(new SqlParameter("@apellidos", cliente.apellidos));
            cmd.Parameters.Add(new SqlParameter("@direccion", cliente.direccion));
            cmd.Parameters.Add(new SqlParameter("@email", cliente.email));
            cmd.Parameters.Add(new SqlParameter("@passwordHash", cliente.passwordHash));


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
    public async Task<GenericResponse> AddArticuloToCarrito(AddArticuloCarritoDto dto)
    {
        try
        {
            DbConnection connection = _dbContext.Database.GetDbConnection();

            using DbCommand cmd = connection.CreateCommand();

            cmd.CommandText = "spAddArticuloToCarrito";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@clienteId", dto.clienteId));
            cmd.Parameters.Add(new SqlParameter("@articuloId", dto.articuloId));
            cmd.Parameters.Add(new SqlParameter("@cantidad", dto.cantidad));


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
    public async Task<GenericResponse> PagarCarrito(int clienteId)
    {
        GenericResponse response = new();
        DbConnection connection = _dbContext.Database.GetDbConnection();

        using SqlCommand cmd = new("spPagarCarritoByClienteId", (SqlConnection)connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@clienteId", clienteId);

        await connection.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            response.Result = reader.GetInt32(0);
            response.Message = reader.GetString(1);
        }

        return response;
    }
    public async Task<GenericResponse> UpdateCliente(Cliente cliente)
    {
        try
        {
            DbConnection connection = _dbContext.Database.GetDbConnection();

            using DbCommand cmd = connection.CreateCommand();

            cmd.CommandText = "spUpdateCliente";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@clienteId", cliente.clienteId));
            cmd.Parameters.Add(new SqlParameter("@nombre", cliente.nombre));
            cmd.Parameters.Add(new SqlParameter("@apellidos", cliente.apellidos));
            cmd.Parameters.Add(new SqlParameter("@direccion", cliente.direccion));
            cmd.Parameters.Add(new SqlParameter("@email", cliente.email));
            cmd.Parameters.Add(new SqlParameter("@passwordHash", cliente.passwordHash));

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
    public async Task<GenericResponse> DeleteCliente(int id)
    {
        try
        {
            DbConnection connection = _dbContext.Database.GetDbConnection();

            using DbCommand cmd = connection.CreateCommand();

            cmd.CommandText = "spDeleteCliente";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@clienteId", id));

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
    public async Task<GenericResponse> DeleteArticuloFromCarrito(int clienteId, int articuloId)
    {
        GenericResponse response = new GenericResponse();

        DbConnection connection = _dbContext.Database.GetDbConnection();
        using DbCommand command = connection.CreateCommand();

        command.CommandText = "spDeleteArticuloFromCarrito";
        command.CommandType = CommandType.StoredProcedure;

        DbParameter pCliente = command.CreateParameter();
        pCliente.ParameterName = "@clienteId";
        pCliente.Value = clienteId;
        command.Parameters.Add(pCliente);

        DbParameter pArticulo = command.CreateParameter();
        pArticulo.ParameterName = "@articuloId";
        pArticulo.Value = articuloId;
        command.Parameters.Add(pArticulo);

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            response.Result = reader.GetInt32(0);
            response.Message = reader.GetString(1);
        }
        return response;
    }

}
