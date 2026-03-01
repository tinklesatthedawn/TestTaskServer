using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Interface.Update
{
    public class UpdateInterface(IInterfaceRepository interfaceRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await interfaceRepository.UpdateAsync(request.Interface, cancellationToken);
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(Domain.Entities.InterfaceEntity @interface, CancellationToken cancellationToken)
        {
            return await Handle(new Request(@interface), cancellationToken);
        }
    }

    public record UseCaseResult(Domain.Entities.InterfaceEntity? @interface) : IUseCaseResult;

    public record Request(Domain.Entities.InterfaceEntity Interface) : IRequest;
}
