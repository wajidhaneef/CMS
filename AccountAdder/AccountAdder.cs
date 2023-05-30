using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using OpenIddict.Abstractions;
using CMS.AuthorizationService;
namespace CMS.AccountAdder
{
    public class AccountAdder
    {
        private readonly CMSDBContext _dbContext;

        //OpenIdDict
        private readonly IOpenIddictApplicationManager _applicationManager;
        private readonly IOpenIddictAuthorizationManager _authorizationManager;
        private readonly IOpenIddictScopeManager _scopeManager;
        private readonly AuthorizationService1 _authService;

        public AccountAdder(CMSDBContext dbContext,
            IOpenIddictApplicationManager applicationManager,
      IOpenIddictAuthorizationManager authorizationManager,
      IOpenIddictScopeManager scopeManager,
      AuthorizationService1 authService)
        {
            _dbContext = dbContext;
            _applicationManager = applicationManager;
            _authorizationManager = authorizationManager;
            _scopeManager = scopeManager;
            _authService = authService;
        }
        
        [FunctionName("AccountAdder")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
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
            try { var user = await _dbContext.Users.ToListAsync();
                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                return new OkObjectResult(ex);
            }
            
        }
    }
}
