using NUnit.Framework;
using System;
using System.Windows.Forms;
using MediaTekDocuments.view;

namespace TestParutionDansAbonnement
{
    public class TestFonctionParutionDansAbonnement
    {

        [Test]
        public void TestDateAvant()
        {
            DateTime avantAbonnement = new DateTime(2023, 4, 20);
            DateTime dateCommande = new DateTime(2023, 4, 21);
            DateTime dateFinAbonnement = new DateTime(2023, 4, 25);
            bool avecDateAnterieur = ParutionDansAbonnement(dateCommande, dateFinAbonnement, avantAbonnement);
        }
    }
}