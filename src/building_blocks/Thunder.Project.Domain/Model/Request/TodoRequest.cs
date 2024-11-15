using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using Thunder.Project.Domain.Enums;
using Esterdigi.Core.Lib.Commands;


namespace Thunder.Project.Domain.Http.Request
{
    public class TodoRegisterRequest :  ICommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class TodoUpdateRequest : TodoRegisterRequest, ICommand
    {
        public Guid Id { get; set; }
    }

    public class TodoUpdateStatusRequest : ICommand
    {
        public Guid Id { get; set; }
        public int Status { get; set; }
    }

    public class TodoFilter : ICustomQueryable
    {
        
        [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false)]
        public string Title { get; set; }

        [QueryOperator(Operator = WhereOperator.Equals, UseOr = false)]
        public Status? Status { get; set; }

        public string SortBy { get; set; }
    }
}