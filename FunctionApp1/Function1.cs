using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FunctionApp1
{
    public static class Function1
    {
        

        [FunctionName("GetALLPolicy")]
        public static async Task<IActionResult> GetAllPolicy(
            [HttpTrigger(AuthorizationLevel.Function,  "post", Route = null)] HttpRequest req)
        {
            string defaultConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
            var options = new DbContextOptionsBuilder<efcore>();
            options.UseSqlServer(defaultConnection);
            var _context =  new efcore(options.Options);

            var postsArray = await _context.Policy_Master.ToArrayAsync();
            return (ActionResult) new OkObjectResult(postsArray);
                
        }
    }
}
