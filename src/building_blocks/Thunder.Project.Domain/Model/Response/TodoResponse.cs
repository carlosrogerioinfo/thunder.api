using Esterdigi.Core.Lib.Commands;

namespace Thunder.Project.Domain.Model.Response
{
    public class TodoResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}
