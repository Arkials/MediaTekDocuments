using System;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace MediaTekDocuments.view

{
    /// <summary>
    /// Classe d'affichage
    /// </summary>
    public partial class FrmMediatek : Form
    {

        #region Commun
        private readonly FrmMediatekController controller;
        private readonly BindingSource bdgGenres = new BindingSource();
        private readonly BindingSource bdgPublics = new BindingSource();
        private readonly BindingSource bdgRayons = new BindingSource();
        private BindingSource bdgSuiviCommande = new BindingSource();
        private List<SuiviCommande> lesSuiviCommande;


        /// <summary>
        /// Constructeur : création du contrôleur lié à ce formulaire
        /// </summary>
        public FrmMediatek()
        {
            InitializeComponent();
            this.controller = new FrmMediatekController();
            abonnementsBientotTermines();
            
        }


        private void abonnementsBientotTermines()
        {
            List<CommandeRevue> listeAbonnementsBientotTermines = controller.GetFinAbonnement() ;
            string listeImprimee ="";
            StringBuilder bld = new StringBuilder();

            foreach (CommandeRevue commande in listeAbonnementsBientotTermines)
            {
                bld.Append( commande.Titre + " " + commande.DateFinAbonnement + "\n");
            }
            listeImprimee = bld.ToString();
            MessageBox.Show("Attention, ces abonnements arrivent à leur terme : "+"\n"+listeImprimee, "Fin d'abonnements", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Rempli un des 3 combo (genre, public, rayon)
        /// </summary>
        /// <param name="lesCategories">liste des objets de type Genre ou Public ou Rayon</param>
        /// <param name="bdg">bindingsource contenant les informations</param>
        /// <param name="cbx">combobox à remplir</param>
        public void RemplirComboCategorie(List<Categorie> lesCategories, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesCategories;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Si vide, rempli la liste des livres 
        /// </summary>
        public void RemplirListeLivres()
        {
            if (lesLivres.Count == 0)
            {
                lesLivres = controller.GetAllLivres();
            }
        }
        /// <summary>
        /// Si vide, rempli la liste des DVDs 
        /// </summary>
        public void RemplirListeDvds()
        {
            if (lesDvd.Count == 0)
            {
                lesDvd = controller.GetAllDvd();
            }
        }

        public void RemplirListeRevues()
        {
            if (lesRevues.Count == 0)
            {
                lesRevues = controller.GetAllRevues();
            }
        }

        /// <summary>
        /// Initialise la liste des suivis de commande et rempli la cbx passée en paramètre
        /// </summary>
        /// <param name="listEtape"></param>
        public void RemplirCbxSuiviCommande(ComboBox listEtape)
        {
            lesSuiviCommande = controller.GetAllSuiviCommande();
            List<SuiviCommande> sortedList;
            sortedList = lesSuiviCommande.OrderBy(o => o.Etape).ToList();
            bdgSuiviCommande.DataSource = sortedList;
            listEtape.DataSource = bdgSuiviCommande;
            listEtape.DisplayMember = "etape";
        }

        /// <summary>
        /// Trie une liste selon le nom de sa propriété
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToSort"></param>
        /// <param name="PropertyName"></param>
        /// <returns></returns>
        public List<T> TriListePropriete<T>(List<T> listToSort, string PropertyName)
        {
            List<T> sortedList;
            sortedList = listToSort.OrderBy(o => o.GetType().GetProperty(PropertyName).GetValue(o)).ToList();
            return sortedList;
        }
        /// <summary>
        /// Mets à jour les sources d'un datagridview
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listeAffichable"></param>
        /// <param name="bdgSource"></param>
        /// <param name="dgv"></param>
        private void UpdateDgv<T>(List<T> listeAffichable, BindingSource bdgSource, DataGridView dgv)
        {
            bdgSource.DataSource = listeAffichable;
            dgv.DataSource = bdgSource;
        }

        private string RecupValeurMaxIdCommande<T>(List<T> listeCommande, string numeroDebut)
        {
            Console.WriteLine(listeCommande.Count);
            if (listeCommande.Count > 0)
            {
               PropertyInfo IdPrimaire = typeof(T).GetProperty("IdPrimaire");
                int idMax= listeCommande.Max<T>(o => int.Parse((string)IdPrimaire.GetValue(o)));
                Console.WriteLine(idMax);
                string nvlIdMaxString = (idMax+1).ToString();
                string nvlId = numeroDebut.Remove(5 - nvlIdMaxString.Length, nvlIdMaxString.Length) + nvlIdMaxString;

                return nvlId;
            }
            else
            {
                return numeroDebut;
            }
        }

        #endregion

        #region Onglet Livres
        private readonly BindingSource bdgLivresListe = new BindingSource();
        private List<Livre> lesLivres = new List<Livre>();

        /// <summary>
        /// Ouverture de l'onglet Livres : 
        /// appel des méthodes pour remplir le datagrid des livres et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabLivres_Enter(object sender, EventArgs e)
        {
            lesLivres = controller.GetAllLivres();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxLivresGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxLivresPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxLivresRayons);
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="livres">liste de livres</param>
        private void RemplirLivresListe(List<Livre> livres)
        {
            bdgLivresListe.DataSource = livres;
            dgvLivresListe.DataSource = bdgLivresListe;
            dgvLivresListe.Columns["isbn"].Visible = false;
            dgvLivresListe.Columns["idRayon"].Visible = false;
            dgvLivresListe.Columns["idGenre"].Visible = false;
            dgvLivresListe.Columns["idPublic"].Visible = false;
            dgvLivresListe.Columns["image"].Visible = false;
            dgvLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLivresListe.Columns["id"].DisplayIndex = 0;
            dgvLivresListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du livre dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbLivresNumRecherche.Text.Equals(""))
            {
                txbLivresTitreRecherche.Text = "";
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbLivresNumRecherche.Text));
                if (livre != null)
                {
                    List<Livre> livres = new List<Livre>() { livre };
                    RemplirLivresListe(livres);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirLivresListeComplete();
                }
            }
            else
            {
                RemplirLivresListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des livres dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbLivresTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbLivresTitreRecherche.Text.Equals(""))
            {
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                txbLivresNumRecherche.Text = "";
                List<Livre> lesLivresParTitre;
                lesLivresParTitre = lesLivres.FindAll(x => x.Titre.ToLower().Contains(txbLivresTitreRecherche.Text.ToLower()));
                RemplirLivresListe(lesLivresParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxLivresGenres.SelectedIndex < 0 && cbxLivresPublics.SelectedIndex < 0 && cbxLivresRayons.SelectedIndex < 0
                    && txbLivresNumRecherche.Text.Equals(""))
                {
                    RemplirLivresListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du livre sélectionné
        /// </summary>
        /// <param name="livre">le livre</param>
        private void AfficheLivresInfos(Livre livre)
        {
            txbLivresAuteur.Text = livre.Auteur;
            txbLivresCollection.Text = livre.Collection;
            txbLivresImage.Text = livre.Image;
            txbLivresIsbn.Text = livre.Isbn;
            txbLivresNumero.Text = livre.Id;
            txbLivresGenre.Text = livre.Genre;
            txbLivresPublic.Text = livre.Public;
            txbLivresRayon.Text = livre.Rayon;
            txbLivresTitre.Text = livre.Titre;
            string image = livre.Image;
            try
            {
                pcbLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbLivresImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre
        /// </summary>
        private void VideLivresInfos()
        {
            txbLivresAuteur.Text = "";
            txbLivresCollection.Text = "";
            txbLivresImage.Text = "";
            txbLivresIsbn.Text = "";
            txbLivresNumero.Text = "";
            txbLivresGenre.Text = "";
            txbLivresPublic.Text = "";
            txbLivresRayon.Text = "";
            txbLivresTitre.Text = "";
            pcbLivresImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresGenres.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Genre genre = (Genre)cbxLivresGenres.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresPublics.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Public lePublic = (Public)cbxLivresPublics.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresRayons.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxLivresRayons.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirLivresListe(livres);
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLivresListe.CurrentCell != null)
            {
                try
                {
                    Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                    AfficheLivresInfos(livre);
                }
                catch
                {
                    VideLivresZones();
                }
            }
            else
            {
                VideLivresInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des livres
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirLivresListeComplete()
        {
            RemplirLivresListe(lesLivres);
            VideLivresZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideLivresZones()
        {
            cbxLivresGenres.SelectedIndex = -1;
            cbxLivresRayons.SelectedIndex = -1;
            cbxLivresPublics.SelectedIndex = -1;
            txbLivresNumRecherche.Text = "";
            txbLivresTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideLivresZones();
            string titreColonne = dgvLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<Livre> sortedList;

            sortedList = TriListePropriete(lesLivres, titreColonne);
            
            RemplirLivresListe(sortedList);
        }
        #endregion

        #region Onglet CommandesLivres
        private readonly BindingSource bdgCommandesLivres = new BindingSource();
        private List<CommandeDocument> LesCommandesLivres = new List<CommandeDocument>();
        private List<CommandeDocument> LesCommandesLivreSelectionne = new List<CommandeDocument>();

        /// <summary>
        /// Ouverture de l'onglet commandesLivres : 
        /// appel des méthodes pour remplir la combobox et les commandes de son 1er livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCommandesLivres_Enter(object sender, EventArgs e)
        {
            RemplirCbxSuiviCommande(cbxSuiviCommandeLivre);
            List<Livre> sortedList;
            sortedList = TriListePropriete(lesLivres, "Id");
            cbxIdLivre.DataSource = sortedList;
            cbxIdLivre.DisplayMember = "id";
            dgvInfosCommandesLivres.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            RemplirCommandesLivreSelectionne(true);
           
        }

        /// <summary>
        /// Mise à jour des commandes si changement de livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxIdLivre_SelectedIndexChanged(object sender, EventArgs e)
        {
            Livre livreselectionne = cbxIdLivre.SelectedItem as Livre;
            PropertyInfo[] proprietesLivreSelectionne = (livreselectionne.GetType()).GetProperties();

            Dictionary<string, string> dictionnaire = new Dictionary<string, string>();

            foreach (PropertyInfo propriete in proprietesLivreSelectionne)
            {
                dictionnaire.Add(propriete.Name, propriete.GetValue(livreselectionne).ToString());
            }
            dictionnaire.Remove("Image");
            dictionnaire.Remove("IdGenre");
            dictionnaire.Remove("IdPublic");
            dictionnaire.Remove("IdRayon");

            dgvInfosLivre.DataSource = new BindingSource(dictionnaire, null);

            dgvInfosLivre.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInfosLivre.ColumnHeadersVisible = false;

            RemplirCommandesLivreSelectionne(false);
        }
        
        /// <summary>
        /// Bouton permettant ajout d'une commande livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouterCommandeLivre_Click(object sender, EventArgs e)
        {

            if (dgvInfosCommandesLivres.SelectedRows.Count > 0)
            {

                dgvInfosCommandesLivres.ClearSelection();
                nudNbExemplaireCommandeLivre.Enabled = true;
                nudNbExemplaireCommandeLivre.Focus();
                nudMontantCommandeLivre.Enabled = true;
                cbxSuiviCommandeLivre.Enabled = false;
            }
            else if ((int)nudNbExemplaireCommandeLivre.Value > 0 && nudMontantCommandeLivre.Value > 0)
            {
                LesCommandesLivres = controller.GetAllCommandesLivres();
                string nvlId = RecupValeurMaxIdCommande(LesCommandesLivres, "00001");                              
                CommandeDocument nvlCommandeLivre = new CommandeDocument(nvlId, DateTime.Today, (double)(nudMontantCommandeLivre.Value), (int)nudNbExemplaireCommandeLivre.Value, ((Livre)cbxIdLivre.SelectedItem).Id, "1", "En cours");                
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir ajouter une commande à " + nvlCommandeLivre.IdLivreDvd + ".", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                controller.CreerCommandeDocument(nvlCommandeLivre);
                RemplirCommandesLivreSelectionne(true);
                }
            }
            else
            {
                MessageBox.Show("Erreur de saisie");
            }

        }
        /// <summary>
        /// Bouton permettant la modification commande livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifierCommandeLivre_Click(object sender, EventArgs e)
        {
            CommandeDocument commandeDocumentSelectionne = (CommandeDocument)dgvInfosCommandesLivres.SelectedRows[0].DataBoundItem;
            string suiviIdCommande = commandeDocumentSelectionne.Suivi_id;
            string suiviIdChangement = ((SuiviCommande)cbxSuiviCommandeLivre.SelectedItem).Id_suivi;
            
            
            if (verificationChangementCommande(suiviIdCommande, suiviIdChangement))
            {
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir modifier " + commandeDocumentSelectionne.IdPrimaire + ".", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                { 
                    Dictionary<string, string> suiviIdChange = new Dictionary<string, string>();
                    suiviIdChange.Add("suivi_id", suiviIdChangement);
                    suiviIdChange.Add("id", commandeDocumentSelectionne.IdPrimaire);
                    controller.ModifierCommandeDocument(suiviIdChange);
                    RemplirCommandesLivreSelectionne(true);
                }
                
            }

        }
        /// <summary>
        /// Bouton permettant la suppresion d'une commande livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimerCommandeLivre_Click(object sender, EventArgs e)
        {
            CommandeDocument commandeDocumentSelectionne = (CommandeDocument)dgvInfosCommandesLivres.SelectedRows[0].DataBoundItem;


            if (int.Parse(commandeDocumentSelectionne.Suivi_id) < 3)
            {
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer " + commandeDocumentSelectionne.IdPrimaire + ".", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Dictionary<string, string> idDocumentSuppr = new Dictionary<string, string>();
                    idDocumentSuppr.Add("id", commandeDocumentSelectionne.IdPrimaire);
                    controller.SupprCommande(idDocumentSuppr);
                    RemplirCommandesLivreSelectionne(true);
                }
            }
            else
            {
                MessageBox.Show("Une commande livrée ou réglée ne peut être supprimée", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            
        }

        /// <summary>
        /// Désactive et réactive les champs d'édition et de création
        /// Si une commande est sélectionné on ne peut modifier que son étape de livraison
        /// Si on veut créer une commande, on n'a pas accès à l'étape        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dvgInfosCommandesLivres_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInfosCommandesLivres.SelectedRows.Count > 0)
            {
                nudMontantCommandeLivre.Enabled = false;
                nudNbExemplaireCommandeLivre.Enabled = false;
                cbxSuiviCommandeLivre.Enabled = true;
            }
            else
            {
                nudMontantCommandeLivre.Enabled = true;
                nudNbExemplaireCommandeLivre.Enabled = true;
                cbxSuiviCommandeLivre.Enabled = false;
            }
        }

        /// <summary>
        /// Tri sur le header cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dvgInfosCommandesLivres_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvInfosCommandesLivres.Columns[e.ColumnIndex].HeaderText;

            LesCommandesLivreSelectionne = TriListePropriete(LesCommandesLivreSelectionne, titreColonne);
            UpdateDgv(LesCommandesLivreSelectionne, bdgCommandesLivres, dgvInfosCommandesLivres);
        }
        
        /// <summary>
        /// Rempli la liste des commandes du livre selectionné et l'affiche dans la dgv 
        /// 
        /// </summary>
        /// <param name="reqApi"></param>
        private void RemplirCommandesLivreSelectionne(bool reqApi)
        {
            if (reqApi)
            {
                LesCommandesLivres = controller.GetAllCommandesLivres();
            }
            LesCommandesLivreSelectionne = LesCommandesLivres.FindAll(c => c.IdLivreDvd == (cbxIdLivre.SelectedItem as Livre).Id);
            UpdateDgv(LesCommandesLivreSelectionne, bdgCommandesLivres, dgvInfosCommandesLivres);
        }

        #endregion

        #region Onglet Dvd
        private readonly BindingSource bdgDvdListe = new BindingSource();
        private List<Dvd> lesDvd = new List<Dvd>();

        /// <summary>
        /// Ouverture de l'onglet Dvds : 
        /// appel des méthodes pour remplir le datagrid des dvd et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabDvd_Enter(object sender, EventArgs e)
        {

            lesDvd = controller.GetAllDvd();
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="Dvds">liste de dvd</param>
        private void RemplirDvdListe(List<Dvd> Dvds)
        {
            bdgDvdListe.DataSource = Dvds;
            dgvDvdListe.DataSource = bdgDvdListe;
            dgvDvdListe.Columns["idRayon"].Visible = false;
            dgvDvdListe.Columns["idGenre"].Visible = false;
            dgvDvdListe.Columns["idPublic"].Visible = false;
            dgvDvdListe.Columns["image"].Visible = false;
            dgvDvdListe.Columns["synopsis"].Visible = false;
            dgvDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDvdListe.Columns["id"].DisplayIndex = 0;
            dgvDvdListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du Dvd dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbDvdNumRecherche.Text.Equals(""))
            {
                txbDvdTitreRecherche.Text = "";
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbDvdNumRecherche.Text));
                if (dvd != null)
                {
                    List<Dvd> Dvd = new List<Dvd>() { dvd };
                    RemplirDvdListe(Dvd);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirDvdListeComplete();
                }
            }
            else
            {
                RemplirDvdListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des Dvd dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbDvdTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbDvdTitreRecherche.Text.Equals(""))
            {
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                txbDvdNumRecherche.Text = "";
                List<Dvd> lesDvdParTitre;
                lesDvdParTitre = lesDvd.FindAll(x => x.Titre.ToLower().Contains(txbDvdTitreRecherche.Text.ToLower()));
                RemplirDvdListe(lesDvdParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxDvdGenres.SelectedIndex < 0 && cbxDvdPublics.SelectedIndex < 0 && cbxDvdRayons.SelectedIndex < 0
                    && txbDvdNumRecherche.Text.Equals(""))
                {
                    RemplirDvdListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du dvd sélectionné
        /// </summary>
        /// <param name="dvd">le dvd</param>
        private void AfficheDvdInfos(Dvd dvd)
        {
            txbDvdRealisateur.Text = dvd.Realisateur;
            txbDvdSynopsis.Text = dvd.Synopsis;
            txbDvdImage.Text = dvd.Image;
            txbDvdDuree.Text = dvd.Duree.ToString();
            txbDvdNumero.Text = dvd.Id;
            txbDvdGenre.Text = dvd.Genre;
            txbDvdPublic.Text = dvd.Public;
            txbDvdRayon.Text = dvd.Rayon;
            txbDvdTitre.Text = dvd.Titre;
            string image = dvd.Image;
            try
            {
                pcbDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbDvdImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du dvd
        /// </summary>
        private void VideDvdInfos()
        {
            txbDvdRealisateur.Text = "";
            txbDvdSynopsis.Text = "";
            txbDvdImage.Text = "";
            txbDvdDuree.Text = "";
            txbDvdNumero.Text = "";
            txbDvdGenre.Text = "";
            txbDvdPublic.Text = "";
            txbDvdRayon.Text = "";
            txbDvdTitre.Text = "";
            pcbDvdImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdGenres.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Genre genre = (Genre)cbxDvdGenres.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdPublics.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Public lePublic = (Public)cbxDvdPublics.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdRayons.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxDvdRayons.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDvdListe.CurrentCell != null)
            {
                try
                {
                    Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
                    AfficheDvdInfos(dvd);
                }
                catch
                {
                    VideDvdZones();
                }
            }
            else
            {
                VideDvdInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des Dvd
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirDvdListeComplete()
        {
            RemplirDvdListe(lesDvd);
            VideDvdZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideDvdZones()
        {
            cbxDvdGenres.SelectedIndex = -1;
            cbxDvdRayons.SelectedIndex = -1;
            cbxDvdPublics.SelectedIndex = -1;
            txbDvdNumRecherche.Text = "";
            txbDvdTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideDvdZones();
            string titreColonne = dgvDvdListe.Columns[e.ColumnIndex].HeaderText;
            List<Dvd> sortedList;

            sortedList = TriListePropriete(lesDvd, titreColonne);            
            RemplirDvdListe(sortedList);
        }
        #endregion


        #region Onglet Commandes Dvd

        private readonly BindingSource bdgCommandesDvd = new BindingSource();
        private List<CommandeDocument> LesCommandesDvd = new List<CommandeDocument>();
        private List<CommandeDocument> LesCommandesDvdSelectionne = new List<CommandeDocument>();


                /// <summary>
        /// Ouverture de l'onglet commandesDvd : 
        /// appel des méthodes pour remplir la combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCommandesDvd_Enter(object sender, EventArgs e)
        {
            LesCommandesDvd = controller.GetAllCommandesDvd();
            RemplirListeDvds();
            RemplirCbxSuiviCommande(cbxSuiviCommandeDvd);
            List<Dvd> sortedList;
            sortedList = lesDvd.OrderBy(o => o.Id).ToList();
            cbxIdDvd.DataSource = sortedList;
            cbxIdDvd.DisplayMember = "id";
            dgvInfosCommandesDvd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

        /// <summary>
        /// Désactive et réactive les champs d'édition et de création
        /// Si une commande est sélectionné on ne peut modifier que son étape de livraison
        /// Si on veut créer une commande, on n'a pas accès à l'étape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dvgInfosCommandesDvd_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInfosCommandesDvd.SelectedRows.Count > 0)
            {
                nudMontantCommandeDvd.Enabled = false;
                nudNbExemplaireCommandeDvd.Enabled = false;
                cbxSuiviCommandeDvd.Enabled = true;
            }
            else
            {
                nudMontantCommandeDvd.Enabled = true;
                nudNbExemplaireCommandeDvd.Enabled = true;
                cbxSuiviCommandeDvd.Enabled = false;
            }

        }

        /// <summary>
        /// Change les commandes selon le DVD sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxIdDvd_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dvd Dvdselectionne = cbxIdDvd.SelectedItem as Dvd;
            PropertyInfo[] proprietesDvdSelectionne = (Dvdselectionne.GetType()).GetProperties();
            Console.WriteLine("dvd");

            Dictionary<string, string> dictionnaire = new Dictionary<string, string>();

            foreach (PropertyInfo propriete in proprietesDvdSelectionne)
            {
                dictionnaire.Add(propriete.Name, propriete.GetValue(Dvdselectionne).ToString());
            }
            dictionnaire.Remove("Image");
            dictionnaire.Remove("IdGenre");
            dictionnaire.Remove("IdPublic");
            dictionnaire.Remove("IdRayon");

            dgvInfosDvd.DataSource = new BindingSource(dictionnaire, null);

            dgvInfosDvd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInfosDvd.ColumnHeadersVisible = false;

            Console.WriteLine((cbxIdDvd.SelectedItem as Dvd).Id);

            RemplirCommandesDvdSelectionne(false);

        }

        /// <summary>
        /// Bouton permettant l'ajout d'une commande DVD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouterCommandeDvd_Click(object sender, EventArgs e)
        {

            if (dgvInfosCommandesDvd.SelectedRows.Count > 0)
            {

                dgvInfosCommandesDvd.ClearSelection();
                nudNbExemplaireCommandeDvd.Enabled = true;
                nudNbExemplaireCommandeDvd.Focus();
                nudMontantCommandeDvd.Enabled = true;
                cbxSuiviCommandeLivre.Enabled = false;
            }
            else if ((int)nudNbExemplaireCommandeDvd.Value > 0 && nudMontantCommandeDvd.Value > 0)
            {
                LesCommandesDvd = controller.GetAllCommandesDvd();
                string nvlId = RecupValeurMaxIdCommande(LesCommandesDvd,"20001");         
                CommandeDocument nvlCommandeDvd = new CommandeDocument(nvlId, DateTime.Today, (double)(nudMontantCommandeDvd.Value), (int)nudNbExemplaireCommandeDvd.Value, ((Dvd)cbxIdDvd.SelectedItem).Id, "1", "En cours");
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir ajouter une commande à " + nvlCommandeDvd.IdLivreDvd + ".", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    controller.CreerCommandeDocument(nvlCommandeDvd);
                    RemplirCommandesDvdSelectionne(true);
                }
            }
            else
            {
                MessageBox.Show("Erreur de saisie");
            }

        }

        /// <summary>
        /// bouton permettant la modification de l'étape de livraison
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifierCommandeDvd_Click(object sender, EventArgs e)
        {
            CommandeDocument commandeDocumentSelectionne = (CommandeDocument)dgvInfosCommandesDvd.SelectedRows[0].DataBoundItem;
            string suiviIdCommande = commandeDocumentSelectionne.Suivi_id;
            Console.WriteLine(suiviIdCommande);
            string suiviIdChangement = ((SuiviCommande)cbxSuiviCommandeDvd.SelectedItem).Id_suivi ;
            Console.WriteLine(suiviIdChangement);

            if (verificationChangementCommande(suiviIdCommande, suiviIdChangement))
            {
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir modifier "+ commandeDocumentSelectionne.IdPrimaire + ".", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Dictionary<string, string> suiviIdChange = new Dictionary<string, string>();
                    suiviIdChange.Add("suivi_id", suiviIdChangement);
                    suiviIdChange.Add("id", commandeDocumentSelectionne.IdPrimaire);
                    controller.ModifierCommandeDocument(suiviIdChange);
                }

                
            }
            RemplirCommandesDvdSelectionne(true);
        }

        /// <summary>
        /// Vérifie selon le changement d'id demandé si possible pou non
        /// </summary>
        /// <param name="suiviIdCommande"></param>
        /// <param name="suiviIdChangement"></param>
        /// <returns></returns>
        private bool verificationChangementCommande(string suiviIdCommande, string suiviIdChangement)
        {
            if (suiviIdCommande != suiviIdChangement)
            {
                int valeurIdCommande = int.Parse(suiviIdCommande);
                int valeurIdChangement = int.Parse(suiviIdChangement);
                if (valeurIdChangement ==4 && valeurIdCommande==3 || valeurIdChangement > valeurIdCommande && valeurIdChangement !=4 )
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("La commande ne peut revenir à un stade antérieure, et une commande doit être livrée avant d'être réglée");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Il n'y a rien à modifier");
                return false;
            }

        }

        /// <summary>
        /// Bouton permettant la suppression d'une commande DVD 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimerCommandeDvd_Click(object sender, EventArgs e)
        {
            CommandeDocument commandeDocumentSelectionne = (CommandeDocument)dgvInfosCommandesDvd.SelectedRows[0].DataBoundItem;

            if(int.Parse(commandeDocumentSelectionne.Suivi_id) < 3)
            {
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer " + commandeDocumentSelectionne.IdPrimaire + ".", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Dictionary<string, string> idDocumentSuppr = new Dictionary<string, string>();
                    idDocumentSuppr.Add("id", commandeDocumentSelectionne.IdPrimaire);
                    controller.SupprCommande(idDocumentSuppr);
                    RemplirCommandesDvdSelectionne(true);
                } 
                
            }
            else
            {
                MessageBox.Show("Une commande livrée ou réglée ne peut être supprimée", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }
        /// <summary>
        /// Rempli la liste (requête API si demandé) et mets à jour la dgv
        /// </summary>
        /// <param name="reqApi"></param>
        private void RemplirCommandesDvdSelectionne(bool reqApi)
        {
            if (reqApi)
            {
                LesCommandesDvd = controller.GetAllCommandesDvd();
            }
            LesCommandesDvdSelectionne = LesCommandesDvd.FindAll(c => c.IdLivreDvd == (cbxIdDvd.SelectedItem as Dvd).Id);
            UpdateDgv(LesCommandesDvdSelectionne, bdgCommandesDvd, dgvInfosCommandesDvd);
        }
        /// <summary>
        /// Tri sur le header cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dvgInfosCommandesDvd_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvInfosCommandesDvd.Columns[e.ColumnIndex].HeaderText;

            LesCommandesDvdSelectionne = TriListePropriete(LesCommandesDvdSelectionne, titreColonne);
            UpdateDgv(LesCommandesDvdSelectionne, bdgCommandesDvd, dgvInfosCommandesDvd);
        }



        #endregion

        #region Onglet Revues
        private readonly BindingSource bdgRevuesListe = new BindingSource();
        private List<Revue> lesRevues = new List<Revue>();

        /// <summary>
        /// Ouverture de l'onglet Revues : 
        /// appel des méthodes pour remplir le datagrid des revues et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabRevues_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxRevuesGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxRevuesPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxRevuesRayons);
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="revues"></param>
        private void RemplirRevuesListe(List<Revue> revues)
        {
            bdgRevuesListe.DataSource = revues;
            dgvRevuesListe.DataSource = bdgRevuesListe;
            dgvRevuesListe.Columns["idRayon"].Visible = false;
            dgvRevuesListe.Columns["idGenre"].Visible = false;
            dgvRevuesListe.Columns["idPublic"].Visible = false;
            dgvRevuesListe.Columns["image"].Visible = false;
            dgvRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRevuesListe.Columns["id"].DisplayIndex = 0;
            dgvRevuesListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage de la revue dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbRevuesNumRecherche.Text.Equals(""))
            {
                txbRevuesTitreRecherche.Text = "";
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbRevuesNumRecherche.Text));
                if (revue != null)
                {
                    List<Revue> revues = new List<Revue>() { revue };
                    RemplirRevuesListe(revues);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirRevuesListeComplete();
                }
            }
            else
            {
                RemplirRevuesListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des revues dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbRevuesTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbRevuesTitreRecherche.Text.Equals(""))
            {
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                txbRevuesNumRecherche.Text = "";
                List<Revue> lesRevuesParTitre;
                lesRevuesParTitre = lesRevues.FindAll(x => x.Titre.ToLower().Contains(txbRevuesTitreRecherche.Text.ToLower()));
                RemplirRevuesListe(lesRevuesParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxRevuesGenres.SelectedIndex < 0 && cbxRevuesPublics.SelectedIndex < 0 && cbxRevuesRayons.SelectedIndex < 0
                    && txbRevuesNumRecherche.Text.Equals(""))
                {
                    RemplirRevuesListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionné
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheRevuesInfos(Revue revue)
        {
            txbRevuesPeriodicite.Text = revue.Periodicite;
            txbRevuesImage.Text = revue.Image;
            txbRevuesDateMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbRevuesNumero.Text = revue.Id;
            txbRevuesGenre.Text = revue.Genre;
            txbRevuesPublic.Text = revue.Public;
            txbRevuesRayon.Text = revue.Rayon;
            txbRevuesTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbRevuesImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbRevuesImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de la reuve
        /// </summary>
        private void VideRevuesInfos()
        {
            txbRevuesPeriodicite.Text = "";
            txbRevuesImage.Text = "";
            txbRevuesDateMiseADispo.Text = "";
            txbRevuesNumero.Text = "";
            txbRevuesGenre.Text = "";
            txbRevuesPublic.Text = "";
            txbRevuesRayon.Text = "";
            txbRevuesTitre.Text = "";
            pcbRevuesImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesGenres.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Genre genre = (Genre)cbxRevuesGenres.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesPublics.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Public lePublic = (Public)cbxRevuesPublics.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesRayons.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxRevuesRayons.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations de la revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRevuesListe.CurrentCell != null)
            {
                try
                {
                    Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
                    AfficheRevuesInfos(revue);
                }
                catch
                {
                    VideRevuesZones();
                }
            }
            else
            {
                VideRevuesInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des revues
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirRevuesListeComplete()
        {
            RemplirRevuesListe(lesRevues);
            VideRevuesZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideRevuesZones()
        {
            cbxRevuesGenres.SelectedIndex = -1;
            cbxRevuesRayons.SelectedIndex = -1;
            cbxRevuesPublics.SelectedIndex = -1;
            txbRevuesNumRecherche.Text = "";
            txbRevuesTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideRevuesZones();
            string titreColonne = dgvRevuesListe.Columns[e.ColumnIndex].HeaderText;
            List<Revue> sortedList;
            sortedList = TriListePropriete(lesRevues, titreColonne);            
            RemplirRevuesListe(sortedList);
        }
        #endregion

        #region Onglet Parutions
        private readonly BindingSource bdgExemplairesListe = new BindingSource();
        private List<Exemplaire> lesExemplaires = new List<Exemplaire>();
        const string ETATNEUF = "00001";

        /// <summary>
        /// Ouverture de l'onglet : récupère le revues et vide tous les champs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabReceptionRevue_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            txbReceptionRevueNumero.Text = "";
        }

        /// <summary>
        /// Remplit le dategrid des exemplaires avec la liste reçue en paramètre
        /// </summary>
        /// <param name="exemplaires">liste d'exemplaires</param>
        private void RemplirReceptionExemplairesListe(List<Exemplaire> exemplaires)
        {
            if (exemplaires != null)
            {
                bdgExemplairesListe.DataSource = exemplaires;
                dgvReceptionExemplairesListe.DataSource = bdgExemplairesListe;
                dgvReceptionExemplairesListe.Columns["idEtat"].Visible = false;
                dgvReceptionExemplairesListe.Columns["id"].Visible = false;
                dgvReceptionExemplairesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvReceptionExemplairesListe.Columns["numero"].DisplayIndex = 0;
                dgvReceptionExemplairesListe.Columns["dateAchat"].DisplayIndex = 1;
            }
            else
            {
                bdgExemplairesListe.DataSource = null;
            }
        }

        /// <summary>
        /// Recherche d'un numéro de revue et affiche ses informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionRechercher_Click(object sender, EventArgs e)
        {
            if (!txbReceptionRevueNumero.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbReceptionRevueNumero.Text));
                if (revue != null)
                {
                    AfficheReceptionRevueInfos(revue);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                }
            }
        }

        /// <summary>
        /// Si le numéro de revue est modifié, la zone de l'exemplaire est vidée et inactive
        /// les informations de la revue son aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbReceptionRevueNumero_TextChanged(object sender, EventArgs e)
        {
            txbReceptionRevuePeriodicite.Text = "";
            txbReceptionRevueImage.Text = "";
            txbReceptionRevueDelaiMiseADispo.Text = "";
            txbReceptionRevueGenre.Text = "";
            txbReceptionRevuePublic.Text = "";
            txbReceptionRevueRayon.Text = "";
            txbReceptionRevueTitre.Text = "";
            pcbReceptionRevueImage.Image = null;
            RemplirReceptionExemplairesListe(null);
            AccesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionnée et les exemplaires
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheReceptionRevueInfos(Revue revue)
        {
            // informations sur la revue
            txbReceptionRevuePeriodicite.Text = revue.Periodicite;
            txbReceptionRevueImage.Text = revue.Image;
            txbReceptionRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbReceptionRevueNumero.Text = revue.Id;
            txbReceptionRevueGenre.Text = revue.Genre;
            txbReceptionRevuePublic.Text = revue.Public;
            txbReceptionRevueRayon.Text = revue.Rayon;
            txbReceptionRevueTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbReceptionRevueImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbReceptionRevueImage.Image = null;
            }
            // affiche la liste des exemplaires de la revue
            AfficheReceptionExemplairesRevue();
        }

        /// <summary>
        /// Récupère et affiche les exemplaires d'une revue
        /// </summary>
        private void AfficheReceptionExemplairesRevue()
        {
            string idDocuement = txbReceptionRevueNumero.Text;
            lesExemplaires = controller.GetExemplairesRevue(idDocuement);
            RemplirReceptionExemplairesListe(lesExemplaires);
            AccesReceptionExemplaireGroupBox(true);
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la réception d'un exemplaire
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesReceptionExemplaireGroupBox(bool acces)
        {
            grpReceptionExemplaire.Enabled = acces;
            txbReceptionExemplaireImage.Text = "";
            txbReceptionExemplaireNumero.Text = "";
            pcbReceptionExemplaireImage.Image = null;
            dtpReceptionExemplaireDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Recherche image sur disque (pour l'exemplaire à insérer)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                // positionnement à la racine du disque où se trouve le dossier actuel
                InitialDirectory = Path.GetPathRoot(Environment.CurrentDirectory),
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbReceptionExemplaireImage.Text = filePath;
            try
            {
                pcbReceptionExemplaireImage.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbReceptionExemplaireImage.Image = null;
            }
        }

        /// <summary>
        /// Enregistrement du nouvel exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireValider_Click(object sender, EventArgs e)
        {
            if (!txbReceptionExemplaireNumero.Text.Equals(""))
            {
                try
                {
                    int numero = int.Parse(txbReceptionExemplaireNumero.Text);
                    DateTime dateAchat = dtpReceptionExemplaireDate.Value;
                    string photo = txbReceptionExemplaireImage.Text;
                    string idEtat = ETATNEUF;
                    string idDocument = txbReceptionRevueNumero.Text;
                    Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocument);
                    if (controller.CreerExemplaire(exemplaire))
                    {
                        AfficheReceptionExemplairesRevue();
                    }
                    else
                    {
                        MessageBox.Show("numéro de publication déjà existant", "Erreur");
                    }
                }
                catch
                {
                    MessageBox.Show("le numéro de parution doit être numérique", "Information");
                    txbReceptionExemplaireNumero.Text = "";
                    txbReceptionExemplaireNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("numéro de parution obligatoire", "Information");
            }
        }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvExemplairesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvReceptionExemplairesListe.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList = new List<Exemplaire>();
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplaires.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = lesExemplaires.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirReceptionExemplairesListe(sortedList);
        }

        /// <summary>
        /// affichage de l'image de l'exemplaire suite à la sélection d'un exemplaire dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceptionExemplairesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReceptionExemplairesListe.CurrentCell != null)
            {
                Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
                string image = exemplaire.Photo;
                try
                {
                    pcbReceptionExemplaireRevueImage.Image = Image.FromFile(image);
                }
                catch
                {
                    pcbReceptionExemplaireRevueImage.Image = null;
                }
            }
            else
            {
                pcbReceptionExemplaireRevueImage.Image = null;
            }
        }

        #endregion

        #region Onglet CommandesRevues

        private readonly BindingSource bdgCommandesRevues = new BindingSource();
        private List<CommandeRevue> LesCommandesRevues = new List<CommandeRevue>();
        private List<CommandeRevue> LesCommandesRevueSelectionne = new List<CommandeRevue>();


        /// <summary>
        /// Ouverture de l'onglet commandesRevue : 
        /// appel des méthodes pour remplir la combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCommandesRevues_Enter(object sender, EventArgs e)
        {
            LesCommandesRevues = controller.GetAllCommandesRevue();
            RemplirListeRevues();
            List<Revue> sortedList;
            sortedList = lesRevues.OrderBy(o => o.Id).ToList();
            cbxIdRevue.DataSource = sortedList;
            cbxIdRevue.DisplayMember = "id";
            dgvInfosCommandesRevue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }


        /// <summary>
        /// Change les commandes selon le DVD sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxIdRevue_SelectedIndexChanged(object sender, EventArgs e)
        {
            Revue Revueselectionne = cbxIdRevue.SelectedItem as Revue;
            PropertyInfo[] proprietesRevueSelectionne = (Revueselectionne.GetType()).GetProperties();
            Console.WriteLine("Revue");

            Dictionary<string, string> dictionnaire = new Dictionary<string, string>();

            foreach (PropertyInfo propriete in proprietesRevueSelectionne)
            {
                dictionnaire.Add(propriete.Name, propriete.GetValue(Revueselectionne).ToString());
            }


            dgvInfosRevue.DataSource = new BindingSource(dictionnaire, null);

            dgvInfosRevue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInfosRevue.ColumnHeadersVisible = false;

            Console.WriteLine((cbxIdRevue.SelectedItem as Revue).Id);

            RemplirCommandesRevueSelectionne(false);

        }

        /// <summary>
        /// Bouton permettant l'ajout d'une commande DVD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouterCommandeRevue_Click(object sender, EventArgs e)
        {
            if (dgvInfosCommandesRevue.SelectedRows.Count > 0)
            {

                dgvInfosCommandesRevue.ClearSelection();
                nudNbExemplaireCommandeRevue.Enabled = true;
                nudNbExemplaireCommandeRevue.Focus();
                nudMontantCommandeRevue.Enabled = true;
                dtpCommandeRevue.Enabled = true;
            }
            else if ((int)nudNbExemplaireCommandeRevue.Value > 0 && nudMontantCommandeRevue.Value > 0 && dtpCommandeRevue.Value>DateTime.Now)
            {
                LesCommandesRevues = controller.GetAllCommandesRevue();
                string nvlId = RecupValeurMaxIdCommande(LesCommandesRevues,"10001");
                CommandeRevue nvlCommandeRevue = new CommandeRevue(nvlId, ((Revue)cbxIdRevue.SelectedItem).Id, ((Revue)cbxIdRevue.SelectedItem).Titre, DateTime.Today, dtpCommandeRevue.Value, (double)(nudMontantCommandeRevue.Value));
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir ajouter une commande à " + nvlCommandeRevue.IdRevue + ".", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    controller.CreerCommandeRevue(nvlCommandeRevue);
                    RemplirCommandesRevueSelectionne(true);
                }
            }
            else
            {
                MessageBox.Show("Erreur de saisie");
            }

        }
             
        /// <summary>
        /// Bouton permettant la suppression d'une commande DVD 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimerCommandeRevue_Click(object sender, EventArgs e)
        {
            CommandeRevue CommandeRevueSelectionne = (CommandeRevue)dgvInfosCommandesRevue.SelectedRows[0].DataBoundItem;

            List <DateTime> datesExemplaire = dateAchatExemplaireRevueSelectionne(CommandeRevueSelectionne.IdRevue);
            Console.WriteLine(CommandeRevueSelectionne.IdRevue);

            bool  aucunExemplaireLie = datesExemplaire.TrueForAll(date=> !ParutionDansAbonnement(CommandeRevueSelectionne.DateCommande, CommandeRevueSelectionne.DateFinAbonnement,date)) ;

            
                if (aucunExemplaireLie)
                {
                    DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer " + CommandeRevueSelectionne.IdPrimaire + ".", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Dictionary<string, string> idDocumentSuppr = new Dictionary<string, string>();
                        idDocumentSuppr.Add("id", CommandeRevueSelectionne.IdPrimaire);
                        controller.SupprCommande(idDocumentSuppr);
                        RemplirCommandesRevueSelectionne(true);
                    }

                }
                else
                {
                    MessageBox.Show("Un exemplaire est lié à cet abonnement", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);                    

                }
            }        

        /// <summary>
        /// Tri sur le header cliqué
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dvgInfosCommandesRevue_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvInfosCommandesRevue.Columns[e.ColumnIndex].HeaderText;

            LesCommandesRevueSelectionne = TriListePropriete(LesCommandesRevueSelectionne, titreColonne);
            UpdateDgv(LesCommandesRevueSelectionne, bdgCommandesRevues, dgvInfosCommandesRevue);
        }

        /// <summary>
        /// Rempli la liste (requête API si demandé) et mets à jour la dgv commandes revues
        /// </summary>
        /// <param name="reqApi"></param>
        private void RemplirCommandesRevueSelectionne(bool reqApi)
        {
            if (reqApi)
            {
                LesCommandesRevues = controller.GetAllCommandesRevue();
            }
            LesCommandesRevueSelectionne = LesCommandesRevues.FindAll(c => c.IdRevue == (cbxIdRevue.SelectedItem as Revue).Id);
            UpdateDgv(LesCommandesRevueSelectionne, bdgCommandesRevues, dgvInfosCommandesRevue);
        }

        private List <DateTime> dateAchatExemplaireRevueSelectionne(string id)
        {
            List<Exemplaire> exemplairesRevue;
            lesExemplaires = controller.GetExemplairesRevue(id);
            exemplairesRevue = lesExemplaires.FindAll(o => o.Id == (id));


            List<DateTime> exemplairesDate = new List<DateTime>();
            exemplairesRevue.ForEach(o => exemplairesDate.Add(o.DateAchat));
            return exemplairesDate;
        }
        public bool ParutionDansAbonnement(DateTime dateCommande, DateTime dateFinAbonnement, DateTime dateAchat)
        {
            Console.WriteLine(dateCommande + " " + dateFinAbonnement + " " + dateAchat);
            if(dateAchat >dateCommande && dateAchat<dateFinAbonnement)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void dgvInfosCommandesRevue_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInfosCommandesRevue.SelectedRows.Count > 0)
            {
                nudMontantCommandeRevue.Enabled = false;
                nudNbExemplaireCommandeRevue.Enabled = false;
                dtpCommandeRevue.Enabled = false;
            }
            else
            {
                nudMontantCommandeRevue.Enabled = true;
                nudNbExemplaireCommandeRevue.Enabled = true;
                dtpCommandeRevue.Enabled = true;
            }
        }

        #endregion


    }
}
