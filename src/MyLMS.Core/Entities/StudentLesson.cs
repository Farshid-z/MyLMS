using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLMS.Entities
{
    public class StudentLesson : Entity,IHasCreationTime
    {
        #region Props
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public DateTime CreationTime { get; set; }
        public LessonState State { get; set; }
        #endregion Props

        #region Relations
        public Student Student { get; set; }
        public Lesson Lesson { get; set; }
        #endregion Relations

        public enum LessonState
        {
            Taken,
            InProgres,
            Done
        }
    }
}
