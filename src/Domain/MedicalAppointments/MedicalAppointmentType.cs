using System.ComponentModel;

namespace Domain.MedicalAppointments
{
    //Enum para guardar possiveis tipos de consulta
    public enum MedicalAppointmentType
    {
        [Description("Sob demanda")]
        OnDemand = 0,

        [Description("Agendada")]
        Scheduled = 1
    }
}
