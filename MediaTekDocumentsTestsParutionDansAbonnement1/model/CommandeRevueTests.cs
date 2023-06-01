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
    public class CommandeRevueTests
    {
        [TestMethod()]
        public void CommandeRevueTest()
        {
            CommandeRevue test = new CommandeRevue("id", "idRevue", "titre", new DateTime(2020, 1, 1), new DateTime(2020, 1, 2), 3);
            Assert.AreEqual("id", test.IdPrimaire);
            Assert.AreEqual("idRevue", test.IdRevue);
            Assert.AreEqual("titre", test.Titre);
            Assert.AreEqual(new DateTime(2020, 1, 1),test.DateCommande);
            Assert.AreEqual(new DateTime(2020, 1, 2), test.DateFinAbonnement);
            Assert.AreEqual(3, test.Montant);
        }
    }
}