using AutoMapper;
using BiharEnergy.Core.Domain;
using BiharEnergy.Core.Domain.Accounting;
using BiharEnergy.Core.Domain.CompanyModule;
using BiharEnergy.Core.Domain.CustomerModule;
using BiharEnergy.Core.Domain.ProductModule;
using BiharEnergy.Core.Domain.TdsModule;
using BiharEnergy.Core.Domain.PurchaseInvoiceModule;
using BiharEnergy.Core.Domain.SalesInvoiceModule;
using BiharEnergy.Core.Domain.SupplierModule;
using BiharEnergy.Persistence;
using BiharEnergy.Persistence.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BiharEnergy.WebApp
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
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IGstr1Repository, Gstr1Repository>();
            services.AddScoped<IGstr1RepositoryForAccountingUnit, Gstr1RepositoryForAccountingUnit>();
            services.AddScoped<ISalesInvoiceRepository, SalesInvoiceRepository>();
            services.AddScoped<IAccountingUnitRepository, AccountingUnitRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IPurchaseInvoiceRepository, PurchaseInvoiceRepository>();
            services.AddScoped<IAccountingUnitRepository, AccountingUnitRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ITdsRepository, TdsRepository>();

            services.AddScoped<IQueryModelDatabase, QueryModelDatabase>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //            services.AddDbContext<ApplicationDbContext>(
            //               options => options.UseInMemoryDatabase()
            //            );

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration.GetSection("Auth0Settings").GetSection("Host").Value;
                options.Audience = Configuration.GetSection("Auth0Settings").GetSection("Audience").Value;
            });



            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("Default"))
            );

            services.AddAutoMapper();
            services.AddMediatR();

            services.AddMvc();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                if (!serviceScope.ServiceProvider.GetService<ApplicationDbContext>().AllMigrationsApplied())
                {
                    serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
                }

                // serviceScope.ServiceProvider.GetService<ApplicationDbContext>().EnsureSeeded();


            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //TODO: Uncomment
                // app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

                // app.Use(async (context, next) =>
                // {
                //     if (context.Request.IsHttps)
                //     {
                //         await next();
                //     }
                //     else
                //     {
                //         var withHttps = "https://" + context.Request.Host + context.Request.Path;
                //         context.Response.Redirect(withHttps);
                //     }
                // });


            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    // spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
