using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Collector.StackExchangeRedis;
using OpenTelemetry.Trace.Configuration;
using Prometheus;
using StackExchange.Redis;

namespace HelloMvp.ProjectService
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddHealthChecks();

            /*var connection = ConnectionMultiplexer.Connect(Configuration.GetServiceHostname("Redis"));
            services.AddSingleton(connection);

            services.AddOpenTelemetry((TracerBuilder b) =>
            {
                b.AddRequestCollector();
                b.UseZipkin(o => 
                {
                    o.ServiceName = "projects"; 
                    o.Endpoint = new Uri("http://zipkin:9411/api/v2/spans");
                });
                b.AddCollector(t =>
                {
                    var collector = new StackExchangeRedisCallsCollector(t);
                    connection.RegisterProfiler(collector.GetProfilerSessionsFactory());
                    return collector;
                });
            });

            ConfigureDatabase(services);*/
        }

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
                endpoints.MapGrpcService<ProjectServiceImpl>();
            });
        }
    }
}