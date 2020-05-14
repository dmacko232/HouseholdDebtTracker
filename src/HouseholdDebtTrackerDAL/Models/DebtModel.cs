using System;
using System.ComponentModel.DataAnnotations;

namespace HouseholdDebtTrackerDAL.Models
{
    public class DebtModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public PersonModel Debtor { get; set; }
        [Required]
        public PersonModel Creditor { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}