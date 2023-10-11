using StoreAPI.Data;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly SouvenirStoreContext _context;

    public EmployeeController(SouvenirStoreContext context)
    {
        _context = context;
    }
    
    //GET api/Employee
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
    {
        return await _context.Employees.ToListAsync();
    }
    
    //GET api/Employee/id
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(Guid id)
    {
        var user = await _context.Employees.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }
        return user;
    }
    
    //POST api/Employee
    [HttpPost]
    public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
    }
    
    //PUT api/Employee/id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
    {
        if (id != employee.EmployeeId)
        {
            return BadRequest();
        }

        _context.Entry(employee).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    //DELETE api/Employee/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}