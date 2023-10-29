using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Registration.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Registration.Controllers
{
    [Route("api/data")]
    [ApiController]

    public class ApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context; 
        }

        // GET: /<controller>
        [HttpGet]
        public async Task<ActionResult<Person>> Index()
        {
            try
            {
                var sql = "EXEC getPersons"; 
                var result =  await _context.Persons.FromSqlRaw(sql).ToListAsync();
                return Ok(result); 
            } catch(Exception ex)
            {
                return StatusCode(500, "An error occurred while inserting the to-do item." + ex.Message);
            }
        }

        [HttpGet("EditPerson/{name}")]
        public async Task<ActionResult<Person>> PersonByName(string name)
        {
            try
            {
                var sql = "EXEC getPersonByName @Name"; 
                var result = await _context.Persons.FromSqlRaw(sql, new SqlParameter("@Name", name)).ToListAsync(); 
                return Ok(result[0]);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while inserting the to-do item." + ex.Message);
            }
        }

        [HttpGet("GetPersonById/{id}")]
        public async Task<ActionResult<Person>> GetPersonById(int id)
        {
            try
            {
                var sql = "EXEC getPersonById @Id";
                var result = await _context.Persons.FromSqlRaw(sql, new SqlParameter("@Id", id)).ToListAsync();
                return Ok(result[0]);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while inserting the to-do item." + ex.Message);
            }
        }

        //POST
        [HttpPost("create")]
        public async Task<ActionResult<Person>> PostReq([FromBody] Person person) 
        {
            try{
                await _context.Database.ExecuteSqlRawAsync("EXEC InsertPerson @Name, @Age, @Occupation",
                new SqlParameter("@Name", person.Name), new SqlParameter("@Age", person.Age), new SqlParameter("@Occupation", person.Occupation));
                return Ok("Success"); 
            }catch(Exception ex)
            {
                return StatusCode(500, "An error occurred while insserting the to-do item." + ex.Message);
            }
        }

        [HttpPatch("update")]
        public async Task<ActionResult<Person>> PatchReq([FromBody] Person person)
        {
            try
            {
                Console.WriteLine(person.Id); 
                await _context.Database.ExecuteSqlRawAsync("EXEC updatePerson @Id, @Name, @Age, @Occupation",
                new SqlParameter("@Id", person.Id), new SqlParameter("@Name", person.Name), new SqlParameter("@Age", person.Age), new SqlParameter("@Occupation", person.Occupation));
                return Ok("Success");
            } catch(Exception ex)
            {
                return StatusCode(500, "An error occurred while inserting the to-do item." + ex.Message);

            }
           
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<Person>> DeleteReq([FromBody] Person person)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC deletePerson @Id", new SqlParameter("@Id", person.Id));
                return Ok(); 
            }catch(Exception ex)
            {
                return StatusCode(500, "An error occurred while inserting the to-do item." + ex.Message); 
            }
        } 
    }
}

