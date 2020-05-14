using HouseholdDebtTrackerDAL.Models;
using HouseholdDebtTrackerDAL.Data;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HouseholdDebtTrackerDAL
{
    public class DebtAccessor : IDebtAccessor
    {
        private readonly DebtTrackerContext _db;

        public async Task<List<DebtModel>> GetDebtsAsync(Func<DebtModel, bool> condition)
        {
            return await _db.Debts.
                Where(debt => condition(debt)).
                ToListAsync();
        }

        public async Task<List<DebtModel>> GetAllDebtsAsync() => await _db.Debts.ToListAsync();

        public async Task<List<DebtModel>> GetAllDebtsByDebtorIdAsync(int debtorId)
        {
            return await _db.Debts.
                Where(debt => debt.Debtor.ID == debtorId).
                ToListAsync();
        }

        public async Task<List<DebtModel>> GetAllDebtsByCreditorIdAsync(int creditorId)
        {
            return await _db.Debts.
                Where(debt => debt.Creditor.ID == creditorId).
                ToListAsync();
        }

        public async Task InsertDebtAsync(DebtModel debt)
        {
            _db.Debts.Add(debt);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteDebtAsync(DebtModel debt)
        {
            _db.Debts.Remove(debt);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateDebtAsync(DebtModel debt)
        {
            _db.Debts.Add(debt);
            await _db.SaveChangesAsync();
        }

        public async Task<DebtModel> GetDebtAsync(int id) 
            => await _db.Debts.FirstOrDefaultAsync(debt => debt.ID == id);
    }
}