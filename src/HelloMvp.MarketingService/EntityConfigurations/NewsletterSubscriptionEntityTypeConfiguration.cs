namespace HelloMvp.MarketingService.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    class NewsletterSubscriptionEntityTypeConfiguration
        : IEntityTypeConfiguration<Domain.NewsletterSubscription>
    {
        public void Configure(EntityTypeBuilder<Domain.NewsletterSubscription> entityConfiguration)
        {
            entityConfiguration.ToTable(nameof(NewsletterSubscription).ToLower() + "s", MarketingContext.DEFAULT_SCHEMA);

            entityConfiguration.HasKey(o => o.Id);

            entityConfiguration.Property(o => o.Id)
                .UseHiLo(nameof(NewsletterSubscription).ToLower() + "seq", MarketingContext.DEFAULT_SCHEMA);

            entityConfiguration
                .Property<string>("EmailAddress")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("EmailAddress")
                .HasMaxLength(80)
                .IsRequired();

            entityConfiguration
                .Property<string>("language")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Language")
                .HasMaxLength(5)
                .IsRequired(true);

            entityConfiguration
                .Property<DateTime>("inserted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Inserted")
                .ValueGeneratedOnAdd()
                .HasDefaultValue(DateTime.Now)
               .HasMaxLength(25)
                .IsRequired();

            entityConfiguration
                .Property<DateTime>("lastUpdated")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("LastUpdated")
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValue(DateTime.Now)
               .HasMaxLength(25)
                .IsRequired();

            entityConfiguration
                .Property<DateTime?>("confirmed")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Confirmed")
               .HasMaxLength(25)
                .IsRequired(false);

            entityConfiguration
                .Property<DateTime?>("cancelled")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Cancelled")
               .HasMaxLength(25)
                .IsRequired(false);
        }
    }
}