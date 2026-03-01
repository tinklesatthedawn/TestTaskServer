using Application.UseCases.Register.GetById;
using Application.UseCases.RegisterValue.GetById;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.RegisterValue.Operations
{
    internal class GetByIdOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            int id;
            if (int.TryParse(request.Arguments.Get("Id"), out id))
            {
                GetByIdRegisterValue usecase = DependencyInjection.DI.Get<GetByIdRegisterValue>();
                var result = await usecase.Handle(id, cancellationToken);
                var registerValueResponded = RegisterValueDto.Of(result.RegisterValue);
                if (registerValueResponded != null)
                {
                    string json = registerValueResponded.ToJson();
                    return new HttpControllerResponse(System.Net.HttpStatusCode.OK, json);
                }
                throw new IdNotExistException(this, id);
            }
            throw new QueryInvalidException(this, "Id", request.Arguments.Get("Id"));
        }
    }
}
