using Abp.Domain.Entities;
using System.Collections.Generic;

namespace MyLMS.Entities
{
    public class Roadmap : Entity
    {
        #region Props
        public string Name { get; set; }
        #endregion Props

        #region Relations
        public ICollection<Course> Course { get; set; } = new List<Course>();
        public ICollection<StudentRoadmap> StudentRoadmaps = new List<StudentRoadmap>();
        #endregion Relations
    }
}
