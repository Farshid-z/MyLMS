using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace MyLMS.Entities
{
    public class StudentCourse : Entity,IHasCreationTime
    {
        #region Props
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public CourseState State { get; set; }
        public DateTime CreationTime { get; set; }
        #endregion Props

        #region Relations
        public Student Student { get; set; }
        public Course Course { get; set; }
        #endregion Relations

        public enum CourseState 
        {
            Taken,
            InProgres,
            Done
        }
    }
}
