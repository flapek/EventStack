using EventStack_API.Models;
using NUnit.Framework;
using EventStack_API.UnitTest.Helpers;

namespace EventStack_API.UnitTest.Model
{
    public class OrganizationTest
    {
        private Organization organization;

        [SetUp]
        public void SetUp() => organization = new Organization();
        
        #region Description

        [Test]
        public void Organization_IsDescriptionHasMaximumOfCharacters_False()
        {
            organization.Description = new string('*', 1001);
            Assert.IsFalse((organization as object).isValid("Description", "The maximum number"));
        }

        #endregion

        #region Email

        [TestCase("eventstack@gmail.com")]
        [TestCase("korona14@gmail.com")]
        [TestCase("jestemmama@interia.pl")]
        [TestCase("koxu15@wp.pl")]
        [TestCase("mariuszlepka@gmail.com")]
        [TestCase("izakoox@wp.pl")]
        [TestCase("abrakadabra13@onet.pl")]
        [TestCase("pysznaszamka@wp.pl")]
        [TestCase("arkadiusz_maczynski@02.pl")]
        [TestCase("tomasz1macionczyk@wp.pl")]
        [TestCase("laleczka1962@02.pl")]
        [TestCase("tomasz_sardynski@gmail.com")]
        public void Organization_IsRegexAcceptEmail_True(string email)
        {
            organization.Email = email;
            Assert.IsTrue((organization as object).isValid("Email", "Email must contain"));
        }

        [TestCase("@com")]
        [TestCase("_a@domena.net")]
        [TestCase("tata1@.pl")]
        [TestCase("arkadiusz@-domena.pl")]
        [TestCase("lepkamariusz@_onet.pl")]
        [TestCase("malgosiawróblewska@kórnik.com")]
        [TestCase("tomasz_jajczyk.onet.pl")]
        [TestCase("tomek@interia")]
        [TestCase("mariusz@wp,pl")]
        [TestCase("-gosia@domena.net")]
        [TestCase("kasia1997@pl")]
        public void Organization_IsRegexRejectEmail_False(string email)
        {
            organization.Email = email;
            Assert.IsFalse((organization as object).isValid("Email", "Email must contain"));
        }

        [Test]
        public void Organization_IsEmailRequired_False()
        {
            organization.Email = null;
            Assert.IsFalse((organization as object).isValid("Email", "Email must be set!"));
        }

        [Test]
        public void Organization_IsEmailCanBeNotNull_True()
        {
            organization.Email = "not null";
            Assert.IsTrue((organization as object).isValid("Email", "Email must be set!"));
        }

        [Test]
        public void Organization_IsEmailHasMaximumOfCharacters_False()
        {
            organization.Email = new string('*', 101);
            Assert.IsFalse((organization as object).isValid("Email", "The maximum number"));
        }

        #endregion

        #region Name
        
        [Test]
        public void Organization_IsNameRequired_False()
        {
            organization.Name = null;
            Assert.IsFalse((organization as object).isValid("Name", "Name must be set!"));
        }

        [Test]
        public void Organization_IsNameCanBeNotNull_True()
        {
            organization.Name = "not null";
            Assert.IsTrue((organization as object).isValid("Name", "Name must be set!"));
        }

        [Test]
        public void Organization_IsNameHasMaximumOfCharacters_False()
        {
            organization.Name = new string('*', 101);
            Assert.IsFalse((organization as object).isValid("Name", "The maximum number"));
        }

        #endregion

        #region NIP

        [TestCase("1234567890")]
        public void Organization_IsRegexAcceptNIP_True(string NIP)
        {
            organization.NIP = NIP;
            Assert.IsTrue((organization as object).isValid("NIP", "NIP must contain"));
        }

        [TestCase("a23456789b")]
        public void Organization_IsRegexRejectNIP_False(string NIP)
        {
            organization.NIP = NIP;
            Assert.IsFalse((organization as object).isValid("NIP", "NIP must contain"));
        }

        #endregion

        #region Password

        [TestCase("EventStack123!")]
        [TestCase("#JamesBond007")]
        [TestCase("$GrzesiuSzlac91$")]
        [TestCase("8^2=6Cztery")]
        [TestCase("Koronawirus,2020")]
        [TestCase("JestemLegenda(2007)")]
        public void Organization_IsRegexAcceptPassword_True(string password)
        {
            organization.Password = password;
            Assert.IsTrue((organization as object).isValid("Password", "Password must contain"));
        }

        [TestCase("123456")]
        [TestCase("Admin123")]
        [TestCase("Haslo")]
        [TestCase("00000")]
        [TestCase("asdfghijklm")]
        [TestCase("sexbomb69")]
        public void Organization_IsRegexRejectPassword_False(string password)
        {
            organization.Password = password;
            Assert.IsFalse((organization as object).isValid("Password", "Password must contain"));
        }

        [Test]
        public void Organization_IsPasswordRequired_False()
        {
            organization.Password = null;
            Assert.IsFalse((organization as object).isValid("Password", "Password must be set!"));
        }

        [Test]
        public void Organization_IsPasswordCanBeNotNull_True()
        {
            organization.Password = "not null";
            Assert.IsTrue((organization as object).isValid("Password", "Password must be set!"));            
        }

        [Test]
        public void Organization_IsPasswordHasMaximumOfCharacters_False()
        {
            organization.Password = new string('*', 31);
            Assert.IsFalse((organization as object).isValid("Password", "The maximum number"));            
        }

        #endregion

        #region Regon
        
        [TestCase("123456789")]
        public void Organization_IsRegexAcceptREGON_True(string REGON)
        {
            organization.REGON = REGON;
            Assert.IsTrue((organization as object).isValid("REGON", "REGON must contain"));
        }

        [TestCase("a2345678b")]
        public void Organization_IsRegexRejectREGON_False(string REGON)
        {
            organization.REGON = REGON;
            Assert.IsFalse((organization as object).isValid("REGON", "REGON must contain"));
        }

        #endregion
    }
}