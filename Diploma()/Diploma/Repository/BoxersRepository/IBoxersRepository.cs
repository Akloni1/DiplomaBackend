using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.Repository
{
    public interface IBoxersRepository
    {
        Task<Boxers> GetBoxer(int id);
        Task<ICollection<Boxers>> GetAllBoxers();
        Task<Boxers> UpdateBoxer(int id, Boxers boxerModel);
        Task<Boxers> AddBoxer(Boxers boxerModel);
        Task<Boxers> DeleteBoxer(int id);
    }
}
