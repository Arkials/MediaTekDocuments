using System;
using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.manager;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Serilog;


namespace MediaTekDocuments.dal
{
    /// <summary>
    /// Classe d'accès aux données
    /// </summary>
    public class Access
    {
        /// <summary>
        /// adresse de l'API
        /// </summary>
        private static readonly string uriApi = "http://localhost/rest_mediatekdocuments";
        private static readonly string connectionName = "MediaTekDocuments.Properties.Settings.mediaTekDocmentsConnectionString";

        /// <summary>
        /// instance unique de la classe
        /// </summary>
        private static Access instance = null;
        /// <summary>
        /// instance de ApiRest pour envoyer des demandes vers l'api et recevoir la réponse
        /// </summary>
        private readonly ApiRest api = null;
        /// <summary>
        /// méthode HTTP pour select
        /// </summary>
        private const string GET = "GET";
        /// <summary>
        /// méthode HTTP pour insert
        /// </summary>
        private const string POST = "POST";
        /// <summary>
        /// méthode HTTP pour update
        /// </summary>
        private const string PUT = "PUT";
        /// <summary>
        /// méthode HTTP pour delete
        /// </summary>
        private const string DELETE = "DELETE";

        /// <summary>
        /// Méthode privée pour créer un singleton
        /// initialise l'accès à l'API
        /// </summary>
        private Access()
        {
            String authenticationString;
            try
            {
                authenticationString = GetConnectionStringByName(connectionName);
                api = ApiRest.GetInstance(uriApi, "admin:adminpwd");
                //api = ApiRest.GetInstance(uriApi, authenticationString);
                Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Verbose()
               .WriteTo.Console()
               .WriteTo.File("logs/log.txt",
               rollingInterval: RollingInterval.Day)
               .CreateLogger();
            }
            catch (Exception e)
            {
                Log.Error(e, "Erreur création d'instance d'Access");
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// Récupération de la chaîne de connexion
        /// </summary>
        /// <param name="name">Nom de la chaîne à récupérer</param>
        /// <returns></returns>
        static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
                returnValue = settings.ConnectionString;
            return returnValue;
        }

        /// <summary>
        /// Création et retour de l'instance unique de la classe
        /// </summary>
        /// <returns>instance unique de la classe</returns>
        public static Access GetInstance()
        {
            if (instance == null)
            {
                instance = new Access();
            }
            return instance;
        }

        /// <summary>
        /// Envoi les identifiants saisis et renvoi un utilisateur s'il y correspond
        /// </summary>
        /// <param name="identifiants">Identifiants envoyés à l'API</param>
        /// <returns>Un utilisateur</returns>
        public List<Utilisateur> GetUtilisateur(Identifiants identifiants)
        {
            String JsonIdentifiants = JsonConvert.SerializeObject(identifiants);
            List<Utilisateur> utilisateurLogged = TraitementRecup<Utilisateur>(GET, "utilisateurs/" + JsonIdentifiants);
            if (utilisateurLogged.Count > 0)
            {
                Log.Information("Utilisateur connecté : " + utilisateurLogged[0].Prenom + " " + utilisateurLogged[0].Nom);
            }
            else
            {
                Log.Information("Erreur d'identification");
            }
            return utilisateurLogged;
        }

        /// <summary>
        /// Récupère les abonnements se finissant dans moins d'un mois
        /// </summary>
        /// <returns>abonnements se finissant dans moins d'un mois </returns>
        public List<CommandeRevue> GetFinAbonnement()
        {
            List<CommandeRevue> lesCommandesRevues = TraitementRecup<CommandeRevue>(GET, "finabonnement");
            Log.Information("Récupération des fins d'abonnements");
            return lesCommandesRevues;
        }

        /// <summary>
        /// Crée une nouvelle commande de revue
        /// </summary>
        /// <param name="nvlCommandeRevue">Objet CommandeRevue</param>
        /// <returns>true si réussi sinon false </returns>
        public bool CreerCommandeRevue(CommandeRevue nvlCommandeRevue)
        {
            String jsonCommandeRevue = JsonConvert.SerializeObject(nvlCommandeRevue, new CustomDateTimeConverter());
            try
            {
                List<CommandeRevue> liste = TraitementRecup<CommandeRevue>(POST, "abonnement/" + jsonCommandeRevue);
                if(liste != null)
                {
                    Log.Information("Création d'une commande de revue");
                }
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Information(ex,"Échec de la création de commande de revue");
            }
            return false;
        }

        /// <summary>
        /// Crée une commande de DVD ou livre
        /// </summary>
        /// <param name="commandeDocument">Objet CommandeDocument</param>
        /// <returns>True si réussi sinon false</returns>
        public bool CreerCommandeDocument(CommandeDocument commandeDocument)
        {
            String jsonCommandeDocument = JsonConvert.SerializeObject(commandeDocument, new CustomDateTimeConverter());
            Console.WriteLine(jsonCommandeDocument);
            try
            {
                List<CommandeDocument> liste = TraitementRecup<CommandeDocument>(POST, "commandedocument/" + jsonCommandeDocument);
                if (liste != null)
                {
                    Log.Information("Création d'une commande de document");
                }
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Information(ex,"Échec de la création de commande de document");

            }
            return false;
        }

        /// <summary>
        /// ecriture d'un exemplaire en base de données
        /// </summary>
        /// <param name="exemplaire">exemplaire à insérer</param>
        /// <returns>true si l'insertion a pu se faire (retour != null)</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(exemplaire, new CustomDateTimeConverter());
            Console.WriteLine(jsonExemplaire);
            try
            {
                // récupération soit d'une liste vide (requête ok) soit de null (erreur)
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(POST, "exemplaire/" + jsonExemplaire);
                if (liste != null)
                {
                    Log.Information("Création d'un exemplaire");
                }
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex,"Échec de la création d'un exemplaire");
            }
            return false;
        }

