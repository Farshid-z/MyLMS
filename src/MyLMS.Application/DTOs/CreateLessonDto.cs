using Abp.AutoMapper;
using MyLMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLMS.DTOs
{
    [AutoMapTo(typeof(Lesson))]
    public class CreateLessonDto
    {
        public string Name { get; set; }
        public int SessionId { get; set; }
    }
}
