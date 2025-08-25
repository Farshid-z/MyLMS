using Abp.Application.Services;
using MyLMS.DTOs;
using MyLMS.Enums;
using System.Threading.Tasks;

namespace MyLMS.Services.Interfaces
{
    public interface ICourseService : IApplicationService
    {
        Task<int> CreateCourse(CreateCourseDto createCourseDto);
        Task<DeleteCourseResult> DeleteCourse(int courseId);
    }
}
