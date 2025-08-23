using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
