using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MdHack.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MdHack
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "issuer",
                        ValidateAudience = true,
                        ValidAudience = "audience",
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-puper-mega-secret")),
                        ValidateIssuerSigningKey = true
                        
                    };
                });


            services.AddDbContext<AppDb>(options =>
            {
                var connectionString =
                    "Server=(local);Database=MdHack;Trusted_Connection=True;MultipleActiveResultSets=true";
                options.UseSqlServer(connectionString);
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("master", info: new Info
                {
                    Title = "MD Case Hack API",
                    Version = "v1",
                    Description = "General API Declaration",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "AEB IT Team",
                        Email = "yamaoto@mail.ru",
                    },
                    License = new License
                    {
                        Name = "MIT License",
                        Url = "https://opensource.org/licenses/MIT"
                    }
                });
                c.DescribeAllEnumsAsStrings();
                c.CustomSchemaIds(type =>
                {
                    if (type.IsGenericType)
                    {
                        return type.Name.Replace("`1", "") +
                               type.GenericTypeArguments.Aggregate("Of", (a, b) => a + b.Name);
                    }

                    return type.FriendlyId().Replace("[", "Of").Replace("]", "");
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseDeveloperExceptionPage();
            app.UseDefaultFiles();            
            app.UseStaticFiles();
            app.UseCors((builder) => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/master/swagger.json", "MD Case Hack");

                // mapped by ocelot to the governot project swagger
                c.SwaggerEndpoint("/governor/swagger/governor/swagger.json", "MD Case Hack V1");
                c.RoutePrefix = "doc";
                c.DocumentTitle = "Title Documentation";
                c.DocExpansion(DocExpansion.None);
            });


            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                // serviceScope.ServiceProvider.GetService<EventRadarDbContext>().Database.EnsureDeleted();         
                serviceScope.ServiceProvider.GetService<AppDb>().Database.Migrate();
            }
        }
    }
}
