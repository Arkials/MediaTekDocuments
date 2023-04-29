using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;
using System;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmMediatek
    /// </summary>
    class FrmMediatekController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmMediatekController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// getter sur la liste des genres
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return access.GetAllGenres();
        }

        /// <summary>
        /// getter sur la liste des livres
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return access.GetAllLivres();
        }

        public  List<CommandeRevue> GetFinAbonnement()
        {
            return access.GetFinAbonnement();
        }

        /// <summary>
        /// getter sur les listes de commandes des livres
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<CommandeDocument> GetAllCommandesLivres()
        {
            return access.GetAllCommandesLivres();
        }

        public bool CreerCommandeDocument(CommandeDocument commandeDocument)
        {
            return access.CreerCommandeDocument(commandeDocument);
        }

        /// <summary>
        /// getter sur la liste des Dvd
        /// </summary>
        /// <returns>Liste d'objets dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return access.GetAllDvd();
        }

        /// <summary>
        /// getter sur les listes de commandes DVD
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<CommandeDocument> GetAllCommandesDvd()
        {
            return access.GetAllCommandesDvd();
        }

        /// <summary>
        /// getter sur la liste des revues
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return access.GetAllRevues();
        }

        /// <summary>
        /// getter sur les rayons
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return access.GetAllRayons();
        }

        /// <summary>
        /// getter sur les publics
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return access.GetAllPublics();
        }
        /// <summary>
        /// getter sur la liste des suivi commande
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<SuiviCommande> GetAllSuiviCommande()
        {
            return access.GetAllSuiviCommande();
        }

        /// <summary>
        /// récupère les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocuement">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocuement)
        {
            return access.GetExemplairesRevue(idDocuement);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return access.CreerExemplaire(exemplaire);
        }

        public bool ModifierCommandeDocument(Dictionary<string,string> suiviIdChange)
        {
            return access.ModifierCommandeDocument(suiviIdChange);
        }

        public bool SupprCommande(Dictionary<string,string> idDocument)
        {
            return access.SupprCommande(idDocument);
        }

        public List<CommandeRevue> GetAllCommandesRevue()
        {
            return access.GetAllCommandesRevues();
        }

        public bool CreerCommandeRevue(CommandeRevue nvlCommandeRevue)
        {
            return access.CreerCommandeRevue(nvlCommandeRevue);
        }
    }
}
