using AMochika.Application.Interfaces;
using AMochika.Core.Entities;
using AMochika.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AMochika.Presentation.Controller;


[Route("api/[controller]")]
[ApiController]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    // Obtener una compra por su ID
    [HttpGet("purchase/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var purchase = await _purchaseService
            .GetByIdAsync(id);
        
       

        var client = purchase?.Client;
        
        if (purchase == null)
        {
            return NotFound();
        }
        return Ok(purchase);
    }

    // Obtener todas las compras de un cliente específico
    [HttpGet("purchaseByclient/{clientId}")]
    public async Task<IActionResult> GetByClientId(int clientId)
    {
        var purchases = await _purchaseService.GetPurchaseByClientIdAsync(clientId);
        return Ok(purchases);
    }

    // Crear una nueva compra
    [HttpPost]
    public async Task<IActionResult> Create(Purchase purchase)
    {
        await _purchaseService.AddAsync(purchase);
        return CreatedAtAction(nameof(GetById), new { id = purchase.Id }, purchase);
    }

    // Actualizar una compra existente
    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, Purchase purchase)
    {
        if (id != purchase.Id)
        {
            return BadRequest();
        }

        await _purchaseService.UpdateAsync(purchase);
        return NoContent();
    }

    // Eliminar una compra de forma lógica (soft delete)
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _purchaseService.DeleteSoftAsync(id);
        return NoContent();
    }

}