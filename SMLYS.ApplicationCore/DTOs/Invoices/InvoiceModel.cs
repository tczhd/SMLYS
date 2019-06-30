using SMLYS.ApplicationCore.Entities.InvoiceAggregate;
using SMLYS.ApplicationCore.Interfaces.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using SMLYS.ApplicationCore.DTOs.Common;

namespace SMLYS.ApplicationCore.DTOs.Invoices
{
    public class InvoiceModel : IResultable<Invoice, InvoiceModel>
    {
        public int InvoiceId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientEmail { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal Total { get; set; }
        public decimal AmountPaid { get; set; }
        public int InvoiceStatus { get; set; }
        public int PaymentStatus { get; set; }
        public string Note { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDateUTC { get; set; }
        public int? UpdatedBy { get; set; }
        public bool ReOccouring { get; set; }
        public int ClinicId { get; set; }
        public AddressModel PatientAddress { get; set; }
        public List<InvoiceItemModel> InvoiceItems { get; set; }
        public List<InvoicePaymentModel> InvoicePayments { get; set; }
        public Expression<Func<Invoice, InvoiceModel>> CreateResult()
        {
            return m => new InvoiceModel
            {
              AmountPaid = m.AmountPaid,
              ClinicId= m.ClinicId,
              CreatedBy = m.CreatedBy,
              DiscountTotal= m.DiscountTotal,
              DoctorId = m.DoctorId,
              DoctorName = m.Doctor != null ? m.Doctor.FirstName + " " + m.Doctor.LastName : string.Empty,
              InvoiceDate = m.InvoiceDate,
              InvoiceId = m.Id,
              InvoiceStatus = m.InvoiceStatus,
              Note = m.Note,
              PatientId = m.PatientId,
              PatientName = m.Patient != null?m.Patient.FirstName + " " + m.Patient.LastName:string.Empty,
              PatientEmail = m.Patient != null ? m.Patient.Email: string.Empty,
                PaymentStatus = m.PaymentStatus,
              ReOccouring = m.ReOccouring,
              Subtotal = m.Subtotal,
              TaxTotal = m.TaxTotal,
              Total = m.Total,
              UpdatedBy = m.UpdatedBy,
              UpdatedDateUTC = m.UpdatedDateUtc ,
              PatientAddress = (m.Patient.Address != null)? m.Patient.Address : null,
              InvoiceItems = m.InvoiceItem.Select(p => (InvoiceItemModel)p).ToList(),
              InvoicePayments = m.InvoicePayment.Select(p => (InvoicePaymentModel)p).ToList()
            };
        }

        public static implicit operator InvoiceModel(Invoice source)
        {
            return new InvoiceModel
            {
                AmountPaid = source.AmountPaid,
                ClinicId = source.ClinicId,
                CreatedBy = source.CreatedBy,
                DiscountTotal = source.DiscountTotal,
                DoctorId = source.DoctorId,
                DoctorName = source.Doctor != null ? source.Doctor.FirstName + " " + source.Doctor.LastName : string.Empty,
                InvoiceDate = source.InvoiceDate,
                InvoiceId = source.Id,
                InvoiceStatus = source.InvoiceStatus,
                Note = source.Note,
                PatientId = source.PatientId,
                PatientName = source.Patient != null ? source.Patient.FirstName + " " + source.Patient.LastName : string.Empty,
                PatientEmail = source.Patient != null ? source.Patient.Email : string.Empty,
                PaymentStatus = source.PaymentStatus,
                ReOccouring = source.ReOccouring,
                Subtotal = source.Subtotal,
                TaxTotal = source.TaxTotal,
                Total = source.Total,
                UpdatedBy = source.UpdatedBy,
                UpdatedDateUTC = source.UpdatedDateUtc,
                PatientAddress = (source.Patient.Address != null)? source.Patient.Address: null,
                InvoiceItems = source.InvoiceItem.Select(p => (InvoiceItemModel)p).ToList(),
                InvoicePayments = source.InvoicePayment.Select(p => (InvoicePaymentModel)p).ToList()
            };
        }

        public static implicit operator Invoice(InvoiceModel source)
        {
            return new Invoice
            {
                AmountPaid = source.AmountPaid,
                ClinicId = source.ClinicId,
                CreatedBy = source.CreatedBy,
                DiscountTotal = source.DiscountTotal,
                DoctorId = source.DoctorId,
                InvoiceDate = source.InvoiceDate,
                Id = source.InvoiceId,
                InvoiceStatus = source.InvoiceStatus,
                Note = source.Note,
                PatientId = source.PatientId,
                PaymentStatus = source.PaymentStatus,
                ReOccouring = source.ReOccouring,
                Subtotal = source.Subtotal,
                TaxTotal = source.TaxTotal,
                Total = source.Total,
                UpdatedBy = source.UpdatedBy,
                UpdatedDateUtc = source.UpdatedDateUTC,
                InvoiceItem = source.InvoiceItems.Select(p => (InvoiceItem)p).ToList(),
                InvoicePayment = source.InvoicePayments.Select(p => (InvoicePayment)p).ToList()
            };
        }

    }
}
