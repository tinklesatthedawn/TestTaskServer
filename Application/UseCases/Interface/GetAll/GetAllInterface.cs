using Application.UseCases;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Interface.GetAll
{
    public class GetAllInterface(IInterfaceRepository interfaceRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await interfaceRepository.GetAllAsync(cancellationToken);
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(CancellationToken cancellationToken)
        {
            return await Handle(new Request(), cancellationToken);
        }
    }

    public record UseCaseResult(List<Domain.Entities.InterfaceEntity> Interfaces) : IUseCaseResult;

    public record Request() : IRequest;
}
