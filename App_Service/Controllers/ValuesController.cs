using App_Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Description;

namespace App_Service.Controllers
{
    public class ValuesController : ApiController
    {
        [ResponseType(typeof(Policy_Master))]
        [HttpGet]
        public async Task<IHttpActionResult> GetPolicy()
        {
            var Url = WebConfigurationManager.AppSettings["Azure_URL"]+"GetAllPolicy"+ WebConfigurationManager.AppSettings["Key"];

            //dynamic content = policy;

            CancellationToken cancellationToken;

            string json = JsonConvert.SerializeObject(Formatting.Indented);
            
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
            using (var httpContent = new StringContent(json))
            {
                request.Content = httpContent;

                using (var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false))
                {
                    var resualtList = response.Content.ReadAsAsync<List<Policy_Master>>();
                    return Ok(resualtList.Result);
                }
            }
            
        }
        [ResponseType(typeof(Policy_Master))]
        [HttpPost]
        public async Task<IHttpActionResult> GetPolicyBYID([FromBody]Policy policy)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Url = WebConfigurationManager.AppSettings["Azure_URL"] + "GetPolicyByNumber" + WebConfigurationManager.AppSettings["Key"];
                    dynamic content = policy;
                    CancellationToken cancellationToken;

                    string json = JsonConvert.SerializeObject(content,Formatting.Indented);
                    using (var client = new HttpClient())
                    using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
                    using (var httpContent = new StringContent(json))
                    {
                        request.Content = httpContent;

                        using (var response = await client
                            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                            .ConfigureAwait(false))
                        {
                            var resualtList = response.Content.ReadAsAsync<List<Policy_Master>>();
                            return Ok(resualtList.Result);
                        }
                    }
                }
                else
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Please pass a PolicyNumber in the request body"));
                }
            }
            catch(Exception ex)
            {
                 return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
           

        }
    }
}
