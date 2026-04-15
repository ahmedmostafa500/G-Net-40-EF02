using EF02.models;

namespace EF02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new AppDbContext();

            context.Database.EnsureCreated();

            // Organizer + Profile
            var organizer = new Organizer
            {
                Name = "Ahmed",
                CompanyName = "Tech Corp",
                IsVerified = true,
                Profile = new OrganizerProfile
                {
                    Bio = "Event Organizer",
                    Website = "www.tech.com",
                    LogoUrl = "logo.png"
                }
            };

            // Attendee + Badge
            var attendee = new Attendee
            {
                FullName = "Ahmed mostafa",
                Email = "ahmed@gmail.com",
                Badge = new Badge
                {
                    BadgeNumber = "B123",
                    IssuedDate = DateTime.Now,
                    Tier = "VIP"
                }
            };

            context.Add(organizer);
            context.Add(attendee);

            context.SaveChanges();

            Console.WriteLine("Data Inserted Successfully ");
        }
    }
}
