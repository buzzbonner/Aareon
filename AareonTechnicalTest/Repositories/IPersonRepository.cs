using AareonTechnicalTest.Models;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> FindPersonAsync(int id);
    }
}