using Diploma.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Diploma.Repository.CoachesRepository
{
    public class CoachesRepository : ICoachesRepository
    {


        private readonly BoxContext _context;
        private readonly PwdHash _pwdHash;

        public CoachesRepository(BoxContext context)
        {
            _pwdHash = new PwdHash();
            _context = context;
        }

        public Coaches AddCoach(Coaches coachModel)
        {
            coachModel.Password = _pwdHash.sha256encrypt(coachModel.Password, coachModel.Login);
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

                var coachNotEdit = _context.Coaches.FirstOrDefault(m => m.CoachId == id);
                _context.Entry(coachNotEdit).State = EntityState.Detached;
                Coaches coach = coachModel;
                coach.CoachId = id;
                coach.Login = coachNotEdit.Login;
                coach.Password = coachNotEdit.Password;

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
