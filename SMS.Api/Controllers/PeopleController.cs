using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMS.Api.Data;
using SMS.Api.Dtos;
using SMS.Api.Models;

namespace SMS.Api.Controllers;

[ApiController]
[Route("api/people")]
public class PeopleController : ControllerBase
{
    private readonly AppDbContext _db;

    public PeopleController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
    {
        var employees = await _db.People
            .OrderBy(e => e.LastName)
            .Select(e => new PersonDto(
                e.Id, e.FirstName, e.LastName, e.Email,
                e.JobTitle, e.HireDate, e.IsActive))
            .ToListAsync();

        return Ok(employees);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PersonDto>> GetById(int id)
    {
        var e = await _db.People.FindAsync(id);
        if (e is null) return NotFound();

        return Ok(new PersonDto(
            e.Id, e.FirstName, e.LastName, e.Email,
            e.JobTitle, e.HireDate, e.IsActive));
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<PersonDto>> DeleteById(int id)
    {
        var e = await _db.People.FindAsync(id);
        if (e is null) return NotFound();

        _db.People.Remove(e);
        await _db.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PersonDto>> Update(PersonDto person)
    {
        var e = await _db.People.FindAsync(person.Id);
        if (e is null) return NotFound();
        
        if (person.FirstName != "") e.FirstName = person.FirstName;
        if (person.LastName != "") e.LastName = person.LastName;
        if (person.Email != "") e.Email = person.Email;
        if (person.JobTitle != "") e.JobTitle = person.JobTitle;
        e.HireDate = person.HireDate;
        e.IsActive = person.IsActive;
        await _db.SaveChangesAsync();
        
        return Ok(person);
    }
    
    

    [HttpPost]
    public async Task<ActionResult<PersonDto>> Create(CreatePersonDto dto)
    {
        var employee = new Person()
        {
            FirstName = dto.FirstName,
            LastName  = dto.LastName,
            Email     = dto.Email,
            JobTitle  = dto.JobTitle,
            HireDate  = dto.HireDate
        };

        _db.People.Add(employee);
        await _db.SaveChangesAsync();

        var result = new PersonDto(
            employee.Id, employee.FirstName, employee.LastName,
            employee.Email, employee.JobTitle, employee.HireDate, employee.IsActive);

        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, result);
    }
}