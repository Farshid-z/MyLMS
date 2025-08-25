using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using MyLMS.DTOs;
using MyLMS.Entities;
using MyLMS.Enums;
using MyLMS.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MyLMS.Services.Implementations
{
    public class RoadmapService : IRoadmapService
    {
        private readonly IRepository<Roadmap> _RoadmapRepository;
        private readonly IObjectMapper _ObjectMapper;
        private readonly IRepository<StudentRoadmap> _StudentRoadmapRepository;
        private readonly IRepository<Course> _CourseRepository;
        public RoadmapService
        (
            IRepository<Roadmap> studentRepository,
            IObjectMapper objectMapper,
            IRepository<StudentRoadmap> studentRoadmapRepository,
            IRepository<Course> courseRepository
        )
        {
            this._RoadmapRepository = studentRepository;
            this._ObjectMapper = objectMapper;
            this._StudentRoadmapRepository = studentRoadmapRepository;
            this._CourseRepository = courseRepository;
        }
        public async Task<int> CreateRoadmap(CreateRoadmapDto createRoadmapDto)
        {
            var roadmap =  _ObjectMapper.Map<Roadmap>(createRoadmapDto);
            int id = await _RoadmapRepository.InsertAndGetIdAsync(roadmap);
            return id;
        }

        public async Task<DeleteRoadmapResult> DeleteRoadmap(int roadmapId)
        {
            var existsRoadmap = _RoadmapRepository.Load(roadmapId);
            if (existsRoadmap == null)
                return DeleteRoadmapResult.RecordNotFound;
            if (_CourseRepository.GetAll().Any(x => x.RoadMapId == roadmapId))
                return DeleteRoadmapResult.RoadmapHasCours;

            var allStudentRoadmaps = _StudentRoadmapRepository.GetAll().Where(x => x.RoadmapId == roadmapId);
            foreach (var item in allStudentRoadmaps)
            {
                await _StudentRoadmapRepository.DeleteAsync(item);
            }

            await _RoadmapRepository.DeleteAsync(existsRoadmap);
            return DeleteRoadmapResult.Success;
        }
    }
}
