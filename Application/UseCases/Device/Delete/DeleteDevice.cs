using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Device.Delete
{
    public class DeleteDevice(IDeviceRepository deviceRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await deviceRepository.DeleteAsync(request.Device, cancellationToken);
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(Domain.Entities.DeviceEntity device, CancellationToken cancellationToken)
        {
            return await Handle(new Request(device), cancellationToken);
        }
    }

    public record UseCaseResult(Domain.Entities.DeviceEntity? Device) : IUseCaseResult;

    public record Request(Domain.Entities.DeviceEntity Device) : IRequest;
}
