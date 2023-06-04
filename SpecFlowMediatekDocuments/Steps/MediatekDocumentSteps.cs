using System;
using System.Windows.Forms;
using MediaTekDocuments.view;
using MediaTekDocuments.model;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace SpecFlowMediatekFormation.Features
{
    [Binding]
    public class MediatekDocumentsSteps
    {
        private readonly FrmMediatek frmMediatek = new FrmMediatek(new Utilisateur("test", "test", 1));

        [Given(@"Le mot catastrophe est tapé")]
        public void GivenLeMotEstTape()
        {
            TextBox textBoxRecherche = (TextBox)frmMediatek.Controls["txbLivresTitreRecherche"];
            textBoxRecherche.Text = "catastrophe";
        }

        [Then(@"La grille contient un seul résultat qui est Catastrophes au Brésil")]
        public void ThenLaGrilleContientUnSeulResultatQuiEst()
        {
            DataGridView dataGrid = (DataGridView)frmMediatek.Controls["dgvLivresListe"];
            int nombreRows = dataGrid.Rows.Count;
            string? valeurRow = dataGrid.Rows[0].Cells[0].Value.ToString();
            Assert.AreEqual(1, nombreRows);
            Assert.AreEqual("Catastrophes au Brésil", valeurRow);
        }
    }
}
