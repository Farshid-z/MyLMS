using Abp.Domain.Repositories;
using Abp.ObjectMapping;
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
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _CourseRepository;
        private readonly IRepository<Teacher> _TeacherRepository;
        private readonly IRepository<Session> _SessionRepository;
        private readonly IRepository<StudentCourse> _StudentCourseRepository;
        private readonly IObjectMapper _ObjectMapper;

        public CourseService
        (
            IRepository<Course> courseRepository,
            IObjectMapper objectMapper,
            IRepository<Teacher> teacherRepository,
            IRepository<Session> sessionRepository,
            IRepository<StudentCourse> studentCourseRepository
        )
        {
            this._CourseRepository = courseRepository;
            this._ObjectMapper = objectMapper;
            this._TeacherRepository = teacherRepository;
            this._SessionRepository = sessionRepository;
            this._StudentCourseRepository = studentCourseRepository;
        }
        // Done
        public async Task<int> CreateCourse(CreateCourseDto createCourseDto)
        {
            if(!_TeacherRepository.GetAll().Any(x=>x.Id == createCourseDto.TeacherId))
                return -1;
            int lastOrderNum = 0;
            var coursesOfSameRoadmap = _CourseRepository.GetAll()
                .Where(x => x.RoadMapId == createCourseDto.RoadMapId)
                .OrderByDescending(m => m.Id)
                .ToList();
            if(coursesOfSameRoadmap != null && coursesOfSameRoadmap.Count() > 0)
            {
                lastOrderNum = coursesOfSameRoadmap[0].Order;
            }
            var course = _ObjectMapper.Map<Course>(createCourseDto);
            course.Order = ++lastOrderNum;
            int id = await _CourseRepository.InsertAndGetIdAsync(course);
            return course.Id;
        }

        public async Task<DeleteCourseResult> DeleteCourse(int courseId)
        {
            var existsCourse = _CourseRepository.Load(courseId);
            if (existsCourse == null)
                return DeleteCourseResult.RecordNotFound;
            if (_SessionRepository.GetAll().Any(x => x.CourseId == courseId))
                return DeleteCourseResult.CourseHasSession ;

            var allStudentRoadmaps = _StudentCourseRepository.GetAll().Where(x => x.CourseId == courseId);
            foreach (var item in allStudentRoadmaps)
            {
                await _StudentCourseRepository.DeleteAsync(item);
            }

            await _CourseRepository.DeleteAsync(existsCourse);
            return DeleteCourseResult.Success ;
        }
    }

}
