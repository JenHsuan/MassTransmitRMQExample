using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using web.Configuration;
using web.Observers;
using web.Consumers;
using web.Contracts;
using System.IO;

namespace web
{
    public class Startup
    {
        private const string RABBITMQ_SECTION_NAME = "RabbitMq";
        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");
            var config = builder.Build();

            services
                .Configure<RabbitMq>(config.GetSection(RABBITMQ_SECTION_NAME))
                .AddOptions<RabbitMq>()
                .Services
                .AddSingleton(provider => provider.GetRequiredService<IOptionsMonitor<RabbitMq>>().CurrentValue);

            services.AddSingleton<BusObserver>();
            services.AddSingleton<SendObserver>();
            services.AddSingleton<ReceiveObserver>();

            services
                .AddMassTransit(x =>
                {
                    x.AddBus(RabbitMqBus.CreateRabbitMqBus);
                    x.AddConsumer<CheckOrderStatusConsumer>();
                    x.AddRequestClient<CheckOrderStatus>();
                })
            .AddHostedService<BusHostService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
