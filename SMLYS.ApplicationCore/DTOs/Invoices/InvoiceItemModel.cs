using SMLYS.ApplicationCore.Entities.InvoiceAggregate;
using SMLYS.ApplicationCore.Interfaces.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.Invoices
{
    public class InvoiceItemModel : IResultable<InvoiceItem, InvoiceItemModel>
    {
        public int InvoiceItemId { get; set; }
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal Total { get; set; }
        public string Note { get; set; }
        public Expression<Func<InvoiceItem, InvoiceItemModel>> CreateResult()
        {
            return m => new InvoiceItemModel
            {
                InvoiceId = m.InvoiceId,
                InvoiceItemId = m.Id,
                ItemId = m.ItemId,
                ItemName = m.Item != null? m.Item.Name: string.Empty,
                Note = m.Note,
                Price = m.Price,
                Quantity = m.Quantity,
                Subtotal = m.Subtotal,
                TaxTotal = m.TaxTotal,
                Total = m.Total,
            };
        }

        public static implicit operator InvoiceItemModel(InvoiceItem source)
        {
            return new InvoiceItemModel
            {
                InvoiceId = source.InvoiceId,
                InvoiceItemId = source.Id,
                ItemId = source.ItemId,
                ItemName = source.Item != null ? source.Item.Name : string.Empty,
                Note = source.Note,
                Price = source.Price,
                Quantity = source.Quantity,
                Subtotal = source.Subtotal,
                TaxTotal = source.TaxTotal,
                Total = source.Total
            };
        }

        public static implicit operator InvoiceItem(InvoiceItemModel source)
        {
            return new InvoiceItem
            {
                InvoiceId = source.InvoiceId,
                Id = source.InvoiceItemId,
                ItemId = source.ItemId,
                Note = source.Note,
                Price = source.Price,
                Quantity = source.Quantity,
                Subtotal = source.Subtotal,
                TaxTotal = source.TaxTotal,
                Total = source.Total
            };
        }

    }
}
