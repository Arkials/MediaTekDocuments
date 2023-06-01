
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Etat (état d'usure d'un document)
    /// </summary>
    public class Etat
    {
        /// <summary>
        /// Id de l'état d'un document
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// libellé correspondant à l'état du document
        /// </summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Constructeur de l'état d'un document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libelle"></param>
        public Etat(string id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }

    }
}