        /// <summary>
        /// Retourne tous les genres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            IEnumerable<Genre> lesGenres = TraitementRecup<Genre>(GET, "genre");
            Log.Information("Récupération des genres");
            return new List<Categorie>(lesGenres);
        }

        /// <summary>
        /// Retourne tous les rayons à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            IEnumerable<Rayon> lesRayons = TraitementRecup<Rayon>(GET, "rayon");
            Log.Information("Récupération des rayons");
            return new List<Categorie>(lesRayons);
        }

        /// <summary>
        /// Retourne toutes les catégories de public à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            IEnumerable<Public> lesPublics = TraitementRecup<Public>(GET, "public");
            Log.Information("Récupération des publics");
            return new List<Categorie>(lesPublics);
        }

        /// <summary>
        /// Retourne toutes les livres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            List<Livre> lesLivres = TraitementRecup<Livre>(GET, "livre");
            Log.Information("Récupération des livres");
            return lesLivres;
        }

        /// <summary>
        /// Retourne toutes les commandes de document à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<CommandeDocument> GetAllCommandesLivres()
        {
            List<CommandeDocument> lesCommandesDocuments = TraitementRecup<CommandeDocument>(GET, "commandeslivres");
            Log.Information("Récupération des commandes de livre");
            return lesCommandesDocuments;
        }

        /// <summary>
        /// Retourne la liste des différentes étapes de commande avec leur ID
        /// </summary>
        /// <returns>Liste de type SuiviCommande</returns>
        public List<SuiviCommande> GetAllSuiviCommande()
        {
            List<SuiviCommande> lesSuivisCommandes = TraitementRecup<SuiviCommande>(GET, "suivicommande");
            Log.Information("Récupération de la liste des suivi de commandes");
            return lesSuivisCommandes;
        }               

        /// <summary>
        /// Récupère les commandes des revues (abonnements)
        /// </summary>
        /// <returns>Liste de CommandeRevue</returns>
        public List<CommandeRevue> GetAllCommandesRevues()
        {
            List<CommandeRevue> lesCommandesRevues = TraitementRecup<CommandeRevue>(GET, "abonnement");
            Log.Information("Récupération des commandes de revue");
            return lesCommandesRevues;
        }        

        /// <summary>
        /// Retourne toutes les dvd à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            List<Dvd> lesDvd = TraitementRecup<Dvd>(GET, "dvd");
            Log.Information("Récupération des DVD");
            return lesDvd;
        }

        /// <summary>
        /// Retourne toutes les commandes Dvd
        /// </summary>
        /// <returns>Liste de CommandeDocument</returns>
        public List<CommandeDocument> GetAllCommandesDvd()
        {
            List<CommandeDocument> lesCommandesDocuments = TraitementRecup<CommandeDocument>(GET, "commandesdvds");
            Log.Information("Récupération des commandes de DVD");
            return lesCommandesDocuments;
        }

        /// <summary>
        /// Retourne toutes les revues à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            List<Revue> lesRevues = TraitementRecup<Revue>(GET, "revue");
            Log.Information("Récupération des revues");
            return lesRevues;
        }

        /// <summary>
        /// Retourne les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocument">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            Dictionary<string, string> idDocumentArray = new Dictionary<string, string>() {
                {"id", idDocument }
            };
            string jsonIdDocument = JsonConvert.SerializeObject(idDocumentArray);
            List<Exemplaire> lesExemplaires = TraitementRecup<Exemplaire>(GET, "exemplaire/" + jsonIdDocument);
            Log.Information("Récupération des exemplaires d'une revue");
            return lesExemplaires;
        }

        /// <summary>
        /// modification d'une commande de document dans la base de données
        /// </summary>
        /// <param name="suiviIdChange">étape à modifier</param>
        /// <returns>true si la modification a pu se faire (retour != null)</returns>
        public bool ModifierCommandeDocument(Dictionary<string, string> suiviIdChange)

        {
            String jsonsuiviIdChange = JsonConvert.SerializeObject(suiviIdChange, new CustomDateTimeConverter());
            try
            {
                // récupération soit d'une liste vide (requête ok) soit de null (erreur)
                List<CommandeDocument> liste = TraitementRecup<CommandeDocument>(PUT, "commandedocument/" + suiviIdChange["id"] + "/" + jsonsuiviIdChange);
                if (liste != null)
                {
                    Log.Information("Modification d'une commande document");
                }
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Information(ex, "Échec de la modification de commande de document");
            }
            return false;
        }

        /// <summary>
        /// Supprime la commande ayant l'ID saisi
        /// </summary>
        /// <param name="idDocument">IdPrimaire du document à supprimer</param>
        /// <returns>True si réussi sinon false</returns>
        public bool SupprCommande(Dictionary<string,string> idDocument)
        {
            String jsonCommandeDocument = JsonConvert.SerializeObject(idDocument);
            Console.WriteLine(jsonCommandeDocument);

            try
            {
                List<Commande> liste = TraitementRecup<Commande>(DELETE, "commande/" + jsonCommandeDocument);
                if (liste != null)
                {
                    Log.Information("Suppression d'une commande");
                }
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Information(ex, "Échec de la suppression de commande");
            }
            return false;
        }

        /// <summary>
        /// Traitement de la récupération du retour de l'api, avec conversion du json en liste pour les select (GET)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methode">verbe HTTP (GET, POST, PUT, DELETE)</param>
        /// <param name="message">information envoyée</param>
        /// <returns>liste d'objets récupérés (ou liste vide)</returns>
        private List<T> TraitementRecup<T>(String methode, String message)
        {
            List<T> liste = new List<T>();
            try
            {
                JObject retour = api.RecupDistant(methode, message);
                Console.WriteLine(retour);
                // extraction du code retourné
                String code = (String)retour["code"];
                if (code.Equals("200"))
                {
                    // dans le cas du GET (select), récupération de la liste d'objets
                    if (methode.Equals(GET))
                    {
                        String resultString = JsonConvert.SerializeObject(retour["result"]);
                        // construction de la liste d'objets à partir du retour de l'api
                        liste = JsonConvert.DeserializeObject<List<T>>(resultString, new CustomBooleanJsonConverter());
                    }
                }
                else
                {
                    Log.Error("code erreur = " + code + " message = " + (String)retour["message"]);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Erreur lors de l'accès à l'API : " + e.Message);
                Environment.Exit(0);
            }
            return liste;
        }

        /// <summary>
        /// Modification du convertisseur Json pour gérer le format de date
        /// </summary>
        private sealed class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "yyyy-MM-dd";
            }
        }

        /// <summary>
        /// Modification du convertisseur Json pour prendre en compte les booléens
        /// classe trouvée sur le site :
        /// https://www.thecodebuzz.com/newtonsoft-jsonreaderexception-could-not-convert-string-to-boolean/
        /// </summary>
        private sealed class CustomBooleanJsonConverter : JsonConverter<bool>
        {
            public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                return Convert.ToBoolean(reader.ValueType == typeof(string) ? Convert.ToByte(reader.Value) : reader.Value);
            }

            public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

    }
}
