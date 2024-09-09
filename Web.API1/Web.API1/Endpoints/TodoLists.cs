using Microsoft.Extensions.DependencyInjection;
using vnpt.mplis.Web.Application.TodoLists.Commands.CreateTodoList;
using vnpt.mplis.Web.Application.TodoLists.Commands.DeleteTodoList;
using vnpt.mplis.Web.Application.TodoLists.Commands.UpdateTodoList;
using vnpt.mplis.Web.Application.TodoLists.Queries.GetTodos;

namespace vnpt.mplis.Web.API1.Endpoints
{
    public class TodoLists : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .RequireAuthorization()
                .MapGet(GetTodoLists)
                .MapPost(CreateTodoList)
                .MapPut(UpdateTodoList, "{id}")
                .MapDelete(DeleteTodoList, "{id}");
        }

        public Task<TodosVm> GetTodoLists(ISender sender)
        {
            return sender.Send(new GetTodosQuery());
        }

        public Task<int> CreateTodoList(ISender sender, CreateTodoListCommand command)
        {
            return sender.Send(command);
        }

        public async Task<IResult> UpdateTodoList(ISender sender, int id, UpdateTodoListCommand command)
        {
            if (id != command.Id) return Results.BadRequest();
            await sender.Send(command);
            return Results.NoContent();
        }

        public async Task<IResult> DeleteTodoList(ISender sender, int id)
        {
            await sender.Send(new DeleteTodoListCommand(id));
            return Results.NoContent();
        }
    }
}
