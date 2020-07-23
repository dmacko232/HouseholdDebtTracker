using System.Collections.Generic;
using HouseholdDebtTracker.BLL.Models;

namespace HouseholdDebtTracker.BLL.Utility
{
    /// <summary>
    /// Debt calculator that can be used to calculate debt status and settlements
    /// </summary>
    public interface IDebtCalculator
    {
        /// <summary>
        /// Calculates debts status from debts history
        /// note: Id, date and type in returned debts will be useless and should not be used
        /// </summary>
        /// <param name="debts"> debts history </param>
        /// <returns> list of debts status </returns>
        List<Debt> CalculateDebtsStatus(List<Debt> debts);

        /// <summary>
        /// Calculates debts settlements from debts history
        /// note: Id and date in returned debts will be useless and should not be used
        /// </summary>
        /// <param name="debts"> debts history </param>
        /// <returns> list of debts settlements </returns>
        List<Debt> CalculateSettlements(List<Debt> debts);

        /// <summary>
        /// Calculates optimal debt settlements from debts history
        /// note: Id and date in returned debts will be useless and should not be used
        /// note: time and memory complexity can be n! (much slower than non-optimal)
        /// </summary>
        /// <param name="debts"> debts history </param>
        /// <returns> list of debts settlements </returns>
        List<Debt> CalculateOptimalSettlements(List<Debt> debts);
    }
}