namespace Maui_spiro
{
    public partial class PatientDetailPage : ContentPage
    {
        public PatientDetailPage(PatientData patientData)
        {
            InitializeComponent();
            PatientNameLabel.Text = $"Navn: {patientData.Name}";
            PatientCPRLabel.Text = $"CPR: {patientData.CPR}";
            PatientAlder.Text = $"Alder: {patientData.Alder}";
            PatientK�n.Text = $"K�n: {patientData.K�n}";
            PatientH�jde.Text = $"H�jde: {patientData.H�jde}";
            PatientV�gt.Text = $"V�gt: {patientData.V�gt}";
            PatientEtnicitet.Text = $"Etnicitet: {patientData.Etnicitet}";
            LoadPatientM�linger(patientData.CPR);
            }

       private async void LoadPatientM�linger (string cprNumber)
        {
            // Hent m�linger fra databasen for den valgte patient
            var m�linger = await App.Database.GetPatientM�lingerByCPRAsync(cprNumber);

            if (m�linger != null)
            {
                var m�lingStrings = m�linger.Select(m => $"Dato: {m.Dato}, FEV1: {m.FEV1}, FCV: {m.FCV}").ToList();
                M�lingerListView.ItemsSource = m�lingStrings;
            }
            else
            {
                // Hvis ingen m�linger findes, vis besked
                await DisplayAlert("Ingen m�linger", "Ingen m�linger fundet for denne patient.", "OK");
            }
        }

        private async void OnReturnButtonClicked (object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }

}