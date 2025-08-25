using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace MyLMS.Entities
{
    public class StudentRoadmap : Entity,IHasCreationTime
    {
        #region Props
        public int StudentId { get; set; }
        public int RoadmapId { get; set; }
        public DateTime CreationTime { get; set; }
        public RoadmapState State { get; set; }

        #endregion Props
        #region Relations
        public Roadmap Roadmap { get; set; }
        public Student Student { get; set; }
        #endregion Relations

        public enum RoadmapState
        {
            Taken,
            InProgres,
            Done
        }
    }
}
