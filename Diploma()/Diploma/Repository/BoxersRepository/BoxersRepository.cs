﻿using Diploma.ViewModels.Boxers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Diploma.Repository
{
    public class BoxersRepository: IBoxersRepository
    {

        private readonly BoxContext _context;
       
        public BoxersRepository(BoxContext context)
        {
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

            var boxer = _context.Add(inputModel).Entity;
            _context.SaveChanges();
            return boxer;

        }



        public Boxers UpdateBoxer(int id, Boxers editModel)
        {
            try
            {
                var boxer = editModel;
                boxer.BoxerId = id;

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

    }
}