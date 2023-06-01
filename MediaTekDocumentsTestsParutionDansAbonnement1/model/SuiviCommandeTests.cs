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
    public class SuiviCommandeTests
    {
        [TestMethod()]
        public void SuiviCommandeTest()
        {
            SuiviCommande test = new SuiviCommande("id", "etape");
            Assert.AreEqual("id", test.Id_suivi);
            Assert.AreEqual("etape", test.Etape);

        }
    }
}