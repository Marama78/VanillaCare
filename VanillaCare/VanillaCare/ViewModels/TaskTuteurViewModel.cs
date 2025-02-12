using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using VanillaCare.Models;
using VanillaCare.Views;
using Xamarin.Forms;

namespace VanillaCare.ViewModels
{
    [QueryProperty(nameof(SiteDeTravail),nameof(SiteDeTravail))]
    public class TaskTuteurViewModel:BaseViewModel
    {

        public Command CMD_ItemTapped { get; }
        public TaskTuteurViewModel()
        {
            DataTaskTuteurs = new ObservableCollection<TaskTuteur>();
            CMD_ItemTapped = new Command<TaskTuteur>(OnItemSelected);
        }
        private string siteDeTravail;
        public string SiteDeTravail { get { return siteDeTravail; } set { SetProperty(ref siteDeTravail, value); } }

        private TaskTuteur selectedItem;
        public TaskTuteur SelectedItem
        {
            get { return selectedItem; }
            set
            {
                SetProperty(ref selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async Task OnLoad()
        {
            IsBusy = true;
            DataTaskTuteurs.Clear();
            var datalist = await App.Tasktuteurdatabase.GetAllItems();

            foreach (var d in datalist)
            {
                if (d.SiteLocation == SiteDeTravail)
                {
                    DataTaskTuteurs.Add(d);
                }
            }
            IsBusy = false;
        }

        private ObservableCollection<TaskTuteur> dataTaskTuteurs;
        public ObservableCollection<TaskTuteur> DataTaskTuteurs
        {
            get { return dataTaskTuteurs; }
            set => SetProperty(ref dataTaskTuteurs, value);
        }


        async void OnItemSelected(TaskTuteur item)
        {
            if (item == null) { return; }

            await Shell.Current.GoToAsync($"{nameof(Page_TaskTuteurDetail)}?{nameof(TaskTuteurDetailViewModel.TuteurID)}=={item.Id}");
        }
    }
}
