using Abp.Application.Services;
using Microsoft.EntityFrameworkCore;
using MyLMS.DTOs;
using MyLMS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLMS.Services.Interfaces
{
    public interface IStudentService : IApplicationService
    {
        Task<int> CreateStudent(CreateStudentDTO createStudentDTO);
        Task<BasicDeleteResult> DeleteStudent(int studentId);
        Task<AsignRoadmapToStudentResult> AsignRoadmapToStudent(AsignRoadmapToStudentDto asignRoadmapToStudentDto);
        Task<StudentLessonActionsResult> StarWatchLession(LessonToStudentDto lessonToStudentDto);
        Task<StudentLessonActionsResult> FinishLesson(LessonToStudentDto lessonToStudentDto);

    }
}
