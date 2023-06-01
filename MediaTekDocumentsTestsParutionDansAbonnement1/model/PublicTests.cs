using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model.Tests
{
    [TestClass()]
    public class PublicTests
    {
        [TestMethod()]
        public void PublicTest()
        {
            Public test = new Public("Id", "Libelle");
            Assert.AreEqual("Id", test.Id);
            Assert.AreEqual("Libelle", test.Libelle);
        }
    }
}