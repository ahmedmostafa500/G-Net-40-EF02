using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF02.models
{
    internal class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MaxAttendees { get; set; }

        // Organizer
        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; }

        // Self Relation
        public int? ParentEventId { get; set; }
        public Event ParentEvent { get; set; }
        public ICollection<Event> Sessions { get; set; }

        // Many-to-Many
        public ICollection<Registration> Registrations { get; set; } = new HashSet<Registration>();
    }
}
