using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;

namespace EWSExample
{
    class TestCasePublicFolder : TestCaseBase
    {
        public TestCasePublicFolder(ExchangeProxy proxy)
            : base(proxy)
        { 
        }

        public override void Execute()
        {
            FolderView view = new FolderView(10);
            FindFoldersResults folders = this.proxy.ExchangeService.FindFolders(WellKnownFolderName.PublicFoldersRoot, view);
            foreach (Folder folder in folders)
            {
                Contact contact = new Contact(this.proxy.ExchangeService);
                contact.GivenName = "Armstrong";
                contact.Surname = "Shi";
                contact.Save(folder.Id);
            }

        }
    }

}
