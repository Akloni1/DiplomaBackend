using System.Collections.Generic;

namespace Diploma.Repository
{
    public interface IBoxersRepository
    {
        Boxers GetBoxer(int id);
        IEnumerable<Boxers> GetAllBoxers();
        Boxers UpdateBoxer(int id, Boxers boxerModel);
        Boxers AddBoxer(Boxers boxerModel);
        Boxers DeleteBoxer(int id);
    }
}
