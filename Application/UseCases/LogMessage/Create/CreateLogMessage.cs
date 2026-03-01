using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.LogMessage.Create
{
    public class CreateLogMessage(ILogMessageRepository logMessageRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await logMessageRepository.AddAsync(request.LogMessage, cancellationToken);
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(LogMessageEntity logMessage, CancellationToken cancellationToken)
        {
            return await Handle(new Request(logMessage), cancellationToken);
        }
    }

    public record UseCaseResult(LogMessageEntity? LogMessage) : IUseCaseResult;

    public record Request(LogMessageEntity LogMessage) : IRequest;
}
