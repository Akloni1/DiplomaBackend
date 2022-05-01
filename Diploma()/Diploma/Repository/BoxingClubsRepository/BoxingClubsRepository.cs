using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma.Repository.BoxingClubsRepository
{
    public class BoxingClubsRepository : IBoxingClubsRepository
    {

        private readonly BoxContext _context;

        public BoxingClubsRepository(BoxContext context)
        {
            _context = context;
        }



        public async Task<BoxingClubs> GetBoxingClub(int id)
        {
            var boxingClub = await _context.BoxingClubs.FirstOrDefaultAsync(m => m.BoxingClubId == id);
            return boxingClub;
        }

        public async Task<IEnumerable<BoxingClubs>> GetAllBoxingClubs()
        {
            var boxingClubs = await _context.BoxingClubs.ToListAsync();
            return boxingClubs;
        }



        public async Task<BoxingClubs> AddBoxingClub(BoxingClubs inputModel)
        {

            var boxingClub = _context.Add(inputModel).Entity;
            await _context.SaveChangesAsync();
            return boxingClub;

        }



        public async Task<BoxingClubs> UpdateBoxingClub(int id, BoxingClubs editModel)
        {
            try
            {
                var boxingClub = editModel;
                boxingClub.BoxingClubId = id;

                _context.Update(boxingClub);
                await _context.SaveChangesAsync();

                return boxingClub;
            }
            catch (DbUpdateException)
            {
                if (!await BoxingClubExists(id))
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }



        public async Task<BoxingClubs> DeleteBoxingClub(int id)
        {
            var boxingClub = await _context.BoxingClubs.FindAsync(id);
            if (boxingClub == null) return null;
            _context.BoxingClubs.Remove(boxingClub);
            await _context.SaveChangesAsync();

            return boxingClub;
        }


        private async Task<bool> BoxingClubExists(int id)
        {
            return await _context.BoxingClubs.AnyAsync(e => e.BoxingClubId == id);
        }

    }
}


