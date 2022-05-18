using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models.Location
{
    public class GeoLocation
    {
        public int Id { get; set; }

        [Required]
        public Point Location { get; set; }

        [Required, MaxLength(4000)]
        public string Details { get; set; }
    }
}
