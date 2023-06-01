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

        private readonly FrmMediatek frmMediatek = new FrmMediatek(new model.Utilisateur("test","test",1));


        [TestMethod()]
        public void ParutionDansAbonnementTest()
        {
            Assert.AreEqual(false,frmMediatek.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateAchatAvant));
            Assert.AreEqual(false,frmMediatek.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateAchatApres));
            Assert.AreEqual(true,frmMediatek.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateAchatPendant));
        }
    }
}