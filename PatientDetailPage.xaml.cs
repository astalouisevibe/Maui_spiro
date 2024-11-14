namespace Maui_spiro
{
    public partial class PatientDetailPage : ContentPage
    {
        private PatientData _patientData;
        private double forventetFVC;
        private double forventetFEV1;
        private double forventetRatio;

        public PatientDetailPage(PatientData patientData)
        {
            _patientData = patientData;

            InitializeComponent();
            DisplayPatientDetails(patientData);
            LoadPatientM�linger(patientData.CPR);
            ForventetResultat(patientData);
        }

        private void DisplayPatientDetails(PatientData patientData)
        {
            PatientNameLabel.Text = $"Navn: {patientData.Name}";
            PatientCPRLabel.Text = $"CPR: {patientData.CPR}";
            PatientAlder.Text = $"Alder: {patientData.Alder}";
            PatientK�n.Text = $"K�n: {patientData.K�n}";
            PatientH�jde.Text = $"H�jde: {patientData.H�jde}";
            PatientV�gt.Text = $"V�gt: {patientData.V�gt}";
            PatientEtnicitet.Text = $"Etnicitet: {patientData.Etnicitet}";
        }

        private async void LoadPatientM�linger(string cprNumber)
        {
            var m�linger = await App.Database.GetPatientM�lingerByCPRAsync(cprNumber);

            if (m�linger != null)
            {
                var m�lingStrings = m�linger.Select(m => $"Dato: {m.Dato}, FEV1: {m.FEV1}, FCV: {m.FCV}, Ratio: {m.Ratio}").Reverse().ToList();
                M�lingerListView.ItemsSource = m�lingStrings;
            }
            else
            {
                await DisplayAlert("Ingen m�linger", "Ingen m�linger fundet for denne patient.", "OK");
            }
        }

        private async void OnReturnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void ForventetResultat(PatientData patientData)
        {
            int hojde = Convert.ToInt16(patientData.H�jde);
            int alder = Convert.ToInt16(patientData.Alder);
            double racefaktor;

            if (patientData.Etnicitet == "Kaukasisk" || patientData.Etnicitet == "Andet")
            {
                racefaktor = 1;
            }
            else if (patientData.Etnicitet == "Afrikansk" || patientData.Etnicitet == "Latino")
            {
                racefaktor = 0.87;
            }
            else if (patientData.Etnicitet == "Asiatisk")
            {
                racefaktor = 0.93;
            }
            else
            {
                DisplayAlert("Opm�rksom", "Kan ikke vise forventet LFU resultater, da patienten ikke har udfyldt sine personlige oplysninger", "OK");
                return;
            }

            if (patientData.K�n == "Mand")
            {
                forventetFVC = ((0.052 * hojde) - (0.022 * alder) - 3.1) * racefaktor;
                forventetFEV1 = ((0.046 * hojde) - (0.025 * alder) - 3.5) * racefaktor;
            }
            else if (patientData.K�n == "Kvinde")
            {
                forventetFVC = ((0.045 * hojde) - (0.020 * alder) - 2.9) * racefaktor;
                forventetFEV1 = ((0.04 * hojde) - (0.022 * alder) - 3.2) * racefaktor;
            }
            else
            {
                DisplayAlert("Fejl", "Fejl i udregning af forventet lungev�rdier", "OK");
                return;
            }

            forventetRatio = (forventetFEV1 / forventetFVC) * 100;
            //ForventetResultatLabel.Text = $"Forventet FVC: {forventetFVC:F2} L, Forventet FEV1: {forventetFEV1:F2} L, Forventet Ratio: {forventetRatio:F2}%";
        }

        private async void OnForventetButtonClicked(object sender, EventArgs e)
        {
            if (forventetFVC == 0 || forventetFEV1 == 0 || forventetRatio == 0)
            {
                await DisplayAlert("Fejl", "Forventede resultater er ikke beregnet korrekt.", "OK");
            }
            else
            {
                await DisplayAlert("Forventet Resultat",
                    $"Ud fra de angivet patientoplysninger ville de forventet resultater v�re\n" +
                    $"FVC: {forventetFVC:F2} L\n" +
                    $"FEV1: {forventetFEV1:F2} L\n" +
                    $"Ratio: {forventetRatio:F2}%",
                    "OK");
            }
        }

    }
}
