using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.LogMessage.GetAll
{
    public class GetAllLogMessage(ILogMessageRepository logMessageRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await logMessageRepository.GetAllAsync(cancellationToken);
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(LogMessageEntity logMessage, CancellationToken cancellationToken)
        {
            return await Handle(new Request(), cancellationToken);
        }
    }

    public record UseCaseResult(List<LogMessageEntity> LogMessage) : IUseCaseResult;

    public record Request() : IRequest;
}
