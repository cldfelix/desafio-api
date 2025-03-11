using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


public partial class User
{
    public class Sales: BaseEntity {

        public Sales( Guid customer, string branch)
        {
            SaleDate = DateTime.Now;
            Customer = customer;
            TotalSaleAmount = 0;
            Branch = branch;
            Items = [];
            TotalAmount = 0;
            IsCancelled = false;
        }

        public DateTime SaleDate { get; private set; }
        public Guid Customer { get; private set; }
        public decimal TotalSaleAmount { get; private set; }
        public string? Branch { get; private set; }
        public List<Item>? Items { get; private set; }
        public decimal? TotalAmount { get; private set; }
        public bool IsCancelled { get; private set; }

    }
}