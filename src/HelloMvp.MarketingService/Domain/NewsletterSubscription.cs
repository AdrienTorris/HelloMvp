namespace HelloMvp.Domain
{
    using System;
    using Google.Protobuf.WellKnownTypes;
    using HelloMvp.Domain.Helpers;

    public class NewsletterSubscription
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime? Confirmed { get; set; }
        public DateTime? Cancelled { get; set; }
        public DateTime LastUpdated { get; set; }
        public string ConfirmationToken { get; set; }
        public string CancellationToken { get; set; }
        public string Language{ get; set; }

        public NewsletterSubscription()
        {
            this.Inserted = DateTime.Now;
            this.LastUpdated = DateTime.Now;
            this.ConfirmationToken = this.GenerateToken();
        }

        public NewsletterSubscription(string email, string language)
        : base()
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (!EmailsHelper.IsEmailFormatValid(email))
            {
                throw new ArgumentException(nameof(email));
            }

            if (string.IsNullOrWhiteSpace(language))
            {
                throw new ArgumentNullException(nameof(language));
            }

            if (language.ToLower().Trim() != "fr" && language.ToLower().Trim() != "en")
            {
                throw new ArgumentOutOfRangeException(nameof(language));
            }

            this.EmailAddress = email.ToLower().Trim();
            this.Language = language.ToLower().Trim();
        }

        private string GenerateToken() =>
            Guid.NewGuid().ToString().Replace("-", String.Empty);

        public MarketingService.NewsletterSubscription ToGrpc()
        {
            var newsletterSubscription = new MarketingService.NewsletterSubscription();
            newsletterSubscription.Email = this.EmailAddress;
            newsletterSubscription.Inserted = Timestamp.FromDateTimeOffset(this.Inserted);
            newsletterSubscription.LastUpdated = Timestamp.FromDateTimeOffset(this.LastUpdated);
            if(this.Confirmed.HasValue){
                newsletterSubscription.Confirmed = Timestamp.FromDateTimeOffset(this.Confirmed.Value);
            }
            if(this.Cancelled.HasValue){
                newsletterSubscription.Cancelled = Timestamp.FromDateTimeOffset(this.Cancelled.Value);
            }
            newsletterSubscription.Language = this.Language;
            return newsletterSubscription;
        }
    }
}