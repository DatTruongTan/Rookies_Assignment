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

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Authenticate/Login";
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

            //services.AddAuthentication(option =>
            //{
            //    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //    .AddJwtBearer(option =>
            //    {
            //        option.SaveToken = true;
            //        option.RequireHttpsMetadata = false;
            //        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidAudience = Configuration["JWT:ValidAudience"],
            //            ValidIssuer = Configuration["JWT:ValidIssuer"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
            //        };
            //    });

            services.AddControllers();
            //services.AddTransient<IAccountRepository, AccountRepository>();

            //services.AddIdentityServer(options =>
            //{
            //    options.Events.RaiseErrorEvents = true;
            //    options.Events.RaiseInformationEvents = true;
            //    options.Events.RaiseFailureEvents = true;
            //    options.Events.RaiseSuccessEvents = true;
            //    options.EmitStaticAudienceClaim = true;
            //})
            //  .AddInMemoryIdentityResources(Config.IdentityResources)
            //  .AddInMemoryApiScopes(Config.ApiScopes)
            //  .AddInMemoryClients(Config.Clients)
            //  .AddAspNetIdentity<User>()
            //  .AddProfileService<CustomProfileService>()
            //  .AddDeveloperSigningCredential();

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //    .AddInMemoryApiScopes(Config.ApiScopes)
            //    .AddInMemoryClients(Config.Clients);

            //services.AddIdentityServer(options =>
            //{
            //    options.Events.RaiseErrorEvents = true;
            //    options.Events.RaiseInformationEvents = true;
            //    options.Events.RaiseFailureEvents = true;
            //    options.Events.RaiseSuccessEvents = true;
            //    options.UserInteraction.LoginUrl = "/Account/Login";
            //    options.UserInteraction.LogoutUrl = "/Account/Logout";
            //    options.Authentication = new AuthenticationOptions()
            //    {
            //        CookieLifetime = TimeSpan.FromHours(10), // ID server cookie timeout set to 10 hours
            //        CookieSlidingExpiration = true
            //    };
            //})
            //    .AddConfigurationStore(options =>
            //    {
            //        options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
            //    })
            //    .AddOperationalStore(options =>
            //    {
            //        options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
            //        options.EnableTokenCleanup = true;
            //    })
            //    .AddAspNetIdentity<IdentityDbContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServerBE", Version = "v1" });
            });

            //services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            //{
            //    options.Authority = "https://localhost:5001";
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = false
            //    };
            //});

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowOrigins",
            //        builder =>
            //        {
            //            builder.WithOrigins("http://localhost:3000")
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServerBE v1"));
            }

            app.UseHttpsRedirection();
            
            //app.UseCors("AllowOrigins");

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
