using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


    public partial class Sales: BaseEntity {

        public Sales( Guid customer, string customerName,  string branch)
        {
            SaleDate = DateTime.Now;
            Customer = customer;
            CustomerName = customerName;
            TotalSaleAmount = 0;
            Branch = branch;
            Items = [];
            TotalAmount = 0;
            IsCancelled = false;
        }

       
       /// <summary>
       /// Represents the date of the sale
       /// </summary>
        public DateTime SaleDate { get; private set; }

        /// <summary>
        /// Represents the customer who made the sale
        /// Must be a valid customer id
        /// </summary>
        public Guid Customer { get; private set; }
        /// <summary>
        /// Represents the name of the customer
        /// Must be string with max length of 50
        /// </summary>
        public string CustomerName { get; private set; }

        /// <summary>
        /// Represents the total amount of the sale
        /// </summary>
        public decimal TotalSaleAmount { get; private set; }

        /// <summary>
        /// Represents the branch where the sale was made
        /// Must be string with max length of 50
        /// </summary>
        public string Branch { get; private set; }

        /// <summary>
        /// Represents the items of the sale
        /// </summary>
        public virtual List<Item> Items { get; private set; }

        /// <summary>
        /// Represents the total amount of the sale
        /// </summary>
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Represents if the sale was cancelled
        /// Must be boolean
        /// </summary>
        public bool IsCancelled { get; private set; }

}