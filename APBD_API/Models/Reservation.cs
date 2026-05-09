

using System.ComponentModel.DataAnnotations;

namespace APBD_API.Models
{
    public class Reservation : IValidatableObject
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        [Required, StringLength(20)]
        public string OrganizerName { get; set; }
        [Required, StringLength(100)]
        public string Topic { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndTime <= StartTime)
            {
                yield return new ValidationResult("EndTime must be after StartTime", new[] { nameof(EndTime) });
            }
        }
    }
}
