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
    public class ExemplaireTests
    {
        [TestMethod()]
        public void ExemplaireTest()
        {
            Exemplaire test = new Exemplaire(1, new DateTime(2000, 1, 1), "Photo", "Etat", "Id");
            Assert.AreEqual(1, test.Numero);
            Assert.AreEqual("Photo", test.Photo);
            Assert.AreEqual("Etat", test.IdEtat);
            Assert.AreEqual("Id", test.Id);
        }
    }
}