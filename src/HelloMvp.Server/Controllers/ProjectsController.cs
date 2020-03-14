namespace HelloMvp.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Net;

    [Route("projects")]
    [ApiController]
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ProjectService.ProjectService.ProjectServiceClient projects;

        public ProjectsController(ProjectService.ProjectService.ProjectServiceClient projects)
        {
            this.projects = projects;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var reply = await this.projects.GetProjectsAsync(new ProjectService.GetProjectsRequest()
            {
                PageNumber=1,
                PageSize=10
            });

            return reply.Projects.OrderByDescending(p => p.Inserted).Select(p => FromGrpc(p)).ToList();
        }

        private static Project FromGrpc(ProjectService.Project grpc)
        {
            var project = new HelloMvp.Project();
            project.Id = grpc.Id;
            project.Inserted = grpc.Inserted.ToDateTime();
            return project;
        }
    }
}