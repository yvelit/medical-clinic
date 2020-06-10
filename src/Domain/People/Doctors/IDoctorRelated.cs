namespace Domain.People.Doctors
{
    //Interface que relaciona um crm a um medico
    public interface IDoctorRelated
    {
        Crm DoctorCode { get; }
        Doctor Doctor { get; }
    }
}
