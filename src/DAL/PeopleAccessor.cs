using HouseholdDebtTracker.DAL.Data;
using HouseholdDebtTracker.DAL.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HouseholdDebtTracker.DAL
{
    public class PeopleAccessor : IPeopleAccessor
    {
        private readonly DebtTrackerContext _db;

        public PeopleAccessor(DebtTrackerContext db) => _db = db;

        public async Task<List<Person>> GetAllPeopleAsync() 
            => await _db.People.AsNoTracking().ToListAsync();

        public async Task<List<Person>> GetAllPeopleWithDebtsAsync() 
            => await _db.People.AsNoTracking().Include(e => e.Debts).ThenInclude(d => d.Creditor).
                    Include(e => e.Credits).ThenInclude(d => d.Debtor).ToListAsync();

        public async Task InsertPersonAsync(Person person)
        {
            _db.People.Add(person);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(Person person)
        {
            _db.People.Remove(person);
            await _db.SaveChangesAsync();
        }

        public async Task UpdatePersonAsync(Person person)
        {
            _db.People.Update(person);
            await _db.SaveChangesAsync();
        }

        public async Task<Person> GetPersonAsync(int id) 
            => await _db.People.AsNoTracking().FirstOrDefaultAsync(person => person.ID == id);

        public async Task<Person> GetPersonWithDebtsAsync(int id)
        {
            var person = await _db.People.AsNoTracking().Include(e => e.Debts).ThenInclude(d => d.Creditor).Include(e => e.Credits).ThenInclude(d => d.Debtor)
                    .FirstOrDefaultAsync(person => person.ID == id);
            return person;
        }

        public async Task InsertPeopleAsync(List<Person> people)
        {
            _db.People.AddRange(people);
            await _db.SaveChangesAsync();
        }
    }
}
