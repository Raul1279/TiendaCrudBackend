using Azure;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/clientes")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService service)
    {
        _clienteService = service;
    }

    [HttpGet("GetAllClientes")]
    public async Task<IActionResult> GetAllClientes()
    {
        IEnumerable<Cliente> tiendas = await _clienteService.GetAllClientes();
        return Ok(tiendas);
    }

    [HttpGet("GetArticulosByCliente/{clienteId}")]
    public async Task<IActionResult> GetArticulosByCliente(int clienteId)
    {
        IEnumerable<CarritoArticuloDto> articulos = await _clienteService.GetArticulosByCliente(clienteId);
        return Ok(articulos);
    }

    [HttpDelete("DeleteArticuloFromCarrito")]
    public async Task<IActionResult> DeleteArticuloFromCarrito(int clienteId, int articuloId)
    {
        GenericResponse result = await _clienteService.DeleteArticuloFromCarrito(clienteId, articuloId);
        return Ok(result);
    }

    [HttpPost("AddArticuloToCarrito")]
    public async Task<IActionResult> AddArticuloToCarrito(AddArticuloCarritoDto dto)
    {
        GenericResponse response =  await _clienteService.AddArticuloToCarrito(dto);
        return response.Result == 1 ? Ok(response) : BadRequest(response.Message);
    }

    [HttpGet("GetClienteById/{id}")]
    public async Task<IActionResult> GetClienteById(int id)
    {
        List<Cliente> tienda = await _clienteService.GetClienteById(id);
        if (tienda == null)
        {
            return NotFound();
        }

        return Ok(tienda);
    }

    [HttpPost("AddCliente")]
    public async Task<IActionResult> AddCliente(Cliente cliente)
    {
        GenericResponse response = await _clienteService.AddCliente(cliente);
        return response.Result == 1 ? Ok(response) : BadRequest(response.Message);
    }

    [HttpPost("PagarCarrito")]
    public async Task<IActionResult> PagarCarrito([FromBody] PagarCarritoDto dto)
    {
        GenericResponse response = await _clienteService.PagarCarrito(dto.clienteId);
        return response.Result == 1 ? Ok(response) : BadRequest(response.Message);
    }

    [HttpPut("UpdateCliente/{id}")]
    public async Task<IActionResult> UpdateCliente(int id, Cliente cliente)
    {
        if (id != cliente.clienteId)
        {
            return BadRequest();
        }
        GenericResponse response = await _clienteService.UpdateCliente(cliente);
        return response.Result == 1 ? Ok(response) : BadRequest(response.Message);
    }

    [HttpDelete("DeleteCliente/{id}")]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        GenericResponse response = await _clienteService.DeleteCliente(id);
        return response.Result == 1 ? Ok(response) : BadRequest(response.Message);
    }
}

