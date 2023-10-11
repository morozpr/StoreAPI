using StoreAPI.Data;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : ControllerBase
{
    private readonly SouvenirStoreContext _context;

    public OrderItemsController(SouvenirStoreContext context)
    {
        _context = context;
    }
    
    //GET api/OrdersItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
    {
        return await _context.OrderItems.ToListAsync();
    }
    
    //GET api/OrdersItems/id
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItem>> GetOrderItems(Guid id)
    {
        var item = await _context.OrderItems.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }
        return item;
    }
    
    //POST api/OrderItems
    [HttpPost]
    public async Task<ActionResult<Order>> PostOrderItem(OrderItem item)
    {
        _context.OrderItems.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrderItems), new { id = item.OrderItemsId}, item);
    }
    
    //PUT api/OrderItems/id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrderItem(Guid id, OrderItem item)
    {
        if (id != item.OrderItemsId)
        {
            return BadRequest();
        }

        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    //DELETE api/OrderItems/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItems(Guid id)
    {
        var item = await _context.OrderItems.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        _context.OrderItems.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    //dummy endpoint to test the DB connection
    [HttpGet("test")]
    public async Task<ActionResult<IEnumerable<OrderItem>>> Test()
    {
        return await _context.OrderItems.ToListAsync();
    }
}