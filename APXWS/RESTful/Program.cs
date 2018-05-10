
namespace Advent.ApxRestApiExample
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            string result = null;
            using (ApxInternalApiProxy proxy = new ApxInternalApiProxy("apxserver", "api", "api"))
            //using (ApxApiProxyV2 proxy = new ApxApiProxyV2("apxserver","api","api"))
            {
                result = proxy.RunSSRSReport("{\"Parameter\":[{\"name\":\"SessionGuid\",\"primitiveType\":1},{\"name\":\"Portfolios\",\"primitiveType\":1,\"Value\":[{\"dataValue\":\"13f\"}]},{\"name\":\"Date\",\"primitiveType\":2,\"Value\":[{\"dataValue\":\"12/31/2008\"}]},{\"name\":\"ReportingCurrencyCode\",\"primitiveType\":1,\"Value\":[{\"dataValue\":\"us\"}]},{\"name\":\"ClassificationID\",\"primitiveType\":3,\"Value\":[{\"dataValue\":\"-19\"}]},{\"name\":\"ClassificationID2\",\"primitiveType\":3},{\"name\":\"ClassificationID3\",\"primitiveType\":3},{\"name\":\"IncludeUnsupervisedAssets\",\"primitiveType\":5,\"Value\":[{\"dataValue\":\"False\"}]},{\"name\":\"DisplayOption\",\"primitiveType\":5,\"Value\":[{\"dataValue\":\"False\"}]},{\"name\":\"DisplayAnnIncome\",\"primitiveType\":5,\"Value\":[{\"dataValue\":\"False\"}]},{\"name\":\"DisplayAcqDate\",\"primitiveType\":5,\"Value\":[{\"dataValue\":\"False\"}]},{\"name\":\"ReportTitle\",\"primitiveType\":1,\"Value\":[{\"dataValue\":\"Portfolio Appraisal\"}]},{\"name\":\"ShowTaxLotsLumped\",\"primitiveType\":3,\"Value\":[{\"dataValue\":\"1\"}]},{\"name\":\"ShowCurrencyFullPrecision\",\"primitiveType\":3,\"Value\":[{\"dataValue\":\"0\"}]},{\"name\":\"AccruedInterestID\",\"primitiveType\":3,\"Value\":[{\"dataValue\":\"8\"}]},{\"name\":\"YieldOptionID\",\"primitiveType\":3,\"Value\":[{\"dataValue\":\"1\"}]},{\"name\":\"BondCostBasisID\",\"primitiveType\":3,\"Value\":[{\"dataValue\":\"5\"}]},{\"name\":\"MFBasisIncludeReinvest\",\"primitiveType\":3,\"Value\":[{\"dataValue\":\"1\"}]},{\"name\":\"UseSettlementDate\",\"primitiveType\":3,\"Value\":[{\"dataValue\":\"0\"}]},{\"name\":\"ShowCurrentMBSFace\",\"primitiveType\":3,\"Value\":[{\"dataValue\":\"0\"}]},{\"name\":\"ShowCurrentTIPSFace\",\"primitiveType\":3,\"Value\":[{\"dataValue\":\"0\"}]},{\"name\":\"LocaleID\",\"primitiveType\":3,\"Value\":[{\"dataValue\":\"1033\"}]},{\"name\":\"PriceTypeID\",\"primitiveType\":3,\"Value\":[{\"dataValue\":\"1\"}]},{\"name\":\"ShowSecuritySymbol\",\"primitiveType\":1,\"Value\":[{\"dataValue\":\"y\"}]},{\"name\":\"OverridePortfolioSettings\",\"primitiveType\":5,\"Value\":[{\"dataValue\":\"True\"}]},{\"name\":\"ServerURL\",\"primitiveType\":1,\"Value\":[{\"dataValue\":\"http://VMAPXBA9/APX/\"}]}],\"Path\":\"PortfolioAppraisal\",\"ReportID\":\"-8003\",\"ReportName\":\"Portfolio Appraisal (SSRS)\"}");
            }
        }
    }
}