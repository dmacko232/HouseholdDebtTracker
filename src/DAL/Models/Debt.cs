using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdDebtTracker.DAL.Models
{
    /// <summary>
    /// Class model that is used for debt to be stored in Database
    /// </summary>
    [Table("Debts")]
    public class Debt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public DebtType Type { get; set; }
        [Required]
        [ForeignKey("Debtor")]
        public int DebtorId { get; set; }
        public Person Debtor { get; set; }
        [Required]
        [ForeignKey("Creditor")]
        public int CreditorId { get; set; }
        public Person Creditor { get; set; }
        [Required]
        [Column(TypeName = "smallmoney")]
        public decimal Amount { get; set; }
    }
}
