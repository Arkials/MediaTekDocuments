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
    public class CommandeTests
    {
        [TestMethod()]
        public void CommandeTest()
        {
            Commande test = new Commande("id", new DateTime(2020, 1, 1), 2);
            Assert.AreEqual("id", test.IdPrimaire);
            Assert.AreEqual(new DateTime(2020, 1, 1), test.DateCommande);
            Assert.AreEqual(2, test.Montant);
        }
    }
}