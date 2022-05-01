using AutoMapper;
using Diploma.Repository;
using Diploma.ViewModels.Boxers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma.Services
{
    public class BoxersServices : IBoxersServices
    {

        private readonly IMapper _mapper;
        private readonly IBoxersRepository _boxersRepository;

        public BoxersServices(IMapper mapper, IBoxersRepository boxersRepository)
        {

            _mapper = mapper;
            _boxersRepository = boxersRepository;
        }
        public BoxerViewModel GetBoxer(int id)
        {
            var boxer = _mapper.Map<BoxerViewModel>(_boxersRepository.GetBoxer(id));

            return boxer;
        }


        public async Task<ICollection<BoxerViewModel>> GetAllBoxers()
        {
            var boxers = _mapper.Map<ICollection<Boxers>, ICollection<BoxerViewModel>>(await _boxersRepository.GetAllBoxers());
            return boxers;
        }



        public BoxerViewModel AddBoxer(InputBoxerViewModel inputModel)
        {
            var boxer = _boxersRepository.AddBoxer(_mapper.Map<Boxers>(inputModel));
            return _mapper.Map<BoxerViewModel>(boxer);

        }



        public EditBoxerViewModel UpdateBoxer(int id, EditBoxerViewModel editModel)
        {

            var boxer = _boxersRepository.UpdateBoxer(id, _mapper.Map<Boxers>(editModel));
            if (boxer == null)
            {
                return null;
            }
            return _mapper.Map<EditBoxerViewModel>(boxer);
        }




        public DeleteBoxerViewModel DeleteBoxer(int id)
        {
            var boxer = _boxersRepository.DeleteBoxer(id);
            if (boxer == null) return null;
            return _mapper.Map<DeleteBoxerViewModel>(boxer);
        }



    }
}
