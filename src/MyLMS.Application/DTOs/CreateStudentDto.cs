using Abp.AutoMapper;
using MyLMS.Entities;

namespace MyLMS.DTOs
{
    [AutoMapTo(typeof(Student))]
    public class CreateStudentDTO
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
    }
}
