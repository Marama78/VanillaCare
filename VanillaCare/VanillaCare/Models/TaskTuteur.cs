using System;
using System.Collections.Generic;
using System.Text;

namespace VanillaCare.Models
{
    public class TaskTuteur
    {
        //-- identification --
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteLocation { get; set; }

        //-- exploitation donnees --
        public DateTime Desherber { get; set; }
        public DateTime Tailler { get; set; }
        public DateTime Ajouter_Compost { get; set; }
        public DateTime Ajouter_AntiLimace { get; set; }
        public DateTime Ajouter_BouillieBordelaise { get; set; }

        //-- triggers
        public int Trigger_Desherber { get; set; }
        public int Trigger_Tailler { get; set; }
        public int Trigger_Compost { get; set; }
        public int Trigger_AntiLimace { get; set; }
        public int Trigger_BouillieBordelaise { get; set; }

        //-- mainPage Trigger
        public int Trigger_MostHigh { get; set; }   
    }
}
