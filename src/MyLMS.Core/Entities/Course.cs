using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLMS.Entities
{
    public class Course : Entity
    {
        #region Props
        public string Name { get; set; }
        public int Order { get; set; }
        public int TeacherId { get; set; }
        public int RoadMapId { get; set; }
        #endregion Props

        #region Relations
        public Teacher Teacher { get; set; }
        public Roadmap Roadmap { get; set; }
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        #endregion Relations
    }
}
