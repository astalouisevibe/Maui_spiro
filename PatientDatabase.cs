using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Maui_spiro
{
    public class PatientDatabase
    {
        private SQLiteAsyncConnection _database;

        public PatientDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<PatientData>().Wait(); // Opretter PatientData-tabellen
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
    }
}
