using System.Collections.Generic;
using System.Threading.Tasks;
using HouseholdDebtTracker.BLL.Models;

namespace HouseholdDebtTracker.BLL
{
    /// <summary>
    /// Class that can be used to manage people
    /// </summary>
    public interface IPeopleManager
    {
        /// <summary>
        /// Gets all people
        /// </summary>
        /// <returns> List of people </returns>
        Task<List<Person>> GetAllPeopleAsync();

        /// <summary>
        /// Gets all people with debts history included
        /// </summary>
        /// <returns> List of people with debts history </returns>
        Task<List<Person>> GetAllPeopleWithDebtsHistoryAsync();

        /// <summary>
        /// Gets all people with debts status included
        /// note: Id, date and type in person's debts should not be used after this call
        /// </summary>
        /// <returns> List of people with debts status</returns>
        Task<List<Person>> GetAllPeopleWithDebtsStatusAsync();

        /// <summary>
        /// Adds person
        /// </summary>
        /// <param name="person"> Valid non-null person</param>
        Task AddPersonAsync(Person person);

        /// <summary>
        /// Removes person
        /// </summary>
        /// <param name="person"> Valid non-null person</param>
        Task RemovePersonAsync(Person person);

        /// <summary>
        /// Updates person
        /// </summary>
        /// <param name="person"> Valid non-null person</param>
        Task UpdatePersonAsync(Person person);

        /// <summary>
        /// Gets person based on id
        /// </summary>
        /// <param name="id"> id of person </param>
        /// <returns> person </returns>
        Task<Person> GetPersonAsync(int id);

        /// <summary>
        /// Gets person based on id with debts history
        /// </summary>
        /// <param name="id"> id of person </param>
        /// <returns> person with debts history </returns>
        Task<Person> GetPersonWithDebtsHistoryAsync(int id);

        /// <summary>
        /// Gets person based on id with debts status
        /// note: Id, date and type in person's debts should not be used after this call
        /// </summary>
        /// <param name="id"> id of person </param>
        /// <returns> person with debts status </returns>
        Task<Person> GetPersonWithDebtsStatusAsync(int id);

        /// <summary>
        /// Exports all people into json
        /// </summary>
        /// <returns> json representation of people </returns>
        Task<byte[]> ExportPeopleAsync();

        /// <summary>
        /// Imports people from json
        /// </summary>
        /// <param name="data"> stream in json format</param>
        Task ImportPeopleAsync(System.IO.MemoryStream data);
    }
}