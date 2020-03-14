namespace HelloMvp.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Net;

    [Route("marketing")]
    [ApiController]
    [Authorize]
    public class MarketingController : Controller
    {
        //private readonly MarketingContext db;
        private readonly MarketingService.MarketingService.MarketingServiceClient marketing;

        public MarketingController(/*MarketingContext db,*/ MarketingService.MarketingService.MarketingServiceClient marketing)
        {
           // this.db = db;
            this.marketing = marketing;
        }

        [Route("subscribetonewsletter")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> SubscribeToNewsletterAsync(string mail, string lang)
        {
            if(string.IsNullOrWhiteSpace(mail))
            {
                throw new ArgumentNullException(nameof(mail));
            }
            
            if(string.IsNullOrWhiteSpace(lang))
            {
                throw new ArgumentNullException(nameof(lang));
            }

            try
            {
                var reply = await this.marketing.SubscribeToNewsletterAsync(new MarketingService.SubscribeToNewsletterRequest()
                {
                    Email = mail,
                    Lang = lang
                });
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}