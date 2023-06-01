//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.AspNetCore.Http;
//using Newtonsoft.Json;
//using Microsoft.EntityFrameworkCore;
//using CMS.Data;
//using OpenIddict.Abstractions;
//using Microsoft.AspNetCore.Authorization;
//using CMS.Models;
//using CMS.Encryption;
//using System.Text;

//namespace CMS.AccountAdder
//{
//    public class AccountAdder
//    {
//        private readonly CMSDBContext _dbContext;

//        public AccountAdder(CMSDBContext db)
//        {
//            _dbContext = db;
//        }

//        [FunctionName("GetAccount")]
//        //[Authorize]
//        public async Task<IActionResult> GetAccount(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "accounts")] HttpRequest req)
//        {
//            //log.LogInformation("C# HTTP trigger function processed a request.");
//            string name = req.Query["name"];

//            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//            dynamic data = JsonConvert.DeserializeObject(requestBody);
//            name = name ?? data?.name;

//            string responseMessage = string.IsNullOrEmpty(name)
//                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
//                : $"Hello, {name}. This HTTP triggered function executed successfully.";
            
//            // Read from the database
//            try { var user = await _dbContext.ApplicationUsers.ToListAsync();
//                return new OkObjectResult(user);
//            }
//            catch (Exception ex)
//            {
//                return new OkObjectResult(ex);
//            }
            
//        }

//        //Add User
//        [FunctionName("AddAccount")]
//        //[Authorize]
//        public async Task<IActionResult> AddAccount(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "add-user")] HttpRequest req)
//        {
//            //log.LogInformation("C# HTTP trigger function processed a request.");
//            string name = req.Query["name"];
//            string password = req.Query["password"];
//            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//            dynamic data = JsonConvert.DeserializeObject(requestBody);
//            name = name ?? data?.name;
//            password = password ?? data?.password;

//            string responseMessage = string.IsNullOrEmpty(name)
//                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
//                : $"Hello, {name}. This HTTP triggered function executed successfully.";

//            //Encrypt password
//            byte[] salt = PasswordEncryption.GenerateSalt();
//            string hashedPassword = PasswordEncryption.HashPassword(password, salt);

//            //End - encrypt password
//            // Add User
//            try
//            {
//                ApplicationUser user = new()
//                {
//                    FirstName = name,
//                    LastName = name,
//                    Email = "wajid.haneef@devsinc.com",
//                    Password = hashedPassword,
//                    Salt = Encoding.UTF8.GetString(salt)

//                };
//                await _dbContext.ApplicationUsers.AddAsync(user);
//                await _dbContext.SaveChangesAsync();
//                return new OkObjectResult(user);
//            }
//            catch (Exception ex)
//            {
//                return new OkObjectResult(ex);
//            }

//        }

//        //Login
//        [FunctionName("LoginUser")]
//        [Authorize]
//        public async Task<IActionResult> Login(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "login")] HttpRequest req)
//        {
//            //log.LogInformation("C# HTTP trigger function processed a request.");
//            string name = req.Query["name"];
//            string password = req.Query["password"];
//            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//            dynamic data = JsonConvert.DeserializeObject(requestBody);
//            name = name ?? data?.name;
//            password = password ?? data?.password;

            

//            //End - encrypt password
//            // Add User
//            return new OkObjectResult("LoggedIn");

//        }

//        // SignUp user
//        //Login
//        [FunctionName("SignUpUser")]
//        [Authorize]
//        public async Task<IActionResult> SignUp(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "signup")] HttpRequest req)
//        {
//            //log.LogInformation("C# HTTP trigger function processed a request.");
//            string name = req.Query["name"];
//            string password = req.Query["password"];
//            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//            dynamic data = JsonConvert.DeserializeObject(requestBody);
//            name = name ?? data?.name;
//            password = password ?? data?.password;

//            string responseMessage = string.IsNullOrEmpty(name)
//                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
//                : $"Hello, {name}. This HTTP triggered function executed successfully.";

//            //Encrypt password
//            byte[] salt = PasswordEncryption.GenerateSalt();
//            string hashedPassword = PasswordEncryption.HashPassword(password, salt);

//            //End - encrypt password
//            // Add User
//            try
//            {
//                ApplicationUser user = new()
//                {
//                    FirstName = name,
//                    LastName = name,
//                    Email = "wajid.haneef@devsinc.com",
//                    Password = hashedPassword,
//                    Salt = Encoding.UTF8.GetString(salt)

//                };
//                await _dbContext.ApplicationUsers.AddAsync(user);
//                await _dbContext.SaveChangesAsync();
//                return new OkObjectResult(user);
//            }
//            catch (Exception ex)
//            {
//                return new OkObjectResult(ex);
//            }

//        }
//    }
//}
