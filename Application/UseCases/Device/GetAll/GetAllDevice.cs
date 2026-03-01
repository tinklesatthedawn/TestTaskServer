using Application.UseCases;
using Domain.Repositories;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Device.GetAll
{
    public class GetAllDevice(IDeviceRepository deviceRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await deviceRepository.GetAllAsync(cancellationToken);
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(CancellationToken cancellationToken)
        {
            return await Handle(new Request(), cancellationToken);
        }
    }

    public record UseCaseResult(List<Domain.Entities.DeviceEntity> Devices) : IUseCaseResult;

    public record Request() : IRequest;
}
