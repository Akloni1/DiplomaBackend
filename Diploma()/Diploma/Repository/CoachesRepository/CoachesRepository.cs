using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Diploma.Repository.CoachesRepository
{
    public class CoachesRepository : ICoachesRepository
    {


        private readonly BoxContext _context;

        public CoachesRepository(BoxContext context)
        {
            _context = context;
        }

        public Coaches AddCoach(Coaches coachModel)
        {
            var coach = _context.Add(coachModel).Entity;
            _context.SaveChanges();
            return coach;
        }

        public Coaches DeleteCoach(int id)
        {
            var coach = _context.Coaches.Find(id);
            if (coach == null) return null;
            _context.Coaches.Remove(coach);
            _context.SaveChanges();

            return coach;
        }

        public IEnumerable<Coaches> GetAllCoaches()
        {
            var coach = _context.Coaches.ToList();
            return coach;
        }

        public Coaches GetCoach(int id)
        {
            var coach = _context.Coaches.FirstOrDefault(m => m.CoachId == id);
            return coach;
        }

        public Coaches UpdateCoach(int id, Coaches coachModel)
        {
            try
            {
                var coach = coachModel;
                coach.CoachId = id;

                _context.Update(coach);
                _context.SaveChanges();

                return coach;
            }
            catch (DbUpdateException)
            {
                if (!CoachExists(id))
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }
        private bool CoachExists(int id)
        {
            return _context.Coaches.Any(e => e.CoachId == id);
        }
    }
}
