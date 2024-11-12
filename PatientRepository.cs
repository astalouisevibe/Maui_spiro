namespace Maui_spiro
{
    public partial class MainPage
    {
        public class PatientRepository
        {
            public async Task<PatientData> FindPatientInDatabase(string cprNumber)
            {
                // Søg efter patient i databasen
                return await App.Database.GetPatientByCPRAsync(cprNumber);
            }
        }


    }
}
