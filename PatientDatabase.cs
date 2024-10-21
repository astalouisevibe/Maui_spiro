using SQLite;

namespace Maui_spiro
{
    public class PatientDatabase
    {
        private SQLiteAsyncConnection _database;

        public PatientDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<PatientData>().Wait();
            _database.CreateTableAsync<PatientMålinger>().Wait();
        }

        // Gemmer en ny patient i databasen
        public Task<int> SavePatientAsync(PatientData patient)
        {
            return _database.InsertAsync(patient);
        }

        // Søger efter en patient via CPR-nummer
        public Task<PatientData> GetPatientByCPRAsync(string cprNumber)
        {
            return _database.Table<PatientData>()
                            .Where(p => p.CPR == cprNumber)
                            .FirstOrDefaultAsync();

        }
        public Task<int> UpdatePatientAsync(PatientData patient)
        {
            var eksisterende_patient = this.GetPatientByCPRAsync(patient.CPR);

            if (eksisterende_patient != null)
            {
                var ny_patient = new PatientData()
                {
                    CPR = patient.CPR,
                    Alder = patient.Alder,
                    Dato = null,
                    Etnicitet = patient.Etnicitet,
                    FCV = null,
                    FEV1 = null,
                    Højde = patient.Højde,
                    Køn = patient.Køn,
                    Name = patient.Name,
                    Vægt = patient.Vægt,
                };

                var maaling = new PatientMålinger()
                {
                    CPR = patient.CPR,
                    Dato = patient.Dato,
                    FCV = patient.FCV,
                    FEV1 = patient.FEV1,
                };

                return _database.InsertAsync(maaling);
            }
            else
            {
                var maaling = new PatientMålinger()
                {
                    CPR = patient.CPR,
                    Dato = patient.Dato,
                    FCV = patient.FCV,
                    FEV1 = patient.FEV1,
                };

                return _database.InsertAsync(maaling);
            }


        }
    }
}
