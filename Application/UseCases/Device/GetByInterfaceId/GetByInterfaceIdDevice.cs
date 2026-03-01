using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Device.GetByInterfaceId
{
    public class GetByInterfaceIdDevice(IDeviceRepository deviceRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = (await deviceRepository.GetAllAsync(cancellationToken)).Where(d => d.InterfaceId == request.InterfaceId).ToList();
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(long interfaceId, CancellationToken cancellationToken)
        {
            return await Handle(new Request(interfaceId), cancellationToken);
        }
    }

    public record UseCaseResult(List<Domain.Entities.DeviceEntity> Devices) : IUseCaseResult;

    public record Request(long InterfaceId) : IRequest;
}
