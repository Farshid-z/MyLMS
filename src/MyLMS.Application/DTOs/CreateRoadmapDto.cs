using Abp.AutoMapper;
using MyLMS.Entities;

namespace MyLMS.DTOs
{
    [AutoMapTo(typeof(Roadmap))]
    public class CreateRoadmapDto
    {
        public string Name { get; set; }
    }
}
