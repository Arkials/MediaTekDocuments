using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    public class CommandeDocument : Commande
    {
            public int NbExemplaire { get;}
            public string IdLivreDvd { get;}
            public string Suivi_id { get;}
            public string Etape { get;}

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
