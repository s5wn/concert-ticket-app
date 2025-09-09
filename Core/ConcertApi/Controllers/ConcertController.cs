using System.Threading.Tasks;
using ConcertApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.VisualBasic;

namespace ConcertApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConcertController : ControllerBase
    {
        public ConcertContext _ctxImpl;
        public ConcertController(ConcertContext ctx)
        {
            _ctxImpl = ctx;
        }
        [HttpGet]
        public async Task<ActionResult<List<Concert>>> GetConcerts()
        {
            List<Concert> List = await _ctxImpl.Concerts.ToListAsync();
            return Ok(List);
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateConcert(Concert Data)
        {
            // Example: Add the concert to the database
            Data.Id = Guid.NewGuid().ToString();
            _ctxImpl.Concerts.Add(Data);
            try
            {
                await _ctxImpl.SaveChangesAsync();
            }
            catch
            {
                return Problem();
            }
            return Ok(Data.Id);
        }
    }
}