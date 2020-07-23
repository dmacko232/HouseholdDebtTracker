using HouseholdDebtTracker.DAL.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HouseholdDebtTracker.DAL
{
    /// <summary>
    /// Accessor that can be used to access debts in database
    /// </summary>
    public interface IDebtAccessor
    {
        /// <summary>
        /// Gets all debts from database
        /// </summary>
        /// <returns> List of all debts </returns>
        Task<List<Debt>> GetAllDebtsAsync();

        /// <summary>
        /// Inserts a debt into database
        /// </summary>
        /// <param name="debt"> Valid non-null debt </param>
        Task InsertDebtAsync(Debt debt);

        /// <summary>
        /// Inserts a debt into database
        /// </summary>
        /// <param name="debt"> Valid non-null debt </param>
        Task DeleteDebtAsync(Debt debt);

        /// <summary>
        /// Inserts a debt into database
        /// </summary>
        /// <param name="debt"> Valid non-null debt </param>

        Task UpdateDebtAsync(Debt debt);

        /// <summary>
        /// Inserts a debt into database
        /// </summary>
        /// <param name="debt"> Valid non-null debt </param>
        Task<Debt> GetDebtAsync(int id);
    }
}
