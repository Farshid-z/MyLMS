using Abp.AutoMapper;
using MyLMS.Entities;

namespace MyLMS.DTOs
{
    [AutoMapTo(typeof(Teacher))]
    public class CreateTeacherDto
    {
        public string FullName { get; set; }
    }
}
