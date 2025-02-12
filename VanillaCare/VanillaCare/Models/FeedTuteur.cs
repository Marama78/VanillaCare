using System;
using System.Collections.Generic;
using System.Text;

namespace VanillaCare.Models
{
    public class FeedTuteur
    {
        //-- identification --
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteLocation { get; set; }

        //-- traitement des données
        public DateTime Add_INPULSE { get; set; }
        public DateTime Add_CALCIMER { get; set; }
        public DateTime Add_FERTILEADER_KALEO { get; set; }
        public DateTime Add_MAXIFRUIT { get; set; }
        public DateTime Add_AZUR { get; set; }

    }
}
