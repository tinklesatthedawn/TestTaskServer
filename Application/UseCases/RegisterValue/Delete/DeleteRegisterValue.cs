using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.RegisterValue.Delete
{
    public class DeleteRegisterValue(IRegisterValueRepository registerValueRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await registerValueRepository.DeleteAsync(request.RegisterValue, cancellationToken);
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(Domain.Entities.RegisterValueEntity registerValue, CancellationToken cancellationToken)
        {
            return await Handle(new Request(registerValue), cancellationToken);
        }
    }

    public record UseCaseResult(Domain.Entities.RegisterValueEntity? RegisterValue) : IUseCaseResult;

    public record Request(Domain.Entities.RegisterValueEntity RegisterValue) : IRequest;
}
