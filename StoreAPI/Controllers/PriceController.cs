using StoreAPI.Data;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PriceController : ControllerBase
{
    private readonly SouvenirStoreContext _context;

    public PriceController(SouvenirStoreContext context)
    {
        _context = context;
    }
    
    //GET api/Prices
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Price>>> GetPrices()
    {
        return await _context.Prices.ToListAsync();
    }
    
    //GET api/Prices/id
    [HttpGet("{id}")]
    public async Task<ActionResult<Price>> GetPrices(Guid id)
    {
        var item = await _context.Prices.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }
        return item;
    }
    
    //POST api/Prices
    [HttpPost]
    public async Task<ActionResult<Order>> PostPrice(Price item)
    {
        _context.Prices.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPrices), new { id = item.PriceId}, item);
    }
    
    //PUT api/Prices/id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrderItem(Guid id, Price item)
    {
        if (id != item.PriceId)
        {
            return BadRequest();
        }

        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    //DELETE api/Prices/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrice(Guid id)
    {
        var item = await _context.Prices.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        _context.Prices.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    //dummy endpoint to test the DB connection
    [HttpGet("test")]
    public async Task<ActionResult<IEnumerable<Price>>> Test()
    {
        return await _context.Prices.ToListAsync();
    }
}