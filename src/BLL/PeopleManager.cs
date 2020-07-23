using System.Collections.Generic;
using System.Threading.Tasks;
using HouseholdDebtTracker.BLL.Models;
using HouseholdDebtTracker.DAL;
using System.Linq;
using System.IO;
using HouseholdDebtTracker.BLL.Utility;
using Newtonsoft.Json;
using System.Text;

namespace HouseholdDebtTracker.BLL
{
    public class PeopleManager : IPeopleManager
    {
        private readonly IPeopleAccessor _accessor;
        private readonly IDALMapper _mapper;
        private readonly IDebtCalculator _calculator;

        public PeopleManager(IPeopleAccessor accesor, IDALMapper mapper, IDebtCalculator calculator) 
            => (_accessor, _mapper, _calculator) = (accesor, mapper, calculator);

        public async Task AddPersonAsync(Person person) 
            => await _accessor.InsertPersonAsync(_mapper.MapToDALPerson(person));

        public async Task<List<Person>> GetAllPeopleAsync() 
            => (await _accessor.GetAllPeopleAsync()).Select(p => _mapper.MapFromDALPerson(p)).ToList();

        public async Task<List<Person>> GetAllPeopleWithDebtsStatusAsync()
        {
            var people = await GetAllPeopleWithDebtsHistoryAsync();
            foreach (var person in people)
            {
                person.Debts = _calculator.CalculateDebtsStatus(person.Debts);
            }
            return people;
        }

        public async Task<List<Person>> GetAllPeopleWithDebtsHistoryAsync()
            => (await _accessor.GetAllPeopleWithDebtsAsync()).Select(p => _mapper.MapFromDALPerson(p)).ToList();

        public async Task<Person> GetPersonAsync(int id)
            => _mapper.MapFromDALPerson((await _accessor.GetPersonAsync(id)));

        public async Task<Person> GetPersonWithDebtsStatusAsync(int id)
        {
            var person = await GetPersonWithDebtsHistoryAsync(id);
            person.Debts = _calculator.CalculateDebtsStatus(person.Debts);
            return person;
        }

        public async Task<Person> GetPersonWithDebtsHistoryAsync(int id)
            => _mapper.MapFromDALPerson(await _accessor.GetPersonWithDebtsAsync(id));
        public async Task RemovePersonAsync(Person person)
            => await _accessor.DeletePersonAsync(_mapper.MapToDALPerson(person));

        public async Task UpdatePersonAsync(Person person)
            => await _accessor.UpdatePersonAsync(_mapper.MapToDALPerson(person));

        public async Task<byte[]> ExportPeopleAsync()
        {
            var people = await GetAllPeopleAsync();
            byte[] result = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(people));
            return result;
        }

        public async Task ImportPeopleAsync(MemoryStream data)
        {
            string jsonRaw = System.Text.Encoding.UTF8.GetString(data.ToArray());
            var people = JsonConvert.DeserializeObject<List<Person>>(jsonRaw);
            if (people != null && people.Count != 0)
            {
                await _accessor.InsertPeopleAsync(people.Select(p => _mapper.MapToDALPerson(p)).ToList());
            }
        }
    }
}