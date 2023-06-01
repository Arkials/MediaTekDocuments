using System;
using System.Windows.Forms;
using MediaTekDocuments.view;
using MediaTekDocuments.model;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace SpecFlowMediatekDocument.Features
{
    [Binding]
    public class MediatekDocumentsSteps
    {

        private readonly FrmMediatek frmMediatek = new FrmMediatek(new Utilisateur("admin", "admin", 1));

        /// <summary>
        /// Fonction specflow simulant un clic sur l'onglet livre
        /// </summary>
        [Given(@"Onglet livres cliqué")]
        public void GivenLivresClique()
        {
            TabControl tabonglet = (TabControl)frmMediatek.Controls["tabOngletsApplication"];
            frmMediatek.Visible = true;
            tabonglet.SelectedTab = (TabPage)tabonglet.Controls["tabLivre"];
        }

        /// <summary>
        /// Fonction specflow simulant un clic sur la textbox livres
        /// </summary>
        [Given(@"la textbox livres est cliqué")]
        public void GivenTxtBoxLivresClique()
        {
            frmMediatek.Visible = true;
            TextBox textBoxRecherche = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["txbLivresTitreRecherche"];
            textBoxRecherche.Select();
        }


        /// <summary>
        /// Fonction specflow simulant le remplissage de la textbox livres avec un mot
        /// </summary>
        /// <param name="mot"></param>
        [Given(@"le mot ""(.*)"" est tapé")]
        public void GivenLeMotEstTape(string mot)
        {
            TextBox textBoxRecherche = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["txbLivresTitreRecherche"];
            textBoxRecherche.Text = mot;
        }

        /// <summary>
        /// Fonction specflow simulant le clic d'une ligne de la combobox choisie correspondant à un mot tapé
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="comboBoxChoisie"></param>
        [Given(@"la ligne ""(.*)"" de la combo box ""(.*)"" est cliqué")]
        public void GivenLaLigneDeLaComboBoxEstClique(string mot, string comboBoxChoisie)
        {
            comboBoxChoisie = "cbxLivres"+ comboBoxChoisie;
            ComboBox comboBoxRecherche = (ComboBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls[comboBoxChoisie];
            int motCorrespondant = comboBoxRecherche.FindStringExact(mot);
            comboBoxRecherche.SelectedIndex = motCorrespondant;
        }


        /// <summary>
        /// Fonction specflow testant le résultat obtenu dans la datagridview des livres
        /// </summary>
        /// <param name="nbResultat"></param>
        /// <param name="stringResultat"></param>
        [Then(@"La grille contient (.*) résultat\(s\) dont le 1er est ""(.*)""")]
        public void ThenLaGrilleContientResultatQuiEst(int nbResultat, string stringResultat)
        {
            DataGridView dataGrid = (DataGridView)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["dgvLivresListe"];
            int nombreRows = dataGrid.Rows.Count;
            string valeurRow = dataGrid.Rows[0].Cells[4].Value.ToString();
            Assert.AreEqual(nbResultat, nombreRows);
            Assert.AreEqual(stringResultat, valeurRow);
        }

    }
}
