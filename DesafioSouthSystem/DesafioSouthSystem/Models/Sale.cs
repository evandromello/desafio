using System.Collections.Generic;

namespace DesafioSouthSystem.Models
{
    public class Sale
    {
        public Sale(int saleId, string salesmanName, decimal totalSale, List<Product> products)
        {
            SaleId = saleId;
            SalesmanName = salesmanName;
            Products = products;
            TotalSale = totalSale;
        }

        public int SaleId { get; set; }
        public string SalesmanName { get; set; }
        public decimal TotalSale { get; set; }
        private List<Product> Products { get; set; }
    }
}