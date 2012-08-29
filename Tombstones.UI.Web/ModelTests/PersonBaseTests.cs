using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tombstones.UI.Web.Models;

namespace ModelTests
{
    [TestClass]
    public class PersonBaseTests
    {
        [TestMethod]
        public void CanCreateEmptyPersonBase()
        {
            var person = new PersonBase();
            Assert.IsNotNull(person);
            
        }

    }
}
