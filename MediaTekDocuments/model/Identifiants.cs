using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    public class Identifiants
    {
        public string Identifiant { get; }
        public string MotDePasse { get; }
    
        public Identifiants(string identifiant, string motDePasse)
        {
            this.Identifiant = identifiant;
            this.MotDePasse = motDePasse;
        }
    
    }
}
