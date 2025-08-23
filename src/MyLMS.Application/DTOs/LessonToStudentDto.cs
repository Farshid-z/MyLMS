using Abp.AutoMapper;
using MyLMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLMS.DTOs
{
    [AutoMapTo(typeof(StudentLesson))]
    public class LessonToStudentDto
    {
        public int StudentId { get; set; }
        public int LessonId { get; set; }
    }
}
