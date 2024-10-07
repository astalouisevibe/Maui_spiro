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
            PatientKøn.Text = $"Køn :{patientData.Køn}";
            PatientHøjde.Text = $"Højde: {patientData.Højde}";
            PatientVægt.Text = $"Vægt: {patientData.Vægt}";
            PatientEtnicitet.Text = $"Etnicitet: {patientData.Etnicitet}";
            LungFunctionLabel.Text = $"Lungefunktion:\n{patientData.FCV} \n{patientData.FEV1}";
      // test
            }
    }

}