using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    public class Commande
    {
        public string IdPrimaire { get;  }
        public DateTime DateCommande { get;  }
        public double Montant { get;  }
    
    
        public Commande(string idPrimaire, DateTime dateCommande, double montant)
    {
        this.IdPrimaire = idPrimaire;
        this.DateCommande = dateCommande;
        this.Montant = montant;
    }
    
    }


}
