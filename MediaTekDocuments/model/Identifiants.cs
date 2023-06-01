using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier des identifiants envoyés à l'API
    /// </summary>
    public class Identifiants
    {
        /// <summary>
        /// Identifiant (login)
        /// </summary>
        public string Identifiant { get; }
        /// <summary>
        /// Mot de passe 
        /// </summary>
        public string MotDePasse { get; }
    
        /// <summary>
        /// Constructeur des identifiants
        /// </summary>
        /// <param name="identifiant"></param>
        /// <param name="motDePasse"></param>
        public Identifiants(string identifiant, string motDePasse)
        {
            this.Identifiant = identifiant;
            this.MotDePasse = motDePasse;
        }
    
    }
}
