using System;
using Core.Extensions;
using Domain.MedicalAppointments;
using Domain.People.Customers;
using Domain.People.Doctors;

namespace Domain.People
{
    public class Doctor : Person
    {
        public Doctor(Crm crm, string name, MedicalSpecialty medicalSpecialty) : base(crm, name)
        {
            MedicalSpecialty = medicalSpecialty;
        }

        public decimal MedicalSpecialtyValue
        {
            get { return GetMedicalAppointmentValue(); }
        }

        #region MedicalSpecialtyValue

        private decimal GetMedicalAppointmentValue()
        {
            switch (MedicalSpecialty)
            {
                case MedicalSpecialty.GeneralClinic:
                    return 80;

                case MedicalSpecialty.Otorhinolaryngology:
                    return 150;
                case MedicalSpecialty.Orthopedy:
                    return 130;
                case MedicalSpecialty.Urologist:
                    return 120;
                case MedicalSpecialty.Anesthesiologist:
                    return 160;
                case MedicalSpecialty.Dermatologist:
                    return 90;
                case MedicalSpecialty.Gynecologist:
                    return 120;
                case MedicalSpecialty.Neurologist:
                    return 150;
                case MedicalSpecialty.Pedriatrician:
                    return 80;
                case MedicalSpecialty.Surgeon:
                    return 180;

                default:
                    throw new NotImplementedException("Especialidade médica ainda não implementada.");
            }
        }

        #endregion MedicalSpecialtyValue

        public MedicalSpecialty MedicalSpecialty
        {
            get { return GetMedicalSpecialty(); }
            set { SetMedicalSpecialty(value); }
        }

        #region MedicalSpecialty

        private MedicalSpecialty _medicalSpecialty;

        public MedicalSpecialty GetMedicalSpecialty()
        {
            return _medicalSpecialty;
        }

        public void SetMedicalSpecialty(MedicalSpecialty value)
        {
            _medicalSpecialty = value;
        }

        #endregion MedicalSpecialty

        //Método que calcula valor da consulta para o médico com base no plano de saúde do cliente
        protected override decimal GetMedicalAppointmentValue(MedicalAppointment medicalAppointment)
        {
            if (medicalAppointment.Customer.CustomerType == CustomerType.Normal)
            {
                return medicalAppointment.Value * 0.8m;
            }

            return 0;
        }

        public override string ToString()
        {
            return base.ToString().Replace("Código", "CRM") + $" - Especialidade médica: {MedicalSpecialty.GetDescription()}";
        }

        public override bool Equals(object obj)
        {
            return obj is Doctor other && Equals(other);
        }

        private bool Equals(Doctor other)
        {
            return MedicalSpecialty == other.MedicalSpecialty && base.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
