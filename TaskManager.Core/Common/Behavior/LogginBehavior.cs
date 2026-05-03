using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.ServicesContract;

namespace TaskManager.Application.Common.Behavior
{
    public class LoggingBehavior<TRequest,TResponse>:IPipelineBehavior<TRequest,TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest,TResponse>> _logger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest,TResponse>> logger, ICurrentUserService currentUser)
        {
            _logger = logger;
            _currentUserService = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId?.ToString() ?? "Anonymous";

            var startTime = DateTime.UtcNow;

            _logger.LogInformation(
                "START Handling {Request} by User {UserId}",
                requestName,
                userId
            );

            try
            {
                var response = await next();

                var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;

                _logger.LogInformation(
                    "END Handling {Request} by User {UserId} in {Duration} ms",
                    requestName,
                    userId,
                    duration
                );

                return response;
            }
            catch (Exception ex)
            {
                var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;

                _logger.LogError(
                    ex,
                    "ERROR Handling {Request} by User {UserId} after {Duration} ms",
                    requestName,
                    userId,
                    duration
                );

                throw;
            }
        }
    }
}
