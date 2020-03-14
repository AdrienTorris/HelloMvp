namespace HelloMvp.MarketingService
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using Grpc.Core;
    using StackExchange.Redis;
    using System;

    public class MarketingServiceImpl : MarketingService.MarketingServiceBase
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly ConnectionMultiplexer multiplexer;

        public MarketingServiceImpl(ConnectionMultiplexer multiplexer)
        {
            this.multiplexer = multiplexer;
        }

        public async override Task<SubscribeToNewsletterReply> SubscribeToNewsletter(SubscribeToNewsletterRequest request, ServerCallContext context)
        {
            if(string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentNullException(nameof(request.Email));
            }

            throw new NotImplementedException();
        }
    }
}