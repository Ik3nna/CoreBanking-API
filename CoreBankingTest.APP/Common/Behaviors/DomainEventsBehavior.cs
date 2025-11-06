using MediatR;
using Microsoft.Extensions.Logging;
using CoreBanking.Application.Common.Interfaces;

namespace CoreBanking.Application.Common.Behaviors
{
    public class DomainEventsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ILogger<DomainEventsBehavior<TRequest, TResponse>> _logger;

        public DomainEventsBehavior(
            IDomainEventDispatcher dispatcher,
            ILogger<DomainEventsBehavior<TRequest, TResponse>> logger)
        {
            this._dispatcher = dispatcher;
            this._logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            this._logger.LogInformation("Processing domain events for {RequestType}", typeof(TRequest).Name);

            var response = await next();

            await this._dispatcher.DispatchDomainEventsAsync(cancellationToken);

            return response;
        }
    }
}
