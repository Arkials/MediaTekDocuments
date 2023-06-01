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
    public class UtilisateurTests
    {
        [TestMethod()]
        public void UtilisateurTest()
        {
            Utilisateur test = new Utilisateur("nom", "prenom", 1);
            Assert.AreEqual("nom", test.Nom);
            Assert.AreEqual("prenom", test.Prenom);
            Assert.AreEqual(1, test.IdService);
        }
    }
}