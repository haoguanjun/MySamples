using Microsoft.Exchange.WebServices.Data;
using System;
using System.Net;

namespace EWSExample
{
    public class ExchangeProxy
    {
        private ExchangeService exchangeService;

        public ExchangeProxy(string url, string username, string password)
        {
            this.exchangeService = new ExchangeService();
            this.exchangeService.Credentials = new WebCredentials(username, password);
            this.exchangeService.Url = new Uri(url.ToUpper());
        }

        public ExchangeService ExchangeService
        {
            get
            {
                return this.exchangeService;
            }
        }

        public string ImpersonatedUser
        {
            get
            {
                return this.exchangeService.ImpersonatedUserId.Id;
            }
            set
            {
                this.exchangeService.ImpersonatedUserId = new ImpersonatedUserId(ConnectingIdType.SmtpAddress, value);
            }
        }
    }

}
