namespace Maui_spiro
{
    public partial class PatientDetailPage : ContentPage
    {
        private PatientData _patientData;
        public PatientDetailPage(PatientData patientData)
        {
            _patientData = patientData;
            InitializeComponent();
            PatientNameLabel.Text = $"Navn: {patientData.Name}";
            PatientCPRLabel.Text = $"CPR: {patientData.CPR}";
            PatientAlder.Text = $"Alder: {patientData.Alder}";
            PatientKøn.Text = $"Køn: {patientData.Køn}";
            PatientHøjde.Text = $"Højde: {patientData.Højde}";
            PatientVægt.Text = $"Vægt: {patientData.Vægt}";
            PatientEtnicitet.Text = $"Etnicitet: {patientData.Etnicitet}";
            LoadPatientMålinger(patientData.CPR);
            ForventetResultat(patientData);
            }

       private async void LoadPatientMålinger (string cprNumber)
        {
            // Hent målinger fra databasen for den valgte patient
            var målinger = await App.Database.GetPatientMålingerByCPRAsync(cprNumber);

            if (målinger != null)
            {
                var målingStrings = målinger.Select(m => $"Dato: {m.Dato}, FEV1: {m.FEV1}, FCV: {m.FCV}, Ratio: {m.Ratio}").ToList();
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
        private async void ForventetResultat(PatientData patientData)
        {
            _patientData = patientData;
            int hojde = Convert.ToInt16(_patientData.Højde);
            int alder = Convert.ToInt16(_patientData.Alder);
            double racefaktor;
            double forventetFVC;
            double forventetFEV1;
            double forventetRatio;
            if (_patientData.Etnicitet == "Kaukasisk" || _patientData.Etnicitet == "Andet")
            {
               racefaktor = 1;
            }
            else if (_patientData.Etnicitet == "Afrikansk" || _patientData.Etnicitet == "Latino")
            {
                racefaktor = 0.87;
            }
            else if (_patientData.Etnicitet == "Asiatisk")
            {
                racefaktor = 0.93;
            }
            else
            {
                await DisplayAlert("Fejl", "fejl i at bestemme racefaktor", "ok");
                return;
            }

            if (_patientData.Køn == "Mand")
            {
                forventetFVC = ((0.052 * hojde) - (0.022 * alder) - 3.1) * racefaktor; //formel fundet fra internettet
                forventetFEV1 = ((0.046 * hojde) - (0.025 * alder) - 3.5) * racefaktor;
                forventetRatio = forventetFEV1 / forventetFVC;
                ForventetResultatLabel.Text = $"Forventet FVC: {forventetFVC:F2}, Forventet FEV1: {forventetFEV1:F2}, Forventet Ratio: {forventetRatio:F2}";
              
            }
            else if (_patientData.Køn == "Kvinde")
            {
                forventetFVC = ((0.045 * hojde) - (0.020 * alder) - 2.9) * racefaktor; //formel fundet fra internettet
                forventetFEV1 = ((0.04 * hojde) - (0.022 * alder) - 3.2) * racefaktor;
                forventetRatio = forventetFEV1 / forventetFVC;
                ForventetResultatLabel.Text = $"Forventet FVC: {forventetFVC:F2}, Forventet FEV1: {forventetFEV1:F2}, Forventet Ratio: {forventetRatio:F2}";

            }
            else
            {
                await DisplayAlert("Fejl", "fejl i udregning af forventet lungeværdier", "ok");
            }
            
            
        }
    
    }

}