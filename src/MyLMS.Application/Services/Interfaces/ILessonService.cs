using Abp.Application.Services;
using MyLMS.DTOs;
using MyLMS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLMS.Services.Interfaces
{
    public interface ILessonService : IApplicationService
    {
        Task<int> CreateLesson(CreateLessonDto createLessonDto);
        Task<BasicDeleteResult> DeleteLesson(int lessonId);
        //Task<bool> AsignLessonToStudent(LessonToStudentDto asignLessonToStudent);

    }
}
