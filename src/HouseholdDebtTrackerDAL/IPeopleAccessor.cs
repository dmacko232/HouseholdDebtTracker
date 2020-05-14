using HouseholdDebtTrackerDAL.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace HouseholdDebtTrackerDAL
{
    public interface IPeopleAccessor
    {
        Task<List<PersonModel>> GetPeopleAsync(Func<PersonModel, bool> condition);
        
        Task<List<PersonModel>> GetAllPeopleAsync();

        Task InsertPersonAsync(PersonModel person);

        Task DeletePersonAsync(PersonModel person);

        Task UpdatePersonAsync(PersonModel person);

        Task<PersonModel> GetPersonAsync(int id);
    }
}