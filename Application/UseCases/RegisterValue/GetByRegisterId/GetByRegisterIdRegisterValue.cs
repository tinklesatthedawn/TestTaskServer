using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.RegisterValue.GetByRegisterId
{
    public class GetByRegisterIdRegisterValue(IRegisterValueRepository registerValueRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = (await registerValueRepository.GetAllAsync(cancellationToken)).Where(r => r.RegisterId == request.RegisterId).ToList();
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(long registerId, CancellationToken cancellationToken)
        {
            return await Handle(new Request(registerId), cancellationToken);
        }
    }

    public record UseCaseResult(List<Domain.Entities.RegisterValueEntity> RegisterValues) : IUseCaseResult;

    public record Request(long RegisterId) : IRequest;
}
