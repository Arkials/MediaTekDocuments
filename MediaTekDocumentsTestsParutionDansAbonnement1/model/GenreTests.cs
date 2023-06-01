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
    public class GenreTests
    {
        [TestMethod()]
        public void GenreTest()
        {
            Genre test = new Genre("Id", "Libelle");
            Assert.AreEqual("Id", test.Id);
            Assert.AreEqual("Libelle", test.Libelle);

        }
    }
}