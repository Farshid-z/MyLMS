using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using MyLMS.DTOs;
using MyLMS.Entities;
using MyLMS.Enums;
using MyLMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLMS.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _StudnetRepository;
        private readonly IRepository<Roadmap> _RoadmapRepository;
        private readonly IRepository<Course> _CourseRepository;
        private readonly IRepository<Session> _SessionRepository;
        private readonly IRepository<Lesson> _LessionRepository;
        private readonly IRepository<StudentRoadmap> _StudentRoadmapRepository;
        private readonly IRepository<StudentCourse> _StudentCourseRepository;
        private readonly IRepository<StudentSession> _StudentSessionRepository;
        private readonly IRepository<StudentLesson> _StudentLessonRepository;
        private readonly IObjectMapper _ObjectMapper;
        public StudentService
        (
            IRepository<Student> studentRepository,
            IRepository<Roadmap> roadmapRepository,
            IRepository<Course> courseRepository,
            IRepository<Session> sessionRepository,
            IRepository<Lesson> lessionRepository,
            IRepository<StudentRoadmap> studentRoadmapRepository,
            IRepository<StudentCourse> studentCourseRepository,
            IRepository<StudentSession> studentSessionRepository,
            IRepository<StudentLesson> studentLessonRepository,
            IObjectMapper objectMapper
        )
        {
            this._StudnetRepository = studentRepository;
            this._RoadmapRepository = roadmapRepository;
            this._CourseRepository = courseRepository;
            this._SessionRepository = sessionRepository;
            this._LessionRepository = lessionRepository;
            this._StudentRoadmapRepository = studentRoadmapRepository;
            this._StudentCourseRepository = studentCourseRepository;
            this._StudentSessionRepository = studentSessionRepository;
            this._StudentLessonRepository = studentLessonRepository;
            this._ObjectMapper = objectMapper;
        }
        public async Task<int> CreateStudent(CreateStudentDTO createStudentDTO)
        {
            var student = _ObjectMapper.Map<Student>(createStudentDTO);
            int id = await _StudnetRepository.InsertAndGetIdAsync(student);
            return id;
        }

        public async Task<BasicDeleteResult> DeleteStudent(int studentId)
        {
            var existsStudent = _StudnetRepository.Load(studentId);
            if (existsStudent == null)
                return BasicDeleteResult.RecordNotFound;

            #region DeleteStudentLessons
            var allStudentLessons = _StudentLessonRepository.GetAll().Where(x => x.StudentId == studentId);
            foreach(var item in allStudentLessons)
            {
                await _StudentLessonRepository.DeleteAsync(item);
            }
            #endregion DeleteStudentLessons

            #region DeleteStudentSessions
            var allStudentSessions = _StudentSessionRepository.GetAll().Where(x => x.StudentId == studentId);
            foreach (var item in allStudentSessions)
            {
                await _StudentSessionRepository.DeleteAsync(item);
            }
            #endregion DeleteStudentSessions

            #region DeleteStudentCourses
            var allStudentCourses = _StudentCourseRepository.GetAll().Where(x => x.StudentId == studentId);
            foreach(var item in allStudentCourses)
            {
                await _StudentCourseRepository.DeleteAsync(item);
            }
            #endregion DeleteStudentCourses

            #region DeleteStudentRoadmaps
            var allStudentRoadMaps = _StudentRoadmapRepository.GetAll().Where(x => x.StudentId == studentId);
            foreach (var item in allStudentRoadMaps)
            {
                await _StudentRoadmapRepository.DeleteAsync(item);
            }
            #endregion DeleteStudentRoadmaps

            _StudnetRepository.Delete(existsStudent);
            return BasicDeleteResult.Success;
        }
        [UnitOfWork]
        public async Task<AsignRoadmapToStudentResult> AsignRoadmapToStudent(AsignRoadmapToStudentDto dto)
        {
            if (!await _StudnetRepository.GetAll().AnyAsync(x => x.Id == dto.StudentId))
                return AsignRoadmapToStudentResult.StudentNotFound;
            if (!await _RoadmapRepository.GetAll().AnyAsync(x => x.Id == dto.RoadmapId))
                return AsignRoadmapToStudentResult.RoadmapNotFound;
            if (await _StudentRoadmapRepository.GetAll().AnyAsync(x => x.StudentId == dto.StudentId &&
                x.RoadmapId == dto.RoadmapId))
                return AsignRoadmapToStudentResult.RoadmapAlreadyAsigned;

            var studentRoadmap = _ObjectMapper.Map<StudentRoadmap>(dto);
            if (await _StudentRoadmapRepository.InsertAndGetIdAsync(studentRoadmap) < 1)
                return AsignRoadmapToStudentResult.FailedToAsigneRoadmap;
             
            var firstCourseOfRoadmap = await _CourseRepository
                .GetAll()
                .OrderBy(x=>x.Order)
                .FirstOrDefaultAsync(x => x.RoadMapId == dto.RoadmapId);

            if (firstCourseOfRoadmap == null)
                return AsignRoadmapToStudentResult.FirstCourseNotFound;

            var studentCourse = new StudentCourse() 
            {
                StudentId = dto.StudentId,
                CourseId = firstCourseOfRoadmap.Id,
                State = StudentCourse.CourseState.Taken
            };
            
            if (await _StudentCourseRepository.InsertAndGetIdAsync(studentCourse) < 1)
                return AsignRoadmapToStudentResult.FailedToAsignCourse;

            var firstSessionOfCourse = await _SessionRepository
                .GetAll()
                .OrderBy(x=>x.Order)
                .FirstOrDefaultAsync(x => x.CourseId == firstCourseOfRoadmap.Id);

            if (firstSessionOfCourse == null)
                return AsignRoadmapToStudentResult.FirstSessionNotFound;

            var studentSession = new StudentSession()
            {
                StudentId = dto.StudentId,
                SessionId = firstSessionOfCourse.Id,
                State = StudentSession.SessionState.Taken
            };

            if (await _StudentSessionRepository.InsertAndGetIdAsync(studentSession) < 1)
                return AsignRoadmapToStudentResult.FailedToAsignSession;

            var firstLessionOfSession = await _LessionRepository
                .GetAll()
                .OrderBy(x=>x.Order)
                .FirstOrDefaultAsync(x => x.SessionId == firstSessionOfCourse.Id);

            if (firstLessionOfSession == null)
                return AsignRoadmapToStudentResult.FirstSessionNotFound;

            var studentLesson = new StudentLesson()
            {
                StudentId = dto.StudentId,
                LessonId = firstSessionOfCourse.Id,
                State = StudentLesson.LessonState.Taken
            };

            if (await _StudentLessonRepository.InsertAndGetIdAsync(studentLesson) < 1)
                return AsignRoadmapToStudentResult.FailedToAsignLesson;

            return AsignRoadmapToStudentResult.Success;
        }

        [UnitOfWork]
        public async Task<StudentLessonActionsResult> StarWatchLession(LessonToStudentDto dto)
        {
            var existLesson = await _LessionRepository.FirstOrDefaultAsync(x => x.Id == dto.LessonId);
            if (existLesson == null)
                return StudentLessonActionsResult.LessonNotFound;

            if (!await _StudnetRepository.GetAll().AnyAsync(x => x.Id == dto.StudentId))
                return StudentLessonActionsResult.LessonNotFound;

            var studentLession =await _StudentLessonRepository.FirstOrDefaultAsync(x => x.StudentId == dto.StudentId &&
                x.LessonId == dto.LessonId);
            if (studentLession == null)
                return StudentLessonActionsResult.NotStudentLesson;


            studentLession.State = StudentLesson.LessonState.InProgres;
            await _StudentLessonRepository.UpdateAsync(studentLession);
            
            if(!await _LessionRepository.GetAll().AnyAsync(x => x.SessionId == existLesson.Id && x.Order < existLesson.Order))
            {
                //existLesson is the first lesson in the Session
                var studentSession = await _StudentSessionRepository.GetAllIncluding(x=>x.Session).FirstOrDefaultAsync(x => x.StudentId == dto.StudentId &&
                    x.SessionId == existLesson.SessionId);
                if (studentSession == null)
                    return StudentLessonActionsResult.StudentSessionNotFound;
                

                studentSession.State = StudentSession.SessionState.InProgres;
                await _StudentSessionRepository.UpdateAsync(studentSession);

                var studentCourse = await _StudentCourseRepository.GetAllIncluding(x=>x.Course).FirstOrDefaultAsync(x => x.StudentId == dto.StudentId &&
                    x.CourseId == studentSession.Session.CourseId);

                if (studentCourse == null)
                    return StudentLessonActionsResult.StudentCourseNotFound;

                studentCourse.State = StudentCourse.CourseState.InProgres;
                await _StudentCourseRepository.UpdateAsync(studentCourse); 
                
                var studentRoadmap = await _StudentRoadmapRepository.FirstOrDefaultAsync(x=>x.StudentId == dto.StudentId &&
                    x.RoadmapId == studentCourse.Course.RoadMapId);
                if (studentRoadmap == null)
                    return StudentLessonActionsResult.StudentRoadmapNotFound;

                studentRoadmap.State = StudentRoadmap.RoadmapState.InProgres;
                await _StudentRoadmapRepository.UpdateAsync(studentRoadmap);

            }

            return StudentLessonActionsResult.Success;
        }
        [UnitOfWork]
        public async Task<StudentLessonActionsResult> FinishLesson(LessonToStudentDto dto)
        {
            if (!await _LessionRepository.GetAll().AnyAsync(x => x.Id == dto.LessonId))
                return StudentLessonActionsResult.LessonNotFound;
            if (!await _StudnetRepository.GetAll().AnyAsync(x => x.Id == dto.StudentId))
                return StudentLessonActionsResult.StudentNotFound;
            var studentLession = await _StudentLessonRepository.GetAllIncluding(x=>x.Lesson).FirstOrDefaultAsync(x => x.StudentId == dto.StudentId &&
                x.LessonId == dto.LessonId);
            if (studentLession == null)
                return StudentLessonActionsResult.NotStudentLesson;

            if (await _StudentLessonRepository.GetAll().AnyAsync(x => x.StudentId == dto.StudentId &&
                x.LessonId == dto.LessonId && x.State != StudentLesson.LessonState.InProgres))
                return StudentLessonActionsResult.LessonAlreadyFinished;

            studentLession.State = StudentLesson.LessonState.Done;
            await _StudentLessonRepository.UpdateAsync(studentLession);

            // next Lesson in Session
            var nextLesson = await _LessionRepository.FirstOrDefaultAsync(x => x.SessionId == studentLession.Lesson.SessionId &&
                x.Order > studentLession.Lesson.Order);
            if(nextLesson != null)
            {
                await CreateStudentLessonAsync(dto.StudentId, nextLesson.Id);
                return StudentLessonActionsResult.Success;
            }
            
            // no more Lesson , Session is Done
            var currentStudentSession = await _StudentSessionRepository.GetAllIncluding(x=>x.Session).FirstOrDefaultAsync(x => x.StudentId == dto.StudentId &&
                x.SessionId == studentLession.Lesson.SessionId);
            if (currentStudentSession == null)
                return StudentLessonActionsResult.StudentSessionNotFound;

            currentStudentSession.State = StudentSession.SessionState.Done;
            await _StudentSessionRepository.UpdateAsync(currentStudentSession);

            // next Session in Course
            var nextSession = await _SessionRepository.FirstOrDefaultAsync(x => x.CourseId == currentStudentSession.Session.CourseId &&
                x.Order > currentStudentSession.Session.Order);
            if (nextSession != null)
            {
                await CreateStudentSessionWithFirstLessonAsync(dto.StudentId, nextSession.Id);
                return StudentLessonActionsResult.Success;
            }

            // no more Session , Course is Done
            var currentStudentCourse = await _StudentCourseRepository.GetAllIncluding(x=>x.Course).FirstOrDefaultAsync(x => x.StudentId == dto.StudentId &&
                x.CourseId == currentStudentSession.Session.CourseId);
            if (currentStudentCourse == null)
                return StudentLessonActionsResult.StudentCourseNotFound;

            currentStudentCourse.State = StudentCourse.CourseState.Done;
            await _StudentCourseRepository.UpdateAsync(currentStudentCourse);

            // next Course in Roadmap
            var nextCourse = await _CourseRepository.FirstOrDefaultAsync(x => x.RoadMapId == currentStudentCourse.Course.RoadMapId &&
                x.Order > currentStudentCourse.Course.Order);
            if(nextCourse != null)
            {
                await CreateStudentCourseAsync(dto.StudentId, nextCourse.Id);
                return StudentLessonActionsResult.Success;
            }

            var currentStudentRoadmap = await _StudentRoadmapRepository.FirstOrDefaultAsync(x => x.StudentId == dto.StudentId &&
                x.RoadmapId == currentStudentCourse.Course.RoadMapId);
            if (currentStudentRoadmap == null)
                return StudentLessonActionsResult.StudentRoadmapNotFound;

            currentStudentRoadmap.State = StudentRoadmap.RoadmapState.Done;
            await _StudentRoadmapRepository.UpdateAsync(currentStudentRoadmap);
            return StudentLessonActionsResult.Success;
        }
        private async Task CreateStudentLessonAsync(int studentId, int lessonId)
        {
            var newLesson = new StudentLesson
            {
                StudentId = studentId,
                LessonId = lessonId,
                State = StudentLesson.LessonState.Taken
            };
            await _StudentLessonRepository.InsertAsync(newLesson);
        }
        private async Task CreateStudentSessionWithFirstLessonAsync(int studentId, int sessionId)
        {
            var newSession = new StudentSession
            {
                StudentId = studentId,
                SessionId = sessionId,
                State = StudentSession.SessionState.Taken
            };
            await _StudentSessionRepository.InsertAsync(newSession);

            var firstLesson = await _LessionRepository.FirstOrDefaultAsync(x => x.SessionId == sessionId);
            if (firstLesson != null)
            {
                await CreateStudentLessonAsync(studentId, firstLesson.Id);
            }
        }
        private async Task CreateStudentCourseAsync(int studentId, int courseId)
        {
            var newCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId,
                State = StudentCourse.CourseState.Taken
            };
            await _StudentCourseRepository.InsertAsync(newCourse);

            var firstSession = await _SessionRepository.FirstOrDefaultAsync(x => x.CourseId == courseId);
            if (firstSession != null)
            {
                await CreateStudentSessionWithFirstLessonAsync(studentId, firstSession.Id);
            }
        }
    }
}
