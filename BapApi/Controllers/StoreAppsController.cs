// https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio
// https://entityframework.net/linq-queries

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BapApi.Models;

namespace BapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreAppsController : ControllerBase
    {
        private readonly StoreAppsContext _context;

        public StoreAppsController(StoreAppsContext context)
        {
            _context = context;
        }

        // GET: api/StoreApps (StoreApps as in StoreAppsController, line 17)
        // Get all the data from the database
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<StoreAppDTO>>> GetStoreApps()
        {
            return await _context.StoreApps.Select(x => StoreAppToDTO(x)).ToListAsync();
        }

        /// <summary> 
        /// DATE: 02/07/2021 
        /// 
        /// 
        /// [1] Async/await
        /// The async modifier is used to specify that a method, lambda expression, or anonymous method 
        /// is asynchronous. If you use this modifier on a method or expression, it's referred to as 
        /// an async method. For more information please look at the following links.
        /// 
        /// The async/await pattern is a syntactic feature of many programming
        /// languages that allows an asynchronous, non-blocking function to be structured in a way 
        /// similar to an ordinary synchronous function. It is semantically related to the concept of a
        /// coroutine and is often implemented using similar techniques, and is primarily intended to 
        /// provide opportunities for the program to execute other code while waiting for a long-running, 
        /// asynchronous task to complete, usually represented by promises or similar data structures.
        /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/async
        /// https://en.wikipedia.org/wiki/Async/await#:~:text=In%20computer%20programming%2C%20the%20async%2Fawait%20pattern%20is%20a,a%20way%20similar%20to%20an%20ordinary%20synchronous%20function.
        /// 
        /// [2] FindAsync(id)
        /// The FindAsync(id) is an entity with the given primary key values. If an entity with the given primary key 
        /// values is being tracked by the context, then it is returned immediately without making a request
        /// to the database. Otherwise, a query is made to the database for an entity with the given primary 
        /// key values and this entity, if found, is attached to the context and returned. If no entity is
        /// found, then null is returned. For more infromation please follow the link.
        /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.findasync?view=efcore-5.0
        /// 
        /// [3] return NotFound()
        /// With API actions, failure to display a friendly error page is unacceptable in a professional application. 
        /// With an API, while not ideal, empty response bodies are far more permissible for many invalid request types. 
        /// Simply returning a 404 status code (with no response body) for an API route that does not exist may provide
        /// the client with enough information to fix their code.
        /// Depending on your requirements, this may be acceptable for many common status codes but it will rarely 
        /// be sufficient for validation failures. If a client passes you invalid data, returning a 400 Bad Request 
        /// is not going to be helpful enough for the client to diagnose the problem. At a minimum, we need to let 
        /// them know which fields are incorrect and ideally, we would return an informative message for each failure.
        /// With ASP.NET Web API.
        /// https://www.devtrends.co.uk/blog/handling-errors-in-asp.net-core-web-api
        /// 
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreAppDTO>> GetStoreApp(int id)
        {
            var storeApp = await _context.StoreApps.FindAsync(id);

            if (storeApp == null)
            {
                return NotFound();
            }

            return StoreAppToDTO(storeApp);
        }

        // GET: api/StoreApps/FirstTen
        // Get the first ten results from the database aftering ordering the data by Id
        [HttpGet("FirstTen")]
        public async Task<ActionResult<IEnumerable<StoreAppDTO>>> GetStoreTopTen()
        {

            var storeTopTen = await _context.StoreApps.Select(x => StoreAppToDTO(x)).Take(10).ToListAsync();

            if (storeTopTen == null)
            {
                return NotFound();
            }
            
            return storeTopTen; 
        }

        // POST: api/StoreApps
        // Add a new record to the database

        // Delete: api/StoreApps/1
        // Delete a single row from the database by Id
        [HttpDelete("{id}")]    // This is setting the request type 
        public async Task<IActionResult> DeleteStoreApps(int id) {
            var storeappitem = await _context.StoreApps.FindAsync(id);
            if (storeappitem == null) {
                return NotFound();
            }

            _context.StoreApps.Remove(storeappitem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DTO helper method. "Production apps typically limit the data that's input and returned using a subset of the model"
        private static StoreAppDTO StoreAppToDTO(StoreApp storeApp) =>
            new StoreAppDTO
            {
                Id = storeApp.Id,
                Name = storeApp.Name,
                Rating = storeApp.Rating,
                People = storeApp.People,
                Category = storeApp.Category,
                Date = storeApp.Date,
                Price = storeApp.Price
            };
    }

}
