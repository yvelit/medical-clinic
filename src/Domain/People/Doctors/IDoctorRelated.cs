namespace Domain.People.Doctors
{
    public interface IDoctorRelated
    {
        Crm DoctorCode { get; }
        Doctor Doctor { get; }
    }
}
