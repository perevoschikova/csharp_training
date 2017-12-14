using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allInformation;
        private string allEmails;

        public ContactData()
        {
        }

        public ContactData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode() + LastName.GetHashCode();
        }

        public override string ToString()
        {
            return "FirstName = " + FirstName + "\nLastName = " + LastName;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            var fullNameThis = LastName + FirstName;
            var fullNameOther = other.LastName + other.FirstName;

            return fullNameThis.CompareTo(fullNameOther);
        }

        [Column(Name = "firstname")]
        public string FirstName { get; set; }

        [Column(Name = "lastname")]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                List<ContactData> list = new List<ContactData>();
                list = (from g in db.Contacts
                        where g.Deprecated == "0000-00-00 00:00:00"
                        select g).ToList();                
                return list;
            }
        }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllInformation
        {
            get
            {
                if (allInformation != null)
                {
                    return allInformation;
                }
                else
                {
                    var name = FirstName + " " + LastName + "\r\n";

                    string address = null;
                    if (Address != "")
                    {
                        address += Address + "\r\n";
                    }
                    address += "\r\n";

                    var phones = PhonesForProperties();
                    var emails = EmailsForProperties();

                    var contact = (name + address + phones + emails).Trim();
                    return contact;
                }
            }
            set
            {
                allInformation = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    var emails = "";
                    emails = Email + Email2 + Email2;
                    return emails;
                }
            }
            set
            {
                allEmails = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[() -]", "") + "\r\n";
        }

        public string EmailsForProperties()
        {
            string emails = null;
            if (Email != "")
            {
                emails += Email + "\r\n";
            }
            if (Email2 != "")
            {
                emails += Email2 + "\r\n";
            }
            if (Email3 != "")
            {
                emails += Email3 + "\r\n";
            }
            return emails;
        }

        public string PhonesForProperties()
        {
            string phones = null;
            if (HomePhone == "" && MobilePhone == "" && WorkPhone == "")
            {
                return phones;
            }

            if (HomePhone != "")
            {
                phones += "H: " + HomePhone + "\r\n";
            }
            if (MobilePhone != "")
            {
                phones += "M: " + MobilePhone + "\r\n";
            }
            if (WorkPhone != "")
            {
                phones += "W: " + WorkPhone + "\r\n";
            }
            return phones + "\r\n";
        }

        public List<GroupData> GetGroups()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Groups
                        from gcr in db.GCR.Where(p => p.ContactId == Id && p.GroupId == c.Id)
                        select c).Distinct().ToList();
            }
        }
    }
}