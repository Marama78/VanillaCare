using System;
using System.Collections.Generic;
using System.Text;

namespace VanillaCare.Models.OnlyForProduction
{
    public class LianeProduction
    {
        //-- identification --
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteLocation { get; set; }

        //-- Lecture données --
        public bool IsBourgeon { get; set; }
        public bool IsBouton { get; set; }
        public bool IsSafe { get; set; }
        public bool IsDead { get; set; }

         public DateTime MariageStart { get; set; }
         public DateTime MariageEnd { get; set; }
        public DateTime HarvestVanillaBeans { get; set; }
        public bool IsHarvest {  get; set; }   
        public int TotalVanillaBeans { get; set; }


    }
}
