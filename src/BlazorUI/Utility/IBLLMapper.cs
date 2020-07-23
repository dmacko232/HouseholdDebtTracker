using HouseholdDebtTracker.BLL.Models;
using HouseholdDebtTracker.BlazorUI.Models;

namespace HouseholdDebtTracker.BlazorUI.Utility
{
    /// <summary>
    /// Mapper that can be used to map display models to business logic layer models
    /// </summary>
    public interface IBLLMapper
    {
        /// <summary>
        /// Maps display person to BLL person
        /// </summary>
        /// <param name="person"> display person </param>
        /// <returns> BLL person </returns>
        Person MapToBLLPerson(DisplayPerson person);

        /// <summary>
        /// Maps display debt to BLL debt
        /// </summary>
        /// <param name="debt"> display debt </param>
        /// <returns> BLL debt </returns>
        Debt MapToBLLDebt(DisplayDebt debt);
    }
}