using HouseholdDebtTracker.DAL.Models;
using HouseholdDebtTracker.DAL.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HouseholdDebtTracker.DAL
{
    public class DebtAccessor : IDebtAccessor
    {
        private readonly DebtTrackerContext _db;

        public DebtAccessor(DebtTrackerContext db) => _db = db;


        public async Task<List<Debt>> GetAllDebtsAsync()
        {
            var result = await _db.Debts.AsNoTracking().Include(e => e.Creditor).Include(e => e.Debtor).ToListAsync();
            return result;
        }

        public async Task InsertDebtAsync(Debt debt)
        {
            debt.Creditor = null;
            debt.Debtor = null;
            await _db.Debts.AddAsync(debt);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteDebtAsync(Debt debt)
        {
            debt.Creditor = null;
            debt.Debtor = null;
            _db.Debts.Remove(debt);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateDebtAsync(Debt debt)
        {
            debt.Creditor = null;
            debt.Debtor = null;
            _db.Debts.Update(debt);
            await _db.SaveChangesAsync();
        }

        public async Task<Debt> GetDebtAsync(int id)
        {
            return await _db.Debts.AsNoTracking().Include(e => e.Creditor).Include(e => e.Debtor).FirstAsync(e => e.ID == id);
        }
    }
}
