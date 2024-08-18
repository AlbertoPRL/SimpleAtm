using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using SimpleAtm.Application.Common.Interfaces;

namespace SimpleAtm.Application.Common.Behaviours;
public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUser _user;
    private readonly IIdentityService _identityService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUser user, IIdentityService identityService)
    {
        _logger = logger;
        _user = user;
        _identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _user.Id ?? Guid.Empty;
        string? userName = string.Empty;

        if (userId != Guid.Empty)
        {
            userName = await _identityService.GetUserNameAsync(userId);
        }

        _logger.LogInformation("SimpleAtm Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
    }
}
