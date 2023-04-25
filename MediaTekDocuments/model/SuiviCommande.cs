using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    public class SuiviCommande
    {
        public string Id_suivi { get; }
        public string Etape { get; }

        public SuiviCommande(string id, string etape)
        {
            this.Id_suivi = id;
            this.Etape = etape;
        }
    }
}
