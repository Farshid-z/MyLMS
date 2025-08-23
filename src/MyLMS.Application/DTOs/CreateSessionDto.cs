using Abp.AutoMapper;
using MyLMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLMS.DTOs
{
    [AutoMapTo(typeof(Session))]
    public class CreateSessionDto
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
    }
}
