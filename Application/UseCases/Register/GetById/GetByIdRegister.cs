using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Register.GetById
{
    public class GetByIdRegister(IRegisterRepository registerRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await registerRepository.GetByIdAsync(request.Id, cancellationToken);
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(long id, CancellationToken cancellationToken)
        {
            return await Handle(new Request(id), cancellationToken);
        }
    }

    public record UseCaseResult(Domain.Entities.RegisterEntity? Register) : IUseCaseResult;

    public record Request(long Id) : IRequest;
}
