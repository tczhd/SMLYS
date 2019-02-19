


namespace SMLYS.ApplicationCore.Entities.InvoiceAggregate
{
    public partial class InvoiceItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal Total { get; set; }
        public string Note { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Item Item { get; set; }
    }
}
