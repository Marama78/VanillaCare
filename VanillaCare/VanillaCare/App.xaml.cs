using System;
using System.IO;
using VanillaCare.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VanillaCare
{
    public partial class App : Application
    {
        static string tasktuteur_DB = "tasktuteur.db3";

        static TaskTuteurServices tasktuteurdatabase;


        public static string PueuName = "Pueu";
        public static string ToahotuName = "Toahotu";
        public static TaskTuteurServices Tasktuteurdatabase
        {
            get
            {
                if (tasktuteurdatabase == null)
                {
                    tasktuteurdatabase = new TaskTuteurServices(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), tasktuteur_DB));
                }
                return tasktuteurdatabase;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
