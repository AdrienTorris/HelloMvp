namespace HelloMvp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Google.Protobuf.WellKnownTypes;

    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime LastUpdated { get; set; }
        public int OwnerId { get; set; }

        public ProjectService.Project ToGrpc()
        {
            var project = new ProjectService.Project();

            project.Id = this.Id;
            project.Title = this.Title;
            project.ShortDescription = this.ShortDescription;
            project.Description = this.Description;
            project.Inserted = Timestamp.FromDateTimeOffset(this.Inserted);
            project.LastUpdated = Timestamp.FromDateTimeOffset(this.LastUpdated);
            project.OwnerId = this.OwnerId.ToString();
            
            return project;
        }
    }
}