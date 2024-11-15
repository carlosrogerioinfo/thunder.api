using AutoMapper;
using Esterdigi.Core.Db.Domain.Model;
using Esterdigi.Core.Lib.Commands;
using Esterdigi.Core.Lib.Notifications;
using Thunder.Project.Core.Constants;
using Thunder.Project.Domain.Entities;
using Thunder.Project.Domain.Enums;
using Thunder.Project.Domain.Http.Request;
using Thunder.Project.Domain.Model.Response;
using Thunder.Project.Domain.Repositories;
using Thunder.Project.Infrastructure.Transactions;

namespace Thunder.Project.Service
{
    public class TodoService : Notifiable
    {
        private readonly ITodoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public TodoService(ITodoRepository repository, IMapper mapper, IUow uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;

        }

        public async Task<PagedResponse<TodoResponse, PagedResult>> GetAllByFilter(PaginationFilter paginationFilter, TodoFilter filter)
        {
            var entity = await _repository.SearchPagedAsync(filter, paginationFilter, filter.SortBy);

            if (entity.Data.Count() <= 0) AddNotification(Consts.PropertyMsgError, Consts.RegisterNotFound);

            if (!IsValid) return default;

            return new PagedResponse<TodoResponse, PagedResult>(_mapper.Map<List<TodoResponse>>(entity.Data.OrderBy(x => x.Title)), entity.Paging);
        }

        public async Task<ICommandResult> Get(Guid id)
        {
            var entity = await _repository.GetAsync(x => x.Id == id);

            if (entity is null) AddNotification(Consts.PropertyMsgError, Consts.RegisterNotFound);

            if (!IsValid) return default;

            return _mapper.Map<TodoResponse>(entity);
        }

        public async Task<ICommandResult> Add(TodoRegisterRequest request)
        {
            await ValidateInsert(request);

            var entity = new Todo(default, request.Title, request.Description, 0);

            AddNotifications(entity.Notifications);

            if (!IsValid) return default;

            await _repository.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<TodoResponse>(entity);
        }

        public async Task<ICommandResult> Update(TodoUpdateRequest request)
        {
            var validation = await ValidateUpdate(request);

            var entity = new Todo(request.Id, request.Title, request.Description, validation.Status, (validation is null ? default : validation.CreatedAt));

            AddNotifications(entity.Notifications);

            if (!IsValid) return default;

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<TodoResponse>(entity);
        }

        public async Task<ICommandResult> Patch(TodoUpdateStatusRequest request)
        {
            var validation = await ValidateUpdateStatus(request);

            var entity = new Todo(request.Id, validation.Title, validation.Description, validation.Status, (validation is null ? default : validation.CreatedAt));

            AddNotifications(entity.Notifications);

            if (!IsValid) return default;

            entity.ChangeStatus(request.Status);
            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<TodoResponse>(entity);
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var validation = await ValidateUpdateStatus(new TodoUpdateStatusRequest { Id = id, Status = 3 });

            var entity = new Todo(id, validation.Title, validation.Description, validation.Status, (validation is null ? default : validation.CreatedAt));

            AddNotifications(entity.Notifications);

            if (!IsValid) return default;

            entity.ChangeStatus((int)Status.Deleted);

            await _repository.UpdateAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<TodoResponse>(entity);
        }


        private async Task<Todo> ValidateInsert(TodoRegisterRequest request)
        {
            var entity = await _repository.GetAsync(x => x.Title.ToLower() == request.Title.ToLower());
            if (entity is not null) AddNotification(Consts.PropertyMsgError, "Já existe um título com esse mesmo nome");

            return entity;
        }

        private async Task<Todo> ValidateUpdate(TodoUpdateRequest request)
        {
            var entity = await _repository.GetAsync(x => x.Id != request.Id && x.Title.ToLower() == request.Title.ToLower());
            if (entity is not null) AddNotification(Consts.PropertyMsgError, "Já existe um título com esse mesmo nome");

            return entity;
        }

        private async Task<Todo> ValidateUpdateStatus(TodoUpdateStatusRequest request)
        {
            var entity = await _repository.GetAsync(x => x.Id == request.Id);
            if (entity is null) AddNotification(Consts.PropertyMsgError, "Registro não encontrado");

            return entity;
        }


    }
}
