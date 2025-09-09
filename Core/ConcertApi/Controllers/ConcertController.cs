using System.Threading.Tasks;
using Azure;
using ConcertApi.Models;
using Json.More;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
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
        public ConcertContext _ctxImpl;
        public ConcertController(ConcertContext ctx)
        {
            _ctxImpl = ctx;
        }
        [HttpGet]
        public ActionResult<List<Concert>> GetConcerts(int pageIndex, int displayNumber)
        {
            return Ok(SqlPage.ToPages(_ctxImpl.Concerts, displayNumber, pageIndex).ToList());
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
        [HttpDelete]
        public async Task<ActionResult> DeleteConcert(string Id)
        {
            var data = _ctxImpl.Concerts.FromSql($"SELECT * FROM Concerts WHERE Id = {Id}");
            if (data is not null)
            {
                try
                {
                    await data.ForEachAsync(obj =>
                {
                    _ctxImpl.Concerts.Remove(obj);
                });
                    return Ok();
                }
                catch
                {
                    return Problem($"failed to iterate over children");
                }
            }
            return Problem($"failed to delete id {Id}");
        }
        [HttpPatch]
        public ActionResult PatchConcert(string id, [FromBody] JsonPatchDocument<Concert> Data)
        {
            var result = _ctxImpl.Concerts.FirstOrDefault(item => item.Id == id);
            if (result is null) return Problem("failed to fetch data");
            Data.ApplyTo(result);
            return Ok(result);
        }
    }

}