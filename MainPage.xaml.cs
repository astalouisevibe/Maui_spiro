using SQLite;

namespace Maui_spiro
{
    public partial class MainPage : ContentPage
    { 
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            string cprNumber = CPRNumberEntry.Text;

            //Cpr Validering
            if (string.IsNullOrWhiteSpace(cprNumber) || cprNumber.Length != 10 || !long.TryParse(cprNumber, out _))
            {
                await DisplayAlert("Ugyldigt CPR", "CPR-nummeret skal være 10 cifre.", "OK");
                return;
            }


            // Søgning efter patient i den globale liste
            var patientData = await FindPatientInDatabase(cprNumber);

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
        private async Task<PatientData> FindPatientInDatabase(string cprNumber)
        {
            // Søg efter patient i databasen
            return await App.Database.GetPatientByCPRAsync(cprNumber);
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
