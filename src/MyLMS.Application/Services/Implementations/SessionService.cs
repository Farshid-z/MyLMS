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
    public class SessionService : ISessionService
    {
        private readonly IRepository<Session> _SessionRepository;
        private readonly IObjectMapper _ObjectMapper;
        private readonly IRepository<StudentSession> _StudentSessionRepository;
        private readonly IRepository<Lesson> _LessonRepository;

        public SessionService
        (
            IRepository<Session> sessionRepository,
            IObjectMapper objectMapper,
            IRepository<StudentSession> studentSessionRepository,
            IRepository<Lesson> lessonRepository
        )
        {
            this._SessionRepository = sessionRepository;
            this._ObjectMapper = objectMapper;
            this._StudentSessionRepository = studentSessionRepository;
            this._LessonRepository = lessonRepository;
        }
        public async Task<int> CreateSession(CreateSessionDto createSessionDto)
        {
            int lastOrderNum = 0;
            var sessionsOfSameCourse = _SessionRepository.GetAll()
                .Where(x => x.CourseId == createSessionDto.CourseId)
                .OrderByDescending(m => m.Id)
                .ToList();
            if(sessionsOfSameCourse != null && sessionsOfSameCourse.Count() > 0)
            {
                lastOrderNum = sessionsOfSameCourse[0].Order;
            }
            var session = _ObjectMapper.Map<Session>(createSessionDto);
            session.Order = ++lastOrderNum;
            int id = await _SessionRepository.InsertAndGetIdAsync(session);
            return id;
        }

        public async Task<DeleteSessionResult> DeleteSession(int sessionId)
        {
            var existsSession = _SessionRepository.Load(sessionId);
            if (existsSession == null)
                return DeleteSessionResult.RecordNotFound;
            if (_LessonRepository.GetAll().Any(x => x.SessionId == sessionId))
                return DeleteSessionResult.SessionHasLession;

            var allStudentSessions = _StudentSessionRepository.GetAll().Where(x => x.SessionId == sessionId);
            foreach (var item in allStudentSessions)
            {
                await _StudentSessionRepository.DeleteAsync(item);
            }

            await _SessionRepository.DeleteAsync(existsSession);
            return DeleteSessionResult.Success;
        }
    }

}
