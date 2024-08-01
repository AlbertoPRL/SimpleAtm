﻿//using SimpleAtm.Application.Common.Models;
//using SimpleAtm.Application.TodoItems.Commands.CreateTodoItem;
//using SimpleAtm.Application.TodoItems.Commands.DeleteTodoItem;
//using SimpleAtm.Application.TodoItems.Commands.UpdateTodoItem;
//using SimpleAtm.Application.TodoItems.Commands.UpdateTodoItemDetail;
//using SimpleAtm.Application.TodoItems.Queries.GetTodoItemsWithPagination;

//namespace SimpleAtm.Web.Endpoints;
//public class TodoItems : EndpointGroupBase
//{
//    public override void Map(WebApplication app)
//    {
//        app.MapGroup(this)
//            .RequireAuthorization()
//            .MapGet(GetTodoItemsWithPagination)
//            .MapPost(CreateTodoItem)
//            .MapPut(UpdateTodoItem, "{id}")
//            .MapPut(UpdateTodoItemDetail, "UpdateDetail/{id}")
//            .MapDelete(DeleteTodoItem, "{id}");
//    }

//    public Task<PaginatedList<TodoItemBriefDto>> GetTodoItemsWithPagination(ISender sender, [AsParameters] GetTodoItemsWithPaginationQuery query)
//    {
//        return sender.Send(query);
//    }

//    public Task<int> CreateTodoItem(ISender sender, CreateTodoItemCommand command)
//    {
//        return sender.Send(command);
//    }

//    public async Task<IResult> UpdateTodoItem(ISender sender, int id, UpdateTodoItemCommand command)
//    {
//        if (id != command.Id) return Results.BadRequest();
//        await sender.Send(command);
//        return Results.NoContent();
//    }

//    public async Task<IResult> UpdateTodoItemDetail(ISender sender, int id, UpdateTodoItemDetailCommand command)
//    {
//        if (id != command.Id) return Results.BadRequest();
//        await sender.Send(command);
//        return Results.NoContent();
//    }

//    public async Task<IResult> DeleteTodoItem(ISender sender, int id)
//    {
//        await sender.Send(new DeleteTodoItemCommand(id));
//        return Results.NoContent();
//    }
//}
