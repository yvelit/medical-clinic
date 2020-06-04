using System.ComponentModel;

namespace Domain.People.Doctors
{
    public enum MedicalSpecialty
    {
        [Description("Urologista")]
        Urologist = 0,
        [Description("Clínica geral")]
        GeneralClinic = 1,

        [Description("Otorrinolaringologia")]
        Otorhinolaryngology = 2,

        [Description("Ortopedia")]
        Orthopedy = 3,
        [Description("Anestesista")]
        Anesthesiologist = 4,
        [Description("Dermatologista")]
        Dermatologist = 5,
        [Description("Ginecologista")]
        Gynecologist = 6,
        [Description("Neurologista")]
        Neurologist = 7,
        [Description("Pediatra")]
        Pedriatrician = 8,
        [Description("Cirurgião")]
        Surgeon = 9
    }
}
