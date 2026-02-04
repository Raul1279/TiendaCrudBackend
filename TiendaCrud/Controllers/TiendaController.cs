using Azure;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tiendas")]
public class TiendaController : ControllerBase
{
    private readonly ITiendaService _tiendaService;

    public TiendaController(ITiendaService service)
    {
        _tiendaService = service;
    }

    [HttpGet("GetAllTiendas")]
    public async Task<IActionResult> GetAllTiendas()
    {
        IEnumerable<Tienda> tiendas = await _tiendaService.GetAllTiendas();
        return Ok(tiendas);
    }

    [HttpGet("GetTiendaById/{id}")]
    public async Task<IActionResult> GetTiendaById(int id)
    {
        List<Tienda> tienda = await _tiendaService.GetTiendaById(id);
        if (tienda == null)
        {
            return NotFound();
        }

        return Ok(tienda);
    }

    [HttpPost("AddTienda")]
    public async Task<IActionResult> AddTienda(Tienda tienda)
    {
        GenericResponse response = await _tiendaService.AddTienda(tienda);
        return response.Result == 1 ? Ok(response) : BadRequest(response.Message);
    }

    [HttpPut("UpdateTienda/{id}")]
    public async Task<IActionResult> UpdateTienda(int id, Tienda tienda)
    {
        if (id != tienda.tiendaId) {
            return BadRequest();
        } 
        GenericResponse response = await _tiendaService.UpdateTienda(tienda);
        return response.Result == 1 ? Ok(response) : BadRequest(response.Message);
    }

    [HttpDelete("DeleteTienda/{id}")]
    public async Task<IActionResult> DeleteTienda(int id)
    {
        GenericResponse response = await _tiendaService.DeleteTienda(id);
        return response.Result == 1 ? Ok(response) : BadRequest(response.Message);
    }
}
