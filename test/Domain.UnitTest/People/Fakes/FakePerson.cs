using Domain.MedicalAppointments;
using Domain.People;

namespace Domain.UnitTest.People.Fakes
{
    internal class FakePerson : Person
    {
        public FakePerson(FakeCode code, string name) : base(code, name)
        {
        }

        protected override decimal GetMedicalAppointmentValue(MedicalAppointment medicalAppointment)
        {
            return 0;
        }
    }
}
