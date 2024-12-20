﻿using System.Diagnostics;

namespace Maui_spiro;

public partial class CreatePatientPage
{
    public class PatientService
    {
        public bool ValidatePatientData(string patientName, string cprNumber)
        {
            return !string.IsNullOrWhiteSpace(cprNumber) && !string.IsNullOrWhiteSpace(patientName) && cprNumber.Length == 10 && long.TryParse(cprNumber, out _);
        }

        public async Task SaveNewPatient(string patientName, string cprNumber)
        {

            // Opret en ny patient med CPR og Navn fra inputfelterne
            var newPatient = new PatientData
            {
                Name = patientName,
                CPR = cprNumber,
                Alder = CalculateAge(cprNumber),
                Køn = GetGender(cprNumber),
            };

            Debug.WriteLine($"Saving patient: Name = {newPatient.Name}, CPR = {newPatient.CPR}, Alder = {newPatient.Alder}");
            await App.Database.SavePatientAsync(newPatient);
        }

        public string GetGender(string cprNumber)
        {
            int GenderString = int.Parse(cprNumber.Substring(9, 1));
            string Male = "Mand";
            string Female = "Kvinde";
            if (GenderString % 2 == 0)
            {
                return Female;
            }
            else
            {
                return Male;
            }
        }
        public string CalculateAge(string cprNumber)
        {
            // Ekstraher fødselsdato fra CPR-nummeret
            string birthDateString = cprNumber.Substring(0, 6);
            int day = int.Parse(birthDateString.Substring(0, 2));
            int month = int.Parse(birthDateString.Substring(2, 2));
            int year = int.Parse(birthDateString.Substring(4, 2));


            if (year < 24)
                year += 2000;
            else
                year += 1900;


            DateTime birthDate = new DateTime(year, month, day);

            // Beregn alder
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now < birthDate.AddYears(age)) age--;

            return Convert.ToString(age);
        }
    }
}