using Abp.AutoMapper;
using MyLMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLMS.DTOs
{
    [AutoMapTo(typeof(Teacher))]
    public class CreateTeacherDto
    {
        public string FullName { get; set; }
    }
}
