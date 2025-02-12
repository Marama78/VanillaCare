using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using VanillaCare.Models;
using Xamarin.Forms;

namespace VanillaCare.ViewModels
{
    public class HomeViewModel
    {
        public Command CMD_EraseTaskTuteurDataBase {get;}
        public Command CMD_CreateTaskTuteurDataBase {get; }
        public HomeViewModel()
        {
            CMD_EraseTaskTuteurDataBase = new Command(async () => await EraseTaskTuteurDataBase());
            CMD_CreateTaskTuteurDataBase = new Command(async () => await CreateTaskTuteurDataBase());
        }

        private async Task EraseTaskTuteurDataBase()
        {
            await App.Tasktuteurdatabase.DeleteAllItem();
        }

        private async Task CreateTaskTuteurDataBase()
        {
            //-- créer des tables vierges pour les deux sites
            int Id = 0;
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    

                    int row = i + 1;
                    int col = j + 1;
                    string PositionName = "L" + row + "C" + col;

                    TaskTuteur newTask_PUEU = new TaskTuteur()
                    { 
                        Id = Id,
                        Name = PositionName,
                        SiteLocation = App.PueuName,

                        Desherber = DateTime.Now,
                        Tailler = DateTime.Now,
                        Ajouter_Compost = DateTime.Now,
                        Ajouter_AntiLimace = DateTime.Now,
                        Ajouter_BouillieBordelaise = DateTime.Now,

                        Trigger_Desherber = 0,
                        Trigger_Tailler = 0,
                        Trigger_Compost = 0,
                        Trigger_AntiLimace = 0,
                        Trigger_BouillieBordelaise = 0,

                        Trigger_MostHigh = 0,
                    };

                    int toahotuID = Id + 1000;


                    TaskTuteur newTask_TOAHOTU = new TaskTuteur()
                    {
                        Id = toahotuID,
                        Name = PositionName,
                        SiteLocation = App.ToahotuName,

                        Desherber = DateTime.Now,
                        Tailler = DateTime.Now,
                        Ajouter_Compost = DateTime.Now,
                        Ajouter_AntiLimace = DateTime.Now,
                        Ajouter_BouillieBordelaise = DateTime.Now,

                        Trigger_Desherber = 0,
                        Trigger_Tailler = 0,
                        Trigger_Compost = 0,
                        Trigger_AntiLimace = 0,
                        Trigger_BouillieBordelaise = 0,

                        Trigger_MostHigh = 0,
                    };

                    await App.Tasktuteurdatabase.SaveItemAsync(newTask_PUEU);
                    await App.Tasktuteurdatabase.SaveItemAsync(newTask_TOAHOTU);

                    Id += 1;
                }

            }
        }
    }
}
