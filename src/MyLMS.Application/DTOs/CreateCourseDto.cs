using Abp.AutoMapper;
using MyLMS.Entities;

namespace MyLMS.DTOs
{
    [AutoMapTo(typeof(Course))]
    public class CreateCourseDto
    {
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public int RoadMapId { get; set; }
    }
}
