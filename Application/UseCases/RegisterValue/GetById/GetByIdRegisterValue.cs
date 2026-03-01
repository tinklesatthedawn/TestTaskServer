using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.RegisterValue.GetById
{
    public class GetByIdRegisterValue(IRegisterValueRepository registerValueRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await registerValueRepository.GetByIdAsync(request.Id, cancellationToken);
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(long id, CancellationToken cancellationToken)
        {
            return await Handle(new Request(id), cancellationToken);
        }
    }

    public record UseCaseResult(Domain.Entities.RegisterValueEntity? RegisterValue) : IUseCaseResult;

    public record Request(long Id) : IRequest;
}
