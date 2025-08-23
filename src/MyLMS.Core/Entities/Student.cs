using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLMS.Entities
{
    public class Student : Entity
    {
        #region Props
        public string FullName { get; set; }
        public string Mobile { get; set; }
        #endregion Props

        #region Relations
        public ICollection<StudentRoadmap> StudentRoadmaps { get; set; } = new HashSet<StudentRoadmap>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<StudentSession> StudentSessions { get; set; } = new List<StudentSession>();
        public ICollection<StudentLesson> StudentLessons { get; set; } = new List<StudentLesson>();
        #endregion Relations

    }
}
