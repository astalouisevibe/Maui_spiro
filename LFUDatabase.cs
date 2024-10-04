using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_spiro
{
    internal class LFUDatabase
    {

        PatientData _patientData = new PatientData();
        public PatientData PatientData
        {
            get { return _patientData; }
        }
    }
}
