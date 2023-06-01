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
    public class RevueTests
    {
        [TestMethod()]
        public void RevueTest()
        {
            Revue test = new Revue("1", "titre", "image", "idGenre", "genre", "idPublic", "lePublic", "idRayon", "rayon","Periodicite",2);
            Assert.AreEqual("1", test.Id);
            Assert.AreEqual("titre", test.Titre);
            Assert.AreEqual("image", test.Image);
            Assert.AreEqual("idGenre", test.IdGenre);
            Assert.AreEqual("genre", test.Genre);
            Assert.AreEqual("idPublic", test.IdPublic);
            Assert.AreEqual("lePublic", test.Public);
            Assert.AreEqual("idRayon", test.IdRayon);
            Assert.AreEqual("rayon", test.Rayon);
            Assert.AreEqual("Periodicite", test.Periodicite);
            Assert.AreEqual(2, test.DelaiMiseADispo);

        }
    }
}