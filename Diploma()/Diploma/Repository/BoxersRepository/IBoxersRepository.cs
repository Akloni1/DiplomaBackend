using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.Repository
{
    public interface IBoxersRepository
    {
        Boxers GetBoxer(int id);
       Task<ICollection<Boxers>> GetAllBoxers();
        Boxers UpdateBoxer(int id, Boxers boxerModel);
        Boxers AddBoxer(Boxers boxerModel);
        Boxers DeleteBoxer(int id);
    }
}
