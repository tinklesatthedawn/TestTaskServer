using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Register.GetAll
{
    public class GetAllRegister(IRegisterRepository registerRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await registerRepository.GetAllAsync(cancellationToken);
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(CancellationToken cancellationToken)
        {
            return await Handle(new Request(), cancellationToken);
        }
    }

    public record UseCaseResult(List<Domain.Entities.RegisterEntity> Registers) : IUseCaseResult;

    public record Request() : IRequest;
}
