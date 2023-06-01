
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Document (réunit les infomations communes à tous les documents : Livre, Revue, Dvd)
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Id du document
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Titre du document
        /// </summary>
        public string Titre { get; }

        /// <summary>
        /// Lien vers l'image du document
        /// </summary>
        public string Image { get; }

        /// <summary>
        /// Id correspondant au genre du document
        /// </summary>
        public string IdGenre { get; }

        /// <summary>
        /// Genre du document
        /// </summary>
        public string Genre { get; }

        /// <summary>
        /// Id du public du document
        /// </summary>
        public string IdPublic { get; }

        /// <summary>
        /// Public visé pour ce document
        /// </summary>
        public string Public { get; }

        /// <summary>
        /// Id du rayon où se trouve le document
        /// </summary>
        public string IdRayon { get; }
        
        /// <summary>
        /// Rayon du document 
        /// </summary>
        public string Rayon { get; }

        /// <summary>
        /// Constructeur du document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="titre"></param>
        /// <param name="image"></param>
        /// <param name="idGenre"></param>
        /// <param name="genre"></param>
        /// <param name="idPublic"></param>
        /// <param name="lePublic"></param>
        /// <param name="idRayon"></param>
        /// <param name="rayon"></param>
        public Document(string id, string titre, string image, string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
        {
            Id = id;
            Titre = titre;
            Image = image;
            IdGenre = idGenre;
            Genre = genre;
            IdPublic = idPublic;
            Public = lePublic;
            IdRayon = idRayon;
            Rayon = rayon;
        }
    }
}
