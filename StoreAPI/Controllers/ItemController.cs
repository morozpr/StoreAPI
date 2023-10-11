using StoreAPI.Data;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly SouvenirStoreContext _context;

    public ItemController(SouvenirStoreContext context)
    {
        _context = context;
    }
    
    //GET api/Items
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetItem()
    {
        return await _context.Items.ToListAsync();
    }
    
    //GET api/Items/id
    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetItem(Guid id)
    {
        var item = await _context.Items.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }
        return item;
    }
    
    //POST api/Items
    [HttpPost]
    public async Task<ActionResult<Item>> PostItem(Item item)
    {
        _context.Items.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetItem), new { id = item.ItemId}, item);
    }
    
    //PUT api/Items/id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutItem(Guid id, Item item)
    {
        if (id != item.ItemId)
        {
            return BadRequest();
        }

        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    //DELETE api/Item/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(Guid id)
    {
        var item = await _context.Items.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    //dummy endpoint to test the DB connection
    [HttpGet("test")]
    public async Task<ActionResult<IEnumerable<Item>>> Test()
    {
        return await _context.Items.ToListAsync();
    }
}