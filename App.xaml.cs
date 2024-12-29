using c971.Data;
using System.IO;
using c971.ViewModels;
using c971.Views;

namespace c971
{
    public partial class App : Application
    {
        private static DatabaseService? _database;

        public App( )
        {
            InitializeComponent();

            // Add test data for evaluation 
            Task.Run( async ( ) => await App.Database.SeedDataAsync() ).Wait();

            // Use AppShell as the main page for Shell-based navigation
            MainPage = new AppShell();
        }

        // Lazy initialization of the database. Only created if the static _database is null
        public static DatabaseService Database
        {
            get
            {
                if ( _database == null )
                {
                    // Get the path to the database file
                    string path = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData ), "c971.db3" );

                    // Create the database service
                    _database = new DatabaseService( path );


                }
                return _database;
            }
        }


       


    }
}