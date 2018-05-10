using Microsoft.Exchange.WebServices.Data;
using System;
using System.Net;

namespace EWSExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExchangeProxy proxy = new ExchangeProxy("https://outlook.office365.com/EWS/Exchange.asmx", "email", "password!");
            proxy.ImpersonatedUser = "email";

            //ExchangeProxy proxy = new ExchangeProxy("https://outlook.office365.com/EWS/Exchange.asmx", "emalil", "password");
            //proxy.ImpersonatedUser = "email";

            TestCaseBase test = new TestCaseMessage(proxy);
            test.Execute();
        }

        public static void CreateContact(ExchangeService service)
        {

            // Create the contact.
            Contact contact = new Contact(service);
            contact.GivenName = "John";
            contact.Surname = "Doe";
            contact.FileAsMapping = FileAsMapping.SurnameCommaGivenName;

            // Specify the business address.
            PhysicalAddressEntry address = new PhysicalAddressEntry();
            address.Street = "600 Townsend";
            address.City = "SF";
            address.State = "CA";
            address.PostalCode = "94103";
            address.CountryOrRegion = "United States";
            contact.PhysicalAddresses[PhysicalAddressKey.Business] = address;

            // Make Business Address as Postal Address
            contact.PostalAddressIndex = PhysicalAddressIndex.Business;
            contact.Save();
        }

        public static void GetContact(ExchangeService service)
        {
            Console.WriteLine(service.RequestedServerVersion);
            ItemView view = new ItemView(1);
            FindItemsResults<Item> contactItems = service.FindItems(WellKnownFolderName.Contacts, view);
            foreach (Item item in contactItems)
            {
                Contact contact = item as Contact;
                Console.WriteLine(contact.PostalAddressIndex);
            }
        }
    }
}
