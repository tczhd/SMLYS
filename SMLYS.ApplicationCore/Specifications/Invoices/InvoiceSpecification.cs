using SMLYS.ApplicationCore.Entities.CommonAggregate;
using SMLYS.ApplicationCore.Entities.InvoiceAggregate;
using SMLYS.ApplicationCore.Entities.PatientAggregate;
using SMLYS.ApplicationCore.Specifications.Base;


namespace SMLYS.ApplicationCore.Specifications.Invoices
{
    public class InvoiceSpecification : BaseSpecification<Invoice>
    {
        public InvoiceSpecification(int clinicId) : base()
        {
            AddCriteria(q => q.ClinicId == clinicId);
            AddInclude(b => b.Doctor);
            AddInclude(b => b.Patient);
            AddInclude($"{nameof(Invoice.Patient)}.{nameof(Patient.Address)}");
            AddInclude($"{nameof(Invoice.Patient)}.{nameof(Patient.PatientCardOnFile)}");
            AddInclude($"{nameof(Invoice.Patient)}.{nameof(Patient.Address)}.{nameof(Address.Country)}");
           // AddInclude($"{nameof(Invoice.Patient)}.{nameof(Patient.Address)}.{nameof(Address.Region)}");
            AddInclude(b => b.InvoiceItem);
            AddInclude($"{nameof(Invoice.InvoiceItem)}.{nameof(InvoiceItem.Item)}");
            AddInclude(b => b.InvoicePayment);
            AddInclude($"{nameof(Invoice.InvoicePayment)}.{nameof(InvoicePayment.Payment)}");
        }

        public void AddInvoiceId(int invoiceId)
        {
            AddCriteria(q => q.Id == invoiceId);
        }


        public void AddPatientId(int patientId)
        {
            AddCriteria(q => q.Id == patientId);
        }

        public void AddFirstName(string firstName)
        {
            AddCriteria(q => q.Patient.FirstName == firstName);
        }

        public void AddLastName(string lastName)
        {
            AddCriteria(q => q.Patient.LastName == lastName);
        }
    }
}
