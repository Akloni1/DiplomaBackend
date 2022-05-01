using AutoMapper;
using Diploma.Repository.BoxingClubsRepository;
using Diploma.ViewModels.BoxingClubs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.Services.BoxingClubsServices
{
    public class BoxingClubsServices : IBoxingClubsServices
    {

        private readonly IMapper _mapper;
        private readonly IBoxingClubsRepository _boxingClubsRepository;

        public BoxingClubsServices(IMapper mapper, IBoxingClubsRepository boxingClubsRepository)
        {
            _mapper = mapper;
            _boxingClubsRepository = boxingClubsRepository;
        }

        public async Task<InputBoxingClubsViewModel> AddBoxingClub(InputBoxingClubsViewModel boxingClubModel)
        {
            var boxingClub = await _boxingClubsRepository.AddBoxingClub(_mapper.Map<BoxingClubs>(boxingClubModel));
            return _mapper.Map<InputBoxingClubsViewModel>(boxingClub);
        }

        public async Task<DeleteBoxingClubsViewModel> DeleteBoxingClub(int id)
        {
            var boxingClub = await _boxingClubsRepository.DeleteBoxingClub(id);
            if (boxingClub == null) return null;
            return _mapper.Map<DeleteBoxingClubsViewModel>(boxingClub);
        }

        public async Task<IEnumerable<BoxingClubsViewModel>> GetAllBoxingClubs()
        {
            var boxingClubs = _mapper.Map<IEnumerable<BoxingClubs>, IEnumerable<BoxingClubsViewModel>>(await _boxingClubsRepository.GetAllBoxingClubs());
            return boxingClubs;
        }

        public async Task<BoxingClubsViewModel> GetBoxingClub(int id)
        {
            var boxingClub = _mapper.Map<BoxingClubsViewModel>(await _boxingClubsRepository.GetBoxingClub(id));

            return boxingClub;
        }

        public async Task<EditBoxingClubsViewModel> UpdateBoxingClub(int id, EditBoxingClubsViewModel boxingClubModel)
        {
            var boxingClub = await _boxingClubsRepository.UpdateBoxingClub(id, _mapper.Map<BoxingClubs>(boxingClubModel));
            if (boxingClub == null)
            {
                return null;
            }
            return _mapper.Map<EditBoxingClubsViewModel>(boxingClub);
        }

    }
}
