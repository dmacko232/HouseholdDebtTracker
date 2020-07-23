using System.Collections.Generic;
using System.Threading.Tasks;
using HouseholdDebtTracker.BLL.Models;
using HouseholdDebtTracker.BLL.Utility;
using HouseholdDebtTracker.DAL;
using System.Linq;
using System;

namespace HouseholdDebtTracker.BLL
{
    public class DebtManager : IDebtManager
    {
        private readonly IDebtAccessor _accessor;
        private readonly IDALMapper _mapper;
        private readonly IDebtCalculator _calculator;

        public DebtManager(IDebtAccessor accesor, IDALMapper mapper, IDebtCalculator calculator) 
            => (_accessor, _mapper, _calculator) = (accesor, mapper, calculator);

        public async Task AddDebtAsync(Debt debt)
            {  await _accessor.InsertDebtAsync(_mapper.MapToDALDebt(debt)); }

        public async Task<List<Debt>> GetDebtsHistoryAsync()
        {
            var result = (await _accessor.GetAllDebtsAsync()).Select(d => _mapper.MapFromDALDebt(d)).ToList();
            return result;
        } 

        public async Task<List<Debt>> GetDebtsStatusAsync()
            { return _calculator.CalculateDebtsStatus(await GetDebtsHistoryAsync()); }

        public async Task<List<Debt>> GetDebtSettlementsAsync()
            { return _calculator.CalculateSettlements(await GetDebtsHistoryAsync()); }

        public async Task<List<Debt>> GetOptimalDebtSettlementsAsync()
            { return _calculator.CalculateOptimalSettlements(await GetDebtsHistoryAsync()); }

        public async Task RemoveDebtAsync(Debt debt)
            { await _accessor.DeleteDebtAsync(_mapper.MapToDALDebt(debt)); }

        public async Task UpdateDebtAsync(Debt debt)
            { await _accessor.UpdateDebtAsync(_mapper.MapToDALDebt(debt)); }

        public async Task<Debt> GetDebtByIdAsync(int id)
            { return _mapper.MapFromDALDebt(await _accessor.GetDebtAsync(id)); }
    }
}