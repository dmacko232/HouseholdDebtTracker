using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace HouseholdDebtTracker.DAL.Models
{
    /// <summary>
    /// Class model that is used to store people in Database
    /// </summary>
    [Table("People")]
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        
#nullable enable
        [MaxLength(40)]
        public string? NickName { get; set; }
#nullable disable

        [InverseProperty("Debtor")]
        public List<Debt> Debts { get; set; }

        [InverseProperty("Creditor")]
        public List<Debt> Credits { get; set; }
    }
}
