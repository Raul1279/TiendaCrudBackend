using Entities.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/articulos")]
public class ArticuloController : ControllerBase
{
    private readonly IArticuloService _articuloService;

    public ArticuloController(IArticuloService service)
    {
        _articuloService = service;
    }

    [HttpGet("GetAllArticulos")]
    public async Task<IActionResult> GetAllArticulos()
    {
        IEnumerable<Articulo> articulo = await _articuloService.GetAllArticulos();
        return Ok(articulo);
    }

    [HttpGet("GetArticuloById/{id}")]
    public async Task<IActionResult> GetArticuloById(int id)
    {
        List<Articulo> articulo = await _articuloService.GetArticuloById(id);
        if (articulo == null)
        {
            return NotFound();
        }

        return Ok(articulo);
    }

    [HttpPost("AddArticulo")]
    public async Task<IActionResult> AddArticulo(Articulo Articulo)
    {
        GenericResponse response = await _articuloService.AddArticulo(Articulo);
        return response.Result == 1 ? Ok(response) : BadRequest(response.Message);
    }

    [HttpPut("UpdateArticulo/{id}")]
    public async Task<IActionResult> UpdateArticulo(int id, Articulo articulo)
    {
        if (id != articulo.articuloId)
        {
            return BadRequest();
        }
        GenericResponse response = await _articuloService.UpdateArticulo(articulo);
        return response.Result == 1 ? Ok(response) : BadRequest(response.Message);
    }

    [HttpDelete("DeleteArticulo/{id}")]
    public async Task<IActionResult> DeleteArticulo(int id)
    {
        GenericResponse response = await _articuloService.DeleteArticulo(id);
        return response.Result == 1 ? Ok(response) : BadRequest(response.Message);
    }
}
