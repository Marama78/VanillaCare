using System;
using System.Collections.Generic;
using System.Text;
using VanillaCare.Views;
using Xamarin.Forms;

namespace VanillaCare.ViewModels
{
    public class TaskTuteurSelectSiteViewModel
    {
        public Command CMD_LoadTaskTuteur_PUEU { get; }
        public Command CMD_LoadTaskTuteur_TOAHOTU { get; }

        async void OnPUEU()
        {
            await Shell.Current.GoToAsync($"{nameof(TaskTuteurViewModel)}?{nameof(TaskTuteurViewModel.SiteDeTravail)}={App.PueuName}");
        }
        async void OnTOAHOTU()
        {
            await Shell.Current.GoToAsync($"{nameof(TaskTuteurViewModel)}?{nameof(TaskTuteurViewModel.SiteDeTravail)}={App.ToahotuName}");
        }
    }
}
