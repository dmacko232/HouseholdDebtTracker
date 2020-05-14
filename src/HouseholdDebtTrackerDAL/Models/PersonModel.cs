using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HouseholdDebtTrackerDAL.Models
{
    public class PersonModel
    {
        [Key]
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

    }
}