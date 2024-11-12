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
            PatientK�n.Text = $"K�n: {patientData.K�n}";
            PatientH�jde.Text = $"H�jde: {patientData.H�jde}";
            PatientV�gt.Text = $"V�gt: {patientData.V�gt}";
            PatientEtnicitet.Text = $"Etnicitet: {patientData.Etnicitet}";
            LoadPatientM�linger(patientData.CPR);
            ForventetResultat(patientData);
            }

       private async void LoadPatientM�linger (string cprNumber)
        {
            // Hent m�linger fra databasen for den valgte patient
            var m�linger = await App.Database.GetPatientM�lingerByCPRAsync(cprNumber);

            if (m�linger != null)
            {
                var m�lingStrings = m�linger.Select(m => $"Dato: {m.Dato}, FEV1: {m.FEV1}, FCV: {m.FCV}, Ratio: {m.Ratio}").ToList();
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
        private async void ForventetResultat(PatientData patientData)
        {
            _patientData = patientData;
            int hojde = Convert.ToInt16(_patientData.H�jde);
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

            if (_patientData.K�n == "Mand")
            {
                forventetFVC = ((0.052 * hojde) - (0.022 * alder) - 3.1) * racefaktor; //formel fundet fra internettet
                forventetFEV1 = ((0.046 * hojde) - (0.025 * alder) - 3.5) * racefaktor;
                forventetRatio = forventetFEV1 / forventetFVC;
                ForventetResultatLabel.Text = $"Forventet FVC: {forventetFVC:F2}, Forventet FEV1: {forventetFEV1:F2}, Forventet Ratio: {forventetRatio:F2}";
              
            }
            else if (_patientData.K�n == "Kvinde")
            {
                forventetFVC = ((0.045 * hojde) - (0.020 * alder) - 2.9) * racefaktor; //formel fundet fra internettet
                forventetFEV1 = ((0.04 * hojde) - (0.022 * alder) - 3.2) * racefaktor;
                forventetRatio = forventetFEV1 / forventetFVC;
                ForventetResultatLabel.Text = $"Forventet FVC: {forventetFVC:F2}, Forventet FEV1: {forventetFEV1:F2}, Forventet Ratio: {forventetRatio:F2}";

            }
            else
            {
                await DisplayAlert("Fejl", "fejl i udregning af forventet lungev�rdier", "ok");
            }
            
            
        }
    
    }

}