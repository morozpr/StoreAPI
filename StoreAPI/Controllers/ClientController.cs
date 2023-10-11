using StoreAPI.Data;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly SouvenirStoreContext _context;

    public ClientController(SouvenirStoreContext context)
    {
        _context = context;
    }
    
    //GET api/Client
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetUsers()
    {
        return await _context.Clients.ToListAsync();
    }
    
    //GET api/Client/id
    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetUser(Guid id)
    {
        var user = await _context.Clients.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }
        return user;
    }
    
    //POST api/Client
    [HttpPost]
    public async Task<ActionResult<Employee>> PostUser(Client user)
    {
        _context.Clients.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.ClientId }, user);
    }
    
    //PUT api/Client/id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(Guid id, Client user)
    {
        if (id != user.ClientId)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    //DELETE api/Client/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _context.Clients.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        _context.Clients.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    //dummy endpoint to test the DB connection
    [HttpGet("test")]
    public async Task<ActionResult<IEnumerable<Client>>> Test()
    {
        return await _context.Clients.ToListAsync();
    }
}