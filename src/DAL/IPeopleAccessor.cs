using HouseholdDebtTracker.DAL.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace HouseholdDebtTracker.DAL
{
    /// <summary>
    /// Accessor that can be used to access people in database
    /// </summary>
    public interface IPeopleAccessor
    {
        /// <summary>
        /// Gets all people stored in database
        /// </summary>
        /// <returns> People without debts </returns>
        Task<List<Person>> GetAllPeopleAsync();

        /// <summary>
        /// Gets all people stored in database with debts
        /// </summary>
        /// <returns> People with debts included </returns>
        Task<List<Person>> GetAllPeopleWithDebtsAsync();

        /// <summary>
        /// Inserts person into database
        /// </summary>
        /// <param name="person"> Person to insert </param>
        Task InsertPersonAsync(Person person);

        /// <summary>
        /// Deletes person from database
        /// </summary>
        /// <param name="person"> Person to delete </param>
        Task DeletePersonAsync(Person person);

        /// <summary>
        /// Updates person in database
        /// </summary>
        /// <param name="person">Person to update </param>
        Task UpdatePersonAsync(Person person);

        /// <summary>
        /// Gets person from database based on id
        /// </summary>
        /// <param name="id"> Id of person </param>
        /// <returns> Searched person, null if not found</returns>
        Task<Person> GetPersonAsync(int id);

        /// <summary>
        /// Gets person from database based on id with debts included
        /// </summary>
        /// <param name="id"> Id of person </param>
        /// <returns> Person with debts, null if not found </returns>
        Task<Person> GetPersonWithDebtsAsync(int id);

        /// <summary>
        /// Inserts people into database
        /// </summary>
        /// <param name="people"> Non-null list of people </param>
        Task InsertPeopleAsync(List<Person> people);
    }
}
