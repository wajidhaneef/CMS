using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CMS.Data;
using CMS.Encryption;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace CMS.AccountAdder
{
    public class AccountController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly CMSDBContext _dbContext;
        public AccountController(ILogger<AccountController> log, CMSDBContext db)
        {
            _logger = log;
            _dbContext = db;
        }


        [FunctionName("GetAccount")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Get All Accounts" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        [Authorize]
        public async Task<IActionResult> GetAccount(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "accounts")] HttpRequest req)
        {
            //log.LogInformation("C# HTTP trigger function processed a request.");
            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            // Read from the database
            try
            {
                var user = await _dbContext.ApplicationUsers.ToListAsync();
                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                return new OkObjectResult(ex);
            }

        }

        //Add User
        [FunctionName("AddAccount")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Add Account" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiParameter(name: "password", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Password** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        [Authorize]
        public async Task<IActionResult> AddAccount(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "add-user")] HttpRequest req)
        {
            //log.LogInformation("C# HTTP trigger function processed a request.");
            string name = req.Query["name"];
            string password = req.Query["password"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            password = password ?? data?.password;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            //Encrypt password
            byte[] salt = PasswordEncryption.GenerateSalt();
            string hashedPassword = PasswordEncryption.HashPassword(password, salt);

            //End - encrypt password
            // Add User
            try
            {
                ApplicationUser user = new()
                {
                    FirstName = name,
                    LastName = name,
                    Email = "wajid.haneef@devsinc.com",
                    Password = hashedPassword,
                    Salt = Encoding.UTF8.GetString(salt)

                };
                await _dbContext.ApplicationUsers.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                return new OkObjectResult(ex);
            }

        }

        //Login
        [FunctionName("LoginUser")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Login" })]
        [OpenApiParameter(name: "userId", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **UserId** parameter")]
        [OpenApiParameter(name: "password", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Password** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        [Authorize]
        public async Task<IActionResult> Login(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "login")] HttpRequest req)
        {
            //log.LogInformation("C# HTTP trigger function processed a request.");
            string name = req.Query["name"];
            string password = req.Query["password"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            password = password ?? data?.password;



            //End - encrypt password
            // Add User
            return new OkObjectResult("LoggedIn");

        }

        // SignUp user
        //Login
        [FunctionName("SignUpUser")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "SignUp" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        [Authorize]
        public async Task<IActionResult> SignUp(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "signup")] HttpRequest req)
        {
            //log.LogInformation("C# HTTP trigger function processed a request.");
            string name = req.Query["name"];
            string password = req.Query["password"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            password = password ?? data?.password;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            //Encrypt password
            byte[] salt = PasswordEncryption.GenerateSalt();
            string hashedPassword = PasswordEncryption.HashPassword(password, salt);

            //End - encrypt password
            // Add User
            try
            {
                ApplicationUser user = new()
                {
                    FirstName = name,
                    LastName = name,
                    Email = "wajid.haneef@devsinc.com",
                    Password = hashedPassword,
                    Salt = Encoding.UTF8.GetString(salt)

                };
                await _dbContext.ApplicationUsers.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                return new OkObjectResult(ex);
            }

        }
    }
}

