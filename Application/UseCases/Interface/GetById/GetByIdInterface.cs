using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Domain.Entities;
using Domain.Repository;

namespace Application.UseCases.Interface.GetById;

public class GetByIdInterface(IInterfaceRepository interfaceRepository) : IUseCase<UseCaseResult, Request>
{
    public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var result = await interfaceRepository.GetByIdAsync(request.Id, cancellationToken);
        return new UseCaseResult(result);
    }

    public async Task<UseCaseResult> Handle(long id, CancellationToken cancellationToken)
    {
        return await Handle(new Request(id), cancellationToken);
    }
}

public record UseCaseResult(Domain.Entities.InterfaceEntity? Interface) : IUseCaseResult;

public record Request(long Id) : IRequest;


