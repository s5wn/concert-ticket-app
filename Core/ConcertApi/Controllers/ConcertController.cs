using System.Threading.Tasks;
using ConcertApi.Models;
using Json.More;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.VisualBasic;
using Util;

namespace ConcertApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConcertController : ControllerBase
    {
        public static int GET_REQ_COUNT = 10;
        public ConcertContext _ctxImpl;
        public ConcertController(ConcertContext ctx)
        {
            _ctxImpl = ctx;
        }
        [HttpGet]
        public ActionResult<List<Concert>> GetConcerts(int pageIndex, int displayNumber)
        {
            return Ok(SqlPage.ToPages(_ctxImpl.Concerts,displayNumber,pageIndex).ToList());
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