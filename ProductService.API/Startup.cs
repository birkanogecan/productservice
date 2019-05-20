using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using ProductService.Domain;
using ProductService.Data.Repository.MongoRepository;
using ProductService.Data.Repository.ESRepository;
using ProductService.Data.Entity.ESEntity;
using ESProductService.Data.Repository.ESRepository;

namespace ProductService.API
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
            services.AddMvc();
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IProductMongoRepository, ProductMongoRepository>();
            services.AddScoped<IESRepository<ESProduct>, ProductESRepository>();
            services.AddScoped<IEventStoreDomain, EventStoreDomain>();
            services.AddScoped<IESDomain, ESDomain>();
            services.AddScoped<IMongoDomain, MongoDomain>();
      
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseMiddleware<ExceptionMiddleware>();

        }
    }
}
