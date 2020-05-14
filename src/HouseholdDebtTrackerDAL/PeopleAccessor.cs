using HouseholdDebtTrackerDAL.Data;
using HouseholdDebtTrackerDAL.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HouseholdDebtTrackerDAL
{
    public class PeopleAccessor : IPeopleAccessor
    {
        private readonly DebtTrackerContext _db;

        public PeopleAccessor(DebtTrackerContext db) => _db = db;

        public async Task<List<PersonModel>> GetPeopleAsync(Func<PersonModel, bool> condition)
        {
            return await _db.People.
                Where(person => condition(person)).
                ToListAsync();
        }

        public async Task<List<PersonModel>> GetAllPeopleAsync() => await _db.People.ToListAsync();

        public async Task InsertPersonAsync(PersonModel person)
        {
            _db.People.Add(person);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(PersonModel person)
        {
            _db.People.Remove(person);
            await _db.SaveChangesAsync();
        }

        public async Task UpdatePersonAsync(PersonModel person)
        {
            _db.People.Update(person);
            await _db.SaveChangesAsync();
        }

        public async Task<PersonModel> GetPersonAsync(int id) 
            => await _db.People.FirstOrDefaultAsync(person => person.ID == id);
    }
}