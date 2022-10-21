using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppFitness.Helpers;
using System.IO;

namespace AppFitness
{
    public partial class App : Application
    {
        static SQLiteDataBaseHelper database;
        public static SQLiteDataBaseHelper Database
        {
            get 
            {
                if (database == null)
                {
                    database = new SQLiteDataBaseHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XamAppFit.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
