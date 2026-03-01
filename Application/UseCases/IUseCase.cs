using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Domain.Repository;

namespace Application.UseCases
{
    public interface IUseCase<T, K> where T : IUseCaseResult where K : IRequest
    {
        public Task<T> Handle(K request, CancellationToken cancellationToken);
    }
}
