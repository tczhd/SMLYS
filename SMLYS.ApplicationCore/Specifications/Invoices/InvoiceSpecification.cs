using SMLYS.ApplicationCore.Entities.InvoiceAggregate;
using SMLYS.ApplicationCore.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Specifications.Invoices
{
    public class InvoiceSpecification : BaseSpecification<Invoice>
    {
        public InvoiceSpecification(int clinicId) : base()
        {
            AddCriteria(q => q.ClinicId == clinicId);
            AddInclude(b => b.Doctor);
            AddInclude(b => b.Patient);
            AddInclude(b => b.InvoiceItem);
            AddInclude($"{nameof(Invoice.InvoiceItem)}.{nameof(InvoiceItem.Item)}");
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
