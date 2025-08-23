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
    public class TeacherService : ITeacherService
    {
        private readonly IRepository<Teacher> _TeacherRepository;
        private readonly IRepository<Course> _CourseRepository;
        private readonly IObjectMapper _ObjectMapper;
        public TeacherService
        (
            IRepository<Teacher> teacherRepository,
            IObjectMapper objectMapper,
            IRepository<Course> courseRepository
        )
        {
            this._TeacherRepository = teacherRepository;
            this._ObjectMapper = objectMapper;
            this._CourseRepository = courseRepository;
        }
        public async Task<int> CreateTeacher(CreateTeacherDto createTeacherDto)
        {
            var teacher = _ObjectMapper.Map<Teacher>(createTeacherDto);
            int id = await _TeacherRepository.InsertAndGetIdAsync(teacher);
            return id;
        }


        public async Task<DeleteTeacherResult> DeleteTeacher(int teacherId)
        {
            var existsTeacher = _TeacherRepository.Load(teacherId);
            if(existsTeacher == null)
                return DeleteTeacherResult.RecordNotFound;

            if (_CourseRepository.GetAll().Any(x => x.TeacherId == teacherId))
                return DeleteTeacherResult.TeacherHasCourse;

            _TeacherRepository.Delete(existsTeacher);
            return DeleteTeacherResult.Success;
        }
    }
}
