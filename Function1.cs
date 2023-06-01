using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CMS
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "test")] HttpRequest req)
        {
            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
        [FunctionName("hello")]
        public static async Task<IActionResult> Hello([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route ="hello")] HttpRequest request)
        {

            //return new OkObjectResult("Wajid");
            string name = request.Query["name"];

            string requestBody = await new StreamReader (request.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject (requestBody);
            name = name ?? data?.name;
            string responseMessage = string.IsNullOrEmpty(name)
                ? "You have not provided any name" : $"Hello, {name}. Your function has run successfully";
            return new OkObjectResult(responseMessage);
        }
    }
}
