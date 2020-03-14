namespace HelloMvp.MarketingService
{
    using System;
    using System.Diagnostics;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using StackExchange.Redis;

    internal class EmailSender : BackgroundService
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly ConnectionMultiplexer multiplexer;
        private readonly ILogger<EmailSender> logger;

        public EmailSender(ConnectionMultiplexer multiplexer, ILogger<EmailSender> logger)
        {
            this.multiplexer = multiplexer;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
        }
    }
}