using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanillaCare.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VanillaCare
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Page_TaskTuteurDetail),typeof(Page_TaskTuteurDetail)); 
        }

        private async void OnRun()
        {
            await Shell.Current.GoToAsync("Home");
        }
    }
}