using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;



namespace Diploma.Repository.BoxingClubsRepository
{
    public class BoxingClubsRepository: IBoxingClubsRepository
    {

            private readonly BoxContext _context;

            public BoxingClubsRepository(BoxContext context)
            {
                _context = context;
            }



            public BoxingClubs GetBoxingClub(int id)
            {
                var boxingClub = _context.BoxingClubs.FirstOrDefault(m => m.BoxingClubId == id);
                return boxingClub;
            }

            public IEnumerable<BoxingClubs> GetAllBoxingClubs()
            {
                var boxingClubs = _context.BoxingClubs.ToList();
                return boxingClubs;
            }



            public BoxingClubs AddBoxingClub(BoxingClubs inputModel)
            {

                var boxingClub = _context.Add(inputModel).Entity;
                _context.SaveChanges();
                return boxingClub;

            }



            public BoxingClubs UpdateBoxingClub(int id, BoxingClubs editModel)
            {
                try
                {
                    var boxingClub = editModel;
                boxingClub.BoxingClubId = id;

                    _context.Update(boxingClub);
                    _context.SaveChanges();

                    return boxingClub;
                }
                catch (DbUpdateException)
                {
                    if (!BoxingClubExists(id))
                    {
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
            }



            public BoxingClubs DeleteBoxingClub(int id)
            {
                var boxingClub = _context.BoxingClubs.Find(id);
                if (boxingClub == null) return null;
                _context.BoxingClubs.Remove(boxingClub);
                _context.SaveChanges();

                return boxingClub;
            }


            private bool BoxingClubExists(int id)
            {
                return _context.BoxingClubs.Any(e => e.BoxingClubId == id);
            }

        }
    }


