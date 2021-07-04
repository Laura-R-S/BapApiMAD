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

        /// <summary>
        /// DATE: 04/07/2021
        /// 
        /// HttpGet and HttpPost
        /// [1] HttpGet and HttpPost are both the methods of posting client data or form data to the server
        /// HTTP is a HyperText Transfer Protocol that is designed to send and receive the data between 
        /// client and server using web pages, HTTPGET and HTTPPOST attributes encode request parameters
        /// as key and value pairs in the HTTP request.The HttpGet protocol and the HttpPost protocol
        /// provide backward compatibility. 
        ///
        /// Whenever the POST request for /StoreApps/FirstTen/ is received, the action invoker creates a list
        /// of all methods of the Controller that match the Search action name. In this case, you would
        /// end up with a list of two methods. Immediately, the invoker looks at all of the 
        /// ActionSelectorAttribute instances applied to each method and calls the IsValidForRequest
        /// method on each. If each attribute returns true, then the method is considered valid for the current action.
        /// let us consider one case; when you ask the first method if it can handle a POST request,
        /// it will respond with false because it only handles GET requests.The second method responds
        /// with true because it can handle the POST request, and it is the one selected to handle the action for post request.
        /// While doing this action, no method is found that meets these criteria, the invoker will call the HandleUnknownAction
        /// method on the Controller, supplying the name of the missing action.If more than one action method meeting these
        /// criteria is found, an InvalidOperationException is thrown.
        /// 
        ///         
        /// The Hypertext Transfer Protocol
        /// [2] The Hypertext Transfer Protocol or HTTP is an application layer protocol for distributed, collaborative, hypermedia
        /// information systems.
        /// HTTP is the foundation of data communication for the World Wide Web or WWW, where hypertext documents
        /// include hyperlinks to other resources that the user can easily access.
        /// Development of HTTP was initiated by Tim Berners-Lee at CERN in 1989. Development of early HTTP Requests for Comments(RFCs)
        /// was a coordinated effort by the Internet Engineering Task Force(IETF) and the World Wide Web Consortium(W3C), with work later
        /// moving to the IETF.
        /// https://datatracker.ietf.org/doc/html/rfc2616
        /// https://www.c-sharpcorner.com/blogs/httpget-and-httppost-attributes-in-asp-net-mvc
        /// 
        /// 
        /// Async/Await Pattern
        /// [3] The Async/Await pattern is a syntactic feature of many programming languages that allows an asynchronous,
        /// non-blocking function to be structured in a way similar to an ordinary synchronous function. It is semantically related to the concept
        /// of a coroutine and is often implemented using similar techniques, and is primarily intended to provide opportunities for the program to
        /// execute other code while waiting for a long-running, asynchronous task to complete, usually represented by promises or similar data 
        /// structures. 
        /// 
        /// Asynchrony, in computer programming, refers to the occurrence of events independent of the main program flow and ways to deal with such
        /// events.hese may be outside events such as the arrival of signals, or actions instigated by a program that take place concurrently with
        /// program execution, without the program blocking to wait for results. Asynchronous input or output is an example of the latter cause of 
        /// asynchrony, and lets programs issue. Commands to storage or network devices that service these requests while the processor continues 
        /// executing the program, doing so provides a degree of parallelism.
        /// https://en.wikipedia.org/wiki/Asynchrony_(computer_programming)
        /// 
        /// IEnumerable
        /// [4] IEnumerable in C# is an interface that defines one method, GetEnumerator which returns an IEnumerator interface. This allows readonly
        /// access to a collection, then a collection that implements IEnumerable can be used with a for-each statement.
        /// key points 
        /// a. IEnumerable interface contains the System.Collections.Generic namespace
        /// b. IEnumerable interface is a generic interface which allows looping over generic or non-generic lists
        /// c. IEnumerable interface also works with linq query expression
        /// d. IEnumerable interface Returns an enumerator that iterates through the collection
        https://www.c-sharpcorner.com/UploadFile/0c1bb2/ienumerable-interface-in-C-Sharp/
        /// 
        /// </summary>
        /// <returns></returns>
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





        // Delete: api/StoreApps/1 (Delete a single row from the database by Id)

        // [HttpDelete] is listening for the request content type - delete. 
        [HttpDelete("{id}")]

        // method to delete app form database. A dynamic property (id) is passed into the DeleteStoreApps method and cast to an int 
        public async Task<IActionResult> DeleteStoreApps(int id) {
            // await _context checks item is in database and FindAsync method returns id or null 
            var storeappitem = await _context.StoreApps.FindAsync(id); 
            // conditional statement to determine item was found in database 
            if (storeappitem == null) { 
                // if not found, return NotFound response (Status 404 NotFound)
                return NotFound(); 
            }

            // storesppitem is passed into the remove method and the method performs the action to remove it from the database context 
            _context.StoreApps.Remove(storeappitem); 
            // changes are applied by using SaveChangesAsync()
            await _context.SaveChangesAsync(); 

            // NoContent() sends a no reponse object to the frontend as we are not returning any values 
            return NoContent(); 
        }

        /// <summary>
        /// [1] Data Transfer Objects(DTO)
        /// DTOs provide an efficient way of abstracting domain objects from the presentation layer.
        /// In effect, your layers are correctly separated. If you want to change the presentation 
        /// layer completely, you can continue with the existing application and domain layers. 
        /// Alternatively, you can re-write your domain layer, completely change the database schema, 
        /// entities and O/RM framework, all without changing the presentation layer. This, of course, 
        /// is as long as the contracts (method signatures and DTOs) of your application services remain unchanged.
        /// 
        /// When using DTO in Serialization & Lazy Load Problems you return data(an object) to the presentation layer, 
        /// it's most likely serialized. For example, in a REST API that returns JSON, your object will be serialized
        /// to JSON and sent to the client. Returning an Entity to the presentation layer can be problematic in that regard, 
        /// especially if you are using a relational database and an ORM provider like Entity Framework Core. 
        /// https://docs.abp.io/en/abp/latest/Data-Transfer-Objects#:~:text=%20Data%20Transfer%20Objects%20%201%20Introduction.%20Data,best%20practices%20%26%20suggestions%20that%20you...%20More%20
        /// 
        /// </summary>
        /// <param name="storeApp"></param>
        /// <returns></returns>
        private static StoreAppDTO StoreAppToDTO(StoreApp storeApp) =>
            new StoreAppDTO
            {
                Id       =  storeApp.Id,
                Name     =  storeApp.Name,
                Rating   =  storeApp.Rating,
                People   =  storeApp.People,
                Category =  storeApp.Category,
                Date     =  storeApp.Date,
                Price    =  storeApp.Price
            };
    }

}
