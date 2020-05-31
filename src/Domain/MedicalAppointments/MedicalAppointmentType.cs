using System.ComponentModel;

namespace Domain.MedicalAppointments
{
    public enum MedicalAppointmentType
    {
        [Description("Sob demanda")]
        OnDemand = 0,

        [Description("Agendada")]
        Scheduled = 1
    }
}
