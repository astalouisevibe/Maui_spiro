namespace Maui_spiro
{
    public partial class LoginPage
    {
        public class LoginValidator
        {
            // Forudbestemt login
            private const string ValidAUHId = "test";  // Forudbestemt AUH-ID
            private const string ValidKode = "1234";        // Forudbestemt kode

            public bool Validate(string auhId, string kode)
            {
                return auhId == ValidAUHId && kode == ValidKode;
            }
        }
    }
}
