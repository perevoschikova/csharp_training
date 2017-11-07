using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            if (app.Contacts.IsContactNotExist())
            {
                app.Contacts.Create(new ContactData("ddddd"));
            }
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Remove(0);

            List<ContactData> newContacts = app.Contacts.GetContactList();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}