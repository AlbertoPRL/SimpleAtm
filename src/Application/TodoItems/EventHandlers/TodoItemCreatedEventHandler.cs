using Microsoft.Extensions.Logging;
using SimpleAtm.Domain.Events;

namespace SimpleAtm.Application.TodoItems.EventHandlers;
public class TodoItemCreatedEventHandler : INotificationHandler<TodoItemCreatedEvent>
{
    private readonly ILogger<TodoItemCreatedEventHandler> _logger;

    public TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SimpleAtm Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
