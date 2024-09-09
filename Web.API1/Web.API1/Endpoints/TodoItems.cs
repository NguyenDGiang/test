using vnpt.mplis.Web.Application.Common.Models;
using vnpt.mplis.Web.Application.TodoItems.Commands.CreateTodoItem;
using vnpt.mplis.Web.Application.TodoItems.Commands.DeleteTodoItem;
using vnpt.mplis.Web.Application.TodoItems.Commands.UpdateTodoItem;
using vnpt.mplis.Web.Application.TodoItems.Commands.UpdateTodoItemDetail;
using vnpt.mplis.Web.Application.TodoItems.Queries.GetTodoItemsWithPagination;

namespace vnpt.mplis.Web.API1.Endpoints
{
    public class TodoItems : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .RequireAuthorization()
                .MapGet(GetTodoItemsWithPagination)
                .MapPost(CreateTodoItem)
                .MapPut(UpdateTodoItem, "{id}")
                .MapPut(UpdateTodoItemDetail, "UpdateDetail/{id}")
                .MapDelete(DeleteTodoItem, "{id}");
        }

        public Task<PaginatedList<TodoItemBriefDto>> GetTodoItemsWithPagination(ISender sender, [AsParameters] GetTodoItemsWithPaginationQuery query)
        {
            return sender.Send(query);
        }

        public Task<int> CreateTodoItem(ISender sender, CreateTodoItemCommand command)
        {
            return sender.Send(command);
        }

        public async Task<IResult> UpdateTodoItem(ISender sender, int id, UpdateTodoItemCommand command)
        {
            if (id != command.Id) return Results.BadRequest();
            await sender.Send(command);
            return Results.NoContent();
        }

        public async Task<IResult> UpdateTodoItemDetail(ISender sender, int id, UpdateTodoItemDetailCommand command)
        {
            if (id != command.Id) return Results.BadRequest();
            await sender.Send(command);
            return Results.NoContent();
        }

        public async Task<IResult> DeleteTodoItem(ISender sender, int id)
        {
            await sender.Send(new DeleteTodoItemCommand(id));
            return Results.NoContent();
        }
    }
}
