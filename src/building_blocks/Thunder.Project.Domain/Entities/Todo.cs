using Thunder.Project.Domain.Enums;
using Thunder.Project.Domain.Validators;
using Esterdigi.Core.Db.Domain.Entities;

namespace Thunder.Project.Domain.Entities
{
    public class Todo : Entity
    {

        protected Todo() { }

        public Todo(Guid id, string title, string description, Status status,
            DateTime createdAt = default)
        {
            if (id != Guid.Empty) Id = id;
            CreatedAt = (id == Guid.Empty ? DateTime.Now : createdAt);
            LastUpdatedAt = (id == Guid.Empty ? null : DateTime.Now);

            Title = title;
            Description = description;
            Status = status;

            Validate();
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public Status Status { get; set; }

        public void ChangeStatus(int status) => Status = (Status)status;

        private void Validate()
        {
            var validator = new TodoValidator();
            var result = validator.Validate(this);
            AddNotifications(result.Errors);
        }
    }
}
