using System.Diagnostics;

namespace Maui_spiro;

public partial class CreatePatientPage : ContentPage
{
    private PatientService _patientService;
    public CreatePatientPage()
    {
        InitializeComponent();
        _patientService = new PatientService();
    }
    // Event handler for 'Opret patient'
    private async void OnCreatePatientButtonClicked(object sender, EventArgs e)
    {
        string patientName = PatientNameEntry.Text;
        string cprNumber = CPRNumberEntry.Text;

        if(!_patientService.ValidatePatientData(patientName,cprNumber))
        {
            await DisplayAlert("Fejl", "Indtast et gyldigt navn og CPR-nummer.", "OK");
            return;
        }

        // Kald metoden for at gemme patienten
        await _patientService.SaveNewPatient(patientName,cprNumber);
        await DisplayAlert("Succes", "Ny patient gemt", "OK");
        await Navigation.PopAsync(); // Gå tilbage til hovedsiden

    }

    private async void OnReturnButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}