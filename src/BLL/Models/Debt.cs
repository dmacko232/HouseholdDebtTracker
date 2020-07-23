using System;

namespace HouseholdDebtTracker.BLL.Models
{
    /// <summary>
    /// Class used to represent business logic debt
    /// </summary>
    public class Debt
    {
        public int? ID { get; set; }

        public DateTime? Date { get; set; }

        public DebtType? Type { get; set; }

        public Person Debtor { get; set; }

        public Person Creditor { get; set; }

        public decimal Amount { get; set; }

        public Debt() {}

        public Debt(int? id, DateTime? date, DebtType? type, Person debtor, Person creditor, decimal amount)
            => (ID, Date, Type, Debtor, Creditor, Amount) = (id, date, type, debtor, creditor, amount);
    }
}