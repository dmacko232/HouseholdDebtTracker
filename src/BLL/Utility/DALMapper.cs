using HouseholdDebtTracker.BLL.Models;
using System.Linq;
using System.Collections.Generic;

namespace HouseholdDebtTracker.BLL.Utility
{
    public class DALMapper : IDALMapper
    {
        private Gender MapFromDALGender(HouseholdDebtTracker.DAL.Models.Gender gender)
        {
            switch (gender)
            {
                case HouseholdDebtTracker.DAL.Models.Gender.Male:
                    return Gender.Male;
                case HouseholdDebtTracker.DAL.Models.Gender.Female: 
                    return Gender.Female;
                default: 
                    return Gender.Other;
            }
        }

        private HouseholdDebtTracker.DAL.Models.Gender MapToDALGender(Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return HouseholdDebtTracker.DAL.Models.Gender.Male;
                case Gender.Female: 
                    return HouseholdDebtTracker.DAL.Models.Gender.Female;
                default: 
                    return HouseholdDebtTracker.DAL.Models.Gender.Other;
            }
        }

        private DebtType MapFromDALDebtType(HouseholdDebtTracker.DAL.Models.DebtType type)
        {
            switch (type)
            {
                case HouseholdDebtTracker.DAL.Models.DebtType.Loan:
                    return DebtType.Loan;
                default:
                    return DebtType.Repayment;
            }
        }

        private HouseholdDebtTracker.DAL.Models.DebtType MapToDALDebtType(DebtType type)
        {
            switch (type)
            {
                case DebtType.Loan:
                    return HouseholdDebtTracker.DAL.Models.DebtType.Loan;
                default:
                    return HouseholdDebtTracker.DAL.Models.DebtType.Repayment;
            }
        }

        public Person MapFromDALPerson(HouseholdDebtTracker.DAL.Models.Person person) 
        {
            if (person == null)
            {
                return null;
            }
            var mappedPerson = new Person();
            mappedPerson.ID = person.ID;
            mappedPerson.Gender = MapFromDALGender(person.Gender);
            mappedPerson.Name = person.Name;
            mappedPerson.NickName = person.NickName;

            var debtsToMap = (person.Debts == null) ? new List<HouseholdDebtTracker.DAL.Models.Debt>() : person.Debts;
            debtsToMap.AddRange(person.Credits ?? new List<HouseholdDebtTracker.DAL.Models.Debt>());
            mappedPerson.Debts = debtsToMap.Select(d => MapFromDALDebt(d)).ToList();
            return mappedPerson;
        }

        public HouseholdDebtTracker.DAL.Models.Person MapToDALPerson(Person person)
        {
            if (person == null)
            {
                return null;
            }
            var mappedPerson = new HouseholdDebtTracker.DAL.Models.Person();
            mappedPerson.ID = person.ID.GetValueOrDefault();
            mappedPerson.Gender = MapToDALGender(person.Gender);
            mappedPerson.Name = person.Name;
            mappedPerson.NickName = person.NickName;
            // EXPLICITLY dont do anything with debts
            return mappedPerson;
        }

        public Debt MapFromDALDebt(HouseholdDebtTracker.DAL.Models.Debt debt)
        {
            if (debt == null)
            {
                return null;
            }
            // set nested debts to null so we dont cycle in mapping whole "tree"
            if (debt.Debtor != null)
            {
                debt.Debtor.Debts = null;
                debt.Debtor.Credits = null;
            }
            if (debt.Creditor != null)
            {
                debt.Creditor.Debts = null;
                debt.Creditor.Credits = null;
            }
            return new Debt(debt.ID, debt.Date, MapFromDALDebtType(debt.Type),
                        MapFromDALPerson(debt.Debtor), MapFromDALPerson(debt.Creditor), debt.Amount);
        }

        public HouseholdDebtTracker.DAL.Models.Debt MapToDALDebt(Debt debt)
        {
            if (debt == null)
            {
                return null;
            }
            var mappedDebt = new HouseholdDebtTracker.DAL.Models.Debt();
            mappedDebt.ID = debt.ID.GetValueOrDefault();
            mappedDebt.Date = debt.Date.GetValueOrDefault();
            mappedDebt.Type = MapToDALDebtType(debt.Type.GetValueOrDefault());
            mappedDebt.Amount = debt.Amount;
            if (debt.Debtor != null)
            {
                mappedDebt.DebtorId = debt.Debtor.ID.GetValueOrDefault();
                mappedDebt.Debtor = MapToDALPerson(debt.Debtor);
            }
            if (debt.Creditor != null)
            {
                mappedDebt.CreditorId = debt.Creditor.ID.GetValueOrDefault();
                mappedDebt.Creditor = MapToDALPerson(debt.Creditor);
            }
            return mappedDebt;
        }
    }
}