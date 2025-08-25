using Abp.AutoMapper;
using MyLMS.Entities;

namespace MyLMS.DTOs
{
    [AutoMapTo(typeof(StudentLesson))]
    public class LessonToStudentDto
    {
        public int StudentId { get; set; }
        public int LessonId { get; set; }
    }
}
