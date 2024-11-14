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
            LoadPatientMålinger(patientData.CPR);
            ForventetResultat(patientData);
        }

        private void DisplayPatientDetails(PatientData patientData)
        {
            PatientNameLabel.Text = $"Navn: {patientData.Name}";
            PatientCPRLabel.Text = $"CPR: {patientData.CPR}";
            PatientAlder.Text = $"Alder: {patientData.Alder}";
            PatientKøn.Text = $"Køn: {patientData.Køn}";
            PatientHøjde.Text = $"Højde: {patientData.Højde}";
            PatientVægt.Text = $"Vægt: {patientData.Vægt}";
            PatientEtnicitet.Text = $"Etnicitet: {patientData.Etnicitet}";
        }

        private async void LoadPatientMålinger(string cprNumber)
        {
            var målinger = await App.Database.GetPatientMålingerByCPRAsync(cprNumber);

            if (målinger != null)
            {
                var målingStrings = målinger.Select(m => $"Dato: {m.Dato}, FEV1: {m.FEV1}, FCV: {m.FCV}, Ratio: {m.Ratio}").Reverse().ToList();
                MålingerListView.ItemsSource = målingStrings;
            }
            else
            {
                await DisplayAlert("Ingen målinger", "Ingen målinger fundet for denne patient.", "OK");
            }
        }

        private async void OnReturnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void ForventetResultat(PatientData patientData)
        {
            int hojde = Convert.ToInt16(patientData.Højde);
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
                DisplayAlert("Opmærksom", "Kan ikke vise forventet LFU resultater, da patienten ikke har udfyldt sine personlige oplysninger", "OK");
                return;
            }

            if (patientData.Køn == "Mand")
            {
                forventetFVC = ((0.052 * hojde) - (0.022 * alder) - 3.1) * racefaktor;
                forventetFEV1 = ((0.046 * hojde) - (0.025 * alder) - 3.5) * racefaktor;
            }
            else if (patientData.Køn == "Kvinde")
            {
                forventetFVC = ((0.045 * hojde) - (0.020 * alder) - 2.9) * racefaktor;
                forventetFEV1 = ((0.04 * hojde) - (0.022 * alder) - 3.2) * racefaktor;
            }
            else
            {
                DisplayAlert("Fejl", "Fejl i udregning af forventet lungeværdier", "OK");
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
                    $"Ud fra de angivet patientoplysninger ville de forventet resultater være\n" +
                    $"FVC: {forventetFVC:F2} L\n" +
                    $"FEV1: {forventetFEV1:F2} L\n" +
                    $"Ratio: {forventetRatio:F2}%",
                    "OK");
            }
        }

    }
}
