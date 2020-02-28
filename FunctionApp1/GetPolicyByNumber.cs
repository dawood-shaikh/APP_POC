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
    public static class GetPolicyByNumber
    {
        [FunctionName("GetPolicyByNumber")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            string defaultConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
            var options = new DbContextOptionsBuilder<efcore>();
            options.UseSqlServer(defaultConnection);
            var _context = new efcore(options.Options);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string PolicyNumber = data.PolicyNumber;
            dynamic postsArray = "";
            if (PolicyNumber != null)
            {
                postsArray = _context.Policy_Master.Where(p => p.PolicyNumber == PolicyNumber).ToArray();
            }
            return PolicyNumber != null
                ? (ActionResult)new OkObjectResult(postsArray)
                : new BadRequestObjectResult("Please pass a PolicyNumber in the request body");
        }
    }
}
