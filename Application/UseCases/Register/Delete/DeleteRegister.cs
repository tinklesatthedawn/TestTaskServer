using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Register.Delete
{
    public class DeleteRegister(IRegisterRepository registerRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await registerRepository.DeleteAsync(request.Register, cancellationToken);
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(Domain.Entities.RegisterEntity register, CancellationToken cancellationToken)
        {
            return await Handle(new Request(register), cancellationToken);
        }
    }

    public record UseCaseResult(Domain.Entities.RegisterEntity? Register) : IUseCaseResult;

    public record Request(Domain.Entities.RegisterEntity Register) : IRequest;
}
