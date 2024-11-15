using Thunder.Project.Domain.Entities;
using Thunder.Project.Domain.Repositories;
using Thunder.Project.Infrastructure.Contexts;
using Esterdigi.Core.Db.Infrastructure.Repository;

namespace Thunder.Project.Infrastructure.Repositories
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        public TodoRepository(TodoDataContext context) : base(context)
        {

        }


    }
}
