using HouseholdDebtTracker.BLL.Models;

namespace HouseholdDebtTracker.BLL.Utility
{
    /// <summary>
    /// Mapper that can be used to map objects between business logic layer and data access layer
    /// </summary>
    public interface IDALMapper
    {
        /// <summary>
        /// Maps DAL person to BLL person
        /// </summary>
        /// <param name="person"> DAL person</param>
        /// <returns> BLL person</returns>
        Person MapFromDALPerson(HouseholdDebtTracker.DAL.Models.Person person);

        /// <summary>
        /// Maps BLL person to DAL person
        /// </summary>
        /// <param name="person"> BLL person </param>
        /// <returns> DAL person </returns>
        HouseholdDebtTracker.DAL.Models.Person MapToDALPerson(Person person);

        /// <summary>
        /// Maps DAL debt to BLL debt
        /// </summary>
        /// <param name="debt"> DAL debt </param>
        /// <returns> BLL debt </returns>
        Debt MapFromDALDebt(HouseholdDebtTracker.DAL.Models.Debt debt);

        /// <summary>
        /// Maps BLL debt to DAL debt
        /// </summary>
        /// <param name="debt"> BLL debt </param>
        /// <returns> DAL debt </returns>
        HouseholdDebtTracker.DAL.Models.Debt MapToDALDebt(Debt debt);
    }
}