using Abp.Domain.Entities;
using System.Collections.Generic;

namespace MyLMS.Entities
{
    public class Session : Entity
    {
        #region Props
        public string Name { get; set; }
        public int CourseId { get; set; }
        public int Order { get; set; }

        #endregion Props

        #region Relations
        public Course Course { get; set; }
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<StudentSession> StudentSessions { get; set; } = new List<StudentSession>();

        #endregion Relations

    }
}
