using Application.UseCases.Interface.Delete;
using Application.UseCases.Interface.GetById;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Interface.Operations
{
    internal class GetByIdOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            int id;            
            if (int.TryParse(request.Arguments.Get("Id"), out id))
            {
                GetByIdInterface usecase = DependencyInjection.DI.Get<GetByIdInterface>();
                var result = await usecase.Handle(id, cancellationToken);
                var interfaceResponsed = InterfaceDto.Of(result.Interface);
                if (interfaceResponsed != null)
                {
                    string json = interfaceResponsed.ToJson();
                    return new HttpControllerResponse(System.Net.HttpStatusCode.OK, json);
                }
                throw new IdNotExistException(this, id);
            }
            throw new QueryInvalidException(this, "Id");
        }
    }
}
