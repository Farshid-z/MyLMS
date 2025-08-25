using Abp.Domain.Entities;
using System.Collections.Generic;

namespace MyLMS.Entities
{   
    public class Teacher : Entity
    {
        #region Props
        public required string FullName { get; set; }
        #endregion Props
        #region Relations
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        #endregion Relations
    }
}
