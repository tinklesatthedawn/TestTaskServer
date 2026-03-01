using Domain.Repositories;

namespace Application.UseCases.Device.GetById;

public class GetByIdDevice(IDeviceRepository deviceRepository) : IUseCase<UseCaseResult, Request>
{
    public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var result = await deviceRepository.GetByIdAsync(request.Id, cancellationToken);
        return new UseCaseResult(result);
    }

    public async Task<UseCaseResult> Handle(long id, CancellationToken cancellationToken)
    {
        return await Handle(new Request(id), cancellationToken);
    }
}

public record UseCaseResult(Domain.Entities.DeviceEntity? Device) : IUseCaseResult;

public record Request(long Id) : IRequest;


