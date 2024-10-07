namespace Maui_spiro
{
    public partial class App : Application
    {
        public static List<PatientData> Patients = new List<PatientData>();
        static PatientDatabase database;

        // Hent den globale databaseinstans
        public static PatientDatabase Database
        {
            get
            {
                if (database == null)
                {
                    // Opret eller hent stien til databasen
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "patients.db3");
                    database = new PatientDatabase(dbPath);
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
