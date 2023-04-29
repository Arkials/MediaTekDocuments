using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    public class CommandeRevue : Commande
    {
        public DateTime DateFinAbonnement { get; }

        public string IdRevue { get; }
        public string Titre { get; }

        public CommandeRevue(string id,string idRevue, string titre, DateTime dateCommande, DateTime dateFinAbonnement, double montant) : base(id, dateCommande, montant)
        {
            this.DateFinAbonnement = dateFinAbonnement;
            this.IdRevue = idRevue;
            this.Titre = titre;
        }
    }
}
