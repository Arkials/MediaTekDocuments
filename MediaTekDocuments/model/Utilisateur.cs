using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    public class Utilisateur
    {
        public string Nom { get; }
        public string Prenom { get; }
        public int IdService { get; }

        public Utilisateur(string nom,string prenom,int service_id)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.IdService = service_id;
        }

    }
}
