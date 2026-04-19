using EF02.models;

namespace EF02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new AppDbContext();

            var organizer = new Organizer
            {
                Name = "Ahmed",
                IsVerified = true,
                Profile = new OrganizerProfile
                {
                    Bio = "Tech Organizer"
                }
            };

            var mainEvent = new Event
            {
                Title = "Tech Conference",
                StartDate = DateTime.Now,
                MaxAttendees = 100,
                Organizer = organizer
            };

            var session = new Event
            {
                Title = "AI Workshop",
                StartDate = DateTime.Now,
                Organizer = organizer,
                ParentEvent = mainEvent
            };

            var attendee = new Attendee
            {
                FullName = "Ali",
                Email = "ali@gmail.com",
                City = "Cairo",
                Country = "Egypt"
            };

            var registration = new Registration
            {
                Attendee = attendee,
                Event = mainEvent,
                RegistrationDate = DateTime.Now
            };

            context.Add(registration);
            context.SaveChanges();
        }
    }
}
