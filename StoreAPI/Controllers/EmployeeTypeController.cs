using StoreAPI.Data;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeTypeController : ControllerBase
{
    private readonly SouvenirStoreContext _context;

    public EmployeeTypeController(SouvenirStoreContext context)
    {
        _context = context;
    }
    
    //GET api/EmployeeType
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeType>>> GetUsers()
    {
        return await _context.EmployeeTypes.ToListAsync();
    }
    
    //GET api/EmployeeType/id
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeType>> GetUser(Guid id)
    {
        var user = await _context.EmployeeTypes.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }
        return user;
    }
    
    //POST api/EmployeeType
    [HttpPost]
    public async Task<ActionResult<EmployeeType>> PostUser(EmployeeType user)
    {
        _context.EmployeeTypes.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.EmpTypeId }, user);
    }
    
    //PUT api/EmployeeType/id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(Guid id, EmployeeType user)
    {
        if (id != user.EmpTypeId)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    //DELETE api/Employee/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _context.EmployeeTypes.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        _context.EmployeeTypes.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}