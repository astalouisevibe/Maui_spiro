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
            PatientKøn.Text = $"Køn: {patientData.Køn}";
            PatientHøjde.Text = $"Højde: {patientData.Højde}";
            PatientVægt.Text = $"Vægt: {patientData.Vægt}";
            PatientEtnicitet.Text = $"Etnicitet: {patientData.Etnicitet}";
            LoadPatientMålinger(patientData.CPR);
            }

       private async void LoadPatientMålinger (string cprNumber)
        {
            // Hent målinger fra databasen for den valgte patient
            var målinger = await App.Database.GetPatientMålingerByCPRAsync(cprNumber);

            if (målinger != null)
            {
                var målingStrings = målinger.Select(m => $"Dato: {m.Dato}, FEV1: {m.FEV1}, FCV: {m.FCV}").ToList();
                MålingerListView.ItemsSource = målingStrings;
            }
            else
            {
                // Hvis ingen målinger findes, vis besked
                await DisplayAlert("Ingen målinger", "Ingen målinger fundet for denne patient.", "OK");
            }
        }

        private async void OnReturnButtonClicked (object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }

}