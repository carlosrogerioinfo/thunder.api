﻿namespace Thunder.Project.Infrastructure.Transactions
{
    public interface IUow
    {
        Task CommitAsync();
        void Commit();
        void Rollback();
    }
}