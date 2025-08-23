using Abp.AutoMapper;
using MyLMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLMS.DTOs
{
    [AutoMapTo(typeof(Student))]
    public class CreateStudentDTO
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
    }
}
