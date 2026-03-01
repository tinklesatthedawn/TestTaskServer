using Application.UseCases;
using Domain.Entities;
using Domain.Repositories;
using Domain.Repository;
using System.Drawing;

namespace Application.UseCases.Device.Filter
{
    public class FilterDevice(IDeviceRepository deviceRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.DeviceEntity> devices = await deviceRepository.GetAllAsync(cancellationToken);

#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            return new UseCaseResult(devices
                //x.Name и x.Description не могут быть null из-за ограничений базы данных
                .Where(x => request.Name == null || x.Name.ToLower().Contains(request.Name.ToLower()))
                .Where(x => request.Description == null || x.Description.ToLower().Contains(request.Description.ToLower()))
                .Where(x => request.IsEnabled == null || x.IsEnabled == request.IsEnabled)
                .Where(x => request.FigureType == null || x.Figure == request.FigureType)
                .Where(x => request.Size == null || x.Size == request.Size)
                .Where(x => request.Color == null || x.Color == request.Color)
                .ToList());
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
        }
    }

    public record UseCaseResult(List<Domain.Entities.DeviceEntity> Devices) : IUseCaseResult;

    public record Request(string? Name, string? Description, bool? IsEnabled, Domain.Entities.DeviceEntity.FigureType? FigureType, int? Size, Color? Color) : IRequest;
}
