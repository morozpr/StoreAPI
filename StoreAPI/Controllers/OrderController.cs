using StoreAPI.Data;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly SouvenirStoreContext _context;

    public OrderController(SouvenirStoreContext context)
    {
        _context = context;
    }
    
    //GET api/Order
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
    {
        return await _context.Orders.ToListAsync();
    }
    
    //GET api/Order/id
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(Guid id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
        {
            return NotFound();
        }
        return order;
    }
    
    //POST api/Order
    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId}, order);
    }
    
    //PUT api/Order/id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(Guid id, Order order)
    {
        if (id != order.OrderId)
        {
            return BadRequest();
        }

        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    //DELETE api/Order/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var item = await _context.Orders.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}