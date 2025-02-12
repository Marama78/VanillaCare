using System;
using System.Collections.Generic;
using System.Text;
using VanillaCare.Models.OnlyForProduction;

namespace VanillaCare.Models
{
    public class ProduceTuteur
    {
        //-- identification --
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteLocation { get; set; }

        //-- exploitation donnees --
        public List<LianeProduction> LianeStatus {  get; set; }
      
       

    }
}
