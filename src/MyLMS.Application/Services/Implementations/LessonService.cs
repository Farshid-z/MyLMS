using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using MyLMS.DTOs;
using MyLMS.Entities;
using MyLMS.Enums;
using MyLMS.Services.Interfaces;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
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
