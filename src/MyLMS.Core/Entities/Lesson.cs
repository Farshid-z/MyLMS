using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLMS.Entities
{
    public class Lesson : Entity
    {
        #region Props
        public string Name { get; set; }
        public int Order { get; set; }
        public int SessionId { get; set; }
        #endregion Props

        #region Relations
        public Session Session { get; set; }
        public ICollection<StudentLesson> StudentLessons { get; set; } = new List<StudentLesson>();
        #endregion Relations
    }
}
