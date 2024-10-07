namespace Maui_spiro
{
    public partial class PatientDetailPage : ContentPage
    {
        public PatientDetailPage(PatientData patientData)
        {
            InitializeComponent();
            PatientNameLabel.Text = $"Navn: {patientData.Name}";
            PatientCPRLabel.Text = $"CPR: {patientData.CPR}";
            PatientDato.Text = $"Dato: {patientData.Dato}";
            PatientK�n.Text = $"K�n :{patientData.K�n}";
            PatientH�jde.Text = $"H�jde: {patientData.H�jde}";
            PatientV�gt.Text = $"V�gt: {patientData.V�gt}";
            PatientEtnicitet.Text = $"Etnicitet: {patientData.Etnicitet}";
            LungFunctionLabel.Text = $"Lungefunktion:\n{patientData.FCV} \n{patientData.FEV1}";
      // test
            }
    }

}