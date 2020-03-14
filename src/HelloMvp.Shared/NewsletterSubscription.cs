namespace HelloMvp
{
    using System;

    public class NewsletterSubscription
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? Confirmed { get; set; }
        public DateTime? Cancelled { get; set; }
        public string Language{ get; set; }
    }
}