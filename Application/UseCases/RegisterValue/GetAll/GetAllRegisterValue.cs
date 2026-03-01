using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.RegisterValue.GetAll
{
    public class GetAllRegisterValue(IRegisterValueRepository registerValueRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await registerValueRepository.GetAllAsync(cancellationToken);
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(CancellationToken cancellationToken)
        {
            return await Handle(new Request(), cancellationToken);
        }
    }

    public record UseCaseResult(List<Domain.Entities.RegisterValueEntity> RegisterValues) : IUseCaseResult;

    public record Request() : IRequest;
}
