using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métiers des commandes, classe mère des autres types de commandes (abonnements, commandedocument)
    /// </summary>
    public class Commande
    {
        /// <summary>
        /// IdPrimaire de la commande
        /// </summary>
        public string IdPrimaire { get;  }
        
        /// <summary>
        /// Date de la commande
        /// </summary>
        public DateTime DateCommande { get;  }
        
        /// <summary>
        /// Valeur du montant de la commande 
        /// </summary>
        public double Montant { get;  }
    
    
        /// <summary>
        /// constructeur de la commande
        /// </summary>
        /// <param name="idPrimaire">IdPrimaire à donner à la commande</param>
        /// <param name="dateCommande">Date à laquelle la commande est effectuée</param>
        /// <param name="montant">Montant de la commande</param>
        public Commande(string idPrimaire, DateTime dateCommande, double montant)
    {
        this.IdPrimaire = idPrimaire;
        this.DateCommande = dateCommande;
        this.Montant = montant;
    }
    
    }


}
