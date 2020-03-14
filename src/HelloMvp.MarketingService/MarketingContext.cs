namespace HelloMvp
{
    using HelloMvp.MarketingService.EntityConfigurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class MarketingContext : DbContext
    {
        internal const string DEFAULT_SCHEMA = "marketing";

        public MarketingContext()
        {
        }

        public MarketingContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Domain.NewsletterSubscription> NewsletterSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NewsletterSubscriptionEntityTypeConfiguration());
        }
    }
}