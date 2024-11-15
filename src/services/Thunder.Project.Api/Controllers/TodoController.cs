using Esterdigi.Core.Db.Domain.Model;
using Esterdigi.Core.Lib.Controller;
using Esterdigi.Core.Lib.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thunder.Project.Domain.Http.Request;
using Thunder.Project.Domain.Model.Response;
using Thunder.Project.Service;

namespace Thunder.Project.Api.Controllers
{
    [Route("todo")]
    public class TodoController : BaseController
    {
        private readonly TodoService _service;

        public TodoController(TodoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna a lista paginada dos registros da tabela to do - lista de tarefas [Status - 0: Pendente, 1: Concluido, 2: Excluido]
        /// </summary>
        /// <response code="200">Registros que foram retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpGet, Route("get-all"), AllowAnonymous]
        [ProducesResponseType(typeof(PagedResponse<TodoResponse, PagedResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter paginationFilter, [FromQuery] TodoFilter filter)
        {
            var data = await _service.GetAllByFilter(paginationFilter, filter);

            if (_service.Notifications.Any()) return BadRequest(BaseErrorResponse.Create(_service.Notifications));

            data.Success = !_service.Notifications.Any();
            return Ok(data);
        }

        /// <summary>
        /// Retorna informacoes da tabela to do - lista de tarefas
        /// </summary>
        /// <response code="200">Registro que foi retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpGet, Route("get"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<TodoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _service.Get(id);
            return await Response(data, _service.Notifications);
        }


        /// <summary>
        /// Insere um registro na tabela to do - lista de tarefas
        /// </summary>
        /// <response code="200">Registro que foi inserido com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPost, Route("add"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<TodoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Post([FromBody] TodoRegisterRequest request)
        {
            var data = await _service.Add(request);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Altera um registro na tabela to do - lista de tarefas
        /// </summary>
        /// <response code="200">Registro que foi inserido com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPut, Route("update"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<TodoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Put([FromBody] TodoUpdateRequest request)
        {
            var data = await _service.Update(request);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Altera o status de uma tarefa 
        /// </summary>
        /// <response code="200">Registro que foi inserido com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPatch, Route("patch"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<TodoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Patch([FromBody] TodoUpdateStatusRequest request)
        {
            var data = await _service.Patch(request);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Deleta um registro na tabela to do - lista de tarefas (exclusao logica)
        /// </summary>
        /// <response code="200">Registro que foi inserido com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpDelete, Route("delete"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<TodoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = await _service.Delete(id);
            return await Response(data, _service.Notifications);
        }


    }
}