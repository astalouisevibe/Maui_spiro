using SQLite;

namespace Maui_spiro
{
    public partial class MainPage : ContentPage
    { 
        private readonly CPRValidator _cprValidator;
        private readonly PatientRepository _patientRepository;
        public MainPage()
        {
            InitializeComponent();
            _cprValidator = new CPRValidator();
            _patientRepository = new PatientRepository();
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            string cprNumber = CPRNumberEntry.Text;

            //Cpr Validering
            if (!_cprValidator.IsValid(cprNumber))
            {
                await DisplayAlert("Ugyldigt CPR", "CPR-nummeret skal være 10 cifre.", "OK");
                return;
            }


            // Søgning efter patient i den globale liste
            var patientData = await _patientRepository.FindPatientInDatabase(cprNumber);

            if (patientData != null)
            {
                // Navigér til patientdetaljeside hvis patienten findes
                await Navigation.PushAsync(new PatientDetailPage(patientData));
            }
            else
            {
                // Vis en besked, hvis patienten ikke findes
                await DisplayAlert("Ikke fundet", "Patienten blev ikke fundet i databasen.", "OK");
            }
        }
        // Event handler for 'Opret ny patient'
        private async void OnCreatePatientButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreatePatientPage());
        }

        private async void OnLogUdButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }


    }
}
