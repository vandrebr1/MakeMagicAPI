using MakeMagic.Data;
using MakeMagic.Filters;
using MakeMagic.HttpClients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using System;

namespace MakeMagic
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
            string connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<DataContext, DataContext>();

            services.Configure<ApiConfig>(Configuration.GetSection(nameof(ApiConfig)));
            services.AddSingleton<IApiConfig>(x => x.GetRequiredService<IOptions<ApiConfig>>().Value);

            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                                                 .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(retryAttempt));


            services.AddHttpClient<IHouseApiClient, HouseApiClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["ApiConfig:BaseUrl"]);
                client.DefaultRequestHeaders.Add("apikey", Configuration["ApiConfig:Token"]);
            }).AddPolicyHandler(retryPolicy);

            services.AddControllers((o) => o.Filters.Add(new ErroResponseExceptionFilter()));

            //Poll policy try 3x/3s

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Make Magic API", Version = "v1", Description = "Character API documentation" });
                c.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MakeMagic v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            UpdateDatabase(app);

        }
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var context = serviceScope.ServiceProvider.GetService<DataContext>())
            {
                context.Database.Migrate();
            }
        }
    }
}
