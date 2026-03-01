using Application.UseCases;
using Domain.Repository;

namespace Application.UseCases.Interface.Filter
{
    public class FilterInterface(IInterfaceRepository interfaceRepository) : IUseCase<UseCaseResult, Request>
    {
        public async Task<UseCaseResult> Handle(Request request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.InterfaceEntity> interfaces = await interfaceRepository.GetAllAsync(cancellationToken);

            return new UseCaseResult(interfaces
                .Where(x => request.Name == null || x.Name.ToLower().Contains(request.Name.ToLower()))
                .Where(x => request.Description == null || x.Description.ToLower().Contains(request.Description.ToLower()))
                .ToList());
        }
    }

    public record UseCaseResult(List<Domain.Entities.InterfaceEntity> Interfaces) : IUseCaseResult;

    public record Request(string? Name, string? Description) : IRequest;
}
