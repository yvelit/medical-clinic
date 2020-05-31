using System.ComponentModel;

namespace Domain.People.Doctors
{
    public enum MedicalSpecialty
    {
        [Description("Clínica geral")]
        GeneralClinic = 1,

        [Description("Otorrinolaringologia")]
        Otorhinolaryngology = 2,

        [Description("Ortopedia")]
        Orthopedy = 3
    }
}
