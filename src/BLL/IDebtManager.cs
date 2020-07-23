using System.Threading.Tasks;
using HouseholdDebtTracker.BLL.Models;
using System.Collections.Generic;

namespace HouseholdDebtTracker.BLL
{
    /// <summary>
    /// Class that can be used to manage debts
    /// </summary>
    public interface IDebtManager
    {
        /// <summary>
        /// Gets all debts ever created (debts history)
        /// </summary>
        /// <returns> List of debts </returns>
        Task<List<Debt>> GetDebtsHistoryAsync();

        /// <summary>
        /// Gets debts status
        /// note: Id, date and type in returned debts will be useless and should not be used
        /// </summary>
        /// <returns> List of debts </returns>
        Task<List<Debt>> GetDebtsStatusAsync();

        /// <summary>
        /// Gets debts settlements
        /// note: Id and date in returned debts will be useless and should not be used
        /// </summary>
        /// <returns> List of debt settlements </returns>
        Task<List<Debt>> GetDebtSettlementsAsync();

        /// <summary>
        /// Gets optimal debt settlements
        /// note: Id and date in returned debts will be useless and should not be used
        /// note: operation can take long time
        /// </summary>
        /// <returns> List of debt settlements </returns>
        Task<List<Debt>> GetOptimalDebtSettlementsAsync();

        /// <summary>
        /// Gets debt by its id
        /// </summary>
        /// <param name="id"> id of debt</param>
        /// <returns> debt </returns>
        Task<Debt> GetDebtByIdAsync(int id);

        /// <summary>
        /// Adds a debt
        /// </summary>
        /// <param name="debt"> Valid non-null debt </param>
        Task AddDebtAsync(Debt debt);

        /// <summary>
        /// Removes a debt
        /// </summary>
        /// <param name="debt"> Valid non-null debt </param>
        Task RemoveDebtAsync(Debt debt);
        /// <summary>
        /// Adds a debt
        /// </summary>
        /// <param name="debt"> Valid non-null debt </param>
        Task UpdateDebtAsync(Debt debt);
    }
}