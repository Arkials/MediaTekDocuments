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
    public class IdentifiantsTests
    {
        [TestMethod()]
        public void IdentifiantsTest()
        {
            Identifiants test = new Identifiants("Identifiant", "Mdp");
            Assert.AreEqual("Identifiant", test.Identifiant);
            Assert.AreEqual("Mdp", test.MotDePasse);

        }
    }
}