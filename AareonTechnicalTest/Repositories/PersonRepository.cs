using AareonTechnicalTest.Models;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationContext _context;

        public PersonRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Person> FindPersonAsync(int id)
        {
            return await _context.Persons.FindAsync(id);
        }
    }
}
