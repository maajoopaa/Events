using AutoMapper;
using Events.Domain.Entities;
using Events.Domain.Models;


namespace Events.Application
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EventEntity, Event>()
                .ConstructUsing((src, context) => new Event(
                    src.Id,
                    src.Title,
                    src.Description,
                    src.HoldedAt,
                    src.Venue,
                    src.Category,
                    src.MaxCountOfParticipant,
                    context.Mapper.Map<List<Participant>>(src.Participants),
                    src.Image
                    ));

            CreateMap<ParticipantEntity, Participant>();

        }
    }
}
