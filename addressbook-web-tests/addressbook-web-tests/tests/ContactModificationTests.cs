using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("Petya");
            newData.Lastname = "Petrov";

            if (app.Contacts.IsContactNotExist())
            {
                app.Contacts.Create(new ContactData("ddddd"));
            }

            app.Contacts.Modify(1, newData);
        }
    }
}
