namespace Maui_spiro
{
    public partial class App : Application
    {
        private static PatientDatabase _database;
        public static PatientDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    var dbPath = @"C:\Users\astal\OneDrive - Aarhus universitet\Semesterprojekt_3\Asta SW_test\patient_database.db";

                    _database = new PatientDatabase(dbPath);
                }
                return _database;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
