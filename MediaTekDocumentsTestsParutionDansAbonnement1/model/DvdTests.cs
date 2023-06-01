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
    public class DvdTests
    {
        [TestMethod()]
        public void DvdTest()
        {
            Dvd test = new Dvd("id", "titre", "image",2,"realisateur","synopsis", "idGenre", "genre", "idPublic", "lePublic", "idRayon", "rayon");
            Assert.AreEqual("id", test.Id);
            Assert.AreEqual("titre", test.Titre);
            Assert.AreEqual("image", test.Image);
            Assert.AreEqual(2, test.Duree);
            Assert.AreEqual("realisateur", test.Realisateur);
            Assert.AreEqual("synopsis", test.Synopsis);
            Assert.AreEqual("idGenre", test.IdGenre);
            Assert.AreEqual("genre", test.Genre);
            Assert.AreEqual("idPublic", test.IdPublic);
            Assert.AreEqual("lePublic", test.Public);
            Assert.AreEqual("idRayon", test.IdRayon);
            Assert.AreEqual("rayon", test.Rayon);
        }
    }
}