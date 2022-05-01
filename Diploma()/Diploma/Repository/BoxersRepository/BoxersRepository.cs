using Diploma.Cryptography;
using Diploma.ViewModels.Boxers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma.Repository
{
    public class BoxersRepository: IBoxersRepository
    {

        private readonly PwdHash _pwdHash;
        private readonly BoxContext _context;
       
        public BoxersRepository(BoxContext context)
        {
            _pwdHash = new PwdHash();
            _context = context;
        }
        public async Task<Boxers> GetBoxer(int id)
        {
            var boxer = await _context.Boxers.FirstOrDefaultAsync(m => m.BoxerId == id);
            return boxer;
        }

        public async Task<ICollection<Boxers>> GetAllBoxers()
        {
            var boxers = await _context.Boxers.ToListAsync();
            return boxers;
        }



        public async Task<Boxers> AddBoxer(Boxers inputModel)
        {
            if (!LoginExists(inputModel.Login)) {
                inputModel.Password = _pwdHash.sha256encrypt(inputModel.Password, inputModel.Login);
                var boxer = _context.Add(inputModel).Entity;
                await _context.SaveChangesAsync();
                return boxer;
            }
            else
            {
                return null;
            }

        }



        public async Task<Boxers> UpdateBoxer(int id, Boxers editModel)
        {
            try
            {
               
                var boxerNotEdit = await _context.Boxers.FirstOrDefaultAsync(m => m.BoxerId == id);
                _context.Entry(boxerNotEdit).State = EntityState.Detached;
                Boxers boxer = editModel;
                boxer.BoxerId = id;
                boxer.Login = boxerNotEdit.Login;
                boxer.Password = boxerNotEdit.Password;



                _context.Update(boxer);
                await _context.SaveChangesAsync();

                return boxer;
            }
            catch (DbUpdateException)
            {
                if (!await BoxerExists(id))
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }



        public async Task<Boxers> DeleteBoxer(int id)
        {
            var boxer = await _context.Boxers.FindAsync(id);
            if (boxer == null) return null;
            _context.Boxers.Remove(boxer);
            await _context.SaveChangesAsync();

            return boxer;
        }


        private async Task<bool> BoxerExists(int id)
        {
            return await _context.Boxers.AnyAsync(e => e.BoxerId == id);
        }

        private bool LoginExists(string login)
        {
            var boxer = _context.Boxers.Any(e => e.Login == login);
            var coach = _context.Coaches.Any(e => e.Login == login);
            var admin = _context.Admins.Any(e => e.Login == login);
            var lead = _context.Leads.Any(e => e.Login == login);
            if (boxer)
            {
                return boxer;
            }
            else if (coach )
            {
                return coach;
            }
            else if (admin )
            {
                return admin;
            }
            else if (lead )
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
