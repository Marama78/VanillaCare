using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using VanillaCare.Models;
using Xamarin.Forms;

namespace VanillaCare.ViewModels
{

    [QueryProperty(nameof(TuteurID), nameof(TuteurID))]
    public class TaskTuteurDetailViewModel:BaseViewModel
    {
        public Command CMD_LoadTaskTuteur { get; }
        public Command CMD_TriggerTicker { get; }

        const int TIMELINE_DESHERBER = 15; //-- 15jours
        const int TIMELINE_TAILLER = 7; //-- 7jours
        const int TIMELINE_COMPOST = 92; //-- 3mois
        const int TIMELINE_ANTILIMACE = 92; //-- 3mois
        const int TIMELINE_BOUILLIEBORDELAISE = 182; //-- 6mois

        const int ALERT_DESHERBER = 5;
        const int ALERT_TAILLER = 2;
        const int ALERT_COMPOST = 30;
        const int ALERT_ANTILIMACE = 5;
        const int ALERT_BOUILLIE = 5;

        public TaskTuteurDetailViewModel()
        {
            //-- Initialiser les commandes
            CMD_LoadTaskTuteur = new Command(async () => await LoadTaskTuteur(TuteurID));

            CMD_TriggerTicker = new Command(TriggerTicks);
        }

