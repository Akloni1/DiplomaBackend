using AutoMapper;
using Diploma.Repository.CoachesRepository;
using Diploma.ViewModels.Coaches;
using System.Collections.Generic;

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

        public InputCoachViewModel AddCoach(InputCoachViewModel coachModel)
        {
            var coach = _coachesRepository.AddCoach(_mapper.Map<Coaches>(coachModel));
            return _mapper.Map<InputCoachViewModel>(coach);
        }

        public DeleteCoachViewModel DeleteCoach(int id)
        {
            var coach = _coachesRepository.DeleteCoach(id);
            if (coach == null) return null;
            return _mapper.Map<DeleteCoachViewModel>(coach);
        }

        public IEnumerable<CoachViewModel> GetAllCoaches()
        {
            var coaches = _mapper.Map<IEnumerable<Coaches>, IEnumerable<CoachViewModel>>(_coachesRepository.GetAllCoaches());
            return coaches;
        }

        public CoachViewModel GetCoach(int id)
        {
            var coach = _mapper.Map<CoachViewModel>(_coachesRepository.GetCoach(id));

            return coach;
        }

        public EditCoachViewModel UpdateCoaches(int id, EditCoachViewModel coachModel)
        {
            var coach = _coachesRepository.UpdateCoach(id, _mapper.Map<Coaches>(coachModel));
            if (coach == null)
            {
                return null;
            }
            return _mapper.Map<EditCoachViewModel>(coach);
        }
    }
}
