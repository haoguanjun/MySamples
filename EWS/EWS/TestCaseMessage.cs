using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;

namespace EWSExample
{
    class TestCaseMessage: TestCaseBase
    {
        public TestCaseMessage(ExchangeProxy proxy)
            : base(proxy)
        { 
        }

        public override void Execute()
        {
            ItemView view = new ItemView(100);
            FindItemsResults<Item> sentItems = this.proxy.ExchangeService.FindItems(WellKnownFolderName.SentItems, view);

            ExtendedPropertyDefinition pdApxIDSave = new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "ApxIDSave", MapiPropertyType.String);
            ExtendedPropertyDefinition pdApxID = new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "ApxID", MapiPropertyType.String);
           
            PropertySet ps = new PropertySet(pdApxIDSave, pdApxID);

            foreach (Item item in sentItems)
            {
                Console.WriteLine(item.Subject);
                item.Load(ps);
                if (item.ExtendedProperties.Count > 0)
                {                    
                    foreach (ExtendedProperty p in item.ExtendedProperties)
                    {
                        Console.WriteLine("{0}={1}", p.PropertyDefinition.Name, p.Value);
                    }
                }

            }

            Console.ReadKey();
        }
    }
}
