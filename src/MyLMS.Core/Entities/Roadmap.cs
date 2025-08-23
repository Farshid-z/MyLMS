using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
