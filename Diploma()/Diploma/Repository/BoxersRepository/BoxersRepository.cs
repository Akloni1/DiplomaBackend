using Diploma.Cryptography;
using Diploma.ViewModels.Boxers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
        public Boxers GetBoxer(int id)
        {
            var boxer = _context.Boxers.FirstOrDefault(m => m.BoxerId == id);
            return boxer;
        }

        public IEnumerable<Boxers> GetAllBoxers()
        {
            var boxers = _context.Boxers.ToList();
            return boxers;
        }



        public Boxers AddBoxer(Boxers inputModel)
        {
            if (!BoxerExists(inputModel.Login)) {
                inputModel.Password = _pwdHash.sha256encrypt(inputModel.Password, inputModel.Login);
                var boxer = _context.Add(inputModel).Entity;
                _context.SaveChanges();
                return boxer;
            }
            else
            {
                return null;
            }

        }



        public Boxers UpdateBoxer(int id, Boxers editModel)
        {
            try
            {
               
                var boxerNotEdit = _context.Boxers.FirstOrDefault(m => m.BoxerId == id);
                _context.Entry(boxerNotEdit).State = EntityState.Detached;
                Boxers boxer = editModel;
                boxer.BoxerId = id;
                boxer.Login = boxerNotEdit.Login;
                boxer.Password = boxerNotEdit.Password;
               


                _context.Update(boxer);
                _context.SaveChanges();

                return boxer;
            }
            catch (DbUpdateException)
            {
                if (!BoxerExists(id))
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }



        public Boxers DeleteBoxer(int id)
        {
            var boxer = _context.Boxers.Find(id);
            if (boxer == null) return null;
            _context.Boxers.Remove(boxer);
            _context.SaveChanges();

            return boxer;
        }


        private bool BoxerExists(int id)
        {
            return _context.Boxers.Any(e => e.BoxerId == id);
        }

        private bool BoxerExists(string login)
        {
            return _context.Boxers.Any(e => e.Login == login);
        }

    }
}
