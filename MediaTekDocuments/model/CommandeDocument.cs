using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe fille de commande qui gère les commandes de DVD et de livres
    /// </summary>
    public class CommandeDocument : Commande
    {
        /// <summary>
        /// Nombre d'exemplaires de la commande
        /// </summary>
            public int NbExemplaire { get;}
        
        /// <summary>
        /// Id du DVD ou du livre
        /// </summary>
            public string IdLivreDvd { get;}

        /// <summary>
        /// Id du suivi de la commande
        /// </summary>
            public string Suivi_id { get;}
        /// <summary>
        /// Etape de la commande (en cours, relancée, livrée, réglée)
        /// </summary>
            public string Etape { get;}

        /// <summary>
        /// Constructeur de la commande
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateCommande"></param>
        /// <param name="montant"></param>
        /// <param name="nbExemplaire"></param>
        /// <param name="idLivreDvd"></param>
        /// <param name="suivi_id"></param>
        /// <param name="etape"></param>
        public CommandeDocument(string id, DateTime dateCommande, double montant, int nbExemplaire, string idLivreDvd, string suivi_id, string etape)
        : base(id, dateCommande, montant)
        {
            this.NbExemplaire = nbExemplaire;
            this.IdLivreDvd = idLivreDvd;
            this.Suivi_id = suivi_id;
            this.Etape = etape;
        }

    }
}
