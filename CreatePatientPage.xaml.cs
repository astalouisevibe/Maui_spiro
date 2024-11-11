using System.Diagnostics;

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
            Alder = CalculateAge(CPRNumberEntry.Text),
            Køn = GetGender(CPRNumberEntry.Text),
            Dato = null,
            Etnicitet = null,
            Højde = null,
            Vægt = null,
            FCV = null, // resultat fra en test
            FEV1 = null // resultat af test
           
        };

        Debug.WriteLine($"Saving patient: Name = {newPatient.Name}, CPR = {newPatient.CPR}, Alder = {newPatient.Alder}");


        // Gem patienten i databasen
        await App.Database.SavePatientAsync(newPatient);

        await DisplayAlert("Succes", "Ny patient gemt", "OK");
        await Navigation.PopAsync(); // Gå tilbage til hovedsiden

    }

    public string GetGender (string cprNumber)
    {
        int GenderString =int.Parse( cprNumber.Substring(9, 1));
        string Male = "Mand";
        string Female = "Kvinde";
        if (GenderString % 2 == 0)
        {
            return Female;
        }
        else
        {
            return Male;
        }
    }
    
    public string CalculateAge(string cprNumber)
    {
        // Ekstraher fødselsdato fra CPR-nummeret
        string birthDateString = cprNumber.Substring(0, 6);
        int day = int.Parse(birthDateString.Substring(0, 2));
        int month = int.Parse(birthDateString.Substring(2, 2));
        int year = int.Parse(birthDateString.Substring(4, 2));

     
        if (year < 24)
            year += 2000;
        else
            year += 1900;


        DateTime birthDate = new DateTime(year, month, day);

        // Beregn alder
        int age = DateTime.Now.Year - birthDate.Year;
        if (DateTime.Now < birthDate.AddYears(age)) age--;

        return Convert.ToString(age);
    }

}