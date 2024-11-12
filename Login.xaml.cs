namespace Maui_spiro
{
    public partial class LoginPage : ContentPage
    {
       private readonly LoginValidator _loginvalidator;

        public LoginPage()
        {
            InitializeComponent();
            _loginvalidator = new LoginValidator();
        }

        // Event handler for login-knap
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string auhId = AUHIdEntry.Text;
            string kode = kodeEntry.Text;

            if (_loginvalidator.Validate(auhId,kode))
            {
                await DisplayAlert("Succes", "Login succesfuldt!", "OK");
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Fejl", "Ugyldigt AU-ID eller kode. Prøv igen.", "OK");
            }
        }
    }
}
