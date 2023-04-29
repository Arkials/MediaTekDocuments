using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.view.Tests
{
    [TestClass()]
    public class FrmMediatekTests
    {
        private readonly DateTime dateAchatAvant = new DateTime(2023, 4, 19);
        private readonly DateTime dateAchatPendant = new DateTime(2023, 4, 25);
        private readonly DateTime dateAchatApres = new DateTime(2023, 5, 1);

        private readonly DateTime dateCommande = new DateTime(2023, 4, 20);
        private readonly DateTime dateFinAbonnement = new DateTime(2023, 4, 30);

        FrmMediatek frmMediatek = new FrmMediatek();


        [TestMethod()]
        public void ParutionDansAbonnementTest()
        {
            Assert.AreEqual(frmMediatek.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateAchatAvant), false);
            Assert.AreEqual(frmMediatek.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateAchatApres), false);
            Assert.AreEqual(frmMediatek.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateAchatPendant), true);
        }
    }
}