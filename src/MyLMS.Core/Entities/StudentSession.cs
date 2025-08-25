using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace MyLMS.Entities
{
    public class StudentSession : Entity,IHasCreationTime
    {
        #region Props
        public int StudentId { get; set; }
        public int SessionId { get; set; }
        public DateTime CreationTime { get; set; }
        public SessionState State { get; set; }

        #endregion Props
        #region Relations
        public Student Student { get; set; }
        public Session Session { get; set; }
        #endregion Relations

        public enum SessionState
        {
            Taken,
            InProgres,
            Done
        }
    }
}
