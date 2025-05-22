using Events.Domain.Entities;
using Events.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Events.DataAccess.Repositories
{
    public class EventsRepository : BaseRepository<EventEntity>, IEventsRepository
    {
        public EventsRepository(EventsDbContext context)
            : base(context) { }
    }
}
