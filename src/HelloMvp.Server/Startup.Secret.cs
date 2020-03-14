namespace HelloMvp.Server
{
    using System;
    using Grpc.Core;
    using Grpc.Net.Client;
    using Microsoft.Extensions.DependencyInjection;

    public partial class Startup
    {
        private void RegisterMarketingGrpcClient(IServiceCollection services, string uri)
        {
            services.AddSingleton<MarketingService.MarketingService.MarketingServiceClient>(s =>
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress(uri, new GrpcChannelOptions()
                {
                    Credentials = ChannelCredentials.Insecure,
                });

                return new MarketingService.MarketingService.MarketingServiceClient(channel);
            });
        }
        private void RegisterProjectGrpcClient(IServiceCollection services, string uri)
        {
            services.AddSingleton<ProjectService.ProjectService.ProjectServiceClient>(s =>
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var channel = GrpcChannel.ForAddress(uri, new GrpcChannelOptions()
                {
                    Credentials = ChannelCredentials.Insecure,
                });

                return new ProjectService.ProjectService.ProjectServiceClient(channel);
            });
        }
    }
}