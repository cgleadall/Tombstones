using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tombstones.UI.Web.Models;

namespace ModelTests
{
    [TestClass]
    public class PersonBaseSetCombinedNameTests
    {
        [TestMethod]
        public void CanSetPersonBaseNameBasedOnCombinedName_LastNameOnly()
        {
            var person = new PersonBase();
            var setValue = "LASTNAME";

            person.SetCombinedName(setValue);

            Assert.IsTrue(!string.IsNullOrEmpty(person.LastName));

        }

        [TestMethod]
        public void CanGetCombinedNameBySettingPersonBaseNameBasedOnCombinedName_LastNameOnly()
        {
            var person = new PersonBase();
            var setValue = "LASTNAME";

            person.SetCombinedName(setValue);

            Assert.IsTrue(!string.IsNullOrEmpty(person.LastName));

            Assert.AreEqual(setValue, person.CombinedName);

        }

        [TestMethod]
        public void CanSetPersonBaseNamesBasedOnCombinedName_BothNames()
        {
            var person = new PersonBase();
            var setValue = "LASTNAME, FIRSTNAME";

            person.SetCombinedName(setValue);

            Assert.IsTrue(!string.IsNullOrEmpty(person.LastName));
            Assert.IsTrue(!string.IsNullOrEmpty(person.FirstName));
        }

        [TestMethod]
        public void CanGetCombinedNameBySettingPersonBaseNamesBasedOnCombinedName_BothNames()
        {
            var person = new PersonBase();
            var setValue = "LASTNAME, FIRSTNAME";

            person.SetCombinedName(setValue);

            Assert.IsTrue(!string.IsNullOrEmpty(person.LastName));
            Assert.IsTrue(!string.IsNullOrEmpty(person.FirstName));

            Assert.AreEqual(setValue, person.CombinedName);
        }

        [TestMethod]
        public void CanSetPersonBaseNamesBasedOnCombinedName_ThreeNames()
        {
            var person = new PersonBase();
            var firstName = "Firstname";
            var middleName = "Middlename";
            var lastName = "Lastname";
            var setValue = string.Format(@"{0}, {1} {2}", lastName, firstName, middleName);

            person.SetCombinedName(setValue);

            Assert.IsTrue(!string.IsNullOrEmpty(person.LastName));
            Assert.IsTrue(!string.IsNullOrEmpty(person.FirstName));
            Assert.IsTrue(!string.IsNullOrEmpty(person.OtherNames));
        }

        [TestMethod]
        public void CanGetCombinedNameBySettingPersonBaseNamesBasedOnCombinedName_ThreeNames()
        {
            var person = new PersonBase();
            var firstName = "Firstname";
            var middleName = "Middlename";
            var lastName = "Lastname";
            var setValue = string.Format(@"{0}, {1} {2}", lastName, firstName, middleName);

            person.SetCombinedName(setValue);

            Assert.IsTrue(!string.IsNullOrEmpty(person.LastName));
            Assert.IsTrue(!string.IsNullOrEmpty(person.FirstName));
            Assert.IsTrue(!string.IsNullOrEmpty(person.OtherNames));
        }


        [TestMethod]
        public void DoesLastNameGetUpperCasedWhenSettingCombinedName_LastNameOnly()
        {
            var person = new PersonBase();
            var setValue = "Lastname";
            person.SetCombinedName(setValue);

            Assert.IsTrue(!string.IsNullOrEmpty(person.LastName));
            Assert.IsTrue(person.LastName.CompareTo(setValue.ToUpper()) == 0);
        }
    }
}
