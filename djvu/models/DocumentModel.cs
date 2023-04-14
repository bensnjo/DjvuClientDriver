using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace djvu.models
{
    internal class DocumentModel
    {
        public string Temp { get; set; }
        public string DocumentPin { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerPin { get; set; }
        public string DocumentType { get; set; }
        public string ItemDescription { get; set; }
        public string TaxType { get; set; }
        public string ItemCode { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double SaleAmount { get; set; }
        public double TaxAmtA { get; set; }
        public double TaxAmtB { get; set; }
        public double TaxAmtC { get; set; }
        public double TaxAmtD { get; set; }
        public double TaxAmtE { get; set; }
        public double TaxableAmount { get; set; }
        public double TotalTax { get; set; }
        public double TotalAmount { get; set; }
        public string QRLocation { get; set; }
    }
}
