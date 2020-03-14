using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Collector.StackExchangeRedis;
using OpenTelemetry.Trace.Configuration;
using StackExchange.Redis;
using Prometheus;

namespace HelloMvp.MarketingService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddHealthChecks();
            services.AddHostedService<EmailSender>();

            var connection = ConnectionMultiplexer.Connect(Configuration.GetServiceHostname("Redis"));
            services.AddSingleton(connection);

            services.AddOpenTelemetry((TracerBuilder b) =>
            {
                b.AddRequestCollector();
                b.UseZipkin(o => 
                {
                    o.ServiceName = "marketing"; 
                    o.Endpoint = new Uri("http://zipkin:9411/api/v2/spans");
                });

                b.AddCollector(t =>
                {
                    var collector = new StackExchangeRedisCallsCollector(t);
                    connection.RegisterProfiler(collector.GetProfilerSessionsFactory());
                    return collector;
                });
            });
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHttpMetrics();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapMetrics();

                endpoints.MapHealthChecks("/healthz");
                endpoints.MapGrpcService<MarketingServiceImpl>();
            });
        }
    }
}
