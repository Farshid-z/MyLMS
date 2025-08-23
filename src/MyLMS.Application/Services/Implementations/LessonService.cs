using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using MyLMS.DTOs;
using MyLMS.Entities;
using MyLMS.Enums;
using MyLMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace MyLMS.Services.Implementations
{
    public class LessonService : ILessonService
    {
        private readonly IRepository<Lesson> _LessonRepository;
        private readonly IRepository<StudentLesson> _StudentLessonRepository;
        private readonly IObjectMapper _ObjectMapper;

        public LessonService
        (
            IRepository<Lesson> lessonRepository,
            IObjectMapper objectMapper,
            IRepository<StudentLesson> studentLessonRepository
        )
        {
            this._LessonRepository = lessonRepository;
            this._ObjectMapper = objectMapper;
            this._StudentLessonRepository = studentLessonRepository;
        }
        //// Done
        //public async Task<bool> AsignLessonToStudent(LessonToStudentDto asignLessonToStudent)
        //{
        //    var existLesson = _SessionRepository.Load(asignLessonToStudent.LessonId);
        //    if (existLesson == null)
        //        return false;
        //    if(!_StudentRepository.GetAll().Any(x=>x.Id == asignLessonToStudent.StudentId))
        //        return false;
        //    //  check is student passed previos lessons of same session
        //    if(existLesson.Order > 1)
        //    {
        //        var studentPassedLessons = _StudentLessonRepository
        //            .GetAll()
        //            .Where
        //            (
        //                x => x.StudentId == asignLessonToStudent.StudentId &&
        //                x.State == StudentLesson.LessonState.Done &&
        //                x.Lesson.SessionId == existLesson.SessionId &&
        //                x.Lesson.Order == existLesson.Order - 1
        //            );

        //        if(studentPassedLessons == null || studentPassedLessons.Count() == 0)
        //            return false;
        //    }

        //    var studentLesson = _ObjectMapper.Map<StudentLesson>(asignLessonToStudent);
        //    int id = await _StudentLessonRepository.InsertAndGetIdAsync(studentLesson);
        //    return id > 0;
        //}
        public async Task<int> CreateLesson(CreateLessonDto createLessonDto)
        {
            int lastOrderNum = 0;
            var lessonsOfSameSession = _LessonRepository.GetAll()
                .Where(x => x.SessionId == createLessonDto.SessionId)
                .OrderByDescending(m=>m.Id)
                .ToList();
            if(lessonsOfSameSession != null && lessonsOfSameSession.Count() > 0)
            {
                lastOrderNum = lessonsOfSameSession[0].Order;
            }
            var lesson = _ObjectMapper.Map<Lesson>(createLessonDto);
            lesson.Order = ++lastOrderNum;
            int id = await _LessonRepository.InsertAndGetIdAsync(lesson);
            return id;
        }

        public async Task<BasicDeleteResult> DeleteLesson(int lessonId)
        {
            var existsLesson = _LessonRepository.Load(lessonId);
            if (existsLesson == null)
                return BasicDeleteResult.RecordNotFound;
            
            var allStudentLessons = _StudentLessonRepository.GetAll().Where(x=>x.LessonId == lessonId);
            foreach(var item in allStudentLessons)
            {
                await _StudentLessonRepository.DeleteAsync(item);
            }

            await _LessonRepository.DeleteAsync(existsLesson);
            return BasicDeleteResult.Success;
        }
    }

}
