using APBD_API.Data;
using Microsoft.AspNetCore.Mvc;

namespace APBD_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRooms([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? activeOnly)
        {
            var rooms = MockData.Rooms;

            if(minCapacity.HasValue)
            {
                rooms = rooms.Where(r => r.Capacity >= minCapacity.Value).ToList();
            }

            if(hasProjector.HasValue)
            {
                rooms = rooms.Where(r => r.HasProjector == hasProjector.Value).ToList();
            }

            if(activeOnly.HasValue && activeOnly.Value)
            {
                rooms = rooms.Where(r => r.IsActive).ToList();
            }

            return Ok(rooms);

            
        }

        [HttpGet("{id}")]
        public IActionResult GetRoomById(int id)
        {
            var room = MockData.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);

        }

        [HttpGet("building/{buildingCode}")]
        public IActionResult GetRoomsByBuilding(string buildingCode)
        {
            var rooms = MockData.Rooms.Where(r => r.BuildingCode == buildingCode).ToList();
            
            return Ok(rooms);
        }

      

        [HttpPost]
        public IActionResult CreateRoom([FromBody] Models.Room newRoom)
        {
            if (newRoom == null )
            {
                return BadRequest("Invalid room data.");
            }
            newRoom.Id = MockData.Rooms.Max(r => r.Id) + 1;
            MockData.Rooms.Add(newRoom);
            return CreatedAtAction(nameof(GetRoomById), new { id = newRoom.Id }, newRoom);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] Models.Room updatedRoom)
        {
            var room = MockData.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            if (updatedRoom == null)
            {
                return BadRequest("Invalid room data.");
            }
            room.Name = updatedRoom.Name;
            room.BuildingCode = updatedRoom.BuildingCode;
            room.Capacity = updatedRoom.Capacity;
            room.HasProjector = updatedRoom.HasProjector;
            room.Floor = updatedRoom.Floor;
            room.IsActive = updatedRoom.IsActive;
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = MockData.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            var reservationCollisions = MockData.Reservations.Any(res => res.RoomId == id);
            if (reservationCollisions)
            {
                return Conflict("Cannot delete room with existing reservations.");
            }

            MockData.Rooms.Remove(room);
            return NoContent(); 
        }
}
}