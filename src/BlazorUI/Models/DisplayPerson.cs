using System.ComponentModel.DataAnnotations;

namespace HouseholdDebtTracker.BlazorUI.Models
{
    /// <summary>
    /// UI Display person
    /// </summary>
    public class DisplayPerson
    {
        [Required]
        [StringLength(30, ErrorMessage = "Name is too long.")]
        [MinLength(3, ErrorMessage = "Name is too short.")]
        public string Name { get; set; }

        [Required]
        public DisplayGender Gender { get; set; }
        
        [StringLength(15, ErrorMessage = "Nickname is too long.")]
        [MinLength(3, ErrorMessage = "Nickname is too short.")]
        public string NickName { get; set; }
    }
}