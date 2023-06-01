using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Exemplaire (exemplaire d'une revue)
    /// </summary>
    public class Exemplaire
    {

        /// <summary>
        /// Numéro de l'exemplaire de la revue
        /// </summary>
        public int Numero { get; set; }

        /// <summary>
        /// Lien vers la photo de la revue
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Date d'achat de l'exemplaire de la revue
        /// </summary>
        public DateTime DateAchat { get; set; }

        /// <summary>
        /// Id de l'état de l'exemplaire
        /// </summary>
        public string IdEtat { get; set; }

        /// <summary>
        /// Id de la revue 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Constructeur de l'exemplaire
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="dateAchat"></param>
        /// <param name="photo"></param>
        /// <param name="idEtat"></param>
        /// <param name="idDocument"></param>
        public Exemplaire(int numero, DateTime dateAchat, string photo, string idEtat, string idDocument)
        {
            this.Numero = numero;
            this.DateAchat = dateAchat;
            this.Photo = photo;
            this.IdEtat = idEtat;
            this.Id = idDocument;
        }

    }
}
