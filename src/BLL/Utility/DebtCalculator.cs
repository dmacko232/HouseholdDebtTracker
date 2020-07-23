using System.Collections.Generic;
using HouseholdDebtTracker.BLL.Models;
using System.Linq;
using System;
using Combinatorics.Collections;

namespace HouseholdDebtTracker.BLL.Utility
{
    public class DebtCalculator : IDebtCalculator
    {
        // flips debt type of given debt
        private static Debt FlipDebtType(Debt debt)
        {
            var type = (debt.Type == DebtType.Repayment) ? DebtType.Loan : DebtType.Repayment;
            return new Debt(debt.ID, debt.Date, type, 
                            debt.Debtor, debt.Creditor, - debt.Amount);
        }

        // flips debtor and creditor when creditor's id is smaller than debtor's
        private static Debt FlipDebtorCreditorById(Debt debt)
        {
            if (debt.Debtor == null)
            {
                return debt;
            }
            if (debt.Creditor == null || debt.Creditor.ID < debt.Debtor.ID)
            {
                return new Debt(debt.ID, debt.Date, debt.Type,
                                 debt.Creditor, debt.Debtor, -debt.Amount);
            }
            return debt;
        }

        private static Debt FlipToPositiveAmount(Debt debt)
        {
            if (debt.Amount < 0)
            {
                return new Debt(debt.ID, debt.Date, debt.Type,
                                 debt.Creditor, debt.Debtor, -debt.Amount);
            }
            return new Debt(debt.ID, debt.Date, debt.Type, debt.Debtor, debt.Creditor, debt.Amount);
        }

        private static Debt ConvertToSettlement(Debt debt)
            => new Debt(debt.ID, debt.Date, DebtType.Repayment, debt.Debtor, debt.Creditor, debt.Amount);

        public List<Debt> CalculateDebtsStatus(List<Debt> debts)
        {
            // flip debt type so we dont have repayments
            // sort by id so debtor and creditor will always appear in same order
            debts = debts.
                Where(d => d.Debtor?.ID != d.Creditor?.ID).
                Select(d => (d.Type == DebtType.Repayment) ? FlipDebtType(d) : d).
                Select(d => FlipDebtorCreditorById(d)). 
                ToList();
            var groupped = new Dictionary<Tuple<int?, int?>, Debt>();
            foreach (var debt in debts) 
            {
                var key = new Tuple<int?, int?>(debt.Debtor?.ID, debt.Creditor?.ID);
                if (groupped.ContainsKey(key))
                {
                    groupped[key].Amount += debt.Amount;
                } 
                else 
                {
                    groupped.Add(key, new Debt(null, null, null, debt.Debtor, debt.Creditor, debt.Amount));
                }
            }
            var res = groupped.Values.Where(d => d.Amount != 0).Select(d => FlipToPositiveAmount(d)).ToList();
            return res;
        }

        private List<Tuple<Person, decimal>> CalculatePeopleBalance(List<Debt> debts)
        {
            var statuses = CalculateDebtsStatus(debts);
            var peopleWithBalance = new Dictionary<int, Tuple<Person, decimal>>();
            foreach (var debtStatus in statuses)
            {
                if (debtStatus.Amount == 0)
                {
                    continue;
                }
                if (!peopleWithBalance.ContainsKey((int)debtStatus.Debtor.ID))
                {
                    peopleWithBalance.Add((int)debtStatus.Debtor.ID, new Tuple<Person,decimal>(debtStatus.Debtor, 0));
                }
                if (!peopleWithBalance.ContainsKey((int)debtStatus.Creditor.ID))
                {
                    peopleWithBalance.Add((int)debtStatus.Creditor.ID, new Tuple<Person,decimal>(debtStatus.Creditor, 0));
                }
                var curr = peopleWithBalance[(int)debtStatus.Debtor.ID];
                peopleWithBalance[(int)debtStatus.Debtor.ID] = new Tuple<Person, decimal>(curr.Item1, curr.Item2 - debtStatus.Amount);
                curr = peopleWithBalance[(int)debtStatus.Creditor.ID];
                peopleWithBalance[(int)debtStatus.Creditor.ID] = new Tuple<Person, decimal>(curr.Item1, curr.Item2 + debtStatus.Amount);
            }
            var sortedPeopleWithBalance = peopleWithBalance.
                Select(p => p.Value).Where(p => p.Item2 != 0).ToList();      
            sortedPeopleWithBalance.Sort((a, b) => decimal.Compare(a.Item2, b.Item2));
            return sortedPeopleWithBalance;
        }

        private List<Debt> CalculateSettlementsBasedOnBalances(List<Tuple<Person, decimal>> balances)
        {
            var settlements = new List<Debt>();
            int startIndex = 0;
            int endIndex = balances.Count() - 1;
            while (endIndex - startIndex > 0)
            {
                var debtor = balances[startIndex];
                var creditor = balances[endIndex];
                // set amount to settle
                decimal amount = (debtor.Item2 * -1 < creditor.Item2) ? debtor.Item2 * -1 : creditor.Item2;
                if (debtor.Item2 * - 1 <= amount)
                {   // we will have to zero out debtor since his whole debt is getting settled
                    startIndex++;
                }
                else
                {
                    balances[startIndex] = new Tuple<Person, decimal>(debtor.Item1, debtor.Item2 + amount);
                }
                if (creditor.Item2 <= amount)
                {   // whole credit is getting repaid, zero out creditor
                    endIndex--;
                }
                else
                {
                    balances[endIndex] = new Tuple<Person, decimal>(creditor.Item1, creditor.Item2 - amount);
                }
                settlements.Add(new Debt(null, null, DebtType.Repayment, debtor.Item1, creditor.Item1, amount));
            }
            return settlements;
        }

        public List<Debt> CalculateSettlements(List<Debt> debts)
        {
            var balances = CalculatePeopleBalance(debts);
            return CalculateSettlementsBasedOnBalances(balances);
        }

        private static bool CombinationIsContainedInSettlement(
            List<Debt> settlements, IList<Tuple<Person, decimal>> combinations)
        {
            foreach (var settlement in settlements)
            {
                foreach (var comb in combinations)
                {
                    if (settlement.Debtor == comb.Item1 || settlement.Creditor == comb.Item1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        
        // source of inspiration for pseudocode: http://www.settleup.info/files/master-thesis-david-vavra.pdf
        public List<Debt> CalculateOptimalSettlements(List<Debt> debts)
        {
            var balances = CalculatePeopleBalance(debts);
            var settlements = new List<Debt>();
            for (int pairs = 2; pairs < balances.Count; pairs++)
            {
                var combinations = new Combinations<Tuple<Person, decimal>>(balances, pairs).ToList();
                foreach (var comb in combinations)
                {
                    if (comb.Sum(p => p.Item2) != 0)
                    {
                        continue; // subgroup cant settle internally
                    }
                    // we found a subgroup of size pairs that can settle internally between themselves
                    var currentSettlements = CalculateSettlementsBasedOnBalances(comb.ToList());
                    settlements.AddRange(currentSettlements);
        
                    // remove balances that contain someone from current settlements
                    balances = balances.Except(comb).ToList();
                    // remove combinations that contain someone from current settlements
                    combinations = combinations.
                        Where(c => !CombinationIsContainedInSettlement(currentSettlements, c)).
                        ToList();
                    
                }
            }
            // we cant find anything more optimal so settle up the rest suboptimally
            if (balances.Count != 0)
            {
                settlements.AddRange(CalculateSettlementsBasedOnBalances(balances));
            }
            return settlements;
        }
    }
}