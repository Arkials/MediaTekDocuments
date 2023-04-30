using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTekDocuments.dal;
using MediaTekDocuments.model;

namespace MediaTekDocuments.controller
{
    class FrmAuthentificationController
    {
       

        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;
        
        /// <summary>
        /// Récupération de l'instance d'access
        /// </summary>
        public FrmAuthentificationController()
        {
            access = Access.GetInstance();
        }

        public List<Utilisateur> GetUtilisateur(Identifiants identifiants)
        {
            return access.GetUtilisateur(identifiants);
        }
    }
}