        private async Task LoadTaskTuteur(int _tuteurID)
        {
            IsBusy = true;

            try
            {
                CurrentTaskTuteur = await App.Tasktuteurdatabase.GetItemAsyncByID(_tuteurID);

                if (CurrentTaskTuteur != null)
                {
                    #region <Set Triggers Values>
                    //-- set all triggers
                    TimeSpan deltaDesherber = DateTime.Now - CurrentTaskTuteur.Desherber;
                    TimeSpan deltaTailler = DateTime.Now - CurrentTaskTuteur.Tailler;
                    TimeSpan deltaCompost = DateTime.Now - CurrentTaskTuteur.Ajouter_Compost;
                    TimeSpan deltaAntiLimace = DateTime.Now - CurrentTaskTuteur.Ajouter_AntiLimace;
                    TimeSpan deltaBouillieBordelaise = DateTime.Now - CurrentTaskTuteur.Ajouter_BouillieBordelaise;

                    int delta_total_desherber = (int)deltaDesherber.TotalDays;
                    int delta_total_tailler = (int)deltaTailler.TotalDays;
                    int delta_total_compost = (int)deltaCompost.TotalDays;
                    int delta_total_antilimace = (int)deltaAntiLimace.TotalDays;
                    int delta_total_bouilliebordelaise = (int)deltaBouillieBordelaise.TotalDays;


                    int delta_tr_desherber = 0;
                    int delta_tr_tailler = 0;
                    int delta_tr_compost = 0;
                    int delta_tr_antilimace = 0;
                    int delta_tr_bouilliebordelaise = 0;


                    if (delta_total_desherber >= TIMELINE_DESHERBER + (ALERT_DESHERBER * 2))
                    { delta_tr_desherber = 4; }
                    else if (delta_total_desherber >= TIMELINE_DESHERBER + ALERT_DESHERBER)
                    { delta_tr_desherber = 3; }
                    else if (delta_total_desherber >= TIMELINE_DESHERBER)
                    { delta_tr_desherber = 2; }
                    else if (delta_total_desherber >= (TIMELINE_DESHERBER - ALERT_DESHERBER))
                    { delta_tr_desherber = 1; }

                    if (delta_total_tailler >= TIMELINE_TAILLER + (ALERT_TAILLER * 2))
                    { delta_tr_tailler = 4; }
                    else if (delta_total_tailler >= TIMELINE_TAILLER + ALERT_TAILLER)
                    { delta_tr_tailler = 3; }
                    else if (delta_total_tailler >= TIMELINE_TAILLER)
                    { delta_tr_tailler = 2; }
                    else if (delta_total_tailler >= (TIMELINE_TAILLER - ALERT_TAILLER))
                    { delta_tr_tailler = 1; }

                    if (delta_total_compost >= TIMELINE_COMPOST + (ALERT_COMPOST * 2))
                    { delta_tr_compost = 4; }
                    else if (delta_total_compost >= TIMELINE_COMPOST + ALERT_COMPOST)
                    { delta_tr_compost = 3; }
                    else if (delta_total_compost >= TIMELINE_COMPOST)
                    { delta_tr_compost = 2; }
                    else if (delta_total_compost >= (TIMELINE_COMPOST - ALERT_COMPOST))
                    { delta_tr_compost = 1; }

                    if (delta_total_antilimace >= TIMELINE_ANTILIMACE + (ALERT_ANTILIMACE * 2))
                    { delta_tr_antilimace = 4; }
                    else if (delta_total_antilimace >= TIMELINE_ANTILIMACE + ALERT_ANTILIMACE)
                    { delta_tr_antilimace = 3; }
                    else if (delta_total_antilimace >= TIMELINE_ANTILIMACE)
                    { delta_tr_antilimace = 2; }
                    else if (delta_total_antilimace >= (TIMELINE_ANTILIMACE - ALERT_ANTILIMACE))
                    { delta_tr_antilimace = 1; }

                    if (delta_total_bouilliebordelaise >= TIMELINE_BOUILLIEBORDELAISE + (ALERT_BOUILLIE * 2))
                    { delta_tr_bouilliebordelaise = 4; }
                    else if (delta_total_bouilliebordelaise >= TIMELINE_BOUILLIEBORDELAISE + ALERT_BOUILLIE)
                    { delta_tr_bouilliebordelaise = 3; }
                    else if (delta_total_bouilliebordelaise >= TIMELINE_BOUILLIEBORDELAISE)
                    { delta_tr_bouilliebordelaise = 2; }
                    else if (delta_total_bouilliebordelaise >= (TIMELINE_BOUILLIEBORDELAISE - ALERT_BOUILLIE))
                    { delta_tr_bouilliebordelaise = 1; }

                    #endregion

                    #region <Set MostHight value>
                    this.Trigger_MostHigh = 0;

                    List<int> AllScore = new List<int>() {
                    delta_tr_desherber,delta_tr_tailler,delta_tr_compost,delta_tr_antilimace,delta_tr_bouilliebordelaise };

                    foreach (int score in AllScore)
                    {
                        if (score > Trigger_MostHigh)
                        {
                            this.Trigger_MostHigh = score;
                        }

                    }
                    #endregion

                    #region <Set CurrentTaskTuteur data>
                    
                    this.Id = CurrentTaskTuteur.Id;
                    this.Name = CurrentTaskTuteur.Name;
                    this.SiteLocation = CurrentTaskTuteur.SiteLocation;

                    this.Desherber = CurrentTaskTuteur.Desherber;
                    this.Tailler = CurrentTaskTuteur.Tailler;
                    this.Compost = CurrentTaskTuteur.Ajouter_Compost;
                    this.AntiLimace = CurrentTaskTuteur.Ajouter_AntiLimace;
                    this.Bouilliebordelaise = CurrentTaskTuteur.Ajouter_BouillieBordelaise;

                    this.Trigger_Desherber = delta_tr_desherber;
                    this.Trigger_Tailler = delta_tr_tailler;
                    this.Trigger_Compost = delta_tr_compost;
                    this.Trigger_AntiLimace = delta_tr_antilimace;
                    this.Trigger_BouillieBordelaise = delta_tr_bouilliebordelaise;
                    
                    #endregion

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        

        private void TriggerTicks()
        {
            IsBusy = true;
            Trigger_BouillieBordelaise++;
            if (Trigger_BouillieBordelaise > 4)
                Trigger_BouillieBordelaise = 0;
        
            IsBusy = false;
        }

        private int tuteurId;
        public int TuteurID
        { 
            get { return tuteurId; } 
            set { tuteurId = value; 
                //-- TODO : LoadItem by Name
                } 
        }

        private TaskTuteur currentTaskTuteur;
        public TaskTuteur CurrentTaskTuteur { get{ return currentTaskTuteur; } set{ SetProperty(ref currentTaskTuteur, value); } }

        #region <VARIABLES TASKTUTEUR ITEM>

        //-- identification
        private int id;
        public int Id{ get { return id; } set { id = value; } }

        private string name;
        public string Name { get { return name; } set { SetProperty(ref name, value); } }

        private string siteLocation;
        public string  SiteLocation { get { return "Site de "+ siteLocation; } set { SetProperty(ref siteLocation, value); } }

        //-- variables
        private DateTime desherber;
        public DateTime Desherber { get { return desherber; } set { SetProperty(ref desherber, value); } }

        private DateTime tailler;
        public DateTime Tailler { get { return tailler; } set { SetProperty(ref tailler, value); } }

        private DateTime compost;
        public DateTime Compost { get { return compost; } set { SetProperty(ref compost, value); } }

        private DateTime antiLimace;
        public DateTime AntiLimace { get { return antiLimace; } set { SetProperty(ref antiLimace, value); } }

        private DateTime bouilliebordelaise;
        public DateTime Bouilliebordelaise { get { return bouilliebordelaise; } set { SetProperty(ref bouilliebordelaise, value); } }

        //-- triggers
        private int trigger_Desherber;
        public int Trigger_Desherber { get { return trigger_Desherber; } set { SetProperty(ref trigger_Desherber, value); } }

        private int trigger_Tailler;
        public int Trigger_Tailler { get { return trigger_Tailler; } set { SetProperty(ref trigger_Tailler, value); } }

        private int trigger_Compost;
        public int Trigger_Compost { get { return trigger_Compost; } set { SetProperty(ref trigger_Compost, value); } }

        private int trigger_AntiLimace;
        public int Trigger_AntiLimace { get { return trigger_AntiLimace; } set { SetProperty(ref trigger_AntiLimace, value); } }

        private int trigger_BouillieBordelaise;
        public int Trigger_BouillieBordelaise { get { return trigger_BouillieBordelaise; } set { SetProperty(ref trigger_BouillieBordelaise, value); } }

        //-- mainTrigger
        private int trigger_MostHigh;
        public int Trigger_MostHigh { get { return trigger_MostHigh; } set { SetProperty(ref trigger_MostHigh, value); } }

        #endregion








    }
}
