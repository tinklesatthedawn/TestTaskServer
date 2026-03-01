using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Device.Update
{
    public class UpdateDevice(IDeviceRepository deviceRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await deviceRepository.UpdateAsync(request.Device, cancellationToken);
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
