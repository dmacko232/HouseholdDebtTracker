using System;
using HouseholdDebtTracker.BLL.Models;
using HouseholdDebtTracker.BlazorUI.Models;

namespace HouseholdDebtTracker.BlazorUI.Utility
{
    public class BLLMapper : IBLLMapper
    {

        private Gender MapToBLLGender(DisplayGender gender)
        {
            switch (gender)
            {
                case DisplayGender.Male:
                    return Gender.Male;
                case DisplayGender.Female:
                    return Gender.Female;
                default:
                    return Gender.Other;
            }
        }

        private DebtType MapToBLLDebtType(DisplayDebtType type)
        {
            switch (type)
            {
                case DisplayDebtType.Loan:
                    return DebtType.Loan;
                default:
                    return DebtType.Repayment;
            }
        }

        public Person MapToBLLPerson(DisplayPerson person)
        {
           var mapped = new Person();
           mapped.Gender = MapToBLLGender(person.Gender);
           mapped.Name = person.Name;
           mapped.NickName = person.NickName;
           return mapped;
        }

        public Debt MapToBLLDebt(DisplayDebt debt)
        {
            var mapped = new Debt();
            mapped.Amount = debt.Amount;
            mapped.Creditor = debt.People[int.Parse(debt.CreditorIndex)];
            mapped.Date = DateTime.Now;
            mapped.Debtor = debt.People[int.Parse(debt.DebtorIndex)];
            mapped.Type = MapToBLLDebtType(debt.Type);
            return mapped;
        }
    }
}