using Abp.AutoMapper;
using MyLMS.Entities;

namespace MyLMS.DTOs
{
    [AutoMapTo(typeof(Session))]
    public class CreateSessionDto
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
    }
}
