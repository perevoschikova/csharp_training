﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager)
            : base(manager)
        {            
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        internal ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(int p, ContactData newdata)
        {
            manager.Navigator.GoToHomePage();
            if (IsContactNotExist())
            {
                Create(new ContactData("ddddd"));
            }
            InitContactModification(p);
            FillContactForm(newdata);
            SubmitContactModification(p);
            ReturnToHomePage();

            return this;
        }

        public ContactHelper Remove(int p)
        {
            manager.Navigator.GoToHomePage();
            if (IsContactNotExist())
            {
                Create(new ContactData("ddddd"));
            }
            InitContactModification(p);
            Delete();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        private bool IsContactNotExist()
        {
            var b = driver.FindElements(By.Name("entry"));
            var c = b.Count == 0;
            return c;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper Delete()
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[3]")).Click();
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
    }
}
