using Abp.AutoMapper;
using MyLMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLMS.DTOs
{
    [AutoMapTo(typeof(StudentRoadmap))]
    public class AsignRoadmapToStudentDto
    {
        public int StudentId { get; set; }
        public int RoadmapId { get; set; }
    }
}
