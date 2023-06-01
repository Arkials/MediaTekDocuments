using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe gérant le suivi de la commande
    /// </summary>
    public class SuiviCommande
    {
        /// <summary>
        /// Id de suivi
        /// </summary>
        public string Id_suivi { get; }
        /// <summary>
        /// Étape correspondant à l'Id
        /// </summary>
        public string Etape { get; }

        /// <summary>
        /// constructeur de suivi de commande
        /// </summary>
        /// <param name="id"></param>
        /// <param name="etape"></param>
        public SuiviCommande(string id, string etape)
        {
            this.Id_suivi = id;
            this.Etape = etape;
        }
    }
}
