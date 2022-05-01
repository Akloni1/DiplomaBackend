using Diploma.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Coaches> AddCoach(Coaches coachModel)
        {
            if (!await LoginExists(coachModel.Login))
            {
                coachModel.Password = _pwdHash.sha256encrypt(coachModel.Password, coachModel.Login);
                var coach = _context.Add(coachModel).Entity;
                await _context.SaveChangesAsync();
                return coach;
            }
            else
            {
                return null;
            }
        }

        public async Task<Coaches> DeleteCoach(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null) return null;
            _context.Coaches.Remove(coach);
            await _context.SaveChangesAsync();

            return coach;
        }

        public async Task<ICollection<Coaches>> GetAllCoaches()
        {
            var coach = await _context.Coaches.AsNoTracking().ToListAsync();
            return coach;
        }

        public async Task<Coaches> GetCoach(int id)
        {
            var coach = await _context.Coaches.FirstOrDefaultAsync(m => m.CoachId == id);
            return coach;
        }

        public async Task<Coaches> UpdateCoach(int id, Coaches coachModel)
        {
            try
            {

                var coachNotEdit = await _context.Coaches.FirstOrDefaultAsync(m => m.CoachId == id);
                _context.Entry(coachNotEdit).State = EntityState.Detached;
                Coaches coach = coachModel;
                coach.CoachId = id;
                coach.Login = coachNotEdit.Login;
                coach.Password = coachNotEdit.Password;

                _context.Update(coach);
                await _context.SaveChangesAsync();

                return coach;
            }
            catch (DbUpdateException)
            {
                if (!await CoachExists(id))
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }
        private async Task<bool> CoachExists(int id)
        {
            return await _context.Coaches.AnyAsync(e => e.CoachId == id);
        }
        private async Task<bool> LoginExists(string login)
        {
            var boxer = await _context.Boxers.AnyAsync(e => e.Login == login);
            var coach = await _context.Coaches.AnyAsync(e => e.Login == login);
            var admin = await _context.Admins.AnyAsync(e => e.Login == login);
            var lead = await _context.Leads.AnyAsync(e => e.Login == login);
            if (boxer)
            {
                return boxer;
            }
            else if (coach)
            {
                return coach;
            }
            else if (admin)
            {
                return admin;
            }
            else if (lead)
            {
                return lead;
            }
            else
            {
                return false;
            }

        }
    }
}
