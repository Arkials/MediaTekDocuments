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
    public class LivreTests
    {
        [TestMethod()]
        public void LivreTest()
        {
            Livre test = new Livre("1", "titre", "image", "testIsbn", "auteur", "collection", "idGenre", "genre", "idPublic", "lePublic", "idRayon", "rayon");
            Assert.AreEqual("1", test.Id);
            Assert.AreEqual("titre", test.Titre);
            Assert.AreEqual("image", test.Image);
            Assert.AreEqual("testIsbn", test.Isbn);
            Assert.AreEqual("auteur", test.Auteur);
            Assert.AreEqual("collection", test.Collection);
            Assert.AreEqual("idGenre", test.IdGenre);
            Assert.AreEqual("genre", test.Genre);
            Assert.AreEqual("idPublic", test.IdPublic);
            Assert.AreEqual("lePublic", test.Public);
            Assert.AreEqual("idRayon", test.IdRayon);
            Assert.AreEqual("rayon", test.Rayon);
        }
    }
}