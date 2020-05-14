using HouseholdDebtTrackerDAL.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace HouseholdDebtTrackerDAL
{
    public interface IDebtAccessor
    {
        Task<List<DebtModel>> GetDebtsAsync(Func<DebtModel, bool> condition);

        Task<List<DebtModel>> GetAllDebtsAsync();

        Task<List<DebtModel>> GetAllDebtsByDebtorIdAsync(int debtorId);

        Task<List<DebtModel>> GetAllDebtsByCreditorIdAsync(int creditorId);

        Task InsertDebtAsync(DebtModel debt);

        Task DeleteDebtAsync(DebtModel debt);

        Task UpdateDebtAsync(DebtModel debt);

        Task<DebtModel> GetDebtAsync(int id);
    }
}