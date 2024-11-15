using Thunder.Project.Infrastructure.Contexts;

namespace Thunder.Project.Infrastructure.Transactions
{
    public class Uow : IUow
    {
        private readonly TodoDataContext _context;

        public Uow(TodoDataContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            // Do Nothing
        }
    }
}
