using AutoMapper;
using Diploma.Repository.BoxingClubsRepository;
using Diploma.ViewModels.BoxingClubs;
using System.Collections.Generic;

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

        public InputBoxingClubsViewModel AddBoxingClub(InputBoxingClubsViewModel boxingClubModel)
        {
            var boxingClub = _boxingClubsRepository.AddBoxingClub(_mapper.Map<BoxingClubs>(boxingClubModel));
            return _mapper.Map<InputBoxingClubsViewModel>(boxingClub);
        }

        public DeleteBoxingClubsViewModel DeleteBoxingClub(int id)
        {
            var boxingClub = _boxingClubsRepository.DeleteBoxingClub(id);
            if (boxingClub == null) return null;
            return _mapper.Map<DeleteBoxingClubsViewModel>(boxingClub);
        }

        public IEnumerable<BoxingClubsViewModel> GetAllBoxingClubs()
        {
            var boxingClubs = _mapper.Map<IEnumerable<BoxingClubs>, IEnumerable<BoxingClubsViewModel>>(_boxingClubsRepository.GetAllBoxingClubs());
            return boxingClubs;
        }

        public BoxingClubsViewModel GetBoxingClub(int id)
        {
            var boxingClub = _mapper.Map<BoxingClubsViewModel>(_boxingClubsRepository.GetBoxingClub(id));

            return boxingClub;
        }

        public EditBoxingClubsViewModel UpdateBoxingClub(int id, EditBoxingClubsViewModel boxingClubModel)
        {
            var boxingClub = _boxingClubsRepository.UpdateBoxingClub(id, _mapper.Map<BoxingClubs>(boxingClubModel));
            if (boxingClub == null)
            {
                return null;
            }
            return _mapper.Map<EditBoxingClubsViewModel>(boxingClub);
        }

    }
}
