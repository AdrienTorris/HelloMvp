namespace HelloMvp.ProjectService
{
    using System;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class ProjectServiceImpl : ProjectService.ProjectServiceBase
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        /*private readonly ProjectContext db;

        public ProjectServiceImpl(ProjectContext db)
        {
            this.db = db;
        }*/

        public ProjectServiceImpl()
        {}

        public async override Task<GetProjectsReply> GetProjects(GetProjectsRequest request, Grpc.Core.ServerCallContext context)
        {
            List<Domain.Project> projects = new List<Domain.Project>()
            {
                new Domain.Project
                {
                    Id = 1,
                    Title = "Project #1",
                    ShortDescription = "Short description of the project #1",
                    Description = "Description of the project #1",
                    Inserted = DateTime.Now.AddDays(-5),
                    LastUpdated = DateTime.Now
                },
                new Domain.Project
                {
                    Id = 2,
                    Title = "Project #2",
                    ShortDescription = "Short description of the project #2",
                    Description = "Description of the project #2",
                    Inserted = DateTime.Now.AddDays(-5),
                    LastUpdated = DateTime.Now
                },
                new Domain.Project
                {
                    Id = 3,
                    Title = "Project #3",
                    ShortDescription = "Short description of the project #3",
                    Description = "Description of the project #3",
                    Inserted = DateTime.Now.AddDays(-3),
                    LastUpdated = DateTime.Now
                }
            };
            
            var reply = new GetProjectsReply();
            reply.TotalCount=3;
            reply.PageSize=10;
            reply.PageNumber=1;
            reply.Projects.AddRange(projects.Select(p => p.ToGrpc()));
            return reply;
        }
    }
}