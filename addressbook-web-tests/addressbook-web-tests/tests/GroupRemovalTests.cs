using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            if (app.Groups.IsGroupNotExist())
            {
                app.Groups.Create(new GroupData("123"));
            }

            app.Groups.Remove(1);
        }                     
    }
}
