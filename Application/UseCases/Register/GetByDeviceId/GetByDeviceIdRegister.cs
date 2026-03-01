using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Register.GetByDeviceId
{
    public class GetByDeviceIdRegister(IRegisterRepository registerRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = (await registerRepository.GetAllAsync(cancellationToken)).Where(r => r.DeviceId == request.DeviceId).ToList();
            return new UseCaseResult(result);
        }

        public async Task<UseCaseResult> Handle(long id, CancellationToken cancellationToken)
        {
            return await Handle(new Request(id), cancellationToken);
        }
    }

    public record UseCaseResult(List<Domain.Entities.RegisterEntity> Registers) : IUseCaseResult;

    public record Request(long DeviceId) : IRequest;
}
