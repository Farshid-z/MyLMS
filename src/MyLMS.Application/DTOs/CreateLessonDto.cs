using Abp.AutoMapper;
using MyLMS.Entities;

namespace MyLMS.DTOs
{
    [AutoMapTo(typeof(Lesson))]
    public class CreateLessonDto
    {
        public string Name { get; set; }
        public int SessionId { get; set; }
    }
}
