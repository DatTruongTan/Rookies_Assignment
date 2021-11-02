using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerBE.Models;
using ServerBE.Data;
using Microsoft.AspNetCore.Identity;
using ServerBE.IdentityServer4;
using Microsoft.IdentityModel.Tokens;
using IdentityServer4.Configuration;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Shared.Constants;
using ServerBE.Security.Authorization.Requirements;
using Microsoft.AspNetCore.Identity.UI.Services;
using ServerBE.Services;
using Microsoft.AspNetCore.Authorization;
using ServerBE.Security.Authorization.Handlers;

namespace ServerBE
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
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("RookieConnection")));

            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Authenticate/SignIn";
                config.LogoutPath = "/Authenticate/Logout";
            });

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
               .AddInMemoryIdentityResources(IS4Config.IdentityResources)
               .AddInMemoryApiScopes(IS4Config.ApiScopes)
               .AddInMemoryClients(IS4Config.Clients)
               .AddAspNetIdentity<User>()
               .AddProfileService<IS4Profile>()
               .AddDeveloperSigningCredential();

            services.AddAuthentication()
            .AddLocalApi("Bearer", option =>
            {
                option.ExpectedScope = "ShoppingAPI";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ConstSecurity.BEARER_POLICY, policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireAuthenticatedUser();
                });

                options.AddPolicy(ConstSecurity.ADMIN_ROLE_POLICY, policy =>
                    policy.Requirements.Add(new AdminRoleRequirement()));
            });
            
            services.AddSingleton<IAuthorizationHandler, AdminRoleHandler>();
            services.AddTransient<IFileStorageService, FileStorageService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopping API Scope", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("/connect/token", UriKind.Relative),
                            AuthorizationUrl = new Uri("/connect/authorize", UriKind.Relative),
                            Scopes = new Dictionary<string, string> { { "shoppingAPI", "Shopping API Scope" } }
                        },
                    },
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>{ "shoppingAPI" }
                    }
                });
            });

            services.AddRazorPages();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowOrigins",
            //        builder =>
            //        {
            //            builder.WithOrigins("http://localhost:3001")
            //                .AllowAnyHeader()
            //                .AllowAnyMethod();
            //        });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.OAuthClientId("swagger");
                    c.OAuthClientSecret("secret");
                    c.OAuthUsePkce();
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopping API Scope");
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("AllowOrigins");

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
