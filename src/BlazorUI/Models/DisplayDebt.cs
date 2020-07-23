using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HouseholdDebtTracker.BLL.Models;

namespace HouseholdDebtTracker.BlazorUI.Models
{
    /// <summary>
    /// UI Display debt model
    /// </summary>
    public class DisplayDebt
    {
        public List<Person> People { get; set; }

        [Required]
        [Range(typeof(decimal), "1", Constants.MaximumDebtValue,
             ErrorMessage="{0} must be in range between {1} and {2}")]
        public decimal Amount { get; set; }

        [Required]
        public string DebtorIndex { get; set; }

        [Required]
        public string CreditorIndex { get; set; }

        [Required]
        public DisplayDebtType Type { get; set; }
    }
}