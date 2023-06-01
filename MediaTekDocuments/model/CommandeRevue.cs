using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier héritant de commande pour gérer les commandes de revue (les abonnements)
    /// </summary>
    public class CommandeRevue : Commande
    {
        /// <summary>
        /// Date de fin d'abonnement
        /// </summary>
        public DateTime DateFinAbonnement { get; }

        /// <summary>
        /// Id de la revue
        /// </summary>
        public string IdRevue { get; }

        /// <summary>
        /// Titre de la revue 
        /// </summary>
        public string Titre { get; }

        /// <summary>
        /// Constructeur de la commande de revue (abonnement)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idRevue"></param>
        /// <param name="titre"></param>
        /// <param name="dateCommande"></param>
        /// <param name="dateFinAbonnement"></param>
        /// <param name="montant"></param>
        public CommandeRevue(string id,string idRevue, string titre, DateTime dateCommande, DateTime dateFinAbonnement, double montant) : base(id, dateCommande, montant)
        {
            this.DateFinAbonnement = dateFinAbonnement;
            this.IdRevue = idRevue;
            this.Titre = titre;
        }
    }
}
