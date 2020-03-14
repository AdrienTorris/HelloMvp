namespace HelloMvp.Client
{
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<OrderState>();

            // Add auth services
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}