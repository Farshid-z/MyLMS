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
    public interface IRoadmapService : IApplicationService
    {
        Task<int> CreateRoadmap(CreateRoadmapDto createRoadmapDto);
        Task<DeleteRoadmapResult> DeleteRoadmap(int roadmapId);
    }
}
