using AutoMapper;
using Diploma.Repository.CoachesRepository;
using Diploma.ViewModels.Coaches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diploma.Services.CoachesServices
{
    public class CoachesServices : ICoachesServices
    {

        private readonly IMapper _mapper;
        private readonly ICoachesRepository _coachesRepository;

        public CoachesServices(IMapper mapper, ICoachesRepository coachesRepository)
        {
            _mapper = mapper;
            _coachesRepository = coachesRepository;
        }

        public async Task<CoachViewModel> AddCoach(InputCoachViewModel coachModel)
        {
            var coach = await _coachesRepository.AddCoach(_mapper.Map<Coaches>(coachModel));
            return _mapper.Map<CoachViewModel>(coach);
        }

        public async Task<DeleteCoachViewModel> DeleteCoach(int id)
        {
            var coach = await _coachesRepository.DeleteCoach(id);
            if (coach == null) return null;
            return _mapper.Map<DeleteCoachViewModel>(coach);
        }

        public async Task<ICollection<CoachViewModel>> GetAllCoaches()
        {
            var coaches = _mapper.Map<ICollection<Coaches>, ICollection<CoachViewModel>>(await _coachesRepository.GetAllCoaches());
            return coaches;
        }

        public async Task<CoachViewModel> GetCoach(int id)
        {
            var coach = _mapper.Map<CoachViewModel>(await _coachesRepository.GetCoach(id));

            return coach;
        }

        public async Task<EditCoachViewModel> UpdateCoaches(int id, EditCoachViewModel coachModel)
        {
            var coach = await _coachesRepository.UpdateCoach(id, _mapper.Map<Coaches>(coachModel));
            if (coach == null)
            {
                return null;
            }
            return _mapper.Map<EditCoachViewModel>(coach);
        }
    }
}
