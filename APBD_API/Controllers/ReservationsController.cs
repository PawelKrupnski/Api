using APBD_API.Data;
using Microsoft.AspNetCore.Mvc;

namespace APBD_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetReservations([FromQuery] DateOnly? date, [FromQuery] string? status, [FromQuery] int? roomId)
        {
            var reservations = MockData.Reservations;

            if (date.HasValue)
            {
                reservations = reservations.Where(r => r.Date == date.Value).ToList();
            }

            if (!string.IsNullOrEmpty(status))
            {
                reservations = reservations.Where(r => r.Status == status).ToList();
            }

            if (roomId.HasValue)
            {
                reservations = reservations.Where(r => r.RoomId == roomId.Value).ToList();
            }

            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservationById(int id)
        {
            var reservation = MockData.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost]
        public IActionResult CreateReservation([FromBody] Models.Reservation reservation)
        {

            var room = MockData.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
            if (room == null) { return NotFound("Room not found."); }
            if (!room.IsActive) { return BadRequest("Room is not active."); }

            bool collision = MockData.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            r.StartTime < reservation.EndTime &&
            r.EndTime > reservation.StartTime);

            if (collision) return Conflict("Reservation overlaps with an existing one.");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            reservation.Id = MockData.Reservations.Max(r => r.Id) + 1;
            MockData.Reservations.Add(reservation);
            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, reservation);


        }

        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] Models.Reservation reservation)
        {

            bool collision = MockData.Reservations.Any(r =>
            r.Id != id &&
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            r.StartTime < reservation.EndTime &&
            r.EndTime > reservation.StartTime);

            if (collision) return Conflict("Reservation overlaps with an existing one.");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existing = MockData.Reservations.FirstOrDefault(r => r.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            existing.RoomId = reservation.RoomId;
            existing.OrganizerName = reservation.OrganizerName;
            existing.Topic = reservation.Topic;
            existing.Date = reservation.Date;
            existing.StartTime = reservation.StartTime;
            existing.EndTime = reservation.EndTime;
            existing.Status = reservation.Status;
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            var reservation = MockData.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            MockData.Reservations.Remove(reservation);
            return NoContent();
        }
    }
}
