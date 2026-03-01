using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repository;
using Domain.Entities;

namespace Application.UseCases.Interface.Create;

public class CreateInterface(IInterfaceRepository interfaceRepository) : IUseCase<UseCaseResult, Request>
{
    public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var result = await interfaceRepository.AddAsync(request.Interface, cancellationToken);
        return new UseCaseResult(result);
    }

    public async Task<UseCaseResult> Handle(Domain.Entities.InterfaceEntity @interface, CancellationToken cancellationToken)
    {
        return await Handle(new Request(@interface), cancellationToken);
    }
}

public record UseCaseResult(Domain.Entities.InterfaceEntity? @interface) : IUseCaseResult;

public record Request(Domain.Entities.InterfaceEntity Interface) : IRequest;





