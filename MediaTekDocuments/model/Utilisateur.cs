using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier de l'utilisateur renvoyé par l'API se correspondance avec des identifiants
    /// </summary>
    public class Utilisateur
    {
        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string Nom { get; }

        /// <summary>
        /// Prénom de l'utilisateur
        /// </summary>
        public string Prenom { get; }
        /// <summary>
        /// Id de service de l'utilisateur qui détermine ses droits
        /// </summary>
        public int IdService { get; }

        /// <summary>
        /// Constructeur de l'utilisateur
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="service_id"></param>
        public Utilisateur(string nom,string prenom,int service_id)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.IdService = service_id;
        }

    }
}
