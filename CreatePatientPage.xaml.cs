using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace Maui_spiro;

public partial class CreatePatientPage : ContentPage
{
	public CreatePatientPage()
	{
		InitializeComponent();
	}
    // Event handler for 'Opret patient'
    private async void OnCreatePatientButtonClicked(object sender, EventArgs e)
    {
        string patientName = PatientNameEntry.Text;
        string cprNumber = CPRNumberEntry.Text;

        if (string.IsNullOrWhiteSpace(patientName) || string.IsNullOrWhiteSpace(cprNumber) || cprNumber.Length != 10 || !long.TryParse(cprNumber, out _))
        {
            await DisplayAlert("Fejl", "Indtast et gyldigt navn og CPR-nummer.", "OK");
            return;
        }

        // Kald metoden for at gemme patienten
        await SaveNewPatient();

    }

    // Asynkron metode til at simulere gemning af patientdata til database
    public async Task SaveNewPatient()
    {

        // Opret en ny patient med CPR og Navn fra inputfelterne
        var newPatient = new PatientData
        {
            Name = PatientNameEntry.Text,
            CPR = CPRNumberEntry.Text,
            FCV = "FCV data", // Dette kan være resultat fra en test
            FEV1 = "FEV1 data"// resultat af test
        };


                // Gem patienten i databasen
                await App.Database.SavePatientAsync(newPatient);

                await DisplayAlert("Succes", "Ny patient gemt", "OK");
                await Navigation.PopAsync(); // Gå tilbage til hovedsiden
          
    
    }
}