using iParty.Api.Extensions;
using iParty.Api.Infra;
using iParty.Business.Infra;
using iParty.Data.Infra;
using iParty.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace iParty.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomAuthentication();
            services.AddCustomDependencyInjection();
            services.AddAutoMapper(typeof(Startup));
            services.AddCors();
            services.AddControllers();
            services.AddCustomSwagger();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseCustomSwagger();
            app.UseCustomHttp();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
