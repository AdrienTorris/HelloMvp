namespace HelloMvp.ProjectService
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public partial class Startup
    {
        private void ConfigureDatabase(IServiceCollection services)
        {
            /*
            services.AddDbContext<ProjectContext>(options => 
            {
                // var filePath = Configuration["Data:Directory"] == null ? "orders.db" : $"{Configuration["Data:Directory"]}/orders.db";
                // options.UseSqlite($"Data Source={filePath}");
                //options.UseSqlServer
            });
            */
        }
    }
}