using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allInformation;
        private string allEmails;

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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        public string Id { get; set; }

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
    }
}