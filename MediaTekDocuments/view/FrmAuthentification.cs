using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaTekDocuments.controller;
using MediaTekDocuments.model;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Classe de vue de la form d'authentification
    /// </summary>
    public partial class FrmAuthentification : Form
    {
        private readonly FrmAuthentificationController controller;

        /// <summary>
        /// Constructeur de la form d'authentification
        /// </summary>
        public FrmAuthentification()
        {
            InitializeComponent();
            this.controller = new FrmAuthentificationController();
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            valider();

        }
        private void btnConnexion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                valider();
            }
        }

        private void txtbxMdp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                valider();
            }
        }
        private void valider()
        {
            Identifiants identifiants = new Identifiants(txtbxIdentifiant.Text, txtbxMdp.Text);
            List<Utilisateur> utilisateurLogged = controller.GetUtilisateur(identifiants);
            if (utilisateurLogged.Count == 0)
            {
                MessageBox.Show("Login ou mot de passe incorrect");
            }
            else
            {
                Console.WriteLine(utilisateurLogged[0].IdService);
                if (utilisateurLogged[0].IdService == 4)
                {
                    MessageBox.Show("Le service culturel n'a pas les autorisations suffisantes \npour accéder à l'application.");
                }
                else
                {
                    this.Hide();
                    FrmMediatek frm = new FrmMediatek(utilisateurLogged[0]);
                    frm.ShowDialog();
                    this.Close();
                }

            }
        }
    }
}
