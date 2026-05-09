using System.ComponentModel.DataAnnotations;

namespace APBD_API.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(20)]
        public string BuildingCode { get; set; }
        public int Floor { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0.")]
        public int Capacity { get; set; }
        public bool HasProjector { get; set; }
        public bool IsActive { get; set; }
    }
}
