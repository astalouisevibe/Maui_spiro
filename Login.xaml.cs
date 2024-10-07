namespace Maui_spiro
{
    public partial class LoginPage : ContentPage
    {
        // Forudbestemt login
        private const string ValidAUHId = "test";  // Forudbestemt AUH-ID
        private const string ValidKode = "1234";        // Forudbestemt kode

        public LoginPage()
        {
            InitializeComponent();
        }

        // Event handler for login-knap
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string auhId = AUHIdEntry.Text;
            string kode = kodeEntry.Text;

            // Simpel validering af login
            if (auhId == ValidAUHId && kode == ValidKode)
            {
                await DisplayAlert("Succes", "Login succesfuldt!", "OK");
                // Naviger til startsiden (MainPage)
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Fejl", "Ugyldigt AU-ID eller kode. Prøv igen.", "OK");
            }
        }
    }
}
