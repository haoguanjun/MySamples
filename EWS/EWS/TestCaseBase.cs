using Microsoft.Exchange.WebServices.Data;
using System;
using System.Net;

namespace EWSExample
{
    public class TestCaseBase
    {
        protected ExchangeProxy proxy = null;
        public TestCaseBase(ExchangeProxy proxy)
        {
            this.proxy = proxy;
        }

        public virtual void Execute()
        { 
        }
    }
}
