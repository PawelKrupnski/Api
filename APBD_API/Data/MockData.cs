namespace APBD_API.Data
{
    public class MockData
    {

        public static List<Models.Room> Rooms = new List<Models.Room>
        {
            new Models.Room { Id = 1, Name = "Room A", BuildingCode = "B1", Floor = 1, Capacity = 10, HasProjector = true, IsActive = true },
            new Models.Room { Id = 2, Name = "Room B", BuildingCode = "B1", Floor = 2, Capacity = 20, HasProjector = false, IsActive = true },
            new Models.Room { Id = 3, Name = "Room C", BuildingCode = "B2", Floor = 1, Capacity = 15, HasProjector = true, IsActive = false },
            new Models.Room { Id = 4, Name = "Room D", BuildingCode = "B2", Floor = 2, Capacity = 25, HasProjector = false, IsActive = true },
            new Models.Room { Id = 5, Name = "Room E", BuildingCode = "B3", Floor = 1, Capacity = 30, HasProjector = true, IsActive = true }
        };

        public static List<Models.Reservation> Reservations = new List<Models.Reservation>
        {
            new Models.Reservation { Id = 1, RoomId = 1, OrganizerName = "Alice", Topic = "Project Meeting", Date = new DateOnly(2024, 7, 1), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(10, 0), Status = "Confirmed" },
            new Models.Reservation { Id = 2, RoomId = 2, OrganizerName = "Bob", Topic = "Team Sync", Date = new DateOnly(2024, 7, 1), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(11, 0), Status = "Confirmed" },
            new Models.Reservation { Id = 3, RoomId = 3, OrganizerName = "Charlie", Topic = "Client Meeting", Date = new DateOnly(2024, 7, 2), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(15, 0), Status = "Cancelled" },
            new Models.Reservation { Id = 4, RoomId = 4, OrganizerName = "David", Topic = "Workshop", Date = new DateOnly(2024, 7, 3), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(12, 0), Status = "Confirmed" },
            new Models.Reservation { Id = 5, RoomId = 5, OrganizerName = "Eve", Topic = "Training Session", Date = new DateOnly(2024, 7, 4), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(16, 0), Status = "Confirmed" }
        };



    }
}
