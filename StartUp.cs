using CMS.Data;
using CMS.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: FunctionsStartup(typeof(CMS.StartUp))]
namespace CMS
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            //builder.Services.AddDbContext<CMSDBContext>();
            //Get Connection string
            string connectionString = Environment.GetEnvironmentVariable("CMSString");
            builder.Services.AddDbContext<CMSDBContext>(
              options =>
              {
                  SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString);
              });
            var authority = Environment.GetEnvironmentVariable("Authority");
            var clientId = Environment.GetEnvironmentVariable("ClientId");
            var clientSecret = Environment.GetEnvironmentVariable("ClientSecret");
            var callbackPath = Environment.GetEnvironmentVariable("CallbackPath");

            //builder.Services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x => {
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.Authority = "https://localhost:7209/";
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(clientSecret)),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});
            builder.Services.AddHttpContextAccessor();
            //builder.Services.AddAuthentication();
            //builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie()
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "https://localhost:7209/Account/Login";
                    options.ClientId = clientId;
                    options.ClientSecret = clientSecret;
                    options.CallbackPath = callbackPath;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.ResponseType = "code";

                    options.TokenValidationParameters.NameClaimType = "name";
                    options.TokenValidationParameters.RoleClaimType = "role";

                    options.Scope.Add("wajid");
                    options.SaveTokens = true;
                });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("customers", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("role", "admin");
                    policy.RequireClaim("role", "user");
                });
            });
            // Add middleware to the pipeline
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMvc();
        }

    }
}
