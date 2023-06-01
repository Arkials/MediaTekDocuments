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
    public class CommandeDocumentTests
    {
        private readonly CommandeDocument test = new CommandeDocument("id", new DateTime(2000, 1, 1), 2,3,"idLivreDvd","Suivi_id","Etape");
        [TestMethod()]
        public void CommandeDocumentTest()
        {
            Assert.AreEqual("id",test.IdPrimaire);
            Assert.AreEqual(new DateTime(2000, 1, 1),test.DateCommande);
            Assert.AreEqual(2, test.Montant);
            Assert.AreEqual(3, test.NbExemplaire);
            Assert.AreEqual("idLivreDvd", test.IdLivreDvd);
            Assert.AreEqual("Suivi_id", test.Suivi_id);
            Assert.AreEqual("Etape", test.Etape);
        }
    }
}